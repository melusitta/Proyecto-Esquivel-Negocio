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
    // CU-007 Seleccionar productos stock mínimo (Encargado de compras). Lo incluye CUN-006 Generar solicitud de compra,
    // que lo abre como diálogo. Paso 1: elige los productos con stock mínimo, analiza los proveedores y le asigna
    // uno a cada producto. Si el proveedor no está, se extiende a CUN-008 Realizar pre-registro (paso 2).
    public partial class FormSeleccionarProductosStockMinimo : Form, IObservadorIdioma
    {
        private List<Proveedor> _proveedores = new List<Proveedor>();
        private readonly List<ItemSolicitud> _yaElegidos;

        // Lo seleccionado y validado, para la solicitud (con DialogResult.OK)
        public List<ItemSolicitud> Seleccion { get; private set; }

        public FormSeleccionarProductosStockMinimo() : this(new List<ItemSolicitud>())
        {
        }

        public FormSeleccionarProductosStockMinimo(List<ItemSolicitud> yaElegidos)
        {
            InitializeComponent();
            _yaElegidos = yaElegidos ?? new List<ItemSolicitud>();
        }

        private void FormSeleccionarProductosStockMinimo_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnAgregar);
            lblSeleccion.ForeColor = Tema.PrincipalOscuro;
            CrearColumnas();

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            CargarProveedores();
            CargarProductos();
        }

        private void FormSeleccionarProductosStockMinimo_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormSeleccionarProductosStockMinimo", clave);
        }

        // ---------- Productos con stock mínimo ----------

        private void CrearColumnas()
        {
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.Columns.Clear();
            dgvProductos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Reponer", FillWeight = 60 });
            AgregarColumnaTexto("Codigo", 50);
            AgregarColumnaTexto("Producto", 170);
            AgregarColumnaTexto("Categoria", 80);
            AgregarColumnaTexto("Existencia", 60);
            AgregarColumnaTexto("StockMinimo", 60);
            AgregarColumnaTexto("ProveedorHabitual", 120);
            dgvProductos.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "Proveedor", FillWeight = 150, DisplayMember = "RazonSocial", ValueMember = "Id",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox, FlatStyle = FlatStyle.Flat
            });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", FillWeight = 60 });
        }

        private void AgregarColumnaTexto(string nombre, float peso)
        {
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { Name = nombre, FillWeight = peso, ReadOnly = true });
        }

        private void CargarProductos()
        {
            try
            {
                dgvProductos.Rows.Clear();
                foreach (Producto p in SolicitudCompraBLL.Instancia.ObtenerProductosAReponer())
                {
                    // Lo que ya estaba en la solicitud vuelve marcado, con su proveedor y cantidad
                    ItemSolicitud elegido = _yaElegidos.FirstOrDefault(i => i.Producto.Id == p.Id);
                    int fila = dgvProductos.Rows.Add(elegido != null, p.CodigoProducto, p.Nombre, p.Categoria, p.Existencia,
                        p.StockMinimo, p.ProveedorHabitual ?? "", null, elegido?.Cantidad ?? SolicitudCompraBLL.CantidadSugerida(p));
                    DataGridViewRow row = dgvProductos.Rows[fila];
                    row.Tag = p;
                    // Por defecto se le asigna su proveedor habitual
                    int? idProveedor = elegido?.Proveedor?.Id ?? p.IdProveedorHabitual;
                    if (idProveedor.HasValue && _proveedores.Any(x => x.Id == idProveedor.Value))
                        row.Cells["Proveedor"].Value = idProveedor.Value;
                }
                MostrarSeleccion();
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
                ((DataGridViewComboBoxColumn)dgvProductos.Columns["Proveedor"]).DataSource = _proveedores.ToList();

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
                Tema.AnchoColumnas(dgvProveedores, ("RazonSocial", 150), ("CUIT", 95), ("Contacto", 100), ("Telefono", 105),
                    ("Rubro", 110), ("CondicionesEntrega", 140), ("Plazo", 75), ("FormaPago", 105), ("CondicionesComerciales", 140), ("Estado", 90));
                TraducirColumnas();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ---------- Selección ----------

        private void dgvProductos_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // El tilde y el combo se confirman al toque (si no, recién al salir de la celda)
            if (dgvProductos.IsCurrentCellDirty && !(dgvProductos.CurrentCell is DataGridViewTextBoxCell))
                dgvProductos.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvProductos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProductos.Columns[e.ColumnIndex].Name == "Reponer")
                MostrarSeleccion();
        }

        private void dgvProductos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private List<DataGridViewRow> FilasMarcadas()
        {
            return dgvProductos.Rows.Cast<DataGridViewRow>().Where(r => Convert.ToBoolean(r.Cells["Reponer"].Value)).ToList();
        }

        private void MostrarSeleccion()
        {
            lblSeleccion.Text = string.Format(T("lblSeleccion"), FilasMarcadas().Count, dgvProductos.Rows.Count);
        }

        // La BLL revisa la selección; si está bien vuelve a la solicitud
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            dgvProductos.EndEdit();
            var items = new List<ItemSolicitud>();
            foreach (DataGridViewRow fila in FilasMarcadas())
            {
                object idProveedor = fila.Cells["Proveedor"].Value;
                int cantidad;
                int.TryParse(Convert.ToString(fila.Cells["Cantidad"].Value), out cantidad);   // si no es un número, la BLL lo rechaza
                items.Add(new ItemSolicitud
                {
                    Producto = (Producto)fila.Tag,
                    Proveedor = idProveedor == null ? null : _proveedores.FirstOrDefault(p => p.Id == Convert.ToInt32(idProveedor)),
                    Cantidad = cantidad
                });
            }
            try
            {
                Seleccion = SolicitudCompraBLL.Instancia.ValidarSeleccion(items);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // Paso 2 (extend): realizar el pre-registro de un proveedor que no está
        private void btnRealizarPreRegistro_Click(object sender, EventArgs e)
        {
            using (var preRegistro = new FormRealizarPreRegistro())
            {
                if (preRegistro.ShowDialog(this) == DialogResult.OK)
                    CargarProveedores();
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex), T("msgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ---------- Idioma ----------

        private void TraducirColumnas()
        {
            foreach (DataGridViewColumn c in dgvProductos.Columns)
                c.HeaderText = T("col" + c.Name);
            foreach (DataGridViewColumn c in dgvProveedores.Columns)
                c.HeaderText = T("colProv" + c.Name);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormSeleccionarProductosStockMinimo"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblProductos.Text = t["lblProductos"]?.ToString();
            lblProveedores.Text = t["lblProveedores"]?.ToString();
            btnRealizarPreRegistro.Text = t["btnRealizarPreRegistro"]?.ToString();
            btnAgregar.Text = t["btnAgregar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            TraducirColumnas();
            if (dgvProveedores.DataSource != null)
                CargarProveedores();   // estados y formas de pago traducidos
            MostrarSeleccion();
        }
    }
}
