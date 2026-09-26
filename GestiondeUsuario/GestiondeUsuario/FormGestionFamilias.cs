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
    public partial class FormGestionFamilias : Form, IObservadorIdioma
    {
        private int _idSeleccionado = -1;
        public FormGestionFamilias()
        {
            InitializeComponent();
        }

        private void FormGestionFamilias_Load(object sender, EventArgs e)
        {
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());

            rbPatente.Checked = true;
            CargarFamilias();
            CargarDisponibles();
            HabilitarBotonera();
            DeshabilitarCampos();
            lblModo.Text = GestorIdioma.Instancia.Obtener("FormGestionFamilias", "lblModoInicial");
        }
        private void FormGestionFamilias_FormClosed(object sender, FormClosedEventArgs e)
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

        private void CargarFamilias()
        {
            lstFamilias.Items.Clear();
            var lista = FamiliaBLL.Instancia.ObtenerTodos();
            foreach (var f in lista)
                lstFamilias.Items.Add(f);
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
                {
                    // No mostramos la familia seleccionada para evitar que se agregue a sí misma
                    if (_idSeleccionado != -1 && f.Id == _idSeleccionado) continue;
                    lstDisponibles.Items.Add(f);
                }
            }
        }
        private void CargarContenido(int idFamilia)
        {
            lstContenido.Items.Clear();
            var patentes = FamiliaBLL.Instancia.ObtenerPatentes(idFamilia);
            foreach (var p in patentes)
                lstContenido.Items.Add("[P] " + p.Nombre);

            var familias = FamiliaBLL.Instancia.ObtenerFamiliasIntegradas(idFamilia);
            foreach (var f in familias)
                lstContenido.Items.Add("[F] " + f.Nombre);
        }

        private void lstFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFamilias.SelectedItem == null) return;

            var familia = (Familia)lstFamilias.SelectedItem;
            _idSeleccionado = familia.Id;
            txtNombre.Text = familia.Nombre;
            txtDescripcion.Text = familia.Descripcion;
            CargarContenido(familia.Id);
            CargarDisponibles();
            lblModo.Text = GestorIdioma.Instancia.Obtener("FormGestionFamilias", "lblModoSeleccionado");
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            _idSeleccionado = -1;
            LimpiarCampos();
            HabilitarCampos();
            ModoAccion();
            lblModo.Text = GestorIdioma.Instancia.Obtener("FormGestionFamilias", "lblModoNuevo");
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (_idSeleccionado == -1)
            {
                MessageBox.Show(g.Obtener("FormGestionFamilias", "msgSeleccionarPrimero"),
                g.Obtener("FormGestionFamilias", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            HabilitarCampos();
            ModoAccion();
            lblModo.Text = GestorIdioma.Instancia.Obtener("FormGestionFamilias", "lblModoModificar");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (_idSeleccionado == -1)
            {
                MessageBox.Show(g.Obtener("FormGestionFamilias", "msgSeleccionarPrimero"),
                g.Obtener("FormGestionFamilias", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(g.Obtener("FormGestionFamilias", "msgConfirmarEliminar"),
            g.Obtener("FormGestionFamilias", "msgConfirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bool ok = FamiliaBLL.Instancia.Eliminar(_idSeleccionado);
                    if (ok)
                    {
                        MessageBox.Show(g.Obtener("FormGestionFamilias", "msgEliminadaExito"),
                        g.Obtener("FormGestionFamilias", "msgExito"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        DeshabilitarCampos();
                        HabilitarBotonera();
                        _idSeleccionado = -1;
                        CargarFamilias();
                        lblModo.Text = GestorIdioma.Instancia.Obtener("FormGestionFamilias", "lblModoInicial");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show(g.Obtener("FormGestionFamilias", "msgNombreObligatorio"),
                    g.Obtener("FormGestionFamilias", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (_idSeleccionado == -1)
                {
                    Familia nueva = new Familia { Nombre = txtNombre.Text, Descripcion = txtDescripcion.Text };
                    bool ok = FamiliaBLL.Instancia.Insertar(nueva);
                    if (ok)
                    {
                        MessageBox.Show(g.Obtener("FormGestionFamilias", "msgCreadoExito"),
                            g.Obtener("FormGestionFamilias", "msgExito"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarFamilias();
                        LimpiarCampos();
                        DeshabilitarCampos();
                        HabilitarBotonera();
                    }
                }
                else
            {
                // MODO MODIFICAR
                Familia modificada = new Familia
                {
                    Id = _idSeleccionado,
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text
                };

                bool ok = FamiliaBLL.Instancia.Modificar(modificada);
                if (ok)
                {
                    MessageBox.Show(g.Obtener("FormGestionFamilias", "msgModificadaExito"),
                    g.Obtener("FormGestionFamilias", "msgExito"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarFamilias();
                    LimpiarCampos();
                    DeshabilitarCampos();
                    HabilitarBotonera();
                    lblModo.Text = GestorIdioma.Instancia.Obtener("FormGestionFamilias", "lblModoInicial");
                }
            }
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                
                MessageBox.Show(g.Obtener("FormGestionFamilias", "msgNombreRepetido"),
                    g.Obtener("FormGestionFamilias", "msgError"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, g.Obtener("FormGestionFamilias", "msgError"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            DeshabilitarCampos();
            HabilitarBotonera();
            lblModo.Text = GestorIdioma.Instancia.Obtener("FormGestionFamilias", "lblModoInicial");
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
                MessageBox.Show(g.Obtener("FormGestionFamilias", "msgSeleccionarPrimero"),
                g.Obtener("FormGestionFamilias", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lstDisponibles.SelectedItem == null)
            {
                MessageBox.Show(g.Obtener("FormGestionFamilias", "msgSeleccionarElemento"),
                g.Obtener("FormGestionFamilias", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (rbPatente.Checked)
                {
                    var patente = (Patente)lstDisponibles.SelectedItem;
                    FamiliaBLL.Instancia.AgregarPatente(_idSeleccionado, patente.Id);
                }
                else
                {
                    var familia = (Familia)lstDisponibles.SelectedItem;
                    FamiliaBLL.Instancia.AgregarFamilia(_idSeleccionado, familia.Id);
                }
                CargarContenido(_idSeleccionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, g.Obtener("FormGestionFamilias", "msgError"),
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
                    var patentes = FamiliaBLL.Instancia.ObtenerPatentes(_idSeleccionado);
                    var patente = patentes.FirstOrDefault(p => p.Nombre == nombre);
                    if (patente != null)
                        FamiliaBLL.Instancia.EliminarPatente(_idSeleccionado, patente.Id);
                }
                else
                {
                    string nombre = item.Replace("[F] ", "");
                    var familias = FamiliaBLL.Instancia.ObtenerFamiliasIntegradas(_idSeleccionado);
                    var familia = familias.FirstOrDefault(f => f.Nombre == nombre);
                    if (familia != null)
                        FamiliaBLL.Instancia.EliminarFamiliaIntegrada(_idSeleccionado, familia.Id);
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

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormGestionFamilias"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["lblTitulo"]?.ToString();
            lblPatentesDisponibles.Text = t["lblPatentesDisponibles"]?.ToString();
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
