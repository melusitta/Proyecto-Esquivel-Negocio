using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace GestiondeUsuario
{
    public partial class FormRecuperar : Form, IObservadorIdioma
    {
        public FormRecuperar()
        {
            InitializeComponent();
        }

        private void FormRecuperar_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnRecuperar);

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            Usuario usuarioActivo = SessionManager.Instancia.ObtenerUsuarioActivo();

            // Si es primer ingreso, ocultamos el boton volver
            if (usuarioActivo != null && usuarioActivo.PrimerIngreso)
                btnVolver.Visible = false;
            else
                btnVolver.Visible = true;
        }
        private void FormRecuperar_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private void btnRecuperar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (string.IsNullOrEmpty(txtDNI.Text) ||
                string.IsNullOrEmpty(txtNuevaPass.Text) ||
                string.IsNullOrEmpty(txtConfirmarPass.Text))
            {
                MessageBox.Show(g.Obtener("FormRecuperar", "msgCamposVacios"),
                    g.Obtener("FormRecuperar", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtNuevaPass.Text != txtConfirmarPass.Text)
            {
                MessageBox.Show(g.Obtener("FormRecuperar", "msgNoCoinciden"),
                    g.Obtener("FormRecuperar", "msgError"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Encriptador.ContraseñaSegura(txtNuevaPass.Text))
            {
                MessageBox.Show(g.Obtener("FormRecuperar", "msgContraseñaInsegura"),
                    g.Obtener("FormRecuperar", "msgContraseñaInseguraTitulo"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                bool ok = UsuarioBLL.Instancia.CambiarContraseña(
                    Convert.ToInt32(txtDNI.Text),
                    txtContraseñaActual.Text,
                    txtNuevaPass.Text);
                if (ok)
                {
                    MessageBox.Show(g.Obtener("FormRecuperar", "msgExito"),
                        g.Obtener("FormRecuperar", "msgExitoTitulo"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new Form1().Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(g.Obtener("FormRecuperar", "msgDNIIncorrecto"),
                        g.Obtener("FormRecuperar", "msgError"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex), g.Obtener("FormRecuperar", "msgError"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            new FormPrincipal().Show();
            this.Close();
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormRecuperar"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblDNI.Text = t["lblDNI"]?.ToString();
            lblContraseñaActual.Text = t["lblContraseñaActual"]?.ToString();
            lblNuevaPass.Text = t["lblNuevaPass"]?.ToString();
            lblConfirmarPass.Text = t["lblConfirmarPass"]?.ToString();
            btnRecuperar.Text = t["btnRecuperar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
        }
    }
}
