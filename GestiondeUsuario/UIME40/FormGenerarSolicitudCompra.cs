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
    // CUN-006 Generar solicitud de compra (Encargado de compras).
    // Incluye CU-007 Seleccionar productos stock mínimo (paso 1); desde ahí se extiende a CUN-008 Realizar pre-registro.
    // Paso 3: fija las cantidades y la prioridad, y la solicitud queda enviada a los proveedores (con su PDF).
    public partial class FormGenerarSolicitudCompra : Form, IObservadorIdioma
    {
        private List<ItemSolicitud> _items = new List<ItemSolicitud>();

        public FormGenerarSolicitudCompra()
        {
            InitializeComponent();
        }

        private void FormGenerarSolicitudCompra_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnGenerar);
            Tema.EstiloPrincipal(btnSeleccionar);
            lblResumen.ForeColor = Tema.PrincipalOscuro;
            CrearColumnasItems();

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            MostrarItems();
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

        private void MostrarItems()
        {
            dgvItems.Rows.Clear();
            foreach (ItemSolicitud i in _items)
            {
                int fila = dgvItems.Rows.Add(i.Producto.CodigoProducto, i.Producto.Nombre, i.Proveedor.RazonSocial,
                    i.Producto.Existencia, i.Producto.StockMinimo, i.Cantidad);
                dgvItems.Rows[fila].Tag = i;
            }
            int proveedores = _items.Select(i => i.Proveedor.Id).Distinct().Count();
            lblResumen.Text = _items.Count == 0 ? T("sinProductos") : string.Format(T("lblResumen"), _items.Count, proveedores);
            btnGenerar.Enabled = btnQuitar.Enabled = _items.Count > 0;
        }

        // Las cantidades editadas en la grilla pasan a los ítems (si no es número queda 0 y la BLL lo rechaza)
        private void LeerCantidades()
        {
            dgvItems.EndEdit();
            foreach (DataGridViewRow fila in dgvItems.Rows)
            {
                int cantidad;
                int.TryParse(Convert.ToString(fila.Cells["Cantidad"].Value), out cantidad);
                ((ItemSolicitud)fila.Tag).Cantidad = cantidad;
            }
        }

        // Include CU-007: elegir los productos con stock mínimo y su proveedor
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            LeerCantidades();
            using (var seleccionar = new FormSeleccionarProductosStockMinimo(_items))
            {
                if (seleccionar.ShowDialog(this) == DialogResult.OK)
                {
                    _items = seleccionar.Seleccion;
                    MostrarItems();
                }
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            DataGridViewRow fila = dgvItems.SelectedRows.Count > 0 ? dgvItems.SelectedRows[0] : dgvItems.CurrentRow;
            if (fila == null) return;
            LeerCantidades();
            _items.Remove((ItemSolicitud)fila.Tag);
            MostrarItems();
        }

        private void dgvItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        // Paso 3: genera la solicitud y queda enviada; cada proveedor recibe su PDF con sus productos
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            LeerCantidades();
            try
            {
                string prioridad = (cmbPrioridad.SelectedItem as OpcionCodigo)?.Codigo;
                SolicitudCompra s = SolicitudCompraBLL.Instancia.GenerarSolicitud(_items, prioridad);
                List<Proveedor> proveedores = SolicitudCompraBLL.Instancia.ProveedoresDe(s);
                _items = new List<ItemSolicitud>();
                MostrarItems();
                ImpresionCompras.InformarEnvio(this,
                    string.Format(T("msgEnviada"), s.NroSolicitud.ToString("D6"), string.Join(", ", proveedores.Select(p => p.RazonSocial))),
                    T("msgExito"),
                    () => proveedores.Select(p => ImpresionCompras.Solicitud(s, ProveedorBLL.Instancia.ObtenerPorId(p.Id))
                        .GuardarPdf(ImpresionCompras.NombreArchivo("archivoSolicitud", s.NroSolicitud.ToString("D6"), p.RazonSocial))).ToList());
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
            foreach (DataGridViewColumn c in dgvItems.Columns)
                c.HeaderText = T("col" + c.Name);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormGenerarSolicitudCompra"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblItems.Text = t["lblItems"]?.ToString();
            lblPrioridad.Text = t["lblPrioridad"]?.ToString();
            btnSeleccionar.Text = t["btnSeleccionar"]?.ToString();
            btnQuitar.Text = t["btnQuitar"]?.ToString();
            btnGenerar.Text = t["btnGenerar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            CargarPrioridades();
            TraducirColumnas();
            if (dgvItems.Columns.Count > 0)
                MostrarItems();
        }
    }
}
