using BE;
using DAL;
using Servicios;
using System;
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

        public FacturaProveedor PagarOrden(FacturaProveedor f)
        {
            OrdenCompra orden = OrdenCompraBLL.Instancia.ObtenerPorId(f.IdOrdenCompra);
            if (orden.Estado != OrdenCompra.EstadoEnviada)
                throw new ErrorNegocio("ORDEN_ESTADO_INVALIDO", orden.NroOrden, orden.Estado);

            f.NroFactura = f.NroFactura?.Trim();
            f.NroComprobante = f.NroComprobante?.Trim();
            if (string.IsNullOrEmpty(f.NroFactura) || string.IsNullOrEmpty(f.NroComprobante))
                throw new ErrorNegocio("FACTURA_PROVEEDOR_DATOS_INCOMPLETOS");
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
            if (!dal.RegistrarPago(f))
                throw new ErrorNegocio("ORDEN_ESTADO_INVALIDO", orden.NroOrden, orden.Estado);

            GestorEventosBLL.Instancia.Notificar(f.Supervisor, "Pagar orden de compra", "Compras", 3);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return dal.ObtenerPorOrden(f.IdOrdenCompra);
        }

        // Paso 8: el proveedor numera sus facturas en forma correlativa, así que se propone la siguiente a la
        // última que se le registró (A-0001-00001234 -> A-0001-00001235). El supervisor la confirma o la corrige.
        // Null si el proveedor todavía no tiene facturas o el número no termina en dígitos.
        public string SugerirNroFactura(int idProveedor)
        {
            string ultimo = new FacturaProveedorDAL().UltimoNroFactura(idProveedor);
            Match numero = Regex.Match(ultimo ?? "", @"(\d+)$");
            if (!numero.Success)
                return null;
            string siguiente = (long.Parse(numero.Value) + 1).ToString().PadLeft(numero.Length, '0');
            return ultimo.Substring(0, numero.Index) + siguiente;
        }

        public FacturaProveedor ObtenerFacturaDeOrden(int idOrdenCompra)
        {
            return new FacturaProveedorDAL().ObtenerPorOrden(idOrdenCompra);
        }
    }
}
