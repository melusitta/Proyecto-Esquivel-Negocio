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
    // CUN-009 Completar registro proveedor (Supervisor). Extiende CUN-008 Generar orden de compra:
    // se abre con "Registrar" cuando el proveedor de la cotización no tiene sus datos bancarios.
    // Paso 6: carga CBU y banco, ve el proveedor actualizado en la grilla y cierra la pantalla.
    public partial class FormCompletarRegistroProveedor : Form, IObservadorIdioma
    {
        private readonly int? _idInicial;
        private List<Proveedor> _proveedores = new List<Proveedor>();

        public FormCompletarRegistroProveedor()
        {
            InitializeComponent();
        }

        public FormCompletarRegistroProveedor(int idProveedor) : this()
        {
            _idInicial = idProveedor;
        }

        private void FormCompletarRegistroProveedor_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnAplicar);
            lblAyuda.ForeColor = Tema.PrincipalOscuro;
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
                _proveedores = ProveedorBLL.Instancia.ObtenerTodos();
                dgvProveedores.SelectionChanged -= dgvProveedores_SelectionChanged;
                dgvProveedores.DataSource = null;
                dgvProveedores.DataSource = _proveedores.Select(p => new
                {
                    p.Id,
                    p.RazonSocial,
                    p.CUIT,
                    p.Contacto,
                    Estado = TextosCompras.Estado(p.Estado),
                    p.Banco,
                    p.CBU
                }).ToList();
                dgvProveedores.Columns["Id"].Visible = false;
                Tema.AnchoColumnas(dgvProveedores, ("RazonSocial", 190), ("CUIT", 110), ("Contacto", 130), ("Estado", 100), ("Banco", 150), ("CBU", 200));
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
            lblDatos.Text = p == null ? "" : string.Format(T("lblDatos"), p.RazonSocial);
            txtCBU.Text = p?.CBU ?? "";
            txtBanco.Text = p?.Banco ?? "";
            btnAplicar.Enabled = p != null;
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            Proveedor p = Seleccionado();
            if (p == null) return;
            try
            {
                ProveedorBLL.Instancia.CompletarRegistro(p.Id, txtCBU.Text, txtBanco.Text);
                Aviso.Exito(string.Format(T("msgRegistrado"), p.RazonSocial));
                CargarProveedores(p.Id);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // Vuelve a CUN-008 (OK si el proveedor de la cotización ya quedó registrado)
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Proveedor inicial = _proveedores.FirstOrDefault(p => p.Id == _idInicial);
            this.DialogResult = inicial != null && inicial.EstaRegistrado ? DialogResult.OK : DialogResult.Cancel;
            this.Close();
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex), T("msgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ---------- Idioma ----------

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
            lblCBU.Text = t["lblCBU"]?.ToString();
            lblBanco.Text = t["lblBanco"]?.ToString();
            lblAyuda.Text = t["lblAyuda"]?.ToString();
            btnAplicar.Text = t["btnAplicar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            TraducirColumnas();
            if (dgvProveedores.DataSource != null)
                CargarProveedores(Seleccionado()?.Id);
        }
    }
}
