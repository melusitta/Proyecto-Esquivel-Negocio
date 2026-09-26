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
    public partial class FormGestionRespaldo : Form, IObservadorIdioma
    {
        public FormGestionRespaldo()
        {
            InitializeComponent();
        }

        private void BackUp_Click(object sender, EventArgs e)
        {

            var g = GestorIdioma.Instancia;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = g.Obtener("FormGestionRespaldo", "fbd_descripcion");

            // Iniciamos en AppData para evitar problemas de permisos
            fbd.SelectedPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "GestionUsuarios", "Backups");

            if (!Directory.Exists(fbd.SelectedPath))
                Directory.CreateDirectory(fbd.SelectedPath);
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BackUpRestoreBLL.Instancia.RealizarBackUp(fbd.SelectedPath);
                    MessageBox.Show(
                        GestorIdioma.Instancia.Obtener("FormGestionRespaldo", "msgBackupOk"),
                        GestorIdioma.Instancia.Obtener("FormGestionRespaldo", "msgExito"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        GestorIdioma.Instancia.Obtener("FormGestionRespaldo", "msgErrorBackup") + GestorIdioma.Instancia.TraducirError(ex),
                        GestorIdioma.Instancia.Obtener("FormGestionRespaldo", "msgError"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Restore_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Backup files (*.bak)|*.bak";
            ofd.Title = GestorIdioma.Instancia.Obtener("FormGestionRespaldo", "msgSeleccionarArchivo");

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DialogResult confirm = MessageBox.Show(
                    GestorIdioma.Instancia.Obtener("FormGestionRespaldo", "msgConfirmarRestore"),
                    GestorIdioma.Instancia.Obtener("FormGestionRespaldo", "msgConfirmar"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        BackUpRestoreBLL.Instancia.RealizarRestore(ofd.FileName);
                        MessageBox.Show(
                            GestorIdioma.Instancia.Obtener("FormGestionRespaldo", "msgRestoreOk"),
                            GestorIdioma.Instancia.Obtener("FormGestionRespaldo", "msgExito"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        new Form1().Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            GestorIdioma.Instancia.Obtener("FormGestionRespaldo", "msgErrorRestore") + GestorIdioma.Instancia.TraducirError(ex),
                            GestorIdioma.Instancia.Obtener("FormGestionRespaldo", "msgError"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Volver_Click(object sender, EventArgs e)
        {
            new FormPrincipal().Show();
            this.Close();
        }
        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormGestionRespaldo"];
            if (t == null) return;
            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["lblTitulo"]?.ToString();
            BackUp.Text = t["Backup"]?.ToString();
            Restore.Text = t["Restore"]?.ToString();
            Volver.Text = t["Volver"]?.ToString();
        }

        private void FormGestionRespaldo_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
        }
        private void FormGestionRespaldo_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }
    }
}
