using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsuarioDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        private Usuario MapearUsuario(SqlDataReader reader)
        {
            return new Usuario()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Nombre = reader["Nombre"].ToString(),
                Apellido = reader["Apellido"].ToString(),
                NombreUsuario = reader["NombreUsuario"].ToString(),
                Email = reader["Email"].ToString(),
                Contraseña = reader["Contraseña"].ToString(),
                DNI = Convert.ToInt32(reader["DNI"]),
                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                Activo = Convert.ToBoolean(reader["Activo"]),
                IntentosFallidos = Convert.ToInt32(reader["IntentosFallidos"]),
                Bloqueado = Convert.ToBoolean(reader["Bloqueado"]),
                Rol = reader["Rol"].ToString(),
                IdRol = reader["IdRol"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdRol"]),
                PrimerIngreso = Convert.ToBoolean(reader["PrimerIngreso"]),
                Idioma = reader["Idioma"].ToString()
            };
        }
        public Usuario ObtenerPorNombreUsuario(string nombreUsuario)
        {
            Usuario usuario = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    usuario = MapearUsuario(reader);
            }
            return usuario;
        }

        public List<Usuario> ObtenerTodos()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Usuarios";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lista.Add(MapearUsuario(reader));
            }
            return lista;
        }

        public List<Usuario> ObtenerActivos()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE Activo = 1";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lista.Add(MapearUsuario(reader));
            }
            return lista;
        }

        public bool Modificar(Usuario u)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Usuarios SET 
                Nombre=@Nombre, Apellido=@Apellido, Email=@Email, DNI=@DNI, Rol=@Rol
                WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", u.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", u.Apellido);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@DNI", u.DNI);
                cmd.Parameters.AddWithValue("@Rol", u.Rol);
                cmd.Parameters.AddWithValue("@Id", u.Id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Deshabilitar(int id, string contraseñaReseteada)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Usuarios SET
                 Activo = 0,
                 Contraseña = @Contraseña,
                 PrimerIngreso = 1
                 WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Contraseña", contraseñaReseteada);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool Habilitar(int id, string contraseñaReseteada)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Usuarios SET 
                Activo=1,
                Contraseña=@Contraseña,
                PrimerIngreso=1
                WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Contraseña", contraseñaReseteada);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool Desbloquear(int id, string contraseñaReseteada)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Usuarios SET 
                Bloqueado=0, 
                IntentosFallidos=0,
                Contraseña=@Contraseña,
                PrimerIngreso=1
                WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Contraseña", contraseñaReseteada);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public Usuario ObtenerPorId(int id)
        {
            Usuario usuario = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    usuario = MapearUsuario(reader);
            }
            return usuario;
        }

        public void ActualizarIntentos(Usuario u)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Usuarios SET IntentosFallidos=@Intentos, Bloqueado=@Bloqueado WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Intentos", u.IntentosFallidos);
                cmd.Parameters.AddWithValue("@Bloqueado", u.Bloqueado);
                cmd.Parameters.AddWithValue("@Id", u.Id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool ActualizarContraseña(Usuario u)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Usuarios SET Contraseña=@Pass, PrimerIngreso=@PrimerIngreso WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Pass", u.Contraseña);
                cmd.Parameters.AddWithValue("@PrimerIngreso", u.PrimerIngreso);
                cmd.Parameters.AddWithValue("@Id", u.Id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Insertar(Usuario u)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Usuarios 
            (Nombre, Apellido, Email, Contraseña, DNI, NombreUsuario, 
             FechaCreacion, Activo, Rol, IdRol, PrimerIngreso, IntentosFallidos, Bloqueado) 
            VALUES 
            (@Nombre, @Apellido, @Email, @Contraseña, @DNI, @NombreUsuario,
             @FechaCreacion, @Activo, @Rol, @IdRol, @PrimerIngreso, 0, 0)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", u.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", u.Apellido);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@Contraseña", u.Contraseña);
                cmd.Parameters.AddWithValue("@DNI", u.DNI);
                cmd.Parameters.AddWithValue("@NombreUsuario", u.NombreUsuario);
                cmd.Parameters.Add("@FechaCreacion", SqlDbType.DateTime).Value = DateTime.UtcNow;
                cmd.Parameters.AddWithValue("@Activo", true);
                cmd.Parameters.AddWithValue("@Rol", u.Rol);
                cmd.Parameters.AddWithValue("@IdRol", u.IdRol == 0 ? (object)DBNull.Value : u.IdRol);
                cmd.Parameters.AddWithValue("@PrimerIngreso", true);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public Usuario ObtenerPorDNI(int dni)
        {
            Usuario usuario = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE DNI = @DNI";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DNI", dni);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    usuario = MapearUsuario(reader);
            }
            return usuario;
        }

        public bool ActualizarIdioma(int id, string idioma)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Usuarios SET Idioma=@Idioma WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Idioma", idioma);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
