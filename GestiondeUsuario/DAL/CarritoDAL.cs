using BE;
using System;
using System.Data.SqlClient;

namespace DAL
{
    public class CarritoDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        private Carrito MapearCarrito(SqlDataReader reader)
        {
            return new Carrito()
            {
                Id = Convert.ToInt32(reader["Id"]),
                DNI = reader["DNI"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["DNI"]),
                Estado = reader["Estado"].ToString(),
                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                Vendedor = reader["Vendedor"].ToString()
            };
        }

        // Crea un carrito vacío en estado Abierto y devuelve su Id
        public int Crear(string vendedor)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Carrito (Estado, FechaCreacion, Vendedor)
                    VALUES (@Estado, GETDATE(), @Vendedor);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Estado", Carrito.EstadoAbierto);
                cmd.Parameters.AddWithValue("@Vendedor", vendedor);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public Carrito ObtenerPorId(int id)
        {
            Carrito carrito = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Carrito WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        carrito = MapearCarrito(reader);
                }
                if (carrito != null)
                    CargarItems(con, carrito);
            }
            return carrito;
        }

        // El carrito asociado más reciente de ese DNI que todavía no se facturó
        public Carrito ObtenerAsociadoPorDNI(int dni)
        {
            Carrito carrito = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT TOP 1 * FROM Carrito
                    WHERE DNI = @DNI AND Estado = @Estado
                    ORDER BY FechaCreacion DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DNI", dni);
                cmd.Parameters.AddWithValue("@Estado", Carrito.EstadoAsociado);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        carrito = MapearCarrito(reader);
                }
                if (carrito != null)
                    CargarItems(con, carrito);
            }
            return carrito;
        }

        private void CargarItems(SqlConnection con, Carrito carrito)
        {
            string query = @"SELECT ic.Cantidad, ic.PrecioUnitario AS PrecioItem, p.*
                FROM ItemCarrito ic
                INNER JOIN Producto p ON p.Id = ic.IdProducto
                WHERE ic.IdCarrito = @IdCarrito
                ORDER BY p.Nombre";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@IdCarrito", carrito.Id);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    carrito.Items.Add(new ItemCarrito
                    {
                        Producto = ProductoDAL.MapearProducto(reader),
                        Cantidad = Convert.ToInt32(reader["Cantidad"]),
                        PrecioUnitario = Convert.ToDecimal(reader["PrecioItem"])
                    });
            }
        }

        // Agrega el producto o, si ya estaba en el carrito, actualiza su cantidad
        public bool GuardarItem(int idCarrito, int idProducto, int cantidad, decimal precioUnitario)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"IF EXISTS (SELECT 1 FROM ItemCarrito WHERE IdCarrito = @IdCarrito AND IdProducto = @IdProducto)
                        UPDATE ItemCarrito SET Cantidad = @Cantidad
                        WHERE IdCarrito = @IdCarrito AND IdProducto = @IdProducto
                    ELSE
                        INSERT INTO ItemCarrito (IdCarrito, IdProducto, Cantidad, PrecioUnitario)
                        VALUES (@IdCarrito, @IdProducto, @Cantidad, @Precio)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdCarrito", idCarrito);
                cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Precio", precioUnitario);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool EliminarItem(int idCarrito, int idProducto)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM ItemCarrito WHERE IdCarrito = @IdCarrito AND IdProducto = @IdProducto";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdCarrito", idCarrito);
                cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Solo se asocia si el carrito sigue Abierto
        public bool Asociar(int idCarrito, int dni)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Carrito SET DNI = @DNI, Estado = @EstadoNuevo
                    WHERE Id = @Id AND Estado = @EstadoActual";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DNI", dni);
                cmd.Parameters.AddWithValue("@EstadoNuevo", Carrito.EstadoAsociado);
                cmd.Parameters.AddWithValue("@Id", idCarrito);
                cmd.Parameters.AddWithValue("@EstadoActual", Carrito.EstadoAbierto);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
