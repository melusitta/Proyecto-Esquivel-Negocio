using BE;
using DAL;
using Servicios;
using System;
using System.Linq;

namespace BLL
{
    // Reportes de ventas (menú Reporte)
    public class ReporteBLL
    {
        private static ReporteBLL _instancia;
        private ReporteBLL() { }
        public static ReporteBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new ReporteBLL();
                return _instancia;
            }
        }

        public enum Periodo { Personalizado, DiaDelNino, NavidadYReyes, AnioCompleto }

        // Fechas de cada período para un año (hasta inclusive)
        //   Día del Niño: del 1 de agosto al tercer domingo de agosto (Día del Niño en Argentina)
        //   Navidad y Reyes: del 1 de diciembre al 6 de enero del año siguiente
        public void CalcularPeriodo(Periodo periodo, int anio, out DateTime desde, out DateTime hasta)
        {
            switch (periodo)
            {
                case Periodo.DiaDelNino:
                    desde = new DateTime(anio, 8, 1);
                    hasta = DiaDelNino(anio);
                    break;
                case Periodo.NavidadYReyes:
                    desde = new DateTime(anio, 12, 1);
                    hasta = new DateTime(anio + 1, 1, 6);
                    break;
                case Periodo.AnioCompleto:
                    desde = new DateTime(anio, 1, 1);
                    hasta = new DateTime(anio, 12, 31);
                    break;
                default:
                    throw new ArgumentException("El período personalizado no tiene fechas fijas");
            }
            // Período en curso: se consulta hasta hoy. Si todavía no empezó, queda en el futuro y el reporte lo rechaza.
            if (desde <= DateTime.Today && hasta > DateTime.Today)
                hasta = DateTime.Today;
        }

        // Tercer domingo de agosto
        public static DateTime DiaDelNino(int anio)
        {
            DateTime dia = new DateTime(anio, 8, 1);
            while (dia.DayOfWeek != DayOfWeek.Sunday)
                dia = dia.AddDays(1);
            return dia.AddDays(14);
        }

        // Ranking de productos vendidos entre dos fechas (inclusive), por unidades o por monto.
        // top = 0 devuelve todos. Los totales y porcentajes son sobre todo el período.
        public ReporteProductosVendidos ProductosMasVendidos(DateTime desde, DateTime hasta, bool porMonto, int top)
        {
            desde = desde.Date;
            hasta = hasta.Date;
            if (desde > DateTime.Today || hasta > DateTime.Today)
                throw new ErrorNegocio("FECHA_FUTURA");
            if (desde > hasta)
                throw new ErrorNegocio("FECHAS_INVALIDAS");

            ReporteDAL dal = new ReporteDAL();
            DateTime hastaExclusivo = hasta.AddDays(1);
            var vendidos = dal.ObtenerProductosVendidos(desde, hastaExclusivo);

            var reporte = new ReporteProductosVendidos
            {
                Desde = desde,
                Hasta = hasta,
                OrdenadoPorMonto = porMonto,
                CantidadFacturas = dal.ContarFacturasCobradas(desde, hastaExclusivo),
                TotalUnidades = vendidos.Sum(v => v.Unidades),
                TotalMonto = vendidos.Sum(v => v.Monto)
            };

            var ordenados = porMonto
                ? vendidos.OrderByDescending(v => v.Monto).ThenByDescending(v => v.Unidades)
                : vendidos.OrderByDescending(v => v.Unidades).ThenByDescending(v => v.Monto);
            int posicion = 0;
            foreach (var v in ordenados.ThenBy(v => v.Nombre))
            {
                v.Posicion = ++posicion;
                v.Porcentaje = porMonto
                    ? (reporte.TotalMonto == 0 ? 0 : Math.Round(v.Monto * 100 / reporte.TotalMonto, 1))
                    : (reporte.TotalUnidades == 0 ? 0 : Math.Round(v.Unidades * 100m / reporte.TotalUnidades, 1));
                reporte.Productos.Add(v);
            }
            if (top > 0)
                reporte.Productos = reporte.Productos.Take(top).ToList();

            GestorEventosBLL.Instancia.Notificar(
                SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Desconocido",
                "Reporte productos más vendidos", "Reportes", 1);
            return reporte;
        }
    }
}
