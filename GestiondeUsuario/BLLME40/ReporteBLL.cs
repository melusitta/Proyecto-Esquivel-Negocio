using BE;
using DAL;
using Servicios;
using System;
using System.Data;
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
        // Columnas: Posicion, CodigoProducto, Nombre, Marca, Unidades, Monto, Porcentaje
        public DataTable ProductosMasVendidos(DateTime desde, DateTime hasta, bool porMonto, int top,
            out int cantidadFacturas, out int totalUnidades, out decimal totalMonto)
        {
            desde = desde.Date;
            hasta = hasta.Date;
            if (desde > DateTime.Today || hasta > DateTime.Today)
                throw new ErrorNegocio("FECHA_FUTURA");
            if (desde > hasta)
                throw new ErrorNegocio("FECHAS_INVALIDAS");

            ReporteDAL dal = new ReporteDAL();
            DateTime hastaExclusivo = hasta.AddDays(1);
            DataTable vendidos = dal.ObtenerProductosVendidos(desde, hastaExclusivo);
            var filas = vendidos.Rows.Cast<DataRow>().ToList();

            cantidadFacturas = dal.ContarFacturasCobradas(desde, hastaExclusivo);
            totalUnidades = filas.Sum(f => Convert.ToInt32(f["Unidades"]));
            totalMonto = filas.Sum(f => Convert.ToDecimal(f["Monto"]));

            var ordenados = porMonto
                ? filas.OrderByDescending(f => Convert.ToDecimal(f["Monto"])).ThenByDescending(f => Convert.ToInt32(f["Unidades"]))
                : filas.OrderByDescending(f => Convert.ToInt32(f["Unidades"])).ThenByDescending(f => Convert.ToDecimal(f["Monto"]));

            DataTable ranking = new DataTable();
            ranking.Columns.Add("Posicion", typeof(int));
            ranking.Columns.Add("CodigoProducto", typeof(int));
            ranking.Columns.Add("Nombre", typeof(string));
            ranking.Columns.Add("Marca", typeof(string));
            ranking.Columns.Add("Unidades", typeof(int));
            ranking.Columns.Add("Monto", typeof(decimal));
            ranking.Columns.Add("Porcentaje", typeof(decimal));
            foreach (DataRow f in ordenados.ThenBy(f => f["Nombre"].ToString()))
            {
                if (top > 0 && ranking.Rows.Count == top)
                    break;
                int unidades = Convert.ToInt32(f["Unidades"]);
                decimal monto = Convert.ToDecimal(f["Monto"]);
                decimal porcentaje = porMonto
                    ? (totalMonto == 0 ? 0 : Math.Round(monto * 100 / totalMonto, 1))
                    : (totalUnidades == 0 ? 0 : Math.Round(unidades * 100m / totalUnidades, 1));
                ranking.Rows.Add(ranking.Rows.Count + 1, Convert.ToInt32(f["CodigoProducto"]), f["Nombre"].ToString(),
                    f["Marca"].ToString(), unidades, monto, porcentaje);
            }

            GestorEventosBLL.Instancia.Notificar(
                SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Desconocido",
                "Reporte productos más vendidos", "Reportes", 1);
            return ranking;
        }
    }
}
