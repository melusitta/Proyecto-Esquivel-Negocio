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
    // PN2: CUN-007 Pre-registrar proveedor (Encargado de compras) y CUN-010 Completar registro de proveedor (Supervisor)
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

        public static readonly string[] FormasPago = { "Transferencia", "Cheque", "Efectivo", "Cuenta corriente" };

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

        // Paso 2: el encargado de compras carga los datos básicos; queda PreRegistrado
        public int PreRegistrar(Proveedor p)
        {
            ValidarDatosBasicos(p);
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

        // Paso 6: el supervisor completa los datos comerciales y el proveedor queda Registrado
        public bool CompletarRegistro(Proveedor p)
        {
            ObtenerPorId(p.Id);
            ValidarDatosBasicos(p);
            ProveedorDAL dal = new ProveedorDAL();
            Proveedor otro = dal.ObtenerPorCUIT(p.CUIT);
            if (otro != null && otro.Id != p.Id)
                throw new ErrorNegocio("PROVEEDOR_DUPLICADO", p.CUIT);

            p.Rubro = p.Rubro?.Trim();
            p.CondicionesEntrega = p.CondicionesEntrega?.Trim();
            if (string.IsNullOrEmpty(p.Rubro) || string.IsNullOrEmpty(p.CondicionesEntrega) || p.PlazoEntregaDias == null)
                throw new ErrorNegocio("PROVEEDOR_DATOS_COMERCIALES_INCOMPLETOS");
            if (p.PlazoEntregaDias < 0)
                throw new ErrorNegocio("PLAZO_INVALIDO");
            if (!FormasPago.Contains(p.FormaPago))
                throw new ErrorNegocio("FORMA_PAGO_INVALIDA");

            bool ok;
            try
            {
                ok = dal.CompletarRegistro(p);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new ErrorNegocio("PROVEEDOR_DUPLICADO", p.CUIT);
            }
            if (ok)
                Registrar("Completar registro de proveedor", 3);
            return ok;
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

        // CUIT argentino: 11 dígitos (con o sin guiones) y dígito verificador correcto.
        // Devuelve el CUIT con formato XX-XXXXXXXX-X, o null si no es válido.
        public static string NormalizarCUIT(string cuit)
        {
            string digitos = Regex.Replace(cuit ?? "", @"[\s\-]", "");
            if (!Regex.IsMatch(digitos, @"^\d{11}$"))
                return null;
            int[] pesos = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            int suma = 0;
            for (int i = 0; i < 10; i++)
                suma += (digitos[i] - '0') * pesos[i];
            int verificador = 11 - suma % 11;
            if (verificador == 11) verificador = 0;
            if (verificador == 10 || verificador != digitos[10] - '0')
                return null;
            return digitos.Substring(0, 2) + "-" + digitos.Substring(2, 8) + "-" + digitos.Substring(10);
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
