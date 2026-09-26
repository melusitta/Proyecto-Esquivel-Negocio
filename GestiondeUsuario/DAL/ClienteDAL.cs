using BE;
using System;
using System.Data.SqlClient;

namespace DAL
{
    public class ClienteDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        private Cliente MapearCliente(SqlDataReader reader)
        {
            return new Cliente()
            {
                Id = Convert.ToInt32(reader["Id"]),
                DNI = Convert.ToInt32(reader["DNI"]),
                Nombre = reader["Nombre"].ToString(),
                Apellido = reader["Apellido"].ToString(),
                Telefono = reader["Telefono"].ToString(),
                Email = reader["Email"].ToString(),
                Direccion = reader["Direccion"].ToString(),
                Localidad = reader["Localidad"].ToString(),
                CodigoPostal = reader["CodigoPostal"].ToString(),
                FechaAlta = Convert.ToDateTime(reader["FechaAlta"])
            };
        }

        public Cliente ObtenerPorDNI(int dni)
        {
            Cliente cliente = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Cliente WHERE DNI = @DNI";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DNI", dni);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    cliente = MapearCliente(reader);
            }
            return cliente;
        }

        public bool Insertar(Cliente c)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Cliente
                    (DNI, Nombre, Apellido, Telefono, Email, Direccion, Localidad, CodigoPostal, FechaAlta)
                    VALUES
                    (@DNI, @Nombre, @Apellido, @Telefono, @Email, @Direccion, @Localidad, @CodigoPostal, GETDATE())";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DNI", c.DNI);
                cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", c.Apellido);
                cmd.Parameters.AddWithValue("@Telefono", c.Telefono);
                cmd.Parameters.AddWithValue("@Email", c.Email);
                cmd.Parameters.AddWithValue("@Direccion", c.Direccion);
                cmd.Parameters.AddWithValue("@Localidad", c.Localidad);
                cmd.Parameters.AddWithValue("@CodigoPostal", c.CodigoPostal);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
