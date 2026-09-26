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
using System.Xml.Linq;

namespace GestiondeUsuario
{
    public partial class Form1 : Form
    {
        private JObject _traducciones;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            pnlMarca.BackColor = Tema.Principal;
            picLogo.Image = Tema.Logo;
            lblBienvenida.ForeColor = Tema.PrincipalOscuro;

            // Cargamos los idiomas disponibles
            cmbIdioma.Items.Clear();
            cmbIdioma.Items.Add("español");
            cmbIdioma.Items.Add("ingles");
            cmbIdioma.Items.Add("coreano");

            // Por defecto español
            cmbIdioma.SelectedItem = "español";
            CargarIdioma("español");
        }

        private void CargarIdioma(string idioma)
        {
            try
            {
                string ruta = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Idiomas", idioma + ".json");

                if (!File.Exists(ruta)) return;

                string json = File.ReadAllText(ruta);
                _traducciones = JObject.Parse(json);
                AplicarIdioma();
            }
            catch { }
        }

        private void AplicarIdioma()
        {
            var t = _traducciones?["Form1"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblUsuario.Text = t["lblUsuario"]?.ToString();
            lblContraseña.Text = t["lblContraseña"]?.ToString();
            btnLogin.Text = t["btnLogin"]?.ToString();
            lblIdioma.Text = t["lblIdioma"]?.ToString();
            lblBienvenida.Text = t["lblBienvenida"]?.ToString();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtNombreUsuario.Text) || string.IsNullOrEmpty(txtContraseña.Text))
            {
                MessageBox.Show(
                    _traducciones?["Form1"]?["msgCamposVacios"]?.ToString() ?? "Completá todos los campos.",
                    _traducciones?["Form1"]?["msgAtencion"]?.ToString() ?? "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                bool acceso = UsuarioBLL.Instancia.Login(txtNombreUsuario.Text, txtContraseña.Text);
                if (acceso)
                {
                    Usuario usuarioActivo = SessionManager.Instancia.ObtenerUsuarioActivo();
                    if (usuarioActivo.PrimerIngreso)
                    {
                        MessageBox.Show(
                            _traducciones?["Form1"]?["msgPrimerIngreso"]?.ToString(),
                            _traducciones?["Form1"]?["msgPrimerIngresoTitulo"]?.ToString(),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        new FormRecuperar().Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show(
                            _traducciones?["Form1"]?["msgBienvenido"]?.ToString(),
                            _traducciones?["Form1"]?["msgExito"]?.ToString(),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        new FormPrincipal().Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show(
                        _traducciones?["Form1"]?["msgCredencialesIncorrectas"]?.ToString(),
                        _traducciones?["Form1"]?["msgError"]?.ToString(),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "INCONSISTENCIA_DV")
                {
                    new FormInconsistencia().Show();
                    this.Hide();
                }
                else if (ex.Message == "SISTEMA_NO_DISPONIBLE")
                {
                    MessageBox.Show(
                        _traducciones?["Form1"]?["msgSistemaNoDisponible"]?.ToString()
                            ?? "Sistema no disponible en este momento.",
                        _traducciones?["Form1"]?["msgAtencion"]?.ToString()
                            ?? "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        ex.Message + "\n" + _traducciones?["Form1"]?["msgCuentaBloqueada"]?.ToString(),
                        _traducciones?["Form1"]?["msgCuentaBloqueadaTitulo"]?.ToString(),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            
        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblRecuperar_Click(object sender, EventArgs e)
        {
            new FormRecuperar().Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbIdioma_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbIdioma.SelectedItem == null) return;
            string idioma = cmbIdioma.SelectedItem.ToString();
            CargarIdioma(idioma);

            SessionManager.Instancia.CambiarIdioma(idioma);
        }
    }
}
