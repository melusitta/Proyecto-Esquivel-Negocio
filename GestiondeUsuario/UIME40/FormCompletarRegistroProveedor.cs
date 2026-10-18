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
    // CUN-010 Completar registro de proveedor (Supervisor).
    // Paso 6: completa los datos comerciales de un proveedor pre-registrado y queda Registrado.
    // Se abre desde el menú Compras o como diálogo desde CUN-009 Generar orden de compra (extend).
    public partial class FormCompletarRegistroProveedor : Form, IObservadorIdioma
    {
        private readonly int? _idInicial;
        private readonly bool _desdeOrden;
        private List<Proveedor> _proveedores = new List<Proveedor>();

        public FormCompletarRegistroProveedor()
        {
            InitializeComponent();
        }

        public FormCompletarRegistroProveedor(int idProveedor) : this()
        {
            _idInicial = idProveedor;
            _desdeOrden = true;
        }

        private void FormCompletarRegistroProveedor_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnCompletar);
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            CargarProveedores(_idInicial);
        }

        private void FormCompletarRegistroProveedor_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormCompletarRegistroProveedor", clave);
        }

        private void CargarProveedores(int? seleccionar)
        {
            try
            {
                _proveedores = ProveedorBLL.Instancia.ObtenerTodos()
                    .OrderBy(p => p.EstaRegistrado ? 1 : 0).ThenBy(p => p.RazonSocial).ToList();
                dgvProveedores.SelectionChanged -= dgvProveedores_SelectionChanged;
                dgvProveedores.DataSource = null;
                dgvProveedores.DataSource = _proveedores.Select(p => new
                {
                    p.Id,
                    p.RazonSocial,
                    p.CUIT,
                    Estado = TextosCompras.Estado(p.Estado)
                }).ToList();
                dgvProveedores.Columns["Id"].Visible = false;
                Tema.AnchoColumnas(dgvProveedores, ("RazonSocial", 180), ("CUIT", 110), ("Estado", 100));
                TraducirColumnas();
                foreach (DataGridViewRow fila in dgvProveedores.Rows)
                    if (seleccionar.HasValue && Convert.ToInt32(fila.Cells["Id"].Value) == seleccionar.Value)
                    {
                        fila.Selected = true;
                        dgvProveedores.CurrentCell = fila.Cells["RazonSocial"];
                    }
                dgvProveedores.SelectionChanged += dgvProveedores_SelectionChanged;
                MostrarProveedor();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private Proveedor Seleccionado()
        {
            DataGridViewRow fila = dgvProveedores.SelectedRows.Count > 0 ? dgvProveedores.SelectedRows[0] : dgvProveedores.CurrentRow;
            if (fila == null) return null;
            int id = Convert.ToInt32(fila.Cells["Id"].Value);
            return _proveedores.FirstOrDefault(p => p.Id == id);
        }

        private void dgvProveedores_SelectionChanged(object sender, EventArgs e)
        {
            MostrarProveedor();
        }

        private void MostrarProveedor()
        {
            Proveedor p = Seleccionado();
            btnCompletar.Enabled = p != null;
            txtCUIT.Text = p?.CUIT ?? "";
            txtRazonSocial.Text = p?.RazonSocial ?? "";
            txtContacto.Text = p?.Contacto ?? "";
            txtTelefono.Text = p?.Telefono ?? "";
            txtEmail.Text = p?.Email ?? "";
            txtCondiciones.Text = p?.CondicionesComerciales ?? "";
            txtRubro.Text = p?.Rubro ?? "";
            txtEntrega.Text = p?.CondicionesEntrega ?? "";
            nudPlazo.Value = Math.Min(p?.PlazoEntregaDias ?? 0, nudPlazo.Maximum);
            int indice = Array.IndexOf(ProveedorBLL.FormasPago, p?.FormaPago);
            cmbFormaPago.SelectedIndex = indice >= 0 ? indice : -1;
            btnCompletar.Text = p != null && p.EstaRegistrado ? T("btnActualizar") : T("btnCompletar");
        }

        private void btnCompletar_Click(object sender, EventArgs e)
        {
            Proveedor p = Seleccionado();
            if (p == null) return;
            var datos = new Proveedor
            {
                Id = p.Id,
                CUIT = txtCUIT.Text,
                RazonSocial = txtRazonSocial.Text,
                Contacto = txtContacto.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                CondicionesComerciales = txtCondiciones.Text,
                Rubro = txtRubro.Text,
                CondicionesEntrega = txtEntrega.Text,
                PlazoEntregaDias = (int)nudPlazo.Value,
                FormaPago = (cmbFormaPago.SelectedItem as OpcionCodigo)?.Codigo
            };
            try
            {
                ProveedorBLL.Instancia.CompletarRegistro(datos);
                MessageBox.Show(string.Format(T("msgRegistrado"), datos.RazonSocial), T("msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (_desdeOrden)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                CargarProveedores(p.Id);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            if (_desdeOrden)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }
            new FormPrincipal().Show();
            this.Close();
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex), T("msgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ---------- Idioma ----------

        private void CargarFormasPago()
        {
            int elegida = cmbFormaPago.SelectedIndex;
            cmbFormaPago.Items.Clear();
            foreach (string fp in ProveedorBLL.FormasPago)
                cmbFormaPago.Items.Add(new OpcionCodigo { Codigo = fp, Texto = TextosCompras.FormaPago(fp) });
            cmbFormaPago.SelectedIndex = elegida;
        }

        private void TraducirColumnas()
        {
            foreach (DataGridViewColumn c in dgvProveedores.Columns)
                c.HeaderText = T("col" + c.Name);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormCompletarRegistroProveedor"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblProveedores.Text = t["lblProveedores"]?.ToString();
            lblDatos.Text = t["lblDatos"]?.ToString();
            foreach (string campo in new[] { "CUIT", "RazonSocial", "Contacto", "Telefono", "Email", "Condiciones", "Rubro", "Entrega", "Plazo", "FormaPago" })
                Controls.Find("lbl" + campo, true)[0].Text = t["lbl" + campo]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            CargarFormasPago();
            TraducirColumnas();
            if (dgvProveedores.DataSource != null)
                CargarProveedores(Seleccionado()?.Id);
        }
    }
}
