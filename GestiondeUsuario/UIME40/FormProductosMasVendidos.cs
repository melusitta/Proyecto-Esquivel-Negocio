using BE;
using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GestiondeUsuario
{
    // Reporte: productos más vendidos en un período (personalizado o festivo: Día del Niño, Navidad y Reyes)
    public partial class FormProductosMasVendidos : Form, IObservadorIdioma
    {
        private ReporteProductosVendidos _reporte;
        private Opcion _periodoReporte;   // período con el que se generó el reporte (para imprimir)
        private Chart _grafico;
        private int _siguienteFilaImpresion;

        private class Opcion
        {
            public object Valor { get; set; }
            public string Clave { get; set; }
            public override string ToString() { return GestorIdioma.Instancia.Obtener("FormProductosMasVendidos", Clave); }
        }

        public FormProductosMasVendidos()
        {
            InitializeComponent();
        }

        private void FormProductosMasVendidos_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnGenerar);
            lblResumen.ForeColor = Tema.PrincipalOscuro;
            CrearGrafico();

            cmbPeriodo.Items.AddRange(new object[]
            {
                new Opcion { Valor = ReporteBLL.Periodo.DiaDelNino, Clave = "perDiaDelNino" },
                new Opcion { Valor = ReporteBLL.Periodo.NavidadYReyes, Clave = "perNavidadYReyes" },
                new Opcion { Valor = ReporteBLL.Periodo.AnioCompleto, Clave = "perAnioCompleto" },
                new Opcion { Valor = ReporteBLL.Periodo.Personalizado, Clave = "perPersonalizado" }
            });
            cmbTop.Items.AddRange(new object[]
            {
                new Opcion { Valor = 10, Clave = "top10" },
                new Opcion { Valor = 20, Clave = "top20" },
                new Opcion { Valor = 0, Clave = "topTodos" }
            });
            cmbTop.SelectedIndex = 0;

            // Por defecto: el último Día del Niño que ya pasó
            int anio = DateTime.Today.Year;
            if (DateTime.Today < ReporteBLL.DiaDelNino(anio))
                anio--;
            nudAnio.Value = anio;
            cmbPeriodo.SelectedIndex = 0;

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            MostrarReporte();
        }

        private void FormProductosMasVendidos_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        // ---------- Filtros ----------

        private ReporteBLL.Periodo PeriodoElegido()
        {
            return (ReporteBLL.Periodo)((Opcion)cmbPeriodo.SelectedItem).Valor;
        }

        private void cmbPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarFechas();
        }

        private void nudAnio_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechas();
        }

        // Los períodos festivos calculan solos sus fechas según el año; el personalizado usa las elegidas
        private void ActualizarFechas()
        {
            if (cmbPeriodo.SelectedItem == null) return;
            bool personalizado = PeriodoElegido() == ReporteBLL.Periodo.Personalizado;
            nudAnio.Enabled = !personalizado;
            dtpDesde.Enabled = personalizado;
            dtpHasta.Enabled = personalizado;
            if (!personalizado)
            {
                DateTime desde, hasta;
                ReporteBLL.Instancia.CalcularPeriodo(PeriodoElegido(), (int)nudAnio.Value, out desde, out hasta);
                dtpDesde.Value = desde;
                dtpHasta.Value = hasta;
            }
        }

        // ---------- Reporte ----------

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                int top = (int)((Opcion)cmbTop.SelectedItem).Valor;
                _reporte = ReporteBLL.Instancia.ProductosMasVendidos(dtpDesde.Value, dtpHasta.Value, rbMonto.Checked, top);
                _periodoReporte = (Opcion)cmbPeriodo.SelectedItem;
                MostrarReporte();
            }
            catch (Exception ex)
            {
                MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex),
                    GestorIdioma.Instancia.Obtener("FormProductosMasVendidos", "msgError"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarReporte()
        {
            var g = GestorIdioma.Instancia;
            var filas = (_reporte?.Productos ?? new List<ProductoVendido>()).Select(p => new
            {
                p.Posicion,
                p.CodigoProducto,
                Producto = p.Nombre,
                p.Marca,
                p.Unidades,
                p.Monto,
                Porcentaje = p.Porcentaje.ToString("N1") + " %"
            }).ToList();
            dgvRanking.DataSource = null;
            dgvRanking.DataSource = filas;
            dgvRanking.Columns["Monto"].DefaultCellStyle.Format = "N2";
            foreach (var c in new[] { "Posicion", "CodigoProducto", "Unidades", "Monto", "Porcentaje" })
                dgvRanking.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Tema.AnchoColumnas(dgvRanking, ("Posicion", 35), ("CodigoProducto", 60), ("Producto", 170), ("Marca", 85),
                ("Unidades", 70), ("Monto", 95), ("Porcentaje", 60));
            TraducirColumnas();

            if (_reporte == null)
                lblResumen.Text = "";
            else if (_reporte.TotalUnidades == 0)
                lblResumen.Text = string.Format(g.Obtener("FormProductosMasVendidos", "sinVentas"),
                    _reporte.Desde.ToString("dd/MM/yyyy"), _reporte.Hasta.ToString("dd/MM/yyyy"));
            else
                lblResumen.Text = TextoResumen();

            ActualizarGrafico();
        }

        private string TextoResumen()
        {
            return string.Format(GestorIdioma.Instancia.Obtener("FormProductosMasVendidos", "lblResumen"),
                _reporte.Desde.ToString("dd/MM/yyyy"), _reporte.Hasta.ToString("dd/MM/yyyy"),
                _reporte.CantidadFacturas, _reporte.TotalUnidades, _reporte.TotalMonto.ToString("N2"));
        }

        // ---------- Gráfico: barras horizontales, una sola serie (sin leyenda), N°1 arriba ----------

        private void CrearGrafico()
        {
            _grafico = new Chart { Dock = DockStyle.Fill, BackColor = Color.White };
            var area = new ChartArea("ranking") { BackColor = Color.White };
            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.MajorTickMark.Enabled = false;
            area.AxisX.LineColor = Tema.Borde;
            area.AxisX.LabelStyle.Font = new Font(Tema.Fuente, 9F);
            area.AxisX.LabelStyle.ForeColor = Tema.Texto;
            area.AxisY.MajorGrid.LineColor = Tema.Borde;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            area.AxisY.MajorTickMark.Enabled = false;
            area.AxisY.LineColor = Tema.Borde;
            area.AxisY.LabelStyle.Font = new Font(Tema.Fuente, 8.5F);
            area.AxisY.LabelStyle.ForeColor = Color.DimGray;
            // Etiquetas en una sola fila y a tamaño normal: si no entran, se muestran menos marcas
            area.AxisY.IsLabelAutoFit = false;
            area.AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            area.AxisX.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.IncreaseFont;
            area.AxisY.TitleFont = new Font(Tema.Fuente, 9F);
            area.AxisY.TitleForeColor = Color.DimGray;
            _grafico.ChartAreas.Add(area);

            var serie = new Series("ventas")
            {
                ChartType = SeriesChartType.Bar,
                Color = Tema.PrincipalOscuro,
                BorderWidth = 0,
                IsValueShownAsLabel = false   // los valores exactos están en la tabla
            };
            serie["PointWidth"] = "0.6";
            _grafico.Series.Add(serie);
            pnlGrafico.Controls.Add(_grafico);
        }

        private void ActualizarGrafico()
        {
            var g = GestorIdioma.Instancia;
            var serie = _grafico.Series["ventas"];
            serie.Points.Clear();
            _grafico.Titles.Clear();

            bool porMonto = _reporte?.OrdenadoPorMonto ?? rbMonto.Checked;
            var area = _grafico.ChartAreas["ranking"];
            area.AxisY.Title = g.Obtener("FormProductosMasVendidos", porMonto ? "ejeMonto" : "ejeUnidades");
            // Montos del eje en miles ($ 120k) para que entren; el monto exacto está en la tabla y en el tooltip
            area.AxisY.LabelStyle.Format = porMonto ? "$ #,0,k" : "#,0";

            if (_reporte == null || _reporte.Productos.Count == 0)
            {
                _grafico.Titles.Add(new Title(g.Obtener("FormProductosMasVendidos", "sinDatosGrafico"),
                    Docking.Top, new Font(Tema.Fuente, 10F), Color.DimGray));
                return;
            }

            // Las barras se dibujan de abajo hacia arriba: se cargan al revés para que el N°1 quede arriba
            foreach (var p in Enumerable.Reverse(_reporte.Productos))
            {
                double valor = porMonto ? (double)p.Monto : p.Unidades;
                int i = serie.Points.AddXY(p.Posicion + ". " + p.Nombre, valor);
                serie.Points[i].ToolTip = porMonto
                    ? p.Nombre + ": $ " + p.Monto.ToString("N2")
                    : p.Nombre + ": " + p.Unidades + " " + g.Obtener("FormProductosMasVendidos", "ejeUnidades").ToLower();
            }
        }

        // ---------- Impresión ----------

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (_reporte == null)
            {
                MessageBox.Show(g.Obtener("FormProductosMasVendidos", "msgGenerarPrimero"),
                    g.Obtener("FormProductosMasVendidos", "msgAtencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Sin impresora instalada se guarda como PDF en Descargas
            string titulo = g.Obtener("FormProductosMasVendidos", "tituloForm");
            using (PrintDocument documento = CrearDocumento())
                Impresion.MostrarOGuardarPdf(documento, this, titulo,
                    titulo + "_" + _reporte.Desde.ToString("yyyy-MM-dd") + "_" + _reporte.Hasta.ToString("yyyy-MM-dd"));
        }

        public PrintDocument CrearDocumento()
        {
            var documento = new PrintDocument();
            documento.DocumentName = GestorIdioma.Instancia.Obtener("FormProductosMasVendidos", "tituloForm");
            documento.BeginPrint += (s, e) => _siguienteFilaImpresion = 0;
            documento.PrintPage += ImprimirPagina;
            return documento;
        }

        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            var t = GestorIdioma.Instancia;
            Graphics gr = e.Graphics;
            Rectangle area = e.MarginBounds;
            float izq = area.Left, der = area.Right, y = area.Top;

            using (var fTitulo = new Font(Tema.Fuente, 16, FontStyle.Bold))
            using (var fNormal = new Font(Tema.Fuente, 9.5F))
            using (var fNegrita = new Font(Tema.Fuente, 9.5F, FontStyle.Bold))
            using (var lila = new SolidBrush(Tema.PrincipalOscuro))
            using (var fondo = new SolidBrush(Tema.Principal))
            using (var derecha = new StringFormat { Alignment = StringAlignment.Far })
            {
                gr.DrawString("Plumy · " + t.Obtener("FormProductosMasVendidos", "tituloForm"), fTitulo, lila, izq, y);
                y += 34;
                gr.DrawString(_periodoReporte + " · " + t.Obtener("FormProductosMasVendidos", _reporte.OrdenadoPorMonto ? "rbMonto" : "rbUnidades"),
                    fNegrita, Brushes.Black, izq, y);
                y += 20;
                gr.DrawString(_reporte.TotalUnidades == 0
                        ? string.Format(t.Obtener("FormProductosMasVendidos", "sinVentas"), _reporte.Desde.ToString("dd/MM/yyyy"), _reporte.Hasta.ToString("dd/MM/yyyy"))
                        : TextoResumen(), fNormal, Brushes.Black, izq, y);
                y += 30;

                // El gráfico solo en la primera página
                if (_siguienteFilaImpresion == 0 && _reporte.Productos.Count > 0)
                {
                    // Se dibuja directo en la hoja (nítido y sin deformarse), no como foto de la pantalla
                    int ancho = (int)(der - izq), alto = Math.Min(340, 90 + _reporte.Productos.Count * 30);
                    _grafico.Printing.PrintPaint(gr, new Rectangle((int)izq, (int)y, ancho, alto));
                    y += alto + 15;
                }

                // Tabla
                float[] cols = { izq + 30, izq + 90, der - 330, der - 200, der - 90, der };   // bordes derechos de columnas numéricas
                gr.FillRectangle(fondo, izq, y, der - izq, 22);
                gr.DrawString("#", fNegrita, Brushes.Black, cols[0], y + 3, derecha);
                gr.DrawString(t.Obtener("FormProductosMasVendidos", "colCodigo"), fNegrita, Brushes.Black, cols[1], y + 3, derecha);
                gr.DrawString(t.Obtener("FormProductosMasVendidos", "colProducto"), fNegrita, Brushes.Black, cols[1] + 12, y + 3);
                gr.DrawString(t.Obtener("FormProductosMasVendidos", "colUnidades"), fNegrita, Brushes.Black, cols[3], y + 3, derecha);
                gr.DrawString(t.Obtener("FormProductosMasVendidos", "colMonto"), fNegrita, Brushes.Black, cols[4], y + 3, derecha);
                gr.DrawString("%", fNegrita, Brushes.Black, cols[5] - 4, y + 3, derecha);
                y += 26;

                while (_siguienteFilaImpresion < _reporte.Productos.Count)
                {
                    if (y > area.Bottom - 20)
                    {
                        e.HasMorePages = true;
                        return;
                    }
                    var p = _reporte.Productos[_siguienteFilaImpresion++];
                    gr.DrawString(p.Posicion.ToString(), fNormal, Brushes.Black, cols[0], y, derecha);
                    gr.DrawString(p.CodigoProducto.ToString(), fNormal, Brushes.Black, cols[1], y, derecha);
                    gr.DrawString(p.Nombre + " (" + p.Marca + ")", fNormal, Brushes.Black, cols[1] + 12, y);
                    gr.DrawString(p.Unidades.ToString(), fNormal, Brushes.Black, cols[3], y, derecha);
                    gr.DrawString("$ " + p.Monto.ToString("N2"), fNormal, Brushes.Black, cols[4], y, derecha);
                    gr.DrawString(p.Porcentaje.ToString("N1") + " %", fNormal, Brushes.Black, cols[5] - 4, y, derecha);
                    y += 20;
                }
                e.HasMorePages = false;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            new FormPrincipal().Show();
            this.Close();
        }

        // ---------- Idioma ----------

        private void TraducirColumnas()
        {
            var g = GestorIdioma.Instancia;
            var columnas = new Dictionary<string, string>
            {
                { "Posicion", "colPosicion" }, { "CodigoProducto", "colCodigo" }, { "Producto", "colProducto" },
                { "Marca", "colMarca" }, { "Unidades", "colUnidades" }, { "Monto", "colMonto" }, { "Porcentaje", "colPorcentaje" }
            };
            foreach (var c in columnas)
                if (dgvRanking.Columns.Contains(c.Key))
                    dgvRanking.Columns[c.Key].HeaderText = g.Obtener("FormProductosMasVendidos", c.Value);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormProductosMasVendidos"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            grpFiltros.Text = t["grpFiltros"]?.ToString();
            lblPeriodo.Text = t["lblPeriodo"]?.ToString();
            lblAnio.Text = t["lblAnio"]?.ToString();
            lblDesde.Text = t["lblDesde"]?.ToString();
            lblHasta.Text = t["lblHasta"]?.ToString();
            lblOrden.Text = t["lblOrden"]?.ToString();
            rbUnidades.Text = t["rbUnidades"]?.ToString();
            rbMonto.Text = t["rbMonto"]?.ToString();
            lblMostrar.Text = t["lblMostrar"]?.ToString();
            btnGenerar.Text = t["btnGenerar"]?.ToString();
            grpGrafico.Text = t["grpGrafico"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            btnImprimir.Text = t["btnImprimir"]?.ToString();
            // Los combos muestran el texto traducido de cada opción: se refrescan conservando la selección
            foreach (var combo in new[] { cmbPeriodo, cmbTop })
            {
                int i = combo.SelectedIndex;
                var items = combo.Items.Cast<object>().ToArray();
                combo.Items.Clear();
                combo.Items.AddRange(items);
                combo.SelectedIndex = i;
            }
            MostrarReporte();
        }
    }
}
