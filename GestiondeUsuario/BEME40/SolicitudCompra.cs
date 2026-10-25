using System;
using System.Collections.Generic;

namespace BE
{
    // PN2: el encargado de compras la genera con los productos a reponer y la envía (Enviada, CUN-006);
    // queda Cerrada cuando todos sus productos tienen orden de compra (CUN-008)
    public class SolicitudCompra
    {
        public const string EstadoBorrador = "Borrador";
        public const string EstadoEnviada = "Enviada";
        public const string EstadoCerrada = "Cerrada";

        public static readonly string[] Prioridades = { "Baja", "Media", "Alta" };

        public int Id { get; set; }
        public int NroSolicitud { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string CreadaPor { get; set; }
        public string Prioridad { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string Supervisor { get; set; }
        public List<ItemSolicitud> Items { get; set; } = new List<ItemSolicitud>();

        public override string ToString()
        {
            return "Solicitud N° " + NroSolicitud.ToString("D6");
        }
    }

    // Producto a reponer con el proveedor al que se le asignó
    public class ItemSolicitud
    {
        public Producto Producto { get; set; }
        public Proveedor Proveedor { get; set; }
        public int Cantidad { get; set; }
    }
}
