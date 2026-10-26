using BE;
using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // Reporte de facturas (PN1): todas las facturas, de la más nueva a la más vieja, con buscador
    // (número, DNI o cliente). Se imprime la elegida con "Imprimir factura" o con doble clic.
    public partial class FormReporteFacturas : Form, IObservadorIdioma
    {
        private List<Factura> _facturas = new List<Factura>();

        public FormReporteFacturas()
        {
            InitializeComponent();
        }

        private void FormReporteFacturas_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.MensajeVacio(dgvFacturas, () => T("sinFacturas"));
            Tema.EstiloPrincipal(btnImprimir);
            lblResumen.ForeColor = Tema.PrincipalOscuro;
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            Cargar();
        }

        private void FormReporteFacturas_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormReporteFacturas", clave);
        }

        private void Cargar()
        {
            try
            {
                _facturas = FacturaBLL.Instancia.ObtenerTodas();
                Mostrar();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void Mostrar()
        {
            string buscado = txtBuscar.Text.Trim().ToLowerInvariant();
            var visibles = _facturas.Where(f => buscado.Length == 0
                || f.NroFactura.ToString().Contains(buscado) || f.NroFactura.ToString("D8").Contains(buscado)
                || f.DNI.ToString().Contains(buscado) || (f.NombreCliente ?? "").ToLowerInvariant().Contains(buscado)).ToList();
            var g = GestorIdioma.Instancia;
            dgvFacturas.DataSource = visibles.Select(f => new
            {
                f.Id,
                Nro = f.NroFactura.ToString("D8"),
                Fecha = f.FechaHora.ToString("dd/MM/yyyy HH:mm"),
                f.DNI,
                Cliente = f.NombreCliente,
                Total = f.Total.ToString("N2"),
                FormaPago = f.FormaPago == null ? "" : g.Obtener("FormCobrarVenta", "fp" + f.FormaPago),
                Estado = T("estado" + f.Estado),
                FechaPago = f.FechaPago?.ToString("dd/MM/yyyy HH:mm") ?? ""
            }).ToList();
            dgvFacturas.Columns["Id"].Visible = false;
            Tema.AnchoColumnas(dgvFacturas, ("Nro", 90), ("Fecha", 120), ("DNI", 90), ("Cliente", 200), ("Total", 100),
                ("FormaPago", 110), ("Estado", 90), ("FechaPago", 120));
            TraducirColumnas();
            lblResumen.Text = string.Format(T("lblResumen"), visibles.Count, visibles.Sum(f => f.Total).ToString("N2"));
            dgvFacturas_SelectionChanged(null, EventArgs.Empty);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void dgvFacturas_SelectionChanged(object sender, EventArgs e)
        {
            btnImprimir.Enabled = Seleccionada() != null;
        }

        private int? Seleccionada()
        {
            DataGridViewRow fila = dgvFacturas.SelectedRows.Count > 0 ? dgvFacturas.SelectedRows[0] : dgvFacturas.CurrentRow;
            return fila == null ? (int?)null : Convert.ToInt32(fila.Cells["Id"].Value);
        }

        private void dgvFacturas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                btnImprimir_Click(sender, e);
        }

        // La misma factura impresa que recibe el cliente
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            int? id = Seleccionada();
            if (id == null) return;
            try
            {
                new ImpresionFactura(FacturaBLL.Instancia.ObtenerPorId(id.Value)).MostrarVistaPrevia(this);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            new FormPrincipal().Show();
            this.Close();
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex), T("msgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ---------- Idioma ----------

        private void TraducirColumnas()
        {
            foreach (DataGridViewColumn c in dgvFacturas.Columns) c.HeaderText = T("col" + c.Name);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormReporteFacturas"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblBuscar.Text = t["lblBuscar"]?.ToString();
            btnImprimir.Text = t["btnImprimir"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            if (dgvFacturas.DataSource != null)
                Mostrar();   // estados y formas de pago traducidos
        }
    }
}
