using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BLL
{
    // PN2: CUN-010 Pagar orden de compra (Supervisor, incluido en CUN-008 Generar orden de compra). Paso 8: el proveedor entrega la factura;
    // paso 9: el supervisor paga la orden y registra la factura con el comprobante (estado Pagado).
    public class PagoProveedorBLL
    {
        private static PagoProveedorBLL _instancia;
        private PagoProveedorBLL() { }
        public static PagoProveedorBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new PagoProveedorBLL();
                return _instancia;
            }
        }

        // Formas en que se le puede pagar la orden: las que aceptaba el proveedor al cotizar (si no tenía
        // ninguna cargada, cualquiera)
        public List<string> FormasPagoDeOrden(OrdenCompra orden)
        {
            Cotizacion c = OrdenCompraBLL.Instancia.ObtenerCotizacion(orden.IdCotizacion);
            List<string> formas = ProveedorBLL.FormasAceptadas(c?.FormaPago).Where(f => ProveedorBLL.FormasPago.Contains(f)).ToList();
            return formas.Count > 0 ? formas : ProveedorBLL.FormasPago.ToList();
        }

        // Paso 9: formaPago es la que elige el supervisor entre las que acepta el proveedor
        public FacturaProveedor PagarOrden(FacturaProveedor f, string formaPago)
        {
            OrdenCompra orden = OrdenCompraBLL.Instancia.ObtenerPorId(f.IdOrdenCompra);
            if (orden.Estado != OrdenCompra.EstadoEnviada)
                throw new ErrorNegocio("ORDEN_ESTADO_INVALIDO", orden.NroOrden, orden.Estado);

            // El número de factura lo asigna el sistema: el siguiente al último de ese proveedor
            f.NroFactura = SiguienteNroFactura(orden.Proveedor.Id);
            f.NroComprobante = f.NroComprobante?.Trim();
            if (string.IsNullOrEmpty(f.NroComprobante))
                throw new ErrorNegocio("FACTURA_PROVEEDOR_DATOS_INCOMPLETOS");
            if (string.IsNullOrEmpty(formaPago))
                throw new ErrorNegocio("FORMA_PAGO_INVALIDA");
            if (!FormasPagoDeOrden(orden).Contains(formaPago))
                throw new ErrorNegocio("FORMA_PAGO_NO_ACEPTADA", orden.Proveedor.RazonSocial, formaPago);
            if (f.Fecha.Date > DateTime.Today || f.FechaOperacion.Date > DateTime.Today)
                throw new ErrorNegocio("FECHA_FUTURA");
            if (f.FechaOperacion.Date < f.Fecha.Date)
                throw new ErrorNegocio("PAGO_ANTERIOR_A_FACTURA");
            if (f.Importe <= 0 || f.Impuestos < 0)
                throw new ErrorNegocio("IMPORTE_INVALIDO");
            // El importe de la factura (sin impuestos) tiene que coincidir con lo acordado en la orden
            if (f.Importe != orden.Total)
                throw new ErrorNegocio("FACTURA_IMPORTE_NO_COINCIDE", orden.Total.ToString("N2"));
            if (f.MontoAbonado != f.Total)
                throw new ErrorNegocio("MONTO_ABONADO_INCORRECTO", f.Total.ToString("N2"));

            FacturaProveedorDAL dal = new FacturaProveedorDAL();
            f.IdProveedor = orden.Proveedor.Id;
            if (dal.ExisteNroFactura(f.IdProveedor, f.NroFactura))
                throw new ErrorNegocio("FACTURA_PROVEEDOR_DUPLICADA", f.NroFactura);

            f.Estado = FacturaProveedor.EstadoPagado;
            f.Supervisor = SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Desconocido";
            if (!dal.RegistrarPago(f, orden.IdCotizacion, formaPago))
                throw new ErrorNegocio("ORDEN_ESTADO_INVALIDO", orden.NroOrden, orden.Estado);

            GestorEventosBLL.Instancia.Notificar(f.Supervisor, "Pagar orden de compra", "Compras", 3);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return dal.ObtenerPorOrden(f.IdOrdenCompra);
        }

        // Paso 8: las facturas de cada proveedor se numeran en forma correlativa; el sistema asigna la siguiente
        // a la última que se le registró (A-0001-00001234 -> A-0001-00001235). La primera es 0001-00000001.
        public string SiguienteNroFactura(int idProveedor)
        {
            string ultimo = new FacturaProveedorDAL().UltimoNroFactura(idProveedor);
            Match numero = Regex.Match(ultimo ?? "", @"(\d+)$");
            if (!numero.Success)
                return string.IsNullOrEmpty(ultimo) ? "0001-00000001" : ultimo + "-1";
            string siguiente = (long.Parse(numero.Value) + 1).ToString().PadLeft(numero.Length, '0');
            return ultimo.Substring(0, numero.Index) + siguiente;
        }

        public FacturaProveedor ObtenerFacturaDeOrden(int idOrdenCompra)
        {
            return new FacturaProveedorDAL().ObtenerPorOrden(idOrdenCompra);
        }
    }
}
