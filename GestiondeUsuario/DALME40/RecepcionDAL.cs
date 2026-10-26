using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // PN2: recepción de la mercadería en el depósito (CUN-011 Registrar productos recibidos)
    public class RecepcionDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        // Reporte de recibos de mercadería: todas las recepciones con su detalle, de la más nueva a la más vieja
        public List<Recepcion> ObtenerTodas()
        {
            var lista = new List<Recepcion>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Recepcion ORDER BY Fecha DESC, Id DESC", con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                        lista.Add(new Recepcion
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            IdOrdenCompra = Convert.ToInt32(reader["IdOrdenCompra"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Responsable = reader["Responsable"].ToString(),
                            Comentario = reader["Comentario"] == DBNull.Value ? null : reader["Comentario"].ToString()
                        });

                foreach (Recepcion r in lista)
                {
                    SqlCommand cmdItems = new SqlCommand(@"SELECT ir.CantidadRecibida, ir.Estado AS EstadoRecibido, ir.Ubicacion, p.*
                        FROM ItemRecepcion ir INNER JOIN Producto p ON p.Id = ir.IdProducto
                        WHERE ir.IdRecepcion = @Id ORDER BY p.Nombre", con);
                    cmdItems.Parameters.AddWithValue("@Id", r.Id);
                    using (SqlDataReader reader = cmdItems.ExecuteReader())
                        while (reader.Read())
                            r.Items.Add(new ItemRecepcion
                            {
                                Producto = ProductoDAL.MapearProducto(reader),
                                CantidadRecibida = Convert.ToInt32(reader["CantidadRecibida"]),
                                Estado = reader["EstadoRecibido"].ToString(),
                                Ubicacion = reader["Ubicacion"].ToString()
                            });
                }
            }
            return lista;
        }

        // Registra la recepción en una transacción: la orden pasa de Pagada a Recibida, se guarda
        // el detalle y se suma al stock lo recibido en buen estado. Devuelve false si la orden ya no estaba Pagada.
        public bool Registrar(Recepcion r)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction();
                try
                {
                    SqlCommand cmdOrden = new SqlCommand(
                        "UPDATE OrdenCompra SET Estado = @Recibida WHERE Id = @Id AND Estado = @Pagada", con, tx);
                    cmdOrden.Parameters.AddWithValue("@Recibida", OrdenCompra.EstadoRecibida);
                    cmdOrden.Parameters.AddWithValue("@Pagada", OrdenCompra.EstadoPagada);
                    cmdOrden.Parameters.AddWithValue("@Id", r.IdOrdenCompra);
                    if (cmdOrden.ExecuteNonQuery() == 0)
                    {
                        tx.Rollback();
                        return false;
                    }

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Recepcion (IdOrdenCompra, Fecha, Responsable, Comentario)
                        VALUES (@IdOrden, @Fecha, @Responsable, @Comentario); SELECT CAST(SCOPE_IDENTITY() AS INT);", con, tx);
                    cmd.Parameters.AddWithValue("@IdOrden", r.IdOrdenCompra);
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = r.Fecha;
                    cmd.Parameters.AddWithValue("@Responsable", r.Responsable);
                    cmd.Parameters.AddWithValue("@Comentario", (object)r.Comentario ?? DBNull.Value);
                    r.Id = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (ItemRecepcion item in r.Items)
                    {
                        SqlCommand cmdItem = new SqlCommand(@"INSERT INTO ItemRecepcion
                            (IdRecepcion, IdProducto, CantidadRecibida, Estado, Ubicacion)
                            VALUES (@IdRecepcion, @IdProducto, @Cantidad, @Estado, @Ubicacion)", con, tx);
                        cmdItem.Parameters.AddWithValue("@IdRecepcion", r.Id);
                        cmdItem.Parameters.AddWithValue("@IdProducto", item.Producto.Id);
                        cmdItem.Parameters.AddWithValue("@Cantidad", item.CantidadRecibida);
                        cmdItem.Parameters.AddWithValue("@Estado", item.Estado);
                        cmdItem.Parameters.AddWithValue("@Ubicacion", item.Ubicacion ?? "");
                        cmdItem.ExecuteNonQuery();

                        if (item.Estado == ItemRecepcion.EstadoBueno && item.CantidadRecibida > 0)
                        {
                            SqlCommand cmdStock = new SqlCommand(
                                "UPDATE Producto SET Existencia = Existencia + @Cantidad WHERE Id = @IdProducto", con, tx);
                            cmdStock.Parameters.AddWithValue("@Cantidad", item.CantidadRecibida);
                            cmdStock.Parameters.AddWithValue("@IdProducto", item.Producto.Id);
                            cmdStock.ExecuteNonQuery();
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
