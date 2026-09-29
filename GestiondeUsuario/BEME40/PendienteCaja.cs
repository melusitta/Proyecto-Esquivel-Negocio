using System;

namespace BE
{
    // Un cliente esperando en la caja: tiene un carrito asociado sin facturar
    // o una factura generada que todavía no se cobró.
    public class PendienteCaja
    {
        public int DNI { get; set; }
        // null si el cliente todavía no está registrado (el vendedor asocia el DNI antes del registro)
        public string NombreCliente { get; set; }
        // null si es un carrito; el número si ya hay una factura pendiente de cobro
        public int? NroFactura { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
    }
}
