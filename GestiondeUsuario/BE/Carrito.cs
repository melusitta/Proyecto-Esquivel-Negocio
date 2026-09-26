using System;
using System.Collections.Generic;
using System.Linq;

namespace BE
{
    public class Carrito
    {
        public const string EstadoAbierto = "Abierto";
        public const string EstadoAsociado = "Asociado";
        public const string EstadoFacturado = "Facturado";

        public int Id { get; set; }
        // DNI del cliente: se completa al asociar el carrito (el cliente puede no estar registrado todavía)
        public int? DNI { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Vendedor { get; set; }
        public List<ItemCarrito> Items { get; set; } = new List<ItemCarrito>();

        public decimal Total
        {
            get { return Items.Sum(i => i.Subtotal); }
        }

        public int CantidadTotal
        {
            get { return Items.Sum(i => i.Cantidad); }
        }
    }
}
