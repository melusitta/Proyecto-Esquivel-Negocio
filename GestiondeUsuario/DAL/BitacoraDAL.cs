using Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class BitacoraDAL : IObservador
    {
        private string connectionString = ConexionDAL.ConnectionString;

        // Implementación del Observer
        public void Actualizar(string usuario, string accion, string modulo, int criticidad)
        {
            Guardar(usuario, accion, modulo, criticidad);
        }

        public void Guardar(string usuario, string accion, string modulo, int criticidad)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Bitacora 
                    (Usuario, Accion, Fecha, Modulo, Criticidad) 
                    VALUES (@Usuario, @Accion, @Fecha, @Modulo, @Criticidad)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Accion", accion);
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);
                cmd.Parameters.AddWithValue("@Modulo", modulo);
                cmd.Parameters.AddWithValue("@Criticidad", criticidad);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Bitacora> ObtenerFiltrado(string login, DateTime? fechaIni, 
            DateTime? fechaFin, string modulo, string evento, int? criticidad)
        {
            var lista = new List<Bitacora>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT b.Id, b.Usuario, b.Accion, b.Fecha, 
                        b.Modulo, b.Criticidad,
                        u.Nombre, u.Apellido
                 FROM Bitacora b
                 LEFT JOIN Usuarios u ON b.Usuario = u.NombreUsuario
                 WHERE 1=1";

                if (fechaIni.HasValue)
                    query += " AND b.Fecha >= @FechaIni";
                if (fechaFin.HasValue)
                    query += " AND b.Fecha < DATEADD(day, 1, @FechaFin)";

                if (!string.IsNullOrEmpty(login))
                    query += " AND b.Usuario = @Login";
                if (!string.IsNullOrEmpty(modulo))
                    query += " AND b.Modulo = @Modulo";
                if (!string.IsNullOrEmpty(evento))
                    query += " AND b.Accion = @Evento";
                if (criticidad.HasValue)
                    query += " AND b.Criticidad = @Criticidad";

                query += " ORDER BY b.Fecha DESC";

                SqlCommand cmd = new SqlCommand(query, con);
                if (fechaIni.HasValue)
                    cmd.Parameters.AddWithValue("@FechaIni", fechaIni.Value);
                if (fechaFin.HasValue)
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin.Value);
                if (!string.IsNullOrEmpty(login))
                    cmd.Parameters.AddWithValue("@Login", login);
                if (!string.IsNullOrEmpty(modulo))
                    cmd.Parameters.AddWithValue("@Modulo", modulo);
                if (!string.IsNullOrEmpty(evento))
                    cmd.Parameters.AddWithValue("@Evento", evento);
                if (criticidad.HasValue)
                    cmd.Parameters.AddWithValue("@Criticidad", criticidad.Value);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Bitacora
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Usuario = reader["Usuario"].ToString(),
                        Accion = reader["Accion"].ToString(),
                        Fecha = Convert.ToDateTime(reader["Fecha"]),
                        Modulo = reader["Modulo"].ToString(),
                        Criticidad = Convert.ToInt32(reader["Criticidad"]),
                        // Nombre y Apellido los guardamos en campos extra
                        Nombre = reader["Nombre"] == DBNull.Value ? "" : reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"] == DBNull.Value ? "" : reader["Apellido"].ToString()
                    });
                }
            }
            return lista;
        }

        // Valores distintos de una columna (Modulo o Accion), opcionalmente solo de un usuario
        public List<string> ObtenerValoresDistintos(string columna, string login)
        {
            if (columna != "Modulo" && columna != "Accion")
                throw new ArgumentException("Columna no permitida: " + columna);

            var lista = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT " + columna + " FROM Bitacora"
                    + (login != null ? " WHERE Usuario = @Login" : "")
                    + " ORDER BY " + columna;
                SqlCommand cmd = new SqlCommand(query, con);
                if (login != null)
                    cmd.Parameters.AddWithValue("@Login", login);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lista.Add(reader[columna].ToString());
            }
            return lista;
        }

        public List<string> ObtenerLogins()
        {
            var lista = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT Usuario FROM Bitacora ORDER BY Usuario";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lista.Add(reader["Usuario"].ToString());
            }
            return lista;
        }
    }
}

