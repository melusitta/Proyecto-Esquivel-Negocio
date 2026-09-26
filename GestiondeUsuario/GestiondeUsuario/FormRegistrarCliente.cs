using BE;
using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // CUN-004 Registrar cliente. Extiende CUN-003 Generar factura, que lo abre como diálogo
    // con el DNI que buscó el cajero.
    public partial class FormRegistrarCliente : Form, IObservadorIdioma
    {
        private readonly int _dni;

        public FormRegistrarCliente()
        {
            InitializeComponent();
        }

        public FormRegistrarCliente(int dni) : this()
        {
            _dni = dni;
        }

        private void FormRegistrarCliente_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnRegistrar);

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            txtDNI.Text = _dni > 0 ? _dni.ToString() : "";
            txtNombre.Focus();
        }

        private void FormRegistrarCliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        // Paso 8: el cliente entrega sus datos y el cajero lo registra
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            try
            {
                var cliente = new Cliente
                {
                    DNI = _dni,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Telefono = txtTelefono.Text,
                    Email = txtEmail.Text,
                    Direccion = txtDireccion.Text,
                    Localidad = txtLocalidad.Text,
                    CodigoPostal = txtCodigoPostal.Text
                };
                ClienteBLL.Instancia.Registrar(cliente);
                MessageBox.Show(g.Obtener("FormRegistrarCliente", "msgClienteRegistrado"),
                    g.Obtener("FormRegistrarCliente", "msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(g.TraducirError(ex), g.Obtener("FormRegistrarCliente", "msgError"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormRegistrarCliente"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblDNI.Text = t["lblDNI"]?.ToString();
            lblNombre.Text = t["lblNombre"]?.ToString();
            lblApellido.Text = t["lblApellido"]?.ToString();
            lblTelefono.Text = t["lblTelefono"]?.ToString();
            lblEmail.Text = t["lblEmail"]?.ToString();
            lblDireccion.Text = t["lblDireccion"]?.ToString();
            lblLocalidad.Text = t["lblLocalidad"]?.ToString();
            lblCodigoPostal.Text = t["lblCodigoPostal"]?.ToString();
            btnRegistrar.Text = t["btnRegistrar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
        }
    }
}
