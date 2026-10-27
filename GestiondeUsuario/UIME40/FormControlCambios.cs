using BE;
using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // Control de cambios del maestro de productos (menú Admin): se elige un producto y se ven todas sus versiones
    // (alta, modificaciones, baja, reactivación, restauraciones), con lo que cambió en cada una resaltado.
    // "Restaurar esta versión" vuelve el producto a esos datos (menos la existencia) y queda como versión nueva.
    public partial class FormControlCambios : Form, IObservadorIdioma
    {
        private List<Producto> _productos = new List<Producto>();
        private List<ProductoVersion> _versiones = new List<ProductoVersion>();

        // Campos que se comparan entre versiones (nombre de columna de la grilla)
        private static readonly string[] Campos = { "CodigoProducto", "Nombre", "Marca", "Color", "Modelo", "PrecioUnitario",
                                                    "Existencia", "Categoria", "StockMinimo", "Proveedor", "Activo" };

        public FormControlCambios()
        {
            InitializeComponent();
        }

        private void FormControlCambios_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnRestaurar);
            Tema.MensajeVacio(dgvVersiones, () => T("sinVersiones"));
            lblAyudaRestaurar.ForeColor = Tema.PrincipalOscuro;
            CrearColumnasVersiones();
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            CargarProductos(null);
        }

        private void FormControlCambios_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormControlCambios", clave);
        }

        // ---------- Productos ----------

        private void CargarProductos(int? seleccionar)
        {
            try
            {
                int? actual = seleccionar ?? Seleccionado()?.Id;
                _productos = ProductoBLL.Instancia.ObtenerParaABM(true, txtBuscar.Text);
                dgvProductos.SelectionChanged -= dgvProductos_SelectionChanged;
                dgvProductos.DataSource = _productos.Select(p => new
                {
                    p.Id,
                    Codigo = p.CodigoProducto,
                    p.Nombre,
                    p.Marca,
                    Precio = p.PrecioUnitario.ToString("N2"),
                    p.Existencia,
                    Estado = p.Activo ? T("activo") : T("inactivo")
                }).ToList();
                dgvProductos.Columns["Id"].Visible = false;
                Tema.AnchoColumnas(dgvProductos, ("Codigo", 70), ("Nombre", 250), ("Marca", 140), ("Precio", 100), ("Existencia", 80), ("Estado", 90));
                foreach (DataGridViewColumn c in dgvProductos.Columns) c.HeaderText = T("colProd" + c.Name);
                foreach (DataGridViewRow fila in dgvProductos.Rows)
                    if (actual.HasValue && Convert.ToInt32(fila.Cells["Id"].Value) == actual.Value)
                        dgvProductos.CurrentCell = fila.Cells["Codigo"];
                dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
                MostrarVersiones();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private Producto Seleccionado()
        {
            DataGridViewRow fila = dgvProductos.SelectedRows.Count > 0 ? dgvProductos.SelectedRows[0] : dgvProductos.CurrentRow;
            if (fila == null) return null;
            int id = Convert.ToInt32(fila.Cells["Id"].Value);
            return _productos.FirstOrDefault(p => p.Id == id);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarProductos(null);
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            MostrarVersiones();
        }

        // ---------- Versiones ----------

        private void CrearColumnasVersiones()
        {
            dgvVersiones.AutoGenerateColumns = false;
            dgvVersiones.Columns.Clear();
            foreach (var c in new[] { ("Version", 40f), ("Fecha", 115f), ("Usuario", 125f), ("Cambio", 105f), ("CodigoProducto", 60f),
                                      ("Nombre", 170f), ("Marca", 105f), ("Color", 75f), ("Modelo", 90f), ("PrecioUnitario", 85f),
                                      ("Existencia", 80f), ("Categoria", 90f), ("StockMinimo", 80f), ("Proveedor", 140f), ("Activo", 55f) })
                dgvVersiones.Columns.Add(new DataGridViewTextBoxColumn { Name = c.Item1, FillWeight = c.Item2, MinimumWidth = 40 });
        }

        private void MostrarVersiones()
        {
            dgvVersiones.Rows.Clear();
            Producto p = Seleccionado();
            _versiones = p == null ? new List<ProductoVersion>() : ProductoBLL.Instancia.ObtenerVersiones(p.Id);
            foreach (ProductoVersion v in _versiones)
            {
                Producto d = v.Datos;
                string cambio = TextoCambio(v);
                int fila = dgvVersiones.Rows.Add(v.NroVersion, v.Fecha.ToString("dd/MM/yyyy HH:mm"), v.Usuario, cambio,
                    d.CodigoProducto, d.Nombre, d.Marca, d.Color, d.Modelo, d.PrecioUnitario.ToString("N2"), d.Existencia,
                    d.Categoria, d.StockMinimo, d.ProveedorHabitual ?? "", d.Activo ? T("si") : T("no"));
                dgvVersiones.Rows[fila].Tag = v;
            }
            // Lo que cambió respecto de la versión anterior (la siguiente fila, porque van de la más nueva a la más vieja)
            for (int i = 0; i < dgvVersiones.Rows.Count - 1; i++)
                foreach (string campo in Campos)
                {
                    DataGridViewCell celda = dgvVersiones.Rows[i].Cells[campo];
                    if (!Equals(Convert.ToString(celda.Value), Convert.ToString(dgvVersiones.Rows[i + 1].Cells[campo].Value)))
                    {
                        celda.Style.BackColor = Tema.AcentoSuave;
                        celda.Style.Font = new Font(dgvVersiones.Font, FontStyle.Bold);
                    }
                }
            dgvVersiones.ClearSelection();
            dgvVersiones_SelectionChanged(null, EventArgs.Empty);
        }

        private string TextoCambio(ProductoVersion v)
        {
            string texto = T("tipo" + v.TipoCambio);
            if (v.TipoCambio == ProductoVersion.TipoRestauracion && v.Detalle != null)
                texto += " (" + string.Format(T("deVersion"), v.Detalle) + ")";
            return texto;
        }

        private ProductoVersion VersionElegida()
        {
            DataGridViewRow fila = dgvVersiones.SelectedRows.Count > 0 ? dgvVersiones.SelectedRows[0] : null;
            return fila?.Tag as ProductoVersion;
        }

        // Se puede restaurar cualquier versión menos la última (que son los datos de hoy)
        private void dgvVersiones_SelectionChanged(object sender, EventArgs e)
        {
            ProductoVersion v = VersionElegida();
            bool sePuede = v != null && _versiones.Count > 0 && v.Id != _versiones[0].Id;
            btnRestaurar.Enabled = sePuede;
            lblAyudaRestaurar.Text = v == null ? T("ayudaElegir") : sePuede ? T("ayudaRestaurar") : T("ayudaActual");
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            ProductoVersion v = VersionElegida();
            Producto p = Seleccionado();
            if (v == null || p == null) return;
            if (MessageBox.Show(string.Format(T("msgConfirmar"), p.Nombre, v.NroVersion, v.Fecha.ToString("dd/MM/yyyy HH:mm")),
                    T("tituloForm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                ProductoBLL.Instancia.RestaurarVersion(v.Id);
                Aviso.Exito(string.Format(T("msgRestaurada"), p.Nombre, v.NroVersion));
                CargarProductos(p.Id);
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

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormControlCambios"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblBuscar.Text = t["lblBuscar"]?.ToString();
            lblProductos.Text = t["lblProductos"]?.ToString();
            lblVersiones.Text = t["lblVersiones"]?.ToString();
            btnRestaurar.Text = t["btnRestaurar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            foreach (DataGridViewColumn c in dgvVersiones.Columns) c.HeaderText = t["col" + c.Name]?.ToString();
            if (dgvProductos.DataSource != null)
                CargarProductos(null);
        }
    }
}
