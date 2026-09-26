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
    public partial class FormGU : Form, IObservadorIdioma
    {
        private int _idSeleccionado = -1;
        private bool _usuarioActivo = false;

        public FormGU()
        {
            InitializeComponent();
        }

        private void FormGU_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            lblModo.ForeColor = Tema.PrincipalOscuro;

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());

            cmbRol.Items.Clear(); //  Cargamos roles desde la BD en lugar de hardcodearlos
            var roles = RolBLL.Instancia.ObtenerTodos();
            foreach (var r in roles)
                cmbRol.Items.Add(r.Nombre);
            cmbRol.SelectedIndex = 0;

            rbActivos.Checked = true;
            CargarGrilla();
            
            HabilitarBotonera();
            lblModo.Text = GestorIdioma.Instancia.Obtener("FormGU", "lblModoInicial");
            DeshabilitarCampos();
        }

        private void FormGU_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private void TraducirColumnas()
        {
            var g = GestorIdioma.Instancia;
            if (dgvUsuarios.Columns.Contains("Id"))
                dgvUsuarios.Columns["Id"].HeaderText = g.Obtener("FormGU", "colId");
            if (dgvUsuarios.Columns.Contains("Nombre"))
                dgvUsuarios.Columns["Nombre"].HeaderText = g.Obtener("FormGU", "colNombre");
            if (dgvUsuarios.Columns.Contains("Apellido"))
                dgvUsuarios.Columns["Apellido"].HeaderText = g.Obtener("FormGU", "colApellido");
            if (dgvUsuarios.Columns.Contains("NombreUsuario"))
                dgvUsuarios.Columns["NombreUsuario"].HeaderText = g.Obtener("FormGU", "colNombreUsuario");
            if (dgvUsuarios.Columns.Contains("Email"))
                dgvUsuarios.Columns["Email"].HeaderText = g.Obtener("FormGU", "colEmail");
            if (dgvUsuarios.Columns.Contains("DNI"))
                dgvUsuarios.Columns["DNI"].HeaderText = g.Obtener("FormGU", "colDNI");
            if (dgvUsuarios.Columns.Contains("Rol"))
                dgvUsuarios.Columns["Rol"].HeaderText = g.Obtener("FormGU", "colRol");
            if (dgvUsuarios.Columns.Contains("Activo"))
                dgvUsuarios.Columns["Activo"].HeaderText = g.Obtener("FormGU", "colActivo");
            if (dgvUsuarios.Columns.Contains("IdRol"))
                dgvUsuarios.Columns["IdRol"].HeaderText = g.Obtener("FormGU", "colIdRol");
            if (dgvUsuarios.Columns.Contains("Idioma"))
                dgvUsuarios.Columns["Idioma"].HeaderText = g.Obtener("FormGU", "colIdioma");
        }

        private void ModoAccion()
        {
            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnDesbloquear.Enabled = false;
            btnDeshabilitar.Enabled = false;
            btnAplicar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void DeshabilitarCampos()
        {
            txtDNI.Enabled = false;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtNombreUsuario.Enabled = false;
            txtCorreo.Enabled = false;
            cmbRol.Enabled = false;
            rbSi.Enabled = false;
            rbNo.Enabled = false;
        }
        private void HabilitarCampos()
        {
            txtDNI.Enabled = true;
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtNombreUsuario.Enabled = true;
            txtCorreo.Enabled = true;
            cmbRol.Enabled = true;
            rbSi.Enabled = true;
            rbNo.Enabled = true;
        }

        private void HabilitarBotonera()
        {
            btnAgregar.Enabled = true;
            btnModificar.Enabled = true;
            btnDesbloquear.Enabled = true;
            btnDeshabilitar.Enabled = true;
            btnAplicar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void CargarGrilla()
        {
            List<Usuario> lista;

            if (rbActivos.Checked)
                lista = UsuarioBLL.Instancia.ObtenerActivos();
            else
                lista = UsuarioBLL.Instancia.ObtenerTodos();

            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.DataSource = null;
            dgvUsuarios.AutoGenerateColumns = true;
            dgvUsuarios.DataSource = lista;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            if (dgvUsuarios.Columns.Contains("Contraseña"))
                dgvUsuarios.Columns["Contraseña"].Visible = false;
            if (dgvUsuarios.Columns.Contains("IntentosFallidos"))
                dgvUsuarios.Columns["IntentosFallidos"].Visible = false;
            if (dgvUsuarios.Columns.Contains("FechaCreacion"))
                dgvUsuarios.Columns["FechaCreacion"].Visible = false;
            if (dgvUsuarios.Columns.Contains("PrimerIngreso"))
                dgvUsuarios.Columns["PrimerIngreso"].Visible = false;
            if (dgvUsuarios.Columns.Contains("Bloqueado"))
                dgvUsuarios.Columns["Bloqueado"].Visible = false;

            _idSeleccionado = -1;
            dgvUsuarios.ClearSelection();
            dgvUsuarios.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            TraducirColumnas();
        }
        private void MostrarModoAgregar()
        {
            var g = GestorIdioma.Instancia;
            HabilitarCampos();
            LimpiarCampos();
            _idSeleccionado = -1;
            lblModo.Text = GestorIdioma.Instancia.Obtener("FormGU", "lblModoAgregar");

            // Habilitamos todos los campos para agregar
            txtDNI.ReadOnly = false;
            txtNombre.ReadOnly = false;
            txtApellido.ReadOnly = false;
            txtCorreo.ReadOnly = false;
            cmbRol.Enabled = true;
            txtDNI.BackColor = Color.White;
            txtNombre.BackColor = Color.White;
            txtApellido.BackColor = Color.White;
            txtCorreo.BackColor = Color.White;

            txtNombreUsuario.ReadOnly = true;
            txtNombreUsuario.BackColor = Color.LightGray;
            txtNombreUsuario.Text = g.Obtener("FormGU", "msgNombreUsuarioAuto");

            btnDeshabilitar.Text = GestorIdioma.Instancia.Obtener("FormGU", "btnDeshabilitar");
            ModoAccion(); // al agregar solo quedan Aplicar y Cancelar
        }
        private void LimpiarCampos()
        {
            txtDNI.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtNombreUsuario.Text = "";
            txtCorreo.Text = "";
            cmbRol.SelectedIndex = 0;
            rbSi.Checked = true;
        }
       
        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            MostrarModoAgregar();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (string.IsNullOrEmpty(txtNombre.Text) ||
            string.IsNullOrEmpty(txtApellido.Text) ||
            string.IsNullOrEmpty(txtDNI.Text) ||
            string.IsNullOrEmpty(txtCorreo.Text))
            {
                
                MessageBox.Show(
                    g.Obtener("FormGU", "msgCamposVacios"),
                    g.Obtener("FormGU", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtDNI.Text, out int dni))
            {
                MessageBox.Show(
                g.Obtener("FormGU", "msgDNIInvalido"),
                g.Obtener("FormGU", "msgError"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_idSeleccionado == -1)
            {
                var roles = RolBLL.Instancia.ObtenerTodos();
                var rolSeleccionado = roles.FirstOrDefault(r => r.Nombre == cmbRol.SelectedItem.ToString());
                // MODO AGREGAR
                Usuario nuevo = new Usuario()
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Email = txtCorreo.Text,
                    DNI = dni,
                    Rol = cmbRol.SelectedItem.ToString(),
                    IdRol = rolSeleccionado != null ? rolSeleccionado.Id : 0,
                    Contraseña = txtApellido.Text + txtDNI.Text,
                    Activo = true,
                    PrimerIngreso = true
                };
                try
                {
                    bool ok = UsuarioBLL.Instancia.CrearUsuario(nuevo);

                    if (ok)
                    {
                        MessageBox.Show(
                        g.Obtener("FormGU", "msgUsuarioCreado") + nuevo.Apellido + dni.ToString(),
                        g.Obtener("FormGU", "msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                       CargarGrilla();
                       LimpiarCampos();
                       HabilitarBotonera();
                       lblModo.Text = GestorIdioma.Instancia.Obtener("FormGU", "lblModoInicial");
                       DeshabilitarCampos();
                    }
                }
            catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                var roles = RolBLL.Instancia.ObtenerTodos();
                var rolSeleccionado = roles.FirstOrDefault(r => r.Nombre == cmbRol.SelectedItem.ToString());
                // MODO MODIFICAR
                Usuario modificado = new Usuario()
                {
                    Id = _idSeleccionado,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Email = txtCorreo.Text,
                    DNI = dni,
                    Rol = cmbRol.SelectedItem.ToString(),
                    IdRol = rolSeleccionado != null ? rolSeleccionado.Id : 0
                };

                bool ok = UsuarioBLL.Instancia.Modificar(modificado);

                if (ok)
                {
                    MessageBox.Show(
                        g.Obtener("FormGU", "msgUsuarioModificado"),
                        g.Obtener("FormGU", "msgExito"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    LimpiarCampos();
                    HabilitarBotonera();
                    lblModo.Text = GestorIdioma.Instancia.Obtener("FormGU", "lblModoInicial");
                    DeshabilitarCampos();
                }
                else
                {
                    MessageBox.Show(
                        g.Obtener("FormGU", "msgErrorModificar"),
                        g.Obtener("FormGU", "msgError"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (_idSeleccionado == -1)
            {
                MessageBox.Show(
                g.Obtener("FormGU", "msgSeleccionarPrimero"),
                g.Obtener("FormGU", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool estaActivo = _usuarioActivo;
            string accion = estaActivo ? "deshabilitar" : "habilitar";

            DialogResult confirm = MessageBox.Show(
            g.Obtener("FormGU", "msgConfirmarAccion") + accion + g.Obtener("FormGU", "msgEsteSuario"),
            g.Obtener("FormGU", "msgConfirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                bool ok = estaActivo
                    ? UsuarioBLL.Instancia.Deshabilitar(_idSeleccionado)
                    : UsuarioBLL.Instancia.Habilitar(_idSeleccionado);

                if (ok)
                {
                    MessageBox.Show(
                    g.Obtener("FormGU", "msgUsuarioAccion") + accion + g.Obtener("FormGU", "msgDo"),
                    g.Obtener("FormGU", "msgExito"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _usuarioActivo = !estaActivo;
                    CargarGrilla();
                    HabilitarBotonera();
                    LimpiarCampos();
                    lblModo.Text = GestorIdioma.Instancia.Obtener("FormGU", "lblModoInicial");
                    DeshabilitarCampos();
                }
            }
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (_idSeleccionado == -1)
            {
                MessageBox.Show(
                g.Obtener("FormGU", "msgSeleccionarPrimero"),
                g.Obtener("FormGU", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow fila = dgvUsuarios.SelectedRows[0];
            bool estaBloqueado = Convert.ToBoolean(fila.Cells["Bloqueado"].Value);

            if(!estaBloqueado)
{
                MessageBox.Show(
                    g.Obtener("FormGU", "msgUsuarioNoBloqueado"),
                    g.Obtener("FormGU", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblModo.Text = GestorIdioma.Instancia.Obtener("FormGU", "lblModoDesbloquear");

            bool ok = UsuarioBLL.Instancia.Desbloquear(_idSeleccionado);
            if (ok)
            {
                MessageBox.Show(g.Obtener("FormGU", "msgUsuarioDesbloqueado"), g.Obtener("FormGU", "msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarGrilla();
                HabilitarBotonera();
                LimpiarCampos();
                lblModo.Text = GestorIdioma.Instancia.Obtener("FormGU", "lblModoInicial");
                DeshabilitarCampos();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DeshabilitarCampos();
            LimpiarCampos();
            HabilitarEdicion(false);
            HabilitarBotonera();
            lblModo.Text = GestorIdioma.Instancia.Obtener("FormGU", "lblModoInicial");
        }

        private void rbActivos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbActivos.Checked)
            {
                CargarGrilla();
                LimpiarCampos();
                DeshabilitarCampos();
                lblModo.Text = GestorIdioma.Instancia.Obtener("FormGU", "lblModoInicial");
            }
        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTodos.Checked)
            {
                CargarGrilla();
                LimpiarCampos();
                DeshabilitarCampos();
                lblModo.Text = GestorIdioma.Instancia.Obtener("FormGU", "lblModoInicial");
            }
        }

        private void dgvUsuarios_SelectionChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedRows.Count == 0) return;

                DataGridViewRow fila = dgvUsuarios.SelectedRows[0];
                _idSeleccionado = Convert.ToInt32(fila.Cells["Id"].Value);
                txtDNI.Text = fila.Cells["DNI"].Value.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtApellido.Text = fila.Cells["Apellido"].Value.ToString();
                txtNombreUsuario.Text = fila.Cells["NombreUsuario"].Value.ToString();
                txtCorreo.Text = fila.Cells["Email"].Value.ToString();
                cmbRol.SelectedItem = fila.Cells["Rol"].Value.ToString();
                bool activo = Convert.ToBoolean(fila.Cells["Activo"].Value);
                rbSi.Checked = activo;
                rbNo.Checked = !activo;
                _usuarioActivo = activo;
                btnDeshabilitar.Text = activo
                    ? GestorIdioma.Instancia.Obtener("FormGU", "btnDeshabilitar")
                    : GestorIdioma.Instancia.Obtener("FormGU", "btnHabilitar");
                HabilitarEdicion(false);
                lblModo.Text = GestorIdioma.Instancia.Obtener("FormGU", "lblModoSeleccionado");
            }
            catch
            {
                // Ignoramos errores de índice al recargar la grilla
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            HabilitarCampos();
            if (_idSeleccionado == -1)
            {
                MessageBox.Show(
                g.Obtener("FormGU", "msgSeleccionarPrimero"),
                 g.Obtener("FormGU", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            HabilitarEdicion(true);
            lblModo.Text = GestorIdioma.Instancia.Obtener("FormGU", "lblModoModificar");
            ModoAccion();
        }

        private void HabilitarEdicion(bool habilitar)
        {
            // DNI, Nombre, Apellido y NombreUsuario siempre bloqueados
            txtDNI.ReadOnly = true;
            txtNombre.ReadOnly = true;
            txtApellido.ReadOnly = true;
            txtNombreUsuario.ReadOnly = true;
            txtDNI.BackColor = Color.LightGray;
            txtNombre.BackColor = Color.LightGray;
            txtApellido.BackColor = Color.LightGray;
            txtNombreUsuario.BackColor = Color.LightGray;

            // Solo Email y Rol se habilitan al modificar
            txtCorreo.ReadOnly = !habilitar;
            cmbRol.Enabled = habilitar;
            txtCorreo.BackColor = habilitar ? Color.White : Color.LightGray;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            new FormPrincipal().Show();
            this.Close();

        }

        private void dgvUsuarios_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Suprimimos el error de índice al recargar
             e.ThrowException = false;
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormGU"];
            if (t == null) return;

            lblTitulo.Text = t["tituloForm"]?.ToString();
            this.Text = t["tituloForm"]?.ToString();
            lblDNI.Text = t["lblDNI"]?.ToString();
            lblNombre.Text = t["lblNombre"]?.ToString();
            lblApellido.Text = t["lblApellido"]?.ToString();
            lblNombreUsuario.Text = t["lblNombreUsuario"]?.ToString();
            lblCorreo.Text = t["lblCorreo"]?.ToString();
            lblRol.Text = t["lblRol"]?.ToString();
            lblActivo.Text = t["lblActivo"]?.ToString();
            btnAgregar.Text = t["btnAgregar"]?.ToString();
            btnModificar.Text = t["btnModificar"]?.ToString();
            btnDesbloquear.Text = t["btnDesbloquear"]?.ToString();
            btnAplicar.Text = t["btnAplicar"]?.ToString();
            btnCancelar.Text = t["btnCancelar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            rbActivos.Text = t["rbActivos"]?.ToString();
            rbTodos.Text = t["rbTodos"]?.ToString();
            bool estaActivo = btnDeshabilitar.Text == "Deshabilitar" ||
                  btnDeshabilitar.Text == "Disable" ||
                  btnDeshabilitar.Text == "비활성화";

            btnDeshabilitar.Text = estaActivo
                ? t["btnDeshabilitar"]?.ToString()
                : t["btnHabilitar"]?.ToString();
            TraducirColumnas();
        }
    }
}
