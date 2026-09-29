using System;
using System.Collections.Generic;

namespace BE
{
    public class Factura
    {
        public const string EstadoPendiente = "Pendiente";
        public const string EstadoPagada = "Pagada";

        public int Id { get; set; }
        public int NroFactura { get; set; }
        public int IdCarrito { get; set; }
        public int IdCliente { get; set; }
        // DNI y nombre del cliente al momento de la venta
        public int DNI { get; set; }
        public string NombreCliente { get; set; }
        public DateTime FechaHora { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public string FormaPago { get; set; }
        public decimal? MontoPagado { get; set; }
        public DateTime? FechaPago { get; set; }
        public string Cajero { get; set; }
        public List<ItemFactura> Items { get; set; } = new List<ItemFactura>();
    }
}
