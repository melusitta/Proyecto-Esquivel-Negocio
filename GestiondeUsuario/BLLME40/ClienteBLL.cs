using BE;
using DAL;
using Servicios;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BLL
{
    // CUN-004 Registrar cliente (Cajero)
    public class ClienteBLL
    {
        private static ClienteBLL _instancia;
        private ClienteBLL() { }
        public static ClienteBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new ClienteBLL();
                return _instancia;
            }
        }

        // Devuelve el cliente o null si ese DNI todavía no está registrado
        public Cliente ObtenerPorDNI(int dni)
        {
            ValidarDNI(dni);
            ClienteDAL dal = new ClienteDAL();
            return dal.ObtenerPorDNI(dni);
        }

        public bool Registrar(Cliente c)
        {
            ValidarDNI(c.DNI);
            if (string.IsNullOrWhiteSpace(c.Nombre) || string.IsNullOrWhiteSpace(c.Apellido) ||
                string.IsNullOrWhiteSpace(c.Telefono) || string.IsNullOrWhiteSpace(c.Email) ||
                string.IsNullOrWhiteSpace(c.Direccion) || string.IsNullOrWhiteSpace(c.Localidad) ||
                string.IsNullOrWhiteSpace(c.CodigoPostal))
                throw new ErrorNegocio("CLIENTE_DATOS_INCOMPLETOS");

            c.Nombre = c.Nombre.Trim();
            c.Apellido = c.Apellido.Trim();
            c.Telefono = c.Telefono.Trim();
            c.Email = c.Email.Trim();
            c.Direccion = c.Direccion.Trim();
            c.Localidad = c.Localidad.Trim();
            c.CodigoPostal = c.CodigoPostal.Trim().ToUpper();

            if (!Regex.IsMatch(c.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ErrorNegocio("EMAIL_INVALIDO");
            if (!Regex.IsMatch(c.Telefono, @"^\+?[0-9\s\-]{6,20}$"))
                throw new ErrorNegocio("TELEFONO_INVALIDO");

            ClienteDAL dal = new ClienteDAL();
            if (dal.ObtenerPorDNI(c.DNI) != null)
                throw new ErrorNegocio("CLIENTE_DUPLICADO", c.DNI);

            try
            {
                bool ok = dal.Insertar(c);
                if (ok)
                {
                    GestorEventosBLL.Instancia.Notificar(
                        SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Desconocido",
                        "Registrar cliente", "Ventas", 2);
                    DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
                }
                return ok;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    throw new ErrorNegocio("CLIENTE_DUPLICADO", c.DNI);
                throw;
            }
        }

        public List<Cliente> ObtenerTodos()
        {
            ClienteDAL dal = new ClienteDAL();
            return dal.ObtenerTodos();
        }

        // ---------- A03 Serialización XML (CU04 Registrar cliente) ----------

        private const string RaizXml = "Clientes";

        // Guarda en el archivo los clientes que el usuario seleccionó en la grilla
        public void Serializar(List<Cliente> seleccionados, string ruta)
        {
            if (seleccionados == null || seleccionados.Count == 0)
                throw new ErrorNegocio("SIN_CLIENTES_SELECCIONADOS");
            if (string.IsNullOrWhiteSpace(ruta))
                throw new ErrorNegocio("RUTA_ARCHIVO_VACIA");
            Serializador.SerializarXml(seleccionados, ruta, RaizXml);
            Notificar("Serializar clientes", 1);
        }

        // Solo lee el archivo y devuelve los clientes para mostrarlos: NO se guarda nada en la base
        public List<Cliente> Deserializar(string ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta))
                throw new ErrorNegocio("RUTA_ARCHIVO_VACIA");
            List<Cliente> clientes = Serializador.DeserializarXml<List<Cliente>>(ruta, RaizXml);
            Notificar("Deserializar clientes", 1);
            return clientes ?? new List<Cliente>();
        }

        private void Notificar(string accion, int criticidad)
        {
            GestorEventosBLL.Instancia.Notificar(
                SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Desconocido",
                accion, "Ventas", criticidad);
        }

        // DNI: numero positivo de hasta 8 dígitos
        public static void ValidarDNI(int dni)
        {
            if (dni <= 0 || dni > 99999999)
                throw new ErrorNegocio("DNI_INVALIDO");
        }
    }
}
