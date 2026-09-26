using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class ProductoDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        internal static Producto MapearProducto(SqlDataReader reader)
        {
            return new Producto()
            {
                Id = Convert.ToInt32(reader["Id"]),
                CodigoProducto = Convert.ToInt32(reader["CodigoProducto"]),
                Nombre = reader["Nombre"].ToString(),
                Marca = reader["Marca"].ToString(),
                Color = reader["Color"].ToString(),
                Modelo = reader["Modelo"].ToString(),
                PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                Existencia = Convert.ToInt32(reader["Existencia"]),
                Activo = Convert.ToBoolean(reader["Activo"])
            };
        }

        public List<Producto> ObtenerActivos()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Producto WHERE Activo = 1 ORDER BY Nombre";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lista.Add(MapearProducto(reader));
            }
            return lista;
        }

        // Busca por código exacto, o por nombre / marca que contengan el texto
        public List<Producto> Buscar(string texto)
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM Producto
                    WHERE Activo = 1
                      AND (Nombre LIKE @Texto OR Marca LIKE @Texto
                           OR CAST(CodigoProducto AS NVARCHAR(20)) = @Codigo)
                    ORDER BY Nombre";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Texto", "%" + texto + "%");
                cmd.Parameters.AddWithValue("@Codigo", texto);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lista.Add(MapearProducto(reader));
            }
            return lista;
        }

        public Producto ObtenerPorCodigo(int codigoProducto)
        {
            Producto producto = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Producto WHERE CodigoProducto = @Codigo AND Activo = 1";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Codigo", codigoProducto);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    producto = MapearProducto(reader);
            }
            return producto;
        }

        public Producto ObtenerPorId(int id)
        {
            Producto producto = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Producto WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    producto = MapearProducto(reader);
            }
            return producto;
        }
    }
}
