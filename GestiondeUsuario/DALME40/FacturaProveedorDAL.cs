using BE;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // PN2: factura del proveedor y su pago (CUN-010 Gestionar pago)
    public class FacturaProveedorDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        public bool ExisteNroFactura(int idProveedor, string nroFactura)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM FacturaProveedor WHERE IdProveedor = @IdProveedor AND NroFactura = @Nro", con);
                cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);
                cmd.Parameters.AddWithValue("@Nro", nroFactura);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        // Número de la última factura registrada de un proveedor, o null si todavía no tiene
        public string UltimoNroFactura(int idProveedor)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT TOP 1 NroFactura FROM FacturaProveedor WHERE IdProveedor = @IdProveedor ORDER BY Fecha DESC, Id DESC", con);
                cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);
                con.Open();
                return cmd.ExecuteScalar() as string;
            }
        }

        // Registra la factura pagada y la orden pasa de Enviada a Pagada, todo junto.
        // Devuelve false si la orden ya no estaba Enviada.
        public bool RegistrarPago(FacturaProveedor f)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction();
                try
                {
                    SqlCommand cmdOrden = new SqlCommand(
                        "UPDATE OrdenCompra SET Estado = @Pagada WHERE Id = @Id AND Estado = @Enviada", con, tx);
                    cmdOrden.Parameters.AddWithValue("@Pagada", OrdenCompra.EstadoPagada);
                    cmdOrden.Parameters.AddWithValue("@Enviada", OrdenCompra.EstadoEnviada);
                    cmdOrden.Parameters.AddWithValue("@Id", f.IdOrdenCompra);
                    if (cmdOrden.ExecuteNonQuery() == 0)
                    {
                        tx.Rollback();
                        return false;
                    }

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO FacturaProveedor
                        (IdOrdenCompra, IdProveedor, NroFactura, Fecha, Importe, Impuestos, MontoAbonado,
                         FechaOperacion, NroComprobante, Estado, Supervisor)
                        VALUES (@IdOrden, @IdProveedor, @Nro, @Fecha, @Importe, @Impuestos, @Monto,
                         @FechaOperacion, @Comprobante, @Estado, @Supervisor);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);", con, tx);
                    cmd.Parameters.AddWithValue("@IdOrden", f.IdOrdenCompra);
                    cmd.Parameters.AddWithValue("@IdProveedor", f.IdProveedor);
                    cmd.Parameters.AddWithValue("@Nro", f.NroFactura);
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = f.Fecha;
                    cmd.Parameters.AddWithValue("@Importe", f.Importe);
                    cmd.Parameters.AddWithValue("@Impuestos", f.Impuestos);
                    cmd.Parameters.AddWithValue("@Monto", f.MontoAbonado);
                    cmd.Parameters.Add("@FechaOperacion", SqlDbType.DateTime).Value = f.FechaOperacion;
                    cmd.Parameters.AddWithValue("@Comprobante", f.NroComprobante);
                    cmd.Parameters.AddWithValue("@Estado", FacturaProveedor.EstadoPagado);
                    cmd.Parameters.AddWithValue("@Supervisor", f.Supervisor);
                    f.Id = Convert.ToInt32(cmd.ExecuteScalar());

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

        public FacturaProveedor ObtenerPorOrden(int idOrdenCompra)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM FacturaProveedor WHERE IdOrdenCompra = @Id", con);
                cmd.Parameters.AddWithValue("@Id", idOrdenCompra);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.Read())
                    return null;
                return new FacturaProveedor
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    IdOrdenCompra = Convert.ToInt32(reader["IdOrdenCompra"]),
                    IdProveedor = Convert.ToInt32(reader["IdProveedor"]),
                    NroFactura = reader["NroFactura"].ToString(),
                    Fecha = Convert.ToDateTime(reader["Fecha"]),
                    Importe = Convert.ToDecimal(reader["Importe"]),
                    Impuestos = Convert.ToDecimal(reader["Impuestos"]),
                    MontoAbonado = Convert.ToDecimal(reader["MontoAbonado"]),
                    FechaOperacion = Convert.ToDateTime(reader["FechaOperacion"]),
                    NroComprobante = reader["NroComprobante"].ToString(),
                    Estado = reader["Estado"].ToString(),
                    Supervisor = reader["Supervisor"].ToString()
                };
            }
        }
    }
}
