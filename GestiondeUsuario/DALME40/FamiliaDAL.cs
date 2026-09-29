using Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FamiliaDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        public List<Familia> ObtenerTodos()
        {
            var lista = new List<Familia>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Familia";
                SqlCommand cmd = new SqlCommand(query, con);
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

        public bool Insertar(Familia f)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Familia (Nombre, Descripcion) VALUES (@Nombre, @Descripcion)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", f.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", f.Descripcion ?? "");
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Modificar(Familia f)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Familia SET Nombre=@Nombre, Descripcion=@Descripcion WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", f.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", f.Descripcion ?? "");
                cmd.Parameters.AddWithValue("@Id", f.Id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Borramos primero las relaciones propias de esta familia
                string queryFamPat = "DELETE FROM Fam_Pat WHERE IdFamilia=@Id";
                SqlCommand cmdFamPat = new SqlCommand(queryFamPat, con);
                cmdFamPat.Parameters.AddWithValue("@Id", id);
                cmdFamPat.ExecuteNonQuery();

                string queryFamFam = "DELETE FROM Fam_Fam WHERE IdFamilia=@Id";
                SqlCommand cmdFamFam = new SqlCommand(queryFamFam, con);
                cmdFamFam.Parameters.AddWithValue("@Id", id);
                cmdFamFam.ExecuteNonQuery();

                // Recién ahora borramos la familia
                string query = "DELETE FROM Familia WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool EstaEnUso(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT COUNT(*) FROM Rol_Fam WHERE IdFamilia=@Id
                UNION ALL
                SELECT COUNT(*) FROM Fam_Fam WHERE IdFamiliaIntegrada=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    if (Convert.ToInt32(reader[0]) > 0) return true;
                return false;
            }
        }

        // Patentes de una familia
        public List<Patente> ObtenerPatentes(int idFamilia)
        {
            var lista = new List<Patente>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT p.* FROM Patente p
                                INNER JOIN Fam_Pat fp ON p.Id = fp.IdPatente
                                WHERE fp.IdFamilia = @IdFamilia";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdFamilia", idFamilia);
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

        // Familias integradas dentro de una familia
        public List<Familia> ObtenerFamiliasIntegradas(int idFamilia)
        {
            var lista = new List<Familia>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT f.* FROM Familia f
                                INNER JOIN Fam_Fam ff ON f.Id = ff.IdFamiliaIntegrada
                                WHERE ff.IdFamilia = @IdFamilia";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdFamilia", idFamilia);
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

        public bool AgregarPatente(int idFamilia, int idPatente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Fam_Pat (IdFamilia, IdPatente) VALUES (@IdFamilia, @IdPatente)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdFamilia", idFamilia);
                cmd.Parameters.AddWithValue("@IdPatente", idPatente);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool AgregarFamilia(int idFamilia, int idFamiliaIntegrada)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Fam_Fam (IdFamilia, IdFamiliaIntegrada) VALUES (@IdFamilia, @IdFamiliaIntegrada)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdFamilia", idFamilia);
                cmd.Parameters.AddWithValue("@IdFamiliaIntegrada", idFamiliaIntegrada);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool EliminarPatente(int idFamilia, int idPatente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Fam_Pat WHERE IdFamilia=@IdFamilia AND IdPatente=@IdPatente";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdFamilia", idFamilia);
                cmd.Parameters.AddWithValue("@IdPatente", idPatente);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool EliminarFamiliaIntegrada(int idFamilia, int idFamiliaIntegrada)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Fam_Fam WHERE IdFamilia=@IdFamilia AND IdFamiliaIntegrada=@IdFamiliaIntegrada";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdFamilia", idFamilia);
                cmd.Parameters.AddWithValue("@IdFamiliaIntegrada", idFamiliaIntegrada);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int ObtenerUltimoId()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP 1 Id FROM Familia ORDER BY Id DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}

