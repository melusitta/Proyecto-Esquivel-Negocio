using Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class DigitoVerificadorDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;

        // Guarda o actualiza el DVH y DVV de una tabla
        public void GuardarDV(string tabla, string dvh, string dvv)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"IF EXISTS (SELECT 1 FROM DV WHERE Tabla = @Tabla)
                    UPDATE DV SET DVH = @DVH, DVV = @DVV WHERE Tabla = @Tabla
                ELSE
                    INSERT INTO DV (Tabla, DVH, DVV) VALUES (@Tabla, @DVH, @DVV)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Tabla", tabla);
                cmd.Parameters.AddWithValue("@DVH", dvh);
                cmd.Parameters.AddWithValue("@DVV", dvv);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Obtiene el DV guardado en BD para una tabla
        public DigitoVerificador ObtenerDV(string tabla)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Tabla, DVH, DVV FROM DV WHERE Tabla = @Tabla";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Tabla", tabla);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    return new DigitoVerificador
                    {
                        Tabla = reader["Tabla"].ToString(),
                        DVH = reader["DVH"].ToString(),
                        DVV = reader["DVV"].ToString()
                    };
                return null;
            }
        }

        // Lee todos los registros de Usuarios para calcular DV
        public List<List<string>> ObtenerDatosUsuarios()
        {
            return ObtenerDatosTabla(
                "SELECT Id, Nombre, Apellido, NombreUsuario, Email, DNI, Rol, Activo, Bloqueado FROM Usuarios");
        }

        public List<List<string>> ObtenerDatosRol()
        {
            return ObtenerDatosTabla("SELECT Id, Nombre FROM Rol");
        }

        public List<List<string>> ObtenerDatosPatente()
        {
            return ObtenerDatosTabla("SELECT Id, Nombre FROM Patente");
        }

        public List<List<string>> ObtenerDatosFamilia()
        {
            return ObtenerDatosTabla("SELECT Id, Nombre FROM Familia");
        }

        // ---- Tablas de negocio (PN1) ----
        public List<List<string>> ObtenerDatosProducto()
        {
            return ObtenerDatosTabla(
                "SELECT Id, CodigoProducto, Nombre, Marca, Color, Modelo, PrecioUnitario, Existencia, Activo FROM Producto");
        }

        public List<List<string>> ObtenerDatosCliente()
        {
            return ObtenerDatosTabla(
                "SELECT Id, DNI, Nombre, Apellido, Telefono, Email, Direccion, Localidad, CodigoPostal FROM Cliente");
        }

        public List<List<string>> ObtenerDatosCarrito()
        {
            return ObtenerDatosTabla("SELECT Id, DNI, Estado, Vendedor FROM Carrito");
        }

        public List<List<string>> ObtenerDatosItemCarrito()
        {
            return ObtenerDatosTabla("SELECT IdCarrito, IdProducto, Cantidad, PrecioUnitario FROM ItemCarrito");
        }

        public List<List<string>> ObtenerDatosFactura()
        {
            return ObtenerDatosTabla(
                "SELECT Id, NroFactura, IdCarrito, IdCliente, DNI, Total, Estado, FormaPago, MontoPagado, Cajero FROM Factura");
        }

        public List<List<string>> ObtenerDatosItemFactura()
        {
            return ObtenerDatosTabla("SELECT IdFactura, IdProducto, Cantidad, PrecioUnitario FROM ItemFactura");
        }

        private List<List<string>> ObtenerDatosTabla(string query)
        {
            var filas = new List<List<string>>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var fila = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        fila.Add(reader[i]?.ToString() ?? "");
                    filas.Add(fila);
                }
            }
            return filas;
        }
    }
}