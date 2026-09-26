using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    public partial class FormInconsistencia : Form, IObservadorIdioma
    {
        public FormInconsistencia()
        {
            InitializeComponent();
        }

        private void FormInconsistencia_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnRecalcular);

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(
                SessionManager.Instancia.ObtenerIdioma());
        }
        private void FormInconsistencia_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private void btnRecalcular_Click(object sender, EventArgs e)
        {
            try
            {
                DigitoVerificadorBLL.Instancia.RecalcularYGuardar();

                // Registramos en bitacora
                GestorEventosBLL.Instancia.Notificar(
                    SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                    "Recálculo de dígito verificador",
                    "Administrador", 5);

                MessageBox.Show(
                    GestorIdioma.Instancia.Obtener("FormInconsistencia", "msgRecalculadoOk"),
                    GestorIdioma.Instancia.Obtener("FormInconsistencia", "tituloForm"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                new Form1().Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex), GestorIdioma.Instancia.Obtener("FormInconsistencia", "msgError"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            new FormRestore().Show();
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormInconsistencia"];
            if (t == null) return;
            this.Text = t["tituloForm"]?.ToString();
            lblMensaje.Text = t["lblMensaje"]?.ToString();
            btnRecalcular.Text = t["btnRecalcular"]?.ToString();
            btnRestore.Text = t["btnRestore"]?.ToString();
            btnSalir.Text = t["btnSalir"]?.ToString();
        }
    }
}
