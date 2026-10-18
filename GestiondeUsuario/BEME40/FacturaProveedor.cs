using System;

namespace BE
{
    // PN2 pasos 8 y 9: factura que entrega el proveedor y su pago (CUN-011 Gestionar pago)
    public class FacturaProveedor
    {
        public const string EstadoPagado = "Pagado";

        public int Id { get; set; }
        public int IdOrdenCompra { get; set; }
        public int IdProveedor { get; set; }
        // Datos de la factura del proveedor
        public string NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
        public decimal Impuestos { get; set; }
        // Datos del pago
        public decimal MontoAbonado { get; set; }
        public DateTime FechaOperacion { get; set; }
        public string NroComprobante { get; set; }
        public string Estado { get; set; }
        public string Supervisor { get; set; }

        public decimal Total
        {
            get { return Importe + Impuestos; }
        }
    }
}
