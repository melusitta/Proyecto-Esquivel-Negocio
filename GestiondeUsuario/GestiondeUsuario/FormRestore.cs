using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    public partial class FormRestore : Form, IObservadorIdioma
    {
        public FormRestore()
        {
            InitializeComponent();
        }

        private void FormRestore_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnRestore);

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(
                SessionManager.Instancia.ObtenerIdioma());
            CargarBackups();
        }

        private void FormRestore_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private void CargarBackups()
        {
            lstBackups.Items.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Backup files (*.bak)|*.bak";
            ofd.Title = GestorIdioma.Instancia.Obtener("FormRestore", "msgSeleccionarArchivoTitle");
            if (ofd.ShowDialog() == DialogResult.OK)
                lstBackups.Items.Add(ofd.FileName);
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (lstBackups.SelectedItem == null)
            {
                MessageBox.Show(
                    g.Obtener("FormRestore", "msgSeleccionarBackup"),
                    g.Obtener("FormRestore", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult confirm = MessageBox.Show(
                g.Obtener("FormRestore", "msgConfirmarRestore"),
                g.Obtener("FormRestore", "msgConfirmar"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string rutaCompleta = lstBackups.SelectedItem.ToString();
                    BackUpRestoreBLL.Instancia.RealizarRestore(rutaCompleta);
                    MessageBox.Show(
                        g.Obtener("FormRestore", "msgRestoreOk"),
                        g.Obtener("FormRestore", "msgExito"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new Form1().Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex), GestorIdioma.Instancia.Obtener("FormRestore", "msgError"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            new FormInconsistencia().Show();
            this.Close();
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormRestore"];
            if (t == null) return;
            this.Text = t["tituloForm"]?.ToString();
            lblMensaje.Text = t["lblMensaje"]?.ToString();
            lblBackups.Text = t["lblBackups"]?.ToString();
            btnRestore.Text = t["btnRestore"]?.ToString();
            btnSalir.Text = t["btnSalir"]?.ToString();
        }
    }
}
