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

namespace GestiondeUsuario
{
    public partial class FormCambiarIdioma : Form, IObservadorIdioma
    {
        public FormCambiarIdioma()
        {
            InitializeComponent();
        }

        private void FormCambiarIdioma_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloTitulo(lblIdiomas);

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());

            cmbIdiomas.Items.Clear();
            cmbIdiomas.Items.Add("español");
            cmbIdiomas.Items.Add("ingles");
            cmbIdiomas.Items.Add("coreano");
            cmbIdiomas.SelectedItem = SessionManager.Instancia.ObtenerIdioma();
        }
        private void FormCambiarIdioma_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (cmbIdiomas.SelectedItem == null)
            {
                MessageBox.Show(g.Obtener("FormCambiarIdioma", "msgSeleccionarIdioma"),
                g.Obtener("FormCambiarIdioma", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string idioma = cmbIdiomas.SelectedItem.ToString();
            if (idioma == SessionManager.Instancia.ObtenerIdioma())
            {
                MessageBox.Show(g.Obtener("FormCambiarIdioma", "msgMismoIdioma"),
                g.Obtener("FormCambiarIdioma", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            GestorIdioma.Instancia.CambiarIdioma(idioma);

            var usuario = SessionManager.Instancia.ObtenerUsuarioActivo();
            UsuarioBLL.Instancia.ActualizarIdioma(usuario.Id, idioma);

            // Registramos en bitácora
            GestorEventosBLL.Instancia.Notificar(
            SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Desconocido",
            "Cambiar Idioma",  // sin concatenar el idioma
            "Usuarios",
            4);

            MessageBox.Show(g.Obtener("FormCambiarIdioma", "msgExito") + idioma,
             g.Obtener("FormCambiarIdioma", "msgExitoTitulo"),
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            new FormPrincipal().Show();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cmbIdiomas.SelectedItem = SessionManager.Instancia.ObtenerIdioma();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            new FormPrincipal().Show();
            this.Close();
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormCambiarIdioma"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblIdiomas.Text = t["lblIdiomas"]?.ToString();
            btnAceptar.Text = t["btnAceptar"]?.ToString();
            btnCancelar.Text = t["btnCancelar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
        }
    }
}
