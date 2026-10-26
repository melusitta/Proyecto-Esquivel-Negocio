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
    // Reporte de recibos de mercadería (PN2): todas las recepciones del depósito, de la más nueva a la más vieja,
    // con buscador (orden, proveedor o responsable). Se imprime el remito de la elegida.
    public partial class FormReporteRecibos : Form, IObservadorIdioma
    {
        private class Fila
        {
            public Recepcion Recepcion;
            public OrdenCompra Orden;
        }

        private List<Fila> _recibos = new List<Fila>();

        public FormReporteRecibos()
        {
            InitializeComponent();
        }

        private void FormReporteRecibos_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.MensajeVacio(dgvRecibos, () => T("sinRecibos"));
            Tema.EstiloPrincipal(btnImprimir);
            lblResumen.ForeColor = Tema.PrincipalOscuro;
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            Cargar();
        }

        private void FormReporteRecibos_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormReporteRecibos", clave);
        }

        private void Cargar()
        {
            try
            {
                _recibos = RecepcionBLL.Instancia.ObtenerRecepciones()
                    .Select(r => new Fila { Recepcion = r, Orden = OrdenCompraBLL.Instancia.ObtenerPorId(r.IdOrdenCompra) }).ToList();
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
            var visibles = _recibos.Where(f => buscado.Length == 0
                || f.Orden.NroOrden.ToString("D6").Contains(buscado) || ImpresionRemito.Numero(f.Recepcion).Contains(buscado)
                || f.Orden.Proveedor.RazonSocial.ToLowerInvariant().Contains(buscado)
                || f.Recepcion.Responsable.ToLowerInvariant().Contains(buscado)).ToList();
            dgvRecibos.DataSource = visibles.Select(f => new
            {
                f.Recepcion.Id,
                Remito = ImpresionRemito.Numero(f.Recepcion),
                Fecha = f.Recepcion.Fecha.ToString("dd/MM/yyyy HH:mm"),
                Orden = f.Orden.NroOrden.ToString("D6"),
                Proveedor = f.Orden.Proveedor.RazonSocial,
                f.Recepcion.Responsable,
                Unidades = f.Recepcion.Items.Sum(i => i.CantidadRecibida),
                Discrepancias = string.IsNullOrEmpty(f.Recepcion.Comentario) ? T("no") : T("si")
            }).ToList();
            dgvRecibos.Columns["Id"].Visible = false;
            Tema.AnchoColumnas(dgvRecibos, ("Remito", 110), ("Fecha", 120), ("Orden", 80), ("Proveedor", 200),
                ("Responsable", 150), ("Unidades", 80), ("Discrepancias", 100));
            TraducirColumnas();
            lblResumen.Text = string.Format(T("lblResumen"), visibles.Count, visibles.Sum(f => f.Recepcion.Items.Sum(i => i.CantidadRecibida)));
            dgvRecibos_SelectionChanged(null, EventArgs.Empty);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            Mostrar();
        }

        private Fila Seleccionada()
        {
            DataGridViewRow fila = dgvRecibos.SelectedRows.Count > 0 ? dgvRecibos.SelectedRows[0] : dgvRecibos.CurrentRow;
            if (fila == null) return null;
            int id = Convert.ToInt32(fila.Cells["Id"].Value);
            return _recibos.FirstOrDefault(f => f.Recepcion.Id == id);
        }

        private void dgvRecibos_SelectionChanged(object sender, EventArgs e)
        {
            btnImprimir.Enabled = Seleccionada() != null;
        }

        private void dgvRecibos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                btnImprimir_Click(sender, e);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Fila f = Seleccionada();
            if (f == null) return;
            try
            {
                ImpresionRemito.MostrarVistaPrevia(this, f.Orden, f.Recepcion);
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
            foreach (DataGridViewColumn c in dgvRecibos.Columns) c.HeaderText = T("col" + c.Name);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormReporteRecibos"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblBuscar.Text = t["lblBuscar"]?.ToString();
            btnImprimir.Text = t["btnImprimir"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            if (dgvRecibos.DataSource != null)
                Mostrar();
        }
    }
}
