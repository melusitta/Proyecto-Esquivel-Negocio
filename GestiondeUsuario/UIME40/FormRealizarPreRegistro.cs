using BE;
using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // CUN-008 Realizar pre-registro (Encargado de compras). Extiende CUN-006 Generar solicitud de compra:
    // se abre como diálogo desde CU-007 cuando el proveedor que se quiere asignar no está.
    // El supervisor completa después su registro (CUN-010).
    public partial class FormRealizarPreRegistro : Form, IObservadorIdioma
    {
        public int IdProveedor { get; private set; }

        public FormRealizarPreRegistro()
        {
            InitializeComponent();
        }

        private void FormRealizarPreRegistro_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnRealizarPreRegistro);
            lblAyuda.ForeColor = Tema.PrincipalOscuro;
            this.AcceptButton = btnRealizarPreRegistro;
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            txtCUIT.Focus();
        }

        private void FormRealizarPreRegistro_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormRealizarPreRegistro", clave);
        }

        private void btnRealizarPreRegistro_Click(object sender, EventArgs e)
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
            var t = traducciones["FormRealizarPreRegistro"];
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
            btnRealizarPreRegistro.Text = t["btnRealizarPreRegistro"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
        }
    }
}
