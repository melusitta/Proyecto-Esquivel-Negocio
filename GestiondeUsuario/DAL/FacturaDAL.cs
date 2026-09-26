using BE;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class FacturaDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        private Factura MapearFactura(SqlDataReader reader)
        {
            return new Factura()
            {
                Id = Convert.ToInt32(reader["Id"]),
                NroFactura = Convert.ToInt32(reader["NroFactura"]),
                IdCarrito = Convert.ToInt32(reader["IdCarrito"]),
                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                DNI = Convert.ToInt32(reader["DNI"]),
                NombreCliente = reader["NombreCliente"].ToString(),
                FechaHora = Convert.ToDateTime(reader["FechaHora"]),
                Total = Convert.ToDecimal(reader["Total"]),
                Estado = reader["Estado"].ToString(),
                FormaPago = reader["FormaPago"] == DBNull.Value ? null : reader["FormaPago"].ToString(),
                MontoPagado = reader["MontoPagado"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["MontoPagado"]),
                FechaPago = reader["FechaPago"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["FechaPago"]),
                Cajero = reader["Cajero"].ToString()
            };
        }

        public Factura ObtenerPorId(int id)
        {
            return Obtener("SELECT * FROM Factura WHERE Id = @Valor", id);
        }

        public Factura ObtenerPorNro(int nroFactura)
        {
            return Obtener("SELECT * FROM Factura WHERE NroFactura = @Valor", nroFactura);
        }

        private Factura Obtener(string query, int valor)
        {
            Factura factura = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Valor", valor);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        factura = MapearFactura(reader);
                }
                if (factura != null)
                    CargarItems(con, factura);
            }
            return factura;
        }

        private void CargarItems(SqlConnection con, Factura factura)
        {
            string query = @"SELECT itf.Cantidad, itf.PrecioUnitario AS PrecioItem, p.*
                FROM ItemFactura itf
                INNER JOIN Producto p ON p.Id = itf.IdProducto
                WHERE itf.IdFactura = @IdFactura
                ORDER BY p.Nombre";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@IdFactura", factura.Id);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    factura.Items.Add(new ItemFactura
                    {
                        Producto = ProductoDAL.MapearProducto(reader),
                        Cantidad = Convert.ToInt32(reader["Cantidad"]),
                        PrecioUnitario = Convert.ToDecimal(reader["PrecioItem"])
                    });
            }
        }

        // Genera la factura en una transacción: número correlativo, cabecera, detalle
        // y el carrito pasa a Facturado. Devuelve el Id de la factura, o 0 si el carrito
        // ya no estaba Asociado (por ejemplo, otro cajero lo facturó primero).
        public int Generar(Factura f)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    SqlCommand cmdCarrito = new SqlCommand(
                        "UPDATE Carrito SET Estado = @Facturado WHERE Id = @IdCarrito AND Estado = @Asociado", con, tx);
                    cmdCarrito.Parameters.AddWithValue("@Facturado", Carrito.EstadoFacturado);
                    cmdCarrito.Parameters.AddWithValue("@Asociado", Carrito.EstadoAsociado);
                    cmdCarrito.Parameters.AddWithValue("@IdCarrito", f.IdCarrito);
                    if (cmdCarrito.ExecuteNonQuery() == 0)
                    {
                        tx.Rollback();
                        return 0;
                    }

                    SqlCommand cmdNro = new SqlCommand(
                        "SELECT ISNULL(MAX(NroFactura), 0) + 1 FROM Factura WITH (UPDLOCK, HOLDLOCK)", con, tx);
                    f.NroFactura = Convert.ToInt32(cmdNro.ExecuteScalar());

                    string query = @"INSERT INTO Factura
                        (NroFactura, IdCarrito, IdCliente, DNI, NombreCliente, FechaHora, Total, Estado, Cajero)
                        VALUES
                        (@NroFactura, @IdCarrito, @IdCliente, @DNI, @NombreCliente, @FechaHora, @Total, @Estado, @Cajero);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    SqlCommand cmd = new SqlCommand(query, con, tx);
                    cmd.Parameters.AddWithValue("@NroFactura", f.NroFactura);
                    cmd.Parameters.AddWithValue("@IdCarrito", f.IdCarrito);
                    cmd.Parameters.AddWithValue("@IdCliente", f.IdCliente);
                    cmd.Parameters.AddWithValue("@DNI", f.DNI);
                    cmd.Parameters.AddWithValue("@NombreCliente", f.NombreCliente);
                    cmd.Parameters.Add("@FechaHora", SqlDbType.DateTime).Value = f.FechaHora;
                    cmd.Parameters.AddWithValue("@Total", f.Total);
                    cmd.Parameters.AddWithValue("@Estado", Factura.EstadoPendiente);
                    cmd.Parameters.AddWithValue("@Cajero", f.Cajero);
                    f.Id = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (ItemFactura item in f.Items)
                    {
                        SqlCommand cmdItem = new SqlCommand(
                            @"INSERT INTO ItemFactura (IdFactura, IdProducto, Cantidad, PrecioUnitario)
                              VALUES (@IdFactura, @IdProducto, @Cantidad, @Precio)", con, tx);
                        cmdItem.Parameters.AddWithValue("@IdFactura", f.Id);
                        cmdItem.Parameters.AddWithValue("@IdProducto", item.Producto.Id);
                        cmdItem.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                        cmdItem.Parameters.AddWithValue("@Precio", item.PrecioUnitario);
                        cmdItem.ExecuteNonQuery();
                    }

                    tx.Commit();
                    return f.Id;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        // Cobra la factura en una transacción: descuenta el stock de cada producto y la marca Pagada.
        // Devuelve false si la factura ya no estaba Pendiente (idProductoSinStock = 0)
        // o si algún producto no tiene stock suficiente (idProductoSinStock = Id de ese producto).
        public bool Cobrar(Factura f, out int idProductoSinStock)
        {
            idProductoSinStock = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction();
                try
                {
                    string query = @"UPDATE Factura
                        SET Estado = @Pagada, FormaPago = @FormaPago, MontoPagado = @Monto, FechaPago = @FechaPago
                        WHERE Id = @Id AND Estado = @Pendiente";
                    SqlCommand cmd = new SqlCommand(query, con, tx);
                    cmd.Parameters.AddWithValue("@Pagada", Factura.EstadoPagada);
                    cmd.Parameters.AddWithValue("@Pendiente", Factura.EstadoPendiente);
                    cmd.Parameters.AddWithValue("@FormaPago", f.FormaPago);
                    cmd.Parameters.AddWithValue("@Monto", f.MontoPagado);
                    cmd.Parameters.Add("@FechaPago", SqlDbType.DateTime).Value = f.FechaPago;
                    cmd.Parameters.AddWithValue("@Id", f.Id);
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        tx.Rollback();
                        return false;
                    }

                    foreach (ItemFactura item in f.Items)
                    {
                        // Solo descuenta si alcanza el stock; si no, no se toca nada
                        SqlCommand cmdStock = new SqlCommand(
                            @"UPDATE Producto SET Existencia = Existencia - @Cantidad
                              WHERE Id = @IdProducto AND Existencia >= @Cantidad", con, tx);
                        cmdStock.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                        cmdStock.Parameters.AddWithValue("@IdProducto", item.Producto.Id);
                        if (cmdStock.ExecuteNonQuery() == 0)
                        {
                            tx.Rollback();
                            idProductoSinStock = item.Producto.Id;
                            return false;
                        }
                    }

                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}
