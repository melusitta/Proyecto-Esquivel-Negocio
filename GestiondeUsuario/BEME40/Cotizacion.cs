using System;
using System.Collections.Generic;

namespace BE
{
    // PN2 paso 4: cotización que remite un proveedor para una solicitud de compra.
    // El supervisor la registra y la elige dentro de CUN-008 Generar orden de compra.
    public class Cotizacion
    {
        public const string EstadoRecibida = "Recibida";
        public const string EstadoAceptada = "Aceptada";

        public int Id { get; set; }
        public int IdSolicitud { get; set; }
        public Proveedor Proveedor { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public int PlazoEntregaDias { get; set; }
        public string FormaPago { get; set; }
        public string Estado { get; set; }
        public List<ItemCotizacion> Items { get; set; } = new List<ItemCotizacion>();
    }

    public class ItemCotizacion
    {
        public Producto Producto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string Marca { get; set; }
        public string Presentacion { get; set; }
        public int CantidadDisponible { get; set; }
    }
}
