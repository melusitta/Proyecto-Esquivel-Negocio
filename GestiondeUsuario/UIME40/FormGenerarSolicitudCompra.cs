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
    // CUN-008 Generar solicitud de compra (Supervisor).
    // Paso 3: toma la solicitud que armó el encargado de compras, fija cantidades y prioridad y la envía a los proveedores.
    public partial class FormGenerarSolicitudCompra : Form, IObservadorIdioma
    {
        public FormGenerarSolicitudCompra()
        {
            InitializeComponent();
        }

        private void FormGenerarSolicitudCompra_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnGenerar);
            lblFecha.ForeColor = Tema.PrincipalOscuro;
            CrearColumnasItems();

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            CargarSolicitudes();
        }

        private void FormGenerarSolicitudCompra_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormGenerarSolicitudCompra", clave);
        }

        private void CrearColumnasItems()
        {
            dgvItems.AutoGenerateColumns = false;
            dgvItems.Columns.Clear();
            foreach (var c in new[] { ("Codigo", 50f), ("Producto", 180f), ("Proveedor", 160f), ("Existencia", 60f), ("StockMinimo", 60f) })
                dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = c.Item1, FillWeight = c.Item2, ReadOnly = true });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", FillWeight = 70 });
        }

        private void CargarSolicitudes()
        {
            try
            {
                List<SolicitudCompra> borradores = SolicitudCompraBLL.Instancia.ObtenerBorradores();
                dgvSolicitudes.DataSource = null;
                dgvSolicitudes.DataSource = borradores.Select(s => new
                {
                    s.Id,
                    Nro = s.NroSolicitud.ToString("D6"),
                    Fecha = s.FechaCreacion.ToString("dd/MM/yyyy HH:mm"),
                    s.CreadaPor,
                    Productos = s.Items.Count,
                    Proveedores = SolicitudCompraBLL.Instancia.ProveedoresDe(s).Count
                }).ToList();
                dgvSolicitudes.Columns["Id"].Visible = false;
                TraducirColumnas();
                MostrarSolicitud();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private int? IdSeleccionada()
        {
            DataGridViewRow fila = dgvSolicitudes.SelectedRows.Count > 0 ? dgvSolicitudes.SelectedRows[0] : dgvSolicitudes.CurrentRow;
            return fila == null ? (int?)null : Convert.ToInt32(fila.Cells["Id"].Value);
        }

        private void dgvSolicitudes_SelectionChanged(object sender, EventArgs e)
        {
            MostrarSolicitud();
        }

        private void MostrarSolicitud()
        {
            dgvItems.Rows.Clear();
            int? id = IdSeleccionada();
            btnGenerar.Enabled = id.HasValue;
            lblFecha.Text = id.HasValue ? string.Format(T("lblFecha"), DateTime.Today.ToString("dd/MM/yyyy")) : T("sinSolicitudes");
            if (!id.HasValue) return;
            try
            {
                SolicitudCompra s = SolicitudCompraBLL.Instancia.ObtenerPorId(id.Value);
                foreach (ItemSolicitud i in s.Items)
                {
                    int fila = dgvItems.Rows.Add(i.Producto.CodigoProducto, i.Producto.Nombre, i.Proveedor.RazonSocial,
                        i.Producto.Existencia, i.Producto.StockMinimo, i.Cantidad);
                    dgvItems.Rows[fila].Tag = i.Producto.Id;
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void dgvItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        // Paso 3: genera la solicitud (cantidades + prioridad) y queda enviada a los proveedores
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            int? id = IdSeleccionada();
            if (!id.HasValue) return;
            dgvItems.EndEdit();
            var cantidades = new Dictionary<int, int>();
            foreach (DataGridViewRow fila in dgvItems.Rows)
            {
                int cantidad;
                int.TryParse(Convert.ToString(fila.Cells["Cantidad"].Value), out cantidad);   // si no es número queda 0 y la BLL lo rechaza
                cantidades[(int)fila.Tag] = cantidad;
            }
            try
            {
                string prioridad = (cmbPrioridad.SelectedItem as OpcionCodigo)?.Codigo;
                SolicitudCompra s = SolicitudCompraBLL.Instancia.GenerarSolicitud(id.Value, cantidades, prioridad);
                string proveedores = string.Join(", ", SolicitudCompraBLL.Instancia.ProveedoresDe(s).Select(p => p.RazonSocial));
                MessageBox.Show(string.Format(T("msgEnviada"), s.NroSolicitud.ToString("D6"), proveedores),
                    T("msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarSolicitudes();
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

        private void CargarPrioridades()
        {
            string elegida = (cmbPrioridad.SelectedItem as OpcionCodigo)?.Codigo ?? "Media";
            cmbPrioridad.Items.Clear();
            foreach (string p in SolicitudCompra.Prioridades)
                cmbPrioridad.Items.Add(new OpcionCodigo { Codigo = p, Texto = TextosCompras.Prioridad(p) });
            cmbPrioridad.SelectedIndex = Array.IndexOf(SolicitudCompra.Prioridades, elegida);
        }

        private void TraducirColumnas()
        {
            foreach (DataGridViewColumn c in dgvSolicitudes.Columns)
                c.HeaderText = T("colSol" + c.Name);
            foreach (DataGridViewColumn c in dgvItems.Columns)
                c.HeaderText = T("col" + c.Name);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormGenerarSolicitudCompra"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblSolicitudes.Text = t["lblSolicitudes"]?.ToString();
            lblItems.Text = t["lblItems"]?.ToString();
            lblPrioridad.Text = t["lblPrioridad"]?.ToString();
            btnGenerar.Text = t["btnGenerar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            CargarPrioridades();
            TraducirColumnas();
        }
    }
}
