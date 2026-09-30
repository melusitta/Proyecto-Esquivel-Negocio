using BE;
using DAL;
using Servicios;
using System;
using System.Text.RegularExpressions;

namespace BLL
{
    // CUN-005 Cobrar venta: pago con tarjeta de débito o crédito
    public class PagoBLL
    {
        private static PagoBLL _instancia;
        private PagoBLL() { }
        public static PagoBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new PagoBLL();
                return _instancia;
            }
        }

        public static bool EsPagoConTarjeta(string formaPago)
        {
            return formaPago == "Debito" || formaPago == "Credito";
        }

        // El efectivo se cobra en mano y no pasa por el banco. La transferencia la confirma el banco
        // (el cajero lo verifica) y la tarjeta la autoriza el banco con AutorizarPagoConTarjeta.
        public static bool RequiereConfirmacionBanco(string formaPago)
        {
            return formaPago != "Efectivo";
        }

        public void ValidarTarjeta(Tarjeta t)
        {
            if (!Regex.IsMatch(t.Numero ?? "", @"^\d{16}$"))
                throw new ErrorNegocio("TARJETA_NUMERO_INVALIDO");
            if (!Regex.IsMatch(t.CVV ?? "", @"^\d{3}$"))
                throw new ErrorNegocio("TARJETA_CVV_INVALIDO");

            // MM/AA leído a mano: con año de 2 dígitos, .NET Framework toma "30" como 1930
            Match venc = Regex.Match(t.Vencimiento ?? "", @"^(0[1-9]|1[0-2])/(\d{2})$");
            if (!venc.Success)
                throw new ErrorNegocio("TARJETA_VENCIMIENTO_INVALIDO");
            int mes = int.Parse(venc.Groups[1].Value);
            int anio = 2000 + int.Parse(venc.Groups[2].Value);
            // La tarjeta sirve hasta el último día de su mes de vencimiento
            if (new DateTime(anio, mes, 1).AddMonths(1) <= DateTime.Now)
                throw new ErrorNegocio("TARJETA_VENCIDA");

            if (string.IsNullOrWhiteSpace(t.Titular))
                throw new ErrorNegocio("TARJETA_TITULAR_VACIO");
        }

        // Pasos 11 a 13 con tarjeta: se valida el cobro y la tarjeta, y el banco confirma la acreditación.
        // Primero se revisan la factura y el monto, para no pedirle al banco un pago que después no se registra.
        // Devuelve true si el banco aprobó; si lo rechaza, ErrorNegocio PAGO_RECHAZADO.
        public bool AutorizarPagoConTarjeta(int idFactura, decimal monto, string formaPago, Tarjeta tarjeta)
        {
            FacturaBLL.Instancia.ValidarCobro(idFactura, monto, formaPago);
            ValidarTarjeta(tarjeta);

            BancoDAL banco = new BancoDAL();
            if (!banco.AutorizarPago(tarjeta, monto))
                throw new ErrorNegocio("PAGO_RECHAZADO");

            // Solo los últimos 4 dígitos: el número completo no se registra en ningún lado
            GestorEventosBLL.Instancia.Notificar(
                SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Desconocido",
                "Pago con tarjeta aprobado (terminada en " + tarjeta.UltimosCuatro + ")", "Ventas", 2);
            return true;
        }
    }
}
