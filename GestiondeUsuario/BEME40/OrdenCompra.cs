using System;
using System.Collections.Generic;

namespace BE
{
    // PN2: Enviada (CUN-008: se genera y se envía al proveedor) -> Pagada (CUN-010, incluido en CUN-008)
    // -> Recibida (CUN-011). Emitida queda de las versiones anteriores.
    public class OrdenCompra
    {
        public const string EstadoEmitida = "Emitida";
        public const string EstadoEnviada = "Enviada";
        public const string EstadoPagada = "Pagada";
        public const string EstadoRecibida = "Recibida";

        public int Id { get; set; }
        public int NroOrden { get; set; }
        public int IdSolicitud { get; set; }
        public int IdCotizacion { get; set; }
        public Proveedor Proveedor { get; set; }
        public DateTime FechaEmision { get; set; }
        public string CondicionesEntrega { get; set; }
        public string CondicionesPago { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string Supervisor { get; set; }
        public List<ItemOrdenCompra> Items { get; set; } = new List<ItemOrdenCompra>();

        public override string ToString()
        {
            return "OC N° " + NroOrden.ToString("D6") + " - " + Proveedor?.RazonSocial;
        }
    }

    public class ItemOrdenCompra
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal
        {
            get { return Cantidad * PrecioUnitario; }
        }
    }
}
