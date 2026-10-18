using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // PN2: cotizaciones de los proveedores y órdenes de compra (CUN-009 Generar orden de compra)
    public class OrdenCompraDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        // ---------- Cotizaciones ----------

        public int InsertarCotizacion(Cotizacion c)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Cotizacion
                        (IdSolicitud, IdProveedor, FechaRecepcion, PlazoEntregaDias, FormaPago, Estado)
                        VALUES (@IdSolicitud, @IdProveedor, @Fecha, @Plazo, @FormaPago, @Estado);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);", con, tx);
                    cmd.Parameters.AddWithValue("@IdSolicitud", c.IdSolicitud);
                    cmd.Parameters.AddWithValue("@IdProveedor", c.Proveedor.Id);
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = c.FechaRecepcion;
                    cmd.Parameters.AddWithValue("@Plazo", c.PlazoEntregaDias);
                    cmd.Parameters.AddWithValue("@FormaPago", c.FormaPago);
                    cmd.Parameters.AddWithValue("@Estado", Cotizacion.EstadoRecibida);
                    c.Id = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (ItemCotizacion item in c.Items)
                    {
                        SqlCommand cmdItem = new SqlCommand(@"INSERT INTO ItemCotizacion
                            (IdCotizacion, IdProducto, PrecioUnitario, Marca, Presentacion, CantidadDisponible)
                            VALUES (@IdCotizacion, @IdProducto, @Precio, @Marca, @Presentacion, @Cantidad)", con, tx);
                        cmdItem.Parameters.AddWithValue("@IdCotizacion", c.Id);
                        cmdItem.Parameters.AddWithValue("@IdProducto", item.Producto.Id);
                        cmdItem.Parameters.AddWithValue("@Precio", item.PrecioUnitario);
                        cmdItem.Parameters.AddWithValue("@Marca", item.Marca);
                        cmdItem.Parameters.AddWithValue("@Presentacion", item.Presentacion);
                        cmdItem.Parameters.AddWithValue("@Cantidad", item.CantidadDisponible);
                        cmdItem.ExecuteNonQuery();
                    }
                    tx.Commit();
                    return c.Id;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public List<Cotizacion> ObtenerCotizaciones(int idSolicitud)
        {
            return LeerCotizaciones("SELECT * FROM Cotizacion WHERE IdSolicitud = @Valor ORDER BY FechaRecepcion", idSolicitud);
        }

        public Cotizacion ObtenerCotizacion(int idCotizacion)
        {
            var lista = LeerCotizaciones("SELECT * FROM Cotizacion WHERE Id = @Valor", idCotizacion);
            return lista.Count > 0 ? lista[0] : null;
        }

        private List<Cotizacion> LeerCotizaciones(string query, int valor)
        {
            var lista = new List<Cotizacion>();
            var idsProveedor = new List<int>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Valor", valor);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        lista.Add(new Cotizacion
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            IdSolicitud = Convert.ToInt32(reader["IdSolicitud"]),
                            FechaRecepcion = Convert.ToDateTime(reader["FechaRecepcion"]),
                            PlazoEntregaDias = Convert.ToInt32(reader["PlazoEntregaDias"]),
                            FormaPago = reader["FormaPago"].ToString(),
                            Estado = reader["Estado"].ToString()
                        });
                        idsProveedor.Add(Convert.ToInt32(reader["IdProveedor"]));
                    }

                foreach (Cotizacion c in lista)
                {
                    SqlCommand cmdItems = new SqlCommand(@"SELECT ic.PrecioUnitario AS PrecioCotizado, ic.Marca AS MarcaCotizada,
                            ic.Presentacion, ic.CantidadDisponible, p.*
                        FROM ItemCotizacion ic INNER JOIN Producto p ON p.Id = ic.IdProducto
                        WHERE ic.IdCotizacion = @Id ORDER BY p.Nombre", con);
                    cmdItems.Parameters.AddWithValue("@Id", c.Id);
                    using (SqlDataReader reader = cmdItems.ExecuteReader())
                        while (reader.Read())
                            c.Items.Add(new ItemCotizacion
                            {
                                Producto = ProductoDAL.MapearProducto(reader),
                                PrecioUnitario = Convert.ToDecimal(reader["PrecioCotizado"]),
                                Marca = reader["MarcaCotizada"].ToString(),
                                Presentacion = reader["Presentacion"].ToString(),
                                CantidadDisponible = Convert.ToInt32(reader["CantidadDisponible"])
                            });
                }
            }
            var proveedores = new ProveedorDAL();
            for (int i = 0; i < lista.Count; i++)
                lista[i].Proveedor = proveedores.ObtenerPorId(idsProveedor[i]);
            return lista;
        }

        // ---------- Órdenes de compra ----------

        // Productos de la solicitud que ya tienen orden de compra
        public HashSet<int> ProductosConOrden(int idSolicitud)
        {
            var ids = new HashSet<int>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"SELECT io.IdProducto FROM ItemOrdenCompra io
                    INNER JOIN OrdenCompra o ON o.Id = io.IdOrdenCompra WHERE o.IdSolicitud = @Id", con);
                cmd.Parameters.AddWithValue("@Id", idSolicitud);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                        ids.Add(Convert.ToInt32(reader[0]));
            }
            return ids;
        }

        // Genera la orden en una transacción: número correlativo, cabecera y detalle; la cotización
        // pasa a Aceptada y la solicitud se cierra si ya tiene orden para todos sus productos.
        // Devuelve el Id de la orden, o 0 si la cotización ya había sido aceptada.
        public int Generar(OrdenCompra o)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    SqlCommand cmdCot = new SqlCommand(
                        "UPDATE Cotizacion SET Estado = @Aceptada WHERE Id = @Id AND Estado = @Recibida", con, tx);
                    cmdCot.Parameters.AddWithValue("@Aceptada", Cotizacion.EstadoAceptada);
                    cmdCot.Parameters.AddWithValue("@Recibida", Cotizacion.EstadoRecibida);
                    cmdCot.Parameters.AddWithValue("@Id", o.IdCotizacion);
                    if (cmdCot.ExecuteNonQuery() == 0)
                    {
                        tx.Rollback();
                        return 0;
                    }

                    SqlCommand cmdNro = new SqlCommand(
                        "SELECT ISNULL(MAX(NroOrden), 0) + 1 FROM OrdenCompra WITH (UPDLOCK, HOLDLOCK)", con, tx);
                    o.NroOrden = Convert.ToInt32(cmdNro.ExecuteScalar());

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO OrdenCompra
                        (NroOrden, IdSolicitud, IdCotizacion, IdProveedor, FechaEmision, CondicionesEntrega,
                         CondicionesPago, Total, Estado, Supervisor)
                        VALUES (@Nro, @IdSolicitud, @IdCotizacion, @IdProveedor, @Fecha, @Entrega, @Pago, @Total, @Estado, @Supervisor);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);", con, tx);
                    cmd.Parameters.AddWithValue("@Nro", o.NroOrden);
                    cmd.Parameters.AddWithValue("@IdSolicitud", o.IdSolicitud);
                    cmd.Parameters.AddWithValue("@IdCotizacion", o.IdCotizacion);
                    cmd.Parameters.AddWithValue("@IdProveedor", o.Proveedor.Id);
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = o.FechaEmision;
                    cmd.Parameters.AddWithValue("@Entrega", o.CondicionesEntrega);
                    cmd.Parameters.AddWithValue("@Pago", o.CondicionesPago);
                    cmd.Parameters.AddWithValue("@Total", o.Total);
                    cmd.Parameters.AddWithValue("@Estado", OrdenCompra.EstadoEmitida);
                    cmd.Parameters.AddWithValue("@Supervisor", o.Supervisor);
                    o.Id = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (ItemOrdenCompra item in o.Items)
                    {
                        SqlCommand cmdItem = new SqlCommand(@"INSERT INTO ItemOrdenCompra (IdOrdenCompra, IdProducto, Cantidad, PrecioUnitario)
                            VALUES (@IdOrden, @IdProducto, @Cantidad, @Precio)", con, tx);
                        cmdItem.Parameters.AddWithValue("@IdOrden", o.Id);
                        cmdItem.Parameters.AddWithValue("@IdProducto", item.Producto.Id);
                        cmdItem.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                        cmdItem.Parameters.AddWithValue("@Precio", item.PrecioUnitario);
                        cmdItem.ExecuteNonQuery();
                    }

                    SqlCommand cmdCerrar = new SqlCommand(@"UPDATE SolicitudCompra SET Estado = @Cerrada
                        WHERE Id = @IdSolicitud AND NOT EXISTS (
                            SELECT 1 FROM ItemSolicitud i WHERE i.IdSolicitud = @IdSolicitud AND NOT EXISTS (
                                SELECT 1 FROM ItemOrdenCompra io INNER JOIN OrdenCompra oc ON oc.Id = io.IdOrdenCompra
                                WHERE oc.IdSolicitud = @IdSolicitud AND io.IdProducto = i.IdProducto))", con, tx);
                    cmdCerrar.Parameters.AddWithValue("@Cerrada", SolicitudCompra.EstadoCerrada);
                    cmdCerrar.Parameters.AddWithValue("@IdSolicitud", o.IdSolicitud);
                    cmdCerrar.ExecuteNonQuery();

                    tx.Commit();
                    return o.Id;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public OrdenCompra ObtenerPorId(int id)
        {
            var lista = LeerOrdenes("SELECT * FROM OrdenCompra WHERE Id = @Valor", id, null);
            return lista.Count > 0 ? lista[0] : null;
        }

        public List<OrdenCompra> ObtenerPorEstado(string estado)
        {
            return LeerOrdenes("SELECT * FROM OrdenCompra WHERE Estado = @Texto ORDER BY NroOrden", 0, estado);
        }

        public List<OrdenCompra> ObtenerPorSolicitud(int idSolicitud)
        {
            return LeerOrdenes("SELECT * FROM OrdenCompra WHERE IdSolicitud = @Valor ORDER BY NroOrden", idSolicitud, null);
        }

        private List<OrdenCompra> LeerOrdenes(string query, int valor, string texto)
        {
            var lista = new List<OrdenCompra>();
            var idsProveedor = new List<int>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.Parameters.AddWithValue("@Texto", (object)texto ?? DBNull.Value);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        lista.Add(new OrdenCompra
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            NroOrden = Convert.ToInt32(reader["NroOrden"]),
                            IdSolicitud = Convert.ToInt32(reader["IdSolicitud"]),
                            IdCotizacion = Convert.ToInt32(reader["IdCotizacion"]),
                            FechaEmision = Convert.ToDateTime(reader["FechaEmision"]),
                            CondicionesEntrega = reader["CondicionesEntrega"].ToString(),
                            CondicionesPago = reader["CondicionesPago"].ToString(),
                            Total = Convert.ToDecimal(reader["Total"]),
                            Estado = reader["Estado"].ToString(),
                            FechaEnvio = reader["FechaEnvio"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["FechaEnvio"]),
                            Supervisor = reader["Supervisor"].ToString()
                        });
                        idsProveedor.Add(Convert.ToInt32(reader["IdProveedor"]));
                    }

                foreach (OrdenCompra o in lista)
                {
                    SqlCommand cmdItems = new SqlCommand(@"SELECT io.Cantidad AS CantidadOrden, io.PrecioUnitario AS PrecioOrden, p.*
                        FROM ItemOrdenCompra io INNER JOIN Producto p ON p.Id = io.IdProducto
                        WHERE io.IdOrdenCompra = @Id ORDER BY p.Nombre", con);
                    cmdItems.Parameters.AddWithValue("@Id", o.Id);
                    using (SqlDataReader reader = cmdItems.ExecuteReader())
                        while (reader.Read())
                            o.Items.Add(new ItemOrdenCompra
                            {
                                Producto = ProductoDAL.MapearProducto(reader),
                                Cantidad = Convert.ToInt32(reader["CantidadOrden"]),
                                PrecioUnitario = Convert.ToDecimal(reader["PrecioOrden"])
                            });
                }
            }
            var proveedores = new ProveedorDAL();
            for (int i = 0; i < lista.Count; i++)
                lista[i].Proveedor = proveedores.ObtenerPorId(idsProveedor[i]);
            return lista;
        }

        // Paso 7: la orden Emitida pasa a Enviada. Devuelve false si ya no estaba Emitida.
        public bool MarcarEnviada(int idOrden, DateTime fecha)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE OrdenCompra SET Estado = @Enviada, FechaEnvio = @Fecha
                    WHERE Id = @Id AND Estado = @Emitida", con);
                cmd.Parameters.AddWithValue("@Enviada", OrdenCompra.EstadoEnviada);
                cmd.Parameters.AddWithValue("@Emitida", OrdenCompra.EstadoEmitida);
                cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = fecha;
                cmd.Parameters.AddWithValue("@Id", idOrden);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
