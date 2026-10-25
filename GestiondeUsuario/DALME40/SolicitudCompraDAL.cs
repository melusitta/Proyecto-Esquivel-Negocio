using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // PN2: solicitudes de compra (CUN-006 Generar solicitud de compra)
    public class SolicitudCompraDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        private SolicitudCompra MapearSolicitud(SqlDataReader reader)
        {
            return new SolicitudCompra
            {
                Id = Convert.ToInt32(reader["Id"]),
                NroSolicitud = Convert.ToInt32(reader["NroSolicitud"]),
                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                CreadaPor = reader["CreadaPor"].ToString(),
                Prioridad = reader["Prioridad"] == DBNull.Value ? null : reader["Prioridad"].ToString(),
                Estado = reader["Estado"].ToString(),
                FechaEnvio = reader["FechaEnvio"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["FechaEnvio"]),
                Supervisor = reader["Supervisor"] == DBNull.Value ? null : reader["Supervisor"].ToString()
            };
        }

        public SolicitudCompra ObtenerPorId(int id)
        {
            SolicitudCompra s = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM SolicitudCompra WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                    if (reader.Read())
                        s = MapearSolicitud(reader);
                if (s != null)
                    CargarItems(con, s);
            }
            return s;
        }

        public List<SolicitudCompra> ObtenerPorEstado(string estado)
        {
            var lista = new List<SolicitudCompra>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM SolicitudCompra WHERE Estado = @Estado ORDER BY NroSolicitud", con);
                cmd.Parameters.AddWithValue("@Estado", estado);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                        lista.Add(MapearSolicitud(reader));
                foreach (SolicitudCompra s in lista)
                    CargarItems(con, s);
            }
            return lista;
        }

        private void CargarItems(SqlConnection con, SolicitudCompra s)
        {
            string query = @"SELECT i.Cantidad, p.*,
                    pr.Id AS ProvId, pr.RazonSocial AS ProvRazonSocial, pr.CUIT AS ProvCUIT, pr.Estado AS ProvEstado
                FROM ItemSolicitud i
                INNER JOIN Producto p ON p.Id = i.IdProducto
                INNER JOIN Proveedor pr ON pr.Id = i.IdProveedor
                WHERE i.IdSolicitud = @Id
                ORDER BY pr.RazonSocial, p.Nombre";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", s.Id);
            using (SqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                    s.Items.Add(new ItemSolicitud
                    {
                        Producto = ProductoDAL.MapearProducto(reader),
                        Proveedor = new Proveedor
                        {
                            Id = Convert.ToInt32(reader["ProvId"]),
                            RazonSocial = reader["ProvRazonSocial"].ToString(),
                            CUIT = reader["ProvCUIT"].ToString(),
                            Estado = reader["ProvEstado"].ToString()
                        },
                        Cantidad = Convert.ToInt32(reader["Cantidad"])
                    });
        }

        // Productos que ya están en reposición: en una solicitud abierta o en una orden que todavía no se recibió
        public HashSet<int> ProductosEnCurso()
        {
            var ids = new HashSet<int>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Lo descartado (cotización Rechazada) deja de estar en reposición y se puede volver a pedir
                string query = @"SELECT i.IdProducto FROM ItemSolicitud i
                        INNER JOIN SolicitudCompra s ON s.Id = i.IdSolicitud
                        WHERE s.Estado IN (@Borrador, @Enviada)
                        AND NOT EXISTS (SELECT 1 FROM Cotizacion c WHERE c.IdSolicitud = i.IdSolicitud
                                        AND c.IdProveedor = i.IdProveedor AND c.Estado = @Rechazada)
                    UNION
                    SELECT io.IdProducto FROM ItemOrdenCompra io
                        INNER JOIN OrdenCompra o ON o.Id = io.IdOrdenCompra
                        WHERE o.Estado <> @Recibida";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Borrador", SolicitudCompra.EstadoBorrador);
                cmd.Parameters.AddWithValue("@Enviada", SolicitudCompra.EstadoEnviada);
                cmd.Parameters.AddWithValue("@Recibida", OrdenCompra.EstadoRecibida);
                cmd.Parameters.AddWithValue("@Rechazada", Cotizacion.EstadoRechazada);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                        ids.Add(Convert.ToInt32(reader[0]));
            }
            return ids;
        }

        // CUN-006: crea la solicitud ya enviada a los proveedores, con número correlativo. Devuelve el Id.
        public int Crear(SolicitudCompra s)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    SqlCommand cmdNro = new SqlCommand(
                        "SELECT ISNULL(MAX(NroSolicitud), 0) + 1 FROM SolicitudCompra WITH (UPDLOCK, HOLDLOCK)", con, tx);
                    s.NroSolicitud = Convert.ToInt32(cmdNro.ExecuteScalar());

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO SolicitudCompra
                        (NroSolicitud, FechaCreacion, CreadaPor, Prioridad, Estado, FechaEnvio)
                        VALUES (@Nro, @Fecha, @CreadaPor, @Prioridad, @Estado, @FechaEnvio); SELECT CAST(SCOPE_IDENTITY() AS INT);", con, tx);
                    cmd.Parameters.AddWithValue("@Nro", s.NroSolicitud);
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = s.FechaCreacion;
                    cmd.Parameters.AddWithValue("@CreadaPor", s.CreadaPor);
                    cmd.Parameters.AddWithValue("@Prioridad", s.Prioridad);
                    cmd.Parameters.AddWithValue("@Estado", SolicitudCompra.EstadoEnviada);
                    cmd.Parameters.Add("@FechaEnvio", SqlDbType.DateTime).Value = s.FechaEnvio ?? s.FechaCreacion;
                    s.Id = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (ItemSolicitud item in s.Items)
                    {
                        SqlCommand cmdItem = new SqlCommand(@"INSERT INTO ItemSolicitud (IdSolicitud, IdProducto, IdProveedor, Cantidad)
                            VALUES (@IdSolicitud, @IdProducto, @IdProveedor, @Cantidad)", con, tx);
                        cmdItem.Parameters.AddWithValue("@IdSolicitud", s.Id);
                        cmdItem.Parameters.AddWithValue("@IdProducto", item.Producto.Id);
                        cmdItem.Parameters.AddWithValue("@IdProveedor", item.Proveedor.Id);
                        cmdItem.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                        cmdItem.ExecuteNonQuery();
                    }
                    tx.Commit();
                    return s.Id;
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
