using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    // PN2: proveedores (CUN-007 Realizar pre-registro, CUN-009 Completar registro proveedor)
    public class ProveedorDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        internal static Proveedor MapearProveedor(SqlDataReader reader)
        {
            return new Proveedor
            {
                Id = Convert.ToInt32(reader["Id"]),
                CUIT = reader["CUIT"].ToString(),
                RazonSocial = reader["RazonSocial"].ToString(),
                Contacto = reader["Contacto"].ToString(),
                Telefono = reader["Telefono"].ToString(),
                Email = reader["Email"].ToString(),
                CondicionesComerciales = reader["CondicionesComerciales"] == DBNull.Value ? null : reader["CondicionesComerciales"].ToString(),
                Rubro = reader["Rubro"] == DBNull.Value ? null : reader["Rubro"].ToString(),
                CondicionesEntrega = reader["CondicionesEntrega"] == DBNull.Value ? null : reader["CondicionesEntrega"].ToString(),
                PlazoEntregaDias = reader["PlazoEntregaDias"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["PlazoEntregaDias"]),
                FormaPago = reader["FormaPago"] == DBNull.Value ? null : reader["FormaPago"].ToString(),
                CBU = reader["CBU"] == DBNull.Value ? null : reader["CBU"].ToString(),
                Banco = reader["Banco"] == DBNull.Value ? null : reader["Banco"].ToString(),
                Estado = reader["Estado"].ToString(),
                FechaAlta = Convert.ToDateTime(reader["FechaAlta"]),
                Activo = Convert.ToBoolean(reader["Activo"])
            };
        }

        public List<Proveedor> ObtenerTodos()
        {
            var lista = new List<Proveedor>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Proveedor WHERE Activo = 1 ORDER BY RazonSocial", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lista.Add(MapearProveedor(reader));
            }
            return lista;
        }

        public Proveedor ObtenerPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Proveedor WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                return reader.Read() ? MapearProveedor(reader) : null;
            }
        }

        public Proveedor ObtenerPorCUIT(string cuit)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Proveedor WHERE CUIT = @CUIT", con);
                cmd.Parameters.AddWithValue("@CUIT", cuit);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                return reader.Read() ? MapearProveedor(reader) : null;
            }
        }

        // Pre-registro: queda PreRegistrado hasta que el supervisor cargue los datos bancarios
        public int PreRegistrar(Proveedor p)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Proveedor
                    (CUIT, RazonSocial, Contacto, Telefono, Email, CondicionesComerciales, Rubro, CondicionesEntrega,
                     PlazoEntregaDias, FormaPago, Estado, FechaAlta, Activo)
                    VALUES (@CUIT, @RazonSocial, @Contacto, @Telefono, @Email, @Condiciones, @Rubro, @Entrega,
                     @Plazo, @FormaPago, @Estado, GETDATE(), 1);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CUIT", p.CUIT);
                cmd.Parameters.AddWithValue("@RazonSocial", p.RazonSocial);
                cmd.Parameters.AddWithValue("@Contacto", p.Contacto);
                cmd.Parameters.AddWithValue("@Telefono", p.Telefono);
                cmd.Parameters.AddWithValue("@Email", p.Email);
                cmd.Parameters.AddWithValue("@Condiciones", (object)p.CondicionesComerciales ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Rubro", (object)p.Rubro ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Entrega", (object)p.CondicionesEntrega ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Plazo", (object)p.PlazoEntregaDias ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FormaPago", (object)p.FormaPago ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", Proveedor.EstadoPreRegistrado);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Carga CBU y banco y lo deja Registrado. Devuelve false si ya tenía datos bancarios.
        public bool CompletarRegistro(int idProveedor, string cbu, string banco)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Proveedor SET CBU = @CBU, Banco = @Banco, Estado = @Estado
                    WHERE Id = @Id AND (CBU IS NULL OR Banco IS NULL)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CBU", cbu);
                cmd.Parameters.AddWithValue("@Banco", banco);
                cmd.Parameters.AddWithValue("@Estado", Proveedor.EstadoRegistrado);
                cmd.Parameters.AddWithValue("@Id", idProveedor);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
