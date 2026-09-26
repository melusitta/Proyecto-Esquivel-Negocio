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

        // Todos los productos, activos e inactivos (para el ABM)
        public List<Producto> ObtenerTodos()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Producto ORDER BY Nombre";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lista.Add(MapearProducto(reader));
            }
            return lista;
        }

        // ¿Hay otro producto (activo o no) con ese código? excluirId permite ignorar al que se está modificando
        public bool ExisteCodigo(int codigoProducto, int excluirId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Producto WHERE CodigoProducto = @Codigo AND Id <> @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Codigo", codigoProducto);
                cmd.Parameters.AddWithValue("@Id", excluirId);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public bool Insertar(Producto p)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Producto
                    (CodigoProducto, Nombre, Marca, Color, Modelo, PrecioUnitario, Existencia, Activo)
                    VALUES (@Codigo, @Nombre, @Marca, @Color, @Modelo, @Precio, @Existencia, 1)";
                SqlCommand cmd = new SqlCommand(query, con);
                CargarParametros(cmd, p);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Modificar(Producto p)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Producto SET
                    CodigoProducto = @Codigo, Nombre = @Nombre, Marca = @Marca, Color = @Color,
                    Modelo = @Modelo, PrecioUnitario = @Precio, Existencia = @Existencia
                    WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                CargarParametros(cmd, p);
                cmd.Parameters.AddWithValue("@Id", p.Id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Baja lógica: el producto queda en carritos y facturas viejas, pero deja de venderse
        public bool CambiarActivo(int id, bool activo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Producto SET Activo = @Activo WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Activo", activo);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private void CargarParametros(SqlCommand cmd, Producto p)
        {
            cmd.Parameters.AddWithValue("@Codigo", p.CodigoProducto);
            cmd.Parameters.AddWithValue("@Nombre", p.Nombre);
            cmd.Parameters.AddWithValue("@Marca", p.Marca);
            cmd.Parameters.AddWithValue("@Color", p.Color);
            cmd.Parameters.AddWithValue("@Modelo", p.Modelo);
            cmd.Parameters.AddWithValue("@Precio", p.PrecioUnitario);
            cmd.Parameters.AddWithValue("@Existencia", p.Existencia);
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
