using System;
using System.Collections.Generic;

namespace BE
{
    // PN2 paso 11: mercadería recibida en el depósito (CUN-011 Registrar productos recibidos)
    public class Recepcion
    {
        public int Id { get; set; }
        public int IdOrdenCompra { get; set; }
        public DateTime Fecha { get; set; }
        public string Responsable { get; set; }
        // Discrepancias entre la orden, la factura y lo entregado (5.1)
        public string Comentario { get; set; }
        public List<ItemRecepcion> Items { get; set; } = new List<ItemRecepcion>();
    }
}
