using BE;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ReporteDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        // Unidades y monto vendidos por producto, solo de facturas cobradas en [desde, hastaExclusivo)
        // Columnas: CodigoProducto, Nombre, Marca, Unidades, Monto
        public DataTable ObtenerProductosVendidos(DateTime desde, DateTime hastaExclusivo)
        {
            DataTable tabla = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT p.CodigoProducto, p.Nombre, p.Marca,
                        SUM(itf.Cantidad) AS Unidades,
                        SUM(itf.Cantidad * itf.PrecioUnitario) AS Monto
                    FROM ItemFactura itf
                    INNER JOIN Factura f ON f.Id = itf.IdFactura
                    INNER JOIN Producto p ON p.Id = itf.IdProducto
                    WHERE f.Estado = @Pagada AND f.FechaPago >= @Desde AND f.FechaPago < @Hasta
                    GROUP BY p.Id, p.CodigoProducto, p.Nombre, p.Marca";
                SqlCommand cmd = new SqlCommand(query, con);
                CargarParametros(cmd, desde, hastaExclusivo);
                new SqlDataAdapter(cmd).Fill(tabla);
            }
            return tabla;
        }

        public int ContarFacturasCobradas(DateTime desde, DateTime hastaExclusivo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT COUNT(*) FROM Factura
                    WHERE Estado = @Pagada AND FechaPago >= @Desde AND FechaPago < @Hasta";
                SqlCommand cmd = new SqlCommand(query, con);
                CargarParametros(cmd, desde, hastaExclusivo);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void CargarParametros(SqlCommand cmd, DateTime desde, DateTime hastaExclusivo)
        {
            cmd.Parameters.AddWithValue("@Pagada", Factura.EstadoPagada);
            cmd.Parameters.Add("@Desde", SqlDbType.DateTime).Value = desde;
            cmd.Parameters.Add("@Hasta", SqlDbType.DateTime).Value = hastaExclusivo;
        }
    }
}
