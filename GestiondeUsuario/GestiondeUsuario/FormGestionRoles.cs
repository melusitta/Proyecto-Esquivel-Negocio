using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    public partial class FormGestionRoles : Form, IObservadorIdioma
    {
        private int _idSeleccionado = -1;
        public FormGestionRoles()
        {
            InitializeComponent();
        }

        private void FormGestionRoles_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            lblModo.ForeColor = Tema.PrincipalOscuro;

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());

            rbPatente.Checked = true;
            CargarRoles();
            CargarDisponibles();
            HabilitarBotonera();
            DeshabilitarCampos();
            lblModo.Text = GestorIdioma.Instancia.Obtener("FormGestionRoles", "lblModoInicial");
        }
        private void FormGestionRoles_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private void HabilitarBotonera()
        {
            btnNuevo.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnAplicar.Enabled = false;
            btnCancelar.Enabled = false;
        }
        private void ModoAccion()
        {
            btnNuevo.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnAplicar.Enabled = true;
            btnCancelar.Enabled = true;
        }
        private void DeshabilitarCampos()
        {
            txtNombre.Enabled = false;
            txtDescripcion.Enabled = false;
        }
        private void HabilitarCampos()
        {
            txtNombre.Enabled = true;
            txtDescripcion.Enabled = true;
        }
        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            lstContenido.Items.Clear();
        }

        private void CargarRoles()
        {
            lstRoles.Items.Clear();
            var lista = RolBLL.Instancia.ObtenerTodos();
            foreach (var r in lista)
                lstRoles.Items.Add(r);
        }
        private void CargarDisponibles()
        {
            lstDisponibles.Items.Clear();
            if (rbPatente.Checked)
            {
                var patentes = PatenteBLL.Instancia.ObtenerTodos();
                foreach (var p in patentes)
                    lstDisponibles.Items.Add(p);
            }
            else
            {
                var familias = FamiliaBLL.Instancia.ObtenerTodos();
                foreach (var f in familias)
                    lstDisponibles.Items.Add(f);
            }
        }
        private void CargarContenido(int idRol)
        {
            lstContenido.Items.Clear();
            var patentes = RolBLL.Instancia.ObtenerPatentes(idRol);
            foreach (var p in patentes)
                lstContenido.Items.Add("[P] " + p.Nombre);

            var familias = RolBLL.Instancia.ObtenerFamilias(idRol);
            foreach (var f in familias)
                lstContenido.Items.Add("[F] " + f.Nombre);
        }

        private void lstRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRoles.SelectedItem == null) return;

            var rol = (Rol)lstRoles.SelectedItem;
            _idSeleccionado = rol.Id;
            txtNombre.Text = rol.Nombre;
            txtDescripcion.Text = rol.Descripcion;
            CargarContenido(rol.Id);
            CargarDisponibles();
            lblModo.Text = GestorIdioma.Instancia.Obtener("FormGestionRoles", "lblModoSeleccionado");
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            _idSeleccionado = -1;
            LimpiarCampos();
            HabilitarCampos();
            ModoAccion();
            lblModo.Text = GestorIdioma.Instancia.Obtener("FormGestionRoles", "lblModoNuevo");
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (_idSeleccionado == -1)
            {
                MessageBox.Show(g.Obtener("FormGestionRoles", "msgSeleccionarPrimero"),
                    g.Obtener("FormGestionRoles", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            HabilitarCampos();
            ModoAccion();
            lblModo.Text = g.Obtener("FormGestionRoles", "lblModoModificar");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (_idSeleccionado == -1)
            {
                MessageBox.Show(g.Obtener("FormGestionRoles", "msgSeleccionarPrimero"),
                    g.Obtener("FormGestionRoles", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult confirm = MessageBox.Show(
                g.Obtener("FormGestionRoles", "msgConfirmarEliminar"),
                g.Obtener("FormGestionRoles", "msgConfirmar"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bool ok = RolBLL.Instancia.Eliminar(_idSeleccionado);
                    if (ok)
                    {
                        MessageBox.Show(g.Obtener("FormGestionRoles", "msgEliminadoExito"),
                            g.Obtener("FormGestionRoles", "msgExito"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        DeshabilitarCampos();
                        HabilitarBotonera();
                        _idSeleccionado = -1;
                        CargarRoles();
                        lblModo.Text = g.Obtener("FormGestionRoles", "lblModoInicial");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, g.Obtener("FormGestionRoles", "msgError"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show(g.Obtener("FormGestionRoles", "msgNombreObligatorio"),
                    g.Obtener("FormGestionRoles", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (_idSeleccionado == -1)
                {
                    Rol nuevo = new Rol { Nombre = txtNombre.Text, Descripcion = txtDescripcion.Text };
                    bool ok = RolBLL.Instancia.Insertar(nuevo);
                    if (ok)
                    {
                        MessageBox.Show(g.Obtener("FormGestionRoles", "msgCreadoExito"),
                            g.Obtener("FormGestionRoles", "msgExito"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarRoles();
                        LimpiarCampos();
                        DeshabilitarCampos();
                        HabilitarBotonera();
                        lblModo.Text = g.Obtener("FormGestionRoles", "lblModoInicial");
                    }
                }
                else
                {
                    Rol modificado = new Rol { Id = _idSeleccionado, Nombre = txtNombre.Text, Descripcion = txtDescripcion.Text };
                    bool ok = RolBLL.Instancia.Modificar(modificado);
                    if (ok)
                    {
                        MessageBox.Show(g.Obtener("FormGestionRoles", "msgModificadoExito"),
                            g.Obtener("FormGestionRoles", "msgExito"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarRoles();
                        LimpiarCampos();
                        DeshabilitarCampos();
                        HabilitarBotonera();
                        lblModo.Text = g.Obtener("FormGestionRoles", "lblModoInicial");
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                MessageBox.Show(g.Obtener("FormGestionRoles", "msgNombreRepetido"),
                    g.Obtener("FormGestionRoles", "msgError"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            DeshabilitarCampos();
            HabilitarBotonera();
            lblModo.Text = GestorIdioma.Instancia.Obtener("FormGestionRoles", "lblModoInicial");
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            new FormPrincipal().Show();
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (_idSeleccionado == -1)
            {
                MessageBox.Show(g.Obtener("FormGestionRoles", "msgSeleccionarPrimero"),
                    g.Obtener("FormGestionRoles", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (lstDisponibles.SelectedItem == null)
            {
                MessageBox.Show(g.Obtener("FormGestionRoles", "msgSeleccionarElemento"),
                    g.Obtener("FormGestionRoles", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (rbPatente.Checked)
                {
                    var patente = (Patente)lstDisponibles.SelectedItem;
                    RolBLL.Instancia.AgregarPatente(_idSeleccionado, patente.Id);
                }
                else
                {
                    var familia = (Familia)lstDisponibles.SelectedItem;
                    RolBLL.Instancia.AgregarFamilia(_idSeleccionado, familia.Id);
                }
                CargarContenido(_idSeleccionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, g.Obtener("FormGestionRoles", "msgError"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == -1 || lstContenido.SelectedItem == null) return;

            string item = lstContenido.SelectedItem.ToString();

            try
            {
                if (item.StartsWith("[P]"))
                {
                    string nombre = item.Replace("[P] ", "");
                    var patentes = RolBLL.Instancia.ObtenerPatentes(_idSeleccionado);
                    var patente = patentes.FirstOrDefault(p => p.Nombre == nombre);
                    if (patente != null)
                        RolBLL.Instancia.EliminarPatente(_idSeleccionado, patente.Id);
                }
                else
                {
                    string nombre = item.Replace("[F] ", "");
                    var familias = RolBLL.Instancia.ObtenerFamilias(_idSeleccionado);
                    var familia = familias.FirstOrDefault(f => f.Nombre == nombre);
                    if (familia != null)
                        RolBLL.Instancia.EliminarFamilia(_idSeleccionado, familia.Id);
                }
                CargarContenido(_idSeleccionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbPatente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPatente.Checked) CargarDisponibles();
        }

        private void rbFamilia_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFamilia.Checked) CargarDisponibles();
        }

        private void lstDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormGestionRoles"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["lblTitulo"]?.ToString();
            lblPatentesDisponibles.Text = t["lblPatentesDisponibles"]?.ToString();
            lblListaRoles.Text = t["lblListaRoles"]?.ToString();
            lblListaContenido.Text = t["lblListaContenido"]?.ToString();
            lblNombre.Text = t["lblNombre"]?.ToString();
            lblDescripcion.Text = t["lblDescripcion"]?.ToString();
            rbPatente.Text = t["rbPatente"]?.ToString();
            rbFamilia.Text = t["rbFamilia"]?.ToString();
            btnNuevo.Text = t["btnNuevo"]?.ToString();
            btnModificar.Text = t["btnModificar"]?.ToString();
            btnEliminar.Text = t["btnEliminar"]?.ToString();
            btnAplicar.Text = t["btnAplicar"]?.ToString();
            btnCancelar.Text = t["btnCancelar"]?.ToString();
            btnAgregar.Text = t["btnAgregar"]?.ToString();
            btnQuitar.Text = t["btnQuitar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
        }
    }
}
