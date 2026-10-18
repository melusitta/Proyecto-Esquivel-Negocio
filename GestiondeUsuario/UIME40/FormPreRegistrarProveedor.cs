using BE;
using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // CUN-007 Pre-registrar proveedor (Encargado de compras). Extiende CUN-006: se abre como diálogo
    // cuando el proveedor que se quiere asignar no está. El supervisor completa después su registro (CUN-010).
    public partial class FormPreRegistrarProveedor : Form, IObservadorIdioma
    {
        public int IdProveedor { get; private set; }

        public FormPreRegistrarProveedor()
        {
            InitializeComponent();
        }

        private void FormPreRegistrarProveedor_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnPreRegistrar);
            lblAyuda.ForeColor = Tema.PrincipalOscuro;
            this.AcceptButton = btnPreRegistrar;
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            txtCUIT.Focus();
        }

        private void FormPreRegistrarProveedor_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormPreRegistrarProveedor", clave);
        }

        private void btnPreRegistrar_Click(object sender, EventArgs e)
        {
            var proveedor = new Proveedor
            {
                CUIT = txtCUIT.Text,
                RazonSocial = txtRazonSocial.Text,
                Contacto = txtContacto.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                CondicionesComerciales = txtCondiciones.Text
            };
            try
            {
                IdProveedor = ProveedorBLL.Instancia.PreRegistrar(proveedor);
                MessageBox.Show(string.Format(T("msgPreRegistrado"), proveedor.RazonSocial),
                    T("msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex), T("msgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormPreRegistrarProveedor"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblCUIT.Text = t["lblCUIT"]?.ToString();
            lblRazonSocial.Text = t["lblRazonSocial"]?.ToString();
            lblContacto.Text = t["lblContacto"]?.ToString();
            lblTelefono.Text = t["lblTelefono"]?.ToString();
            lblEmail.Text = t["lblEmail"]?.ToString();
            lblCondiciones.Text = t["lblCondiciones"]?.ToString();
            lblAyuda.Text = t["lblAyuda"]?.ToString();
            btnPreRegistrar.Text = t["btnPreRegistrar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
        }
    }
}
