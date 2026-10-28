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
    // Paso 1: del listado de productos con stock mínimo elige los que van a la solicitud (grilla de la derecha),
    // analiza los proveedores disponibles y le asigna uno a cada producto. Si el proveedor no está,
    // se extiende a CUN-007 Realizar pre-registro (paso 2). Paso 3: con "Finalizar" se guarda la solicitud
    // y queda enviada a los proveedores, cada uno con su PDF.
    public partial class FormGenerarSolicitudCompra : Form, IObservadorIdioma
    {
        private List<Producto> _productos = new List<Producto>();
        private List<Proveedor> _proveedores = new List<Proveedor>();

        public FormGenerarSolicitudCompra()
        {
            InitializeComponent();
        }

        private void FormGenerarSolicitudCompra_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.MensajeVacio(dgvProductos, () => T("vacioProductos"));
            Tema.MensajeVacio(dgvItems, () => T("vacioItems"));
            Tema.EstiloPrincipal(btnFinalizar);
            Tema.EstiloPrincipal(btnAgregar);
            lblResumen.ForeColor = Tema.PrincipalOscuro;
            CrearColumnas();

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            CargarProveedores();
            CargarProductos();
        }

        private void FormGenerarSolicitudCompra_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormGenerarSolicitudCompra", clave);
        }

        private void CrearColumnas()
        {
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.Columns.Clear();
            foreach (var c in new[] { ("Codigo", 55f), ("Producto", 170f), ("Categoria", 90f), ("Existencia", 60f), ("StockMinimo", 60f), ("ProveedorHabitual", 140f) })
                dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { Name = c.Item1, FillWeight = c.Item2 });

            dgvItems.AutoGenerateColumns = false;
            dgvItems.Columns.Clear();
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Codigo", FillWeight = 55, ReadOnly = true });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Producto", FillWeight = 170, ReadOnly = true });
            dgvItems.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "Proveedor", FillWeight = 170, DisplayMember = "RazonSocial", ValueMember = "Id",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox, FlatStyle = FlatStyle.Flat
            });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", FillWeight = 65 });
        }

        // ---------- Productos con stock mínimo ----------

        private List<int> IdsEnSolicitud()
        {
            return dgvItems.Rows.Cast<DataGridViewRow>().Select(r => ((Producto)r.Tag).Id).ToList();
        }

        // Los que ya pasaron a la solicitud no se repiten a la izquierda
        private void CargarProductos()
        {
            try
            {
                _productos = SolicitudCompraBLL.Instancia.ObtenerProductosAReponer();
                MostrarProductos();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void MostrarProductos()
        {
            List<int> enSolicitud = IdsEnSolicitud();
            dgvProductos.Rows.Clear();
            foreach (Producto p in _productos.Where(p => !enSolicitud.Contains(p.Id)))
            {
                int fila = dgvProductos.Rows.Add(p.CodigoProducto, p.Nombre, p.Categoria, p.Existencia, p.StockMinimo, p.ProveedorHabitual ?? "");
                dgvProductos.Rows[fila].Tag = p;
            }
            dgvProductos.ClearSelection();
            MostrarResumen();
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                Agregar(new List<DataGridViewRow> { dgvProductos.Rows[e.RowIndex] });
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Agregar(dgvProductos.SelectedRows.Cast<DataGridViewRow>().OrderBy(r => r.Index).ToList());
        }

        // Pasan a la solicitud con su proveedor habitual y la cantidad sugerida; el encargado los ajusta.
        // Quedan marcados para que, si toca un proveedor de abajo, se les asigne a todos juntos.
        private void Agregar(List<DataGridViewRow> filas)
        {
            if (filas.Count == 0)
            {
                Aviso.Info(T("msgElegirProducto"));
                return;
            }
            var agregadas = new List<int>();
            foreach (DataGridViewRow r in filas)
            {
                Producto p = (Producto)r.Tag;
                int fila = dgvItems.Rows.Add(p.CodigoProducto, p.Nombre, null, SolicitudCompraBLL.CantidadSugerida(p));
                dgvItems.Rows[fila].Tag = p;
                if (p.IdProveedorHabitual.HasValue && _proveedores.Any(x => x.Id == p.IdProveedorHabitual.Value))
                    dgvItems.Rows[fila].Cells["Proveedor"].Value = p.IdProveedorHabitual.Value;
                agregadas.Add(fila);
            }
            MostrarProductos();
            dgvItems.ClearSelection();
            foreach (int fila in agregadas)
                dgvItems.Rows[fila].Selected = true;
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> filas = dgvItems.SelectedRows.Cast<DataGridViewRow>().ToList();
            if (filas.Count == 0 && dgvItems.CurrentRow != null)
                filas.Add(dgvItems.CurrentRow);
            if (filas.Count == 0) return;
            foreach (DataGridViewRow fila in filas)
                dgvItems.Rows.Remove(fila);
            MostrarProductos();
        }

        // ---------- Solicitud ----------

        private void dgvItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // El combo de proveedor se confirma al toque (si no, recién al salir de la celda)
            if (dgvItems.IsCurrentCellDirty && dgvItems.CurrentCell is DataGridViewComboBoxCell)
                dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvItems.Columns[e.ColumnIndex].Name == "Proveedor")
                MostrarResumen();
        }

        private void dgvItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        // Lo que está en la grilla de la derecha (si la cantidad no es número queda 0 y la BLL lo rechaza)
        private List<ItemSolicitud> ItemsDeLaGrilla()
        {
            dgvItems.EndEdit();
            var items = new List<ItemSolicitud>();
            foreach (DataGridViewRow fila in dgvItems.Rows)
            {
                object idProveedor = fila.Cells["Proveedor"].Value;
                int cantidad;
                int.TryParse(Convert.ToString(fila.Cells["Cantidad"].Value), out cantidad);
                items.Add(new ItemSolicitud
                {
                    Producto = (Producto)fila.Tag,
                    Proveedor = idProveedor == null ? null : _proveedores.FirstOrDefault(p => p.Id == Convert.ToInt32(idProveedor)),
                    Cantidad = cantidad
                });
            }
            return items;
        }

        private void MostrarResumen()
        {
            int productos = dgvItems.Rows.Count;
            int proveedores = dgvItems.Rows.Cast<DataGridViewRow>().Select(r => r.Cells["Proveedor"].Value)
                .Where(v => v != null).Distinct().Count();
            lblResumen.Text = productos == 0 ? T("sinProductos") : string.Format(T("lblResumen"), productos, proveedores);
            btnFinalizar.Enabled = btnQuitar.Enabled = productos > 0;
            btnAgregar.Enabled = dgvProductos.Rows.Count > 0;
        }

        // Paso 3: guarda la solicitud y queda enviada; cada proveedor recibe su PDF con sus productos
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                string prioridad = (cmbPrioridad.SelectedItem as OpcionCodigo)?.Codigo;
                SolicitudCompra s = SolicitudCompraBLL.Instancia.GenerarSolicitud(ItemsDeLaGrilla(), prioridad);
                List<Proveedor> proveedores = SolicitudCompraBLL.Instancia.ProveedoresDe(s);
                dgvItems.Rows.Clear();
                CargarProductos();
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

        // ---------- Proveedores disponibles ----------

        private void CargarProveedores()
        {
            try
            {
                _proveedores = ProveedorBLL.Instancia.ObtenerTodos();
                ((DataGridViewComboBoxColumn)dgvItems.Columns["Proveedor"]).DataSource = _proveedores.ToList();

                dgvProveedores.DataSource = null;
                dgvProveedores.DataSource = _proveedores.Select(p => new
                {
                    p.RazonSocial,
                    p.CUIT,
                    p.Contacto,
                    p.Telefono,
                    p.Rubro,
                    p.CondicionesEntrega,
                    Plazo = p.PlazoEntregaDias,
                    FormaPago = TextosCompras.FormaPago(p.FormaPago),
                    p.CondicionesComerciales,
                    Estado = TextosCompras.Estado(p.Estado)
                }).ToList();
                Tema.AnchoColumnas(dgvProveedores, ("RazonSocial", 160), ("CUIT", 100), ("Contacto", 110), ("Telefono", 115),
                    ("Rubro", 120), ("CondicionesEntrega", 160), ("Plazo", 75), ("FormaPago", 110), ("CondicionesComerciales", 150), ("Estado", 95));
                TraducirColumnas();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // Tocar un proveedor de la lista se lo asigna a los productos marcados en la solicitud
        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < _proveedores.Count)
                AsignarProveedor(_proveedores[e.RowIndex]);
        }

        private void AsignarProveedor(Proveedor proveedor)
        {
            List<DataGridViewRow> filas = dgvItems.SelectedRows.Cast<DataGridViewRow>().ToList();
            if (filas.Count == 0)
            {
                if (dgvItems.Rows.Count > 0)
                    Aviso.Info(T("msgElegirItem"));
                return;
            }
            dgvItems.EndEdit();
            foreach (DataGridViewRow fila in filas)
                fila.Cells["Proveedor"].Value = proveedor.Id;
            MostrarResumen();
            Aviso.Info(string.Format(T("msgAsignado"), proveedor.RazonSocial, filas.Count));
        }

        // 6.1 (extend CUN-007): el proveedor no está y se pre-registra; queda asignado a los productos marcados
        private void btnPreRegistrar_Click(object sender, EventArgs e)
        {
            using (var preRegistro = new FormRealizarPreRegistro())
            {
                if (preRegistro.ShowDialog(this) != DialogResult.OK) return;
                CargarProveedores();
                int indice = _proveedores.FindIndex(p => p.Id == preRegistro.IdProveedor);
                if (indice < 0) return;
                dgvProveedores.ClearSelection();
                dgvProveedores.Rows[indice].Selected = true;
                dgvProveedores.FirstDisplayedScrollingRowIndex = indice;
                AsignarProveedor(_proveedores[indice]);
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
            foreach (DataGridViewColumn c in dgvProductos.Columns) c.HeaderText = T("col" + c.Name);
            foreach (DataGridViewColumn c in dgvItems.Columns) c.HeaderText = T("col" + c.Name);
            foreach (DataGridViewColumn c in dgvProveedores.Columns) c.HeaderText = T("colProv" + c.Name);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormGenerarSolicitudCompra"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblProductos.Text = t["lblProductos"]?.ToString();
            lblItems.Text = t["lblItems"]?.ToString();
            lblProveedores.Text = t["lblProveedores"]?.ToString();
            lblPrioridad.Text = t["lblPrioridad"]?.ToString();
            btnAgregar.Text = t["btnAgregar"]?.ToString();
            btnQuitar.Text = t["btnQuitar"]?.ToString();
            btnPreRegistrar.Text = t["btnPreRegistrar"]?.ToString();
            btnFinalizar.Text = t["btnFinalizar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            CargarPrioridades();
            TraducirColumnas();
            if (dgvProveedores.DataSource != null)
                CargarProveedores();   // estados y formas de pago traducidos
            MostrarResumen();
        }
    }
}
