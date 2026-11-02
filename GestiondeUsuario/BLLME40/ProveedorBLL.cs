using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace BLL
{
    // PN2: CUN-007 Realizar pre-registro (Encargado de compras) y CUN-009 Completar registro proveedor (Supervisor).
    // Desde el menú Maestro, Gestión de proveedores cambia sus condiciones comerciales y de entrega.
    public class ProveedorBLL
    {
        private static ProveedorBLL _instancia;
        private ProveedorBLL() { }
        public static ProveedorBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new ProveedorBLL();
                return _instancia;
            }
        }

        // Formas en que se le puede pagar a un proveedor; cada uno acepta algunas (se guardan juntas en FormaPago)
        public static readonly string[] FormasPago = { "Transferencia", "Cheque", "Efectivo", "Tarjeta" };

        // "Transferencia, Efectivo" -> [Transferencia, Efectivo]. Vacío si no tiene ninguna cargada.
        public static List<string> FormasAceptadas(string guardadas)
        {
            return (guardadas ?? "").Split(',').Select(f => f.Trim()).Where(f => f.Length > 0).ToList();
        }

        public static string UnirFormas(IEnumerable<string> formas)
        {
            return string.Join(", ", FormasPago.Where(f => formas != null && formas.Contains(f)));
        }

        public List<Proveedor> ObtenerTodos()
        {
            return new ProveedorDAL().ObtenerTodos();
        }

        public Proveedor ObtenerPorId(int id)
        {
            Proveedor p = new ProveedorDAL().ObtenerPorId(id);
            if (p == null || !p.Activo)
                throw new ErrorNegocio("PROVEEDOR_NO_ENCONTRADO");
            return p;
        }

        // Paso 2: el encargado de compras carga los datos básicos (y, si los tiene, los comerciales); queda PreRegistrado
        public int PreRegistrar(Proveedor p)
        {
            ValidarDatosBasicos(p);
            ValidarDatosComerciales(p);
            ProveedorDAL dal = new ProveedorDAL();
            if (dal.ObtenerPorCUIT(p.CUIT) != null)
                throw new ErrorNegocio("PROVEEDOR_DUPLICADO", p.CUIT);
            try
            {
                p.Id = dal.PreRegistrar(p);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new ErrorNegocio("PROVEEDOR_DUPLICADO", p.CUIT);
            }
            Registrar("Pre-registrar proveedor", 2);
            return p.Id;
        }

        // Paso 6: el supervisor carga CBU y banco del proveedor, que queda Registrado
        public void CompletarRegistro(int idProveedor, string cbu, string banco)
        {
            Proveedor p = ObtenerPorId(idProveedor);
            if (p.TieneDatosBancarios)
                throw new ErrorNegocio("PROVEEDOR_YA_TIENE_DATOS_BANCARIOS", p.RazonSocial);
            banco = banco?.Trim();
            if (string.IsNullOrEmpty(cbu?.Trim()) || string.IsNullOrEmpty(banco))
                throw new ErrorNegocio("DATOS_BANCARIOS_INCOMPLETOS");
            string cbuValido = NormalizarCBU(cbu);
            if (cbuValido == null)
                throw new ErrorNegocio("CBU_INVALIDO");

            if (!new ProveedorDAL().CompletarRegistro(p.Id, cbuValido, banco))
                throw new ErrorNegocio("PROVEEDOR_YA_TIENE_DATOS_BANCARIOS", p.RazonSocial);
            Registrar("Completar registro proveedor", 3);
        }

        // Gestión de proveedores (menú Maestro): solo se cambian las condiciones comerciales y de entrega,
        // el plazo y las formas de pago que acepta; el resto de los datos del proveedor queda como está
        public void Modificar(Proveedor cambios)
        {
            Proveedor p = ObtenerPorId(cambios.Id);
            p.CondicionesComerciales = string.IsNullOrWhiteSpace(cambios.CondicionesComerciales) ? null : cambios.CondicionesComerciales.Trim();
            p.CondicionesEntrega = cambios.CondicionesEntrega;
            p.PlazoEntregaDias = cambios.PlazoEntregaDias;
            p.FormaPago = cambios.FormaPago;
            ValidarDatosComerciales(p);

            if (!new ProveedorDAL().ModificarCondiciones(p))
                throw new ErrorNegocio("PROVEEDOR_NO_ENCONTRADO");
            Registrar("Modificar proveedor", 2);
        }

        // Rubro, entrega, plazo y formas de pago: opcionales; si vienen, tienen que ser válidos
        private void ValidarDatosComerciales(Proveedor p)
        {
            p.Rubro = string.IsNullOrWhiteSpace(p.Rubro) ? null : p.Rubro.Trim();
            p.CondicionesEntrega = string.IsNullOrWhiteSpace(p.CondicionesEntrega) ? null : p.CondicionesEntrega.Trim();
            if (p.PlazoEntregaDias < 0)
                throw new ErrorNegocio("PLAZO_INVALIDO");
            List<string> formas = FormasAceptadas(p.FormaPago);
            if (formas.Any(f => !FormasPago.Contains(f)))
                throw new ErrorNegocio("FORMA_PAGO_INVALIDA");
            p.FormaPago = formas.Count == 0 ? null : UnirFormas(formas);
        }

        private void ValidarDatosBasicos(Proveedor p)
        {
            p.RazonSocial = p.RazonSocial?.Trim();
            p.Contacto = p.Contacto?.Trim();
            p.Telefono = p.Telefono?.Trim();
            p.Email = p.Email?.Trim();
            p.CondicionesComerciales = string.IsNullOrWhiteSpace(p.CondicionesComerciales) ? null : p.CondicionesComerciales.Trim();
            if (string.IsNullOrEmpty(p.RazonSocial) || string.IsNullOrEmpty(p.Contacto) ||
                string.IsNullOrEmpty(p.Telefono) || string.IsNullOrEmpty(p.Email))
                throw new ErrorNegocio("PROVEEDOR_DATOS_INCOMPLETOS");

            string cuit = NormalizarCUIT(p.CUIT);
            if (cuit == null)
                throw new ErrorNegocio("CUIT_INVALIDO");
            p.CUIT = cuit;

            if (!Regex.IsMatch(p.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ErrorNegocio("EMAIL_INVALIDO");
            if (!Regex.IsMatch(p.Telefono, @"^\+?[0-9\s\-]{6,20}$"))
                throw new ErrorNegocio("TELEFONO_INVALIDO");
        }

        // CUIT: 11 dígitos (con o sin guiones). No se controla el dígito verificador.
        // Devuelve el CUIT con formato XX-XXXXXXXX-X, o null si no es válido.
        public static string NormalizarCUIT(string cuit)
        {
            string digitos = Regex.Replace(cuit ?? "", @"[\s\-]", "");
            if (!Regex.IsMatch(digitos, @"^\d{11}$"))
                return null;
            return digitos.Substring(0, 2) + "-" + digitos.Substring(2, 8) + "-" + digitos.Substring(10);
        }

        // CBU: 22 dígitos (con o sin guiones). No se controlan los dígitos verificadores.
        // Devuelve los 22 dígitos, o null si no es válido.
        public static string NormalizarCBU(string cbu)
        {
            string digitos = Regex.Replace(cbu ?? "", @"[\s\-]", "");
            return Regex.IsMatch(digitos, @"^\d{22}$") ? digitos : null;
        }

        private void Registrar(string accion, int criticidad)
        {
            GestorEventosBLL.Instancia.Notificar(
                SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Desconocido",
                accion, "Compras", criticidad);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
        }
    }
}
