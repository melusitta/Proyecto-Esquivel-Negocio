using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ReporteDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        // Unidades y monto vendidos por producto, solo de facturas cobradas en [desde, hastaExclusivo)
        public List<ProductoVendido> ObtenerProductosVendidos(DateTime desde, DateTime hastaExclusivo)
        {
            var lista = new List<ProductoVendido>();
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
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lista.Add(new ProductoVendido
                    {
                        CodigoProducto = Convert.ToInt32(reader["CodigoProducto"]),
                        Nombre = reader["Nombre"].ToString(),
                        Marca = reader["Marca"].ToString(),
                        Unidades = Convert.ToInt32(reader["Unidades"]),
                        Monto = Convert.ToDecimal(reader["Monto"])
                    });
            }
            return lista;
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
