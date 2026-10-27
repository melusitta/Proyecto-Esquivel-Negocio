using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    // Control de cambios del maestro de productos: versiones guardadas en ProductoVersion
    public class ProductoVersionDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        private const string Consulta = @"SELECT v.Id AS IdVersion, v.NroVersion, v.TipoCambio, v.Fecha, v.Usuario, v.Detalle,
                v.IdProducto AS Id, v.CodigoProducto, v.Nombre, v.Marca, v.Color, v.Modelo, v.PrecioUnitario, v.Existencia,
                v.Activo, v.Categoria, v.StockMinimo, v.IdProveedorHabitual, pr.RazonSocial AS ProveedorHabitual
            FROM ProductoVersion v LEFT JOIN Proveedor pr ON pr.Id = v.IdProveedorHabitual";

        // Guarda como versión nueva los datos que el producto tiene ahora en la base
        public void Guardar(int idProducto, string tipoCambio, string usuario, string detalle)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO ProductoVersion
                    (IdProducto, NroVersion, TipoCambio, Fecha, Usuario, Detalle, CodigoProducto, Nombre, Marca, Color, Modelo,
                     PrecioUnitario, Existencia, Activo, Categoria, StockMinimo, IdProveedorHabitual)
                    SELECT p.Id,
                           ISNULL((SELECT MAX(NroVersion) FROM ProductoVersion WITH (UPDLOCK, HOLDLOCK) WHERE IdProducto = p.Id), 0) + 1,
                           @Tipo, GETDATE(), @Usuario, @Detalle, p.CodigoProducto, p.Nombre, p.Marca, p.Color, p.Modelo,
                           p.PrecioUnitario, p.Existencia, p.Activo, p.Categoria, p.StockMinimo, p.IdProveedorHabitual
                    FROM Producto p WHERE p.Id = @IdProducto";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                cmd.Parameters.AddWithValue("@Tipo", tipoCambio);
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Detalle", (object)detalle ?? DBNull.Value);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Versiones de un producto, de la más nueva a la más vieja
        public List<ProductoVersion> ObtenerPorProducto(int idProducto)
        {
            return Leer(Consulta + " WHERE v.IdProducto = @Valor ORDER BY v.NroVersion DESC", idProducto);
        }

        public ProductoVersion ObtenerPorId(int idVersion)
        {
            var lista = Leer(Consulta + " WHERE v.Id = @Valor", idVersion);
            return lista.Count > 0 ? lista[0] : null;
        }

        private List<ProductoVersion> Leer(string query, int valor)
        {
            var lista = new List<ProductoVersion>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Valor", valor);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                        lista.Add(new ProductoVersion
                        {
                            Id = Convert.ToInt32(reader["IdVersion"]),
                            NroVersion = Convert.ToInt32(reader["NroVersion"]),
                            TipoCambio = reader["TipoCambio"].ToString(),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Usuario = reader["Usuario"].ToString(),
                            Detalle = reader["Detalle"] == DBNull.Value ? null : reader["Detalle"].ToString(),
                            Datos = ProductoDAL.MapearProducto(reader)
                        });
            }
            return lista;
        }
    }
}
