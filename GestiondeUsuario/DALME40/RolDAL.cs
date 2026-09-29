using Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RolDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        public List<Rol> ObtenerTodos()
        {
            var lista = new List<Rol>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Rol";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Rol
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"] == DBNull.Value ? "" : reader["Descripcion"].ToString()
                    });
            }
            return lista;
        }

        public bool Insertar(Rol r)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Rol (Nombre, Descripcion) VALUES (@Nombre, @Descripcion)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", r.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", r.Descripcion ?? "");
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Modificar(Rol r)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Rol SET Nombre=@Nombre, Descripcion=@Descripcion WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", r.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", r.Descripcion ?? "");
                cmd.Parameters.AddWithValue("@Id", r.Id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Rol WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool EstaEnUso(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Usuarios WHERE Rol = (SELECT Nombre FROM Rol WHERE Id=@Id)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool AgregarPatente(int idRol, int idPatente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Rol_Pat (IdRol, IdPatente) VALUES (@IdRol, @IdPatente)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdRol", idRol);
                cmd.Parameters.AddWithValue("@IdPatente", idPatente);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool AgregarFamilia(int idRol, int idFamilia)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Rol_Fam (IdRol, IdFamilia) VALUES (@IdRol, @IdFamilia)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdRol", idRol);
                cmd.Parameters.AddWithValue("@IdFamilia", idFamilia);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool EliminarPatente(int idRol, int idPatente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Rol_Pat WHERE IdRol=@IdRol AND IdPatente=@IdPatente";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdRol", idRol);
                cmd.Parameters.AddWithValue("@IdPatente", idPatente);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool EliminarFamilia(int idRol, int idFamilia)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Rol_Fam WHERE IdRol=@IdRol AND IdFamilia=@IdFamilia";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdRol", idRol);
                cmd.Parameters.AddWithValue("@IdFamilia", idFamilia);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Patente> ObtenerPatentes(int idRol)
        {
            var lista = new List<Patente>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT p.* FROM Patente p
                                INNER JOIN Rol_Pat rp ON p.Id = rp.IdPatente
                                WHERE rp.IdRol = @IdRol";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdRol", idRol);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Patente
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"].ToString()
                    });
            }
            return lista;
        }

        public List<Familia> ObtenerFamilias(int idRol)
        {
            var lista = new List<Familia>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT f.* FROM Familia f
                                INNER JOIN Rol_Fam rf ON f.Id = rf.IdFamilia
                                WHERE rf.IdRol = @IdRol";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdRol", idRol);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Familia
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"].ToString()
                    });
            }
            return lista;
        }
    }
}

