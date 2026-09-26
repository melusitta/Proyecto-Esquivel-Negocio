using System;
using System.Collections.Generic;

namespace BE
{
    // Resultado del reporte "Productos más vendidos" para un período
    public class ReporteProductosVendidos
    {
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }          // inclusive
        public bool OrdenadoPorMonto { get; set; }
        public int CantidadFacturas { get; set; }
        public int TotalUnidades { get; set; }       // de todo el período, no solo del top
        public decimal TotalMonto { get; set; }
        public List<ProductoVendido> Productos { get; set; } = new List<ProductoVendido>();
    }
}
