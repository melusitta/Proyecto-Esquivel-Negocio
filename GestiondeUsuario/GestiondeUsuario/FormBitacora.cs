using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    public partial class FormBitacora : Form, IObservadorIdioma
    {
        private float _printY = 50;
        private int _printRowIndex = 0;
        public FormBitacora()
        {
            InitializeComponent();
        }

        private void FormBitacora_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);

            var g = GestorIdioma.Instancia;
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            dtpFechaIni.Checked = false;
            dtpFechaFin.Checked = false;
            try
            {
                CargarCombos();

                // Si es usuario General bloqueamos login y lo forzamos a ver solo el suyo
                string rol = SessionManager.Instancia.ObtenerUsuarioActivo()?.Rol ?? "General";
                if (rol != "Admin")
                {
                    string nombreUsuario = SessionManager.Instancia
                        .ObtenerUsuarioActivo()?.NombreUsuario ?? "";
                    cmbLogin.SelectedItem = nombreUsuario;
                    cmbLogin.Enabled = false;
                }

                CargarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(g.Obtener("FormBitacora", "msgErrorCargar") + GestorIdioma.Instancia.TraducirError(ex),
                g.Obtener("FormBitacora", "msgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormBitacora_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private void CargarCombos()
        {
            cmbLogin.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModulo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEvento.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCriticidad.DropDownStyle = ComboBoxStyle.DropDownList;

            // Login
            cmbLogin.Items.Clear();
            cmbLogin.Items.Add("");
            foreach (var l in BitacoraBLL.Instancia.ObtenerLogins())
                cmbLogin.Items.Add(l);
            cmbLogin.SelectedIndex = 0;

            // Módulo según rol
            cmbModulo.Items.Clear();
            cmbModulo.Items.Add("");
            string rol = SessionManager.Instancia.ObtenerUsuarioActivo()?.Rol ?? "General";
            if (rol == "Admin")
            {
                cmbModulo.Items.Add("Administrador");
                cmbModulo.Items.Add("Usuarios");
                cmbModulo.Items.Add("Roles");
                cmbModulo.Items.Add("Familias");
            }
            else
            {
                cmbModulo.Items.Add("Usuarios");
            }
            cmbModulo.SelectedIndex = 0;

            // Eventos
            cmbEvento.Items.Clear();
            cmbEvento.Items.Add("");
            if (rol == "Admin")
            {
                cmbEvento.Items.Add("Crear Usuario");
                cmbEvento.Items.Add("Modificar Usuario");
                cmbEvento.Items.Add("Deshabilitar Usuario");
                cmbEvento.Items.Add("Habilitar Usuario");
                cmbEvento.Items.Add("Desbloquear Usuario");
                cmbEvento.Items.Add("Crear Rol");
                cmbEvento.Items.Add("Modificar Rol");
                cmbEvento.Items.Add("Eliminar Rol");
                cmbEvento.Items.Add("Crear Familia");
                cmbEvento.Items.Add("Modificar Familia");
                cmbEvento.Items.Add("Eliminar Familia");
                cmbEvento.Items.Add("Agregar Patente a Familia");
                cmbEvento.Items.Add("Agregar Familia a Familia");
                cmbEvento.Items.Add("Agregar Patente a Rol");
                cmbEvento.Items.Add("Agregar Familia a Rol");
            }
            cmbEvento.Items.Add("Login");
            cmbEvento.Items.Add("Logout");
            cmbEvento.Items.Add("Cambiar Clave");
            cmbEvento.Items.Add("Login fallido");
            cmbEvento.Items.Add("Login fallido - usuario no existe");
            cmbEvento.Items.Add("Intento en cuenta bloqueada");
            cmbEvento.Items.Add("Cambiar Idioma");
            cmbEvento.SelectedIndex = 0;

            // Criticidad
            cmbCriticidad.Items.Clear();
            cmbCriticidad.Items.Add("Todas");
            for (int i = 1; i <= 5; i++)
                cmbCriticidad.Items.Add(i.ToString());
            cmbCriticidad.SelectedIndex = 0;

            cmbCriticidad.Items.Clear();
            cmbCriticidad.Items.Add(GestorIdioma.Instancia.Obtener("FormBitacora", "cmbTodasCriticidades"));
            for (int i = 1; i <= 5; i++)
                cmbCriticidad.Items.Add(i.ToString());
            cmbCriticidad.SelectedIndex = 0;
        }

        private void CargarGrilla()
        {
            string login = cmbLogin.SelectedItem?.ToString();
            DateTime? fechaIni = dtpFechaIni.Checked ? dtpFechaIni.Value.Date : (DateTime?)null;
            DateTime? fechaFin = dtpFechaFin.Checked ? dtpFechaFin.Value.Date : (DateTime?)null;
            string modulo = cmbModulo.SelectedItem?.ToString();
            string evento = cmbEvento.SelectedItem?.ToString();
            int? criticidad = null;
            if (cmbCriticidad.SelectedIndex > 0)
                criticidad = int.Parse(cmbCriticidad.SelectedItem.ToString());
            var lista = BitacoraBLL.Instancia.ObtenerFiltrado(login, fechaIni, fechaFin, modulo, evento, criticidad);

            // Proyectamos para separar Fecha y Hora en columnas distintas
            var vista = new List<object>();
            foreach (var b in lista)
            {
                vista.Add(new
                {
                    Login = b.Usuario,
                    Fecha = b.Fecha.ToShortDateString(),
                    Hora = b.Fecha.ToString("HH:mm"),
                    Modulo = b.Modulo,
                    Evento = b.Accion,
                    Criticidad = b.Criticidad,
                    _nombre = b.Nombre,
                    _apellido = b.Apellido
                });
            }

            dgvBitacora.DataSource = null;
            dgvBitacora.DataSource = vista;

            if (dgvBitacora.Columns.Contains("_nombre"))
                dgvBitacora.Columns["_nombre"].Visible = false;
            if (dgvBitacora.Columns.Contains("_apellido"))
                dgvBitacora.Columns["_apellido"].Visible = false;

            // Seleccionar primer registro por defecto
            if (dgvBitacora.Rows.Count > 0)
            {
                dgvBitacora.Rows[0].Selected = true;
                MostrarNombreApellido();
            }
            else
            {
                txtNombre.Text = "";
                txtApellido.Text = "";
            }
            TraducirColumnas();
        }

        private void MostrarNombreApellido()
        {
            if(dgvBitacora.SelectedRows.Count == 0) return;

            var fila = dgvBitacora.SelectedRows[0];
            string nombreUsuario = fila.Cells["Login"].Value?.ToString() ?? "";

            // Traemos Nombre y Apellido con una query
            Usuario usuario = UsuarioBLL.Instancia.ObtenerPorNombreUsuario(nombreUsuario);

            if (usuario != null)
            {
                txtNombre.Text = usuario.Nombre;
                txtApellido.Text = usuario.Apellido;
            }
            else
            {
                txtNombre.Text = "";
                txtApellido.Text = "";
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            MostrarNombreApellido();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (dtpFechaIni.Checked && dtpFechaFin.Checked)
            {
                if (dtpFechaIni.Value.Date > dtpFechaFin.Value.Date)
                {
                    MessageBox.Show(g.Obtener("FormBitacora", "msgFechasInvalidas"),
                    g.Obtener("FormBitacora", "msgFechasInvalidasTitulo"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            CargarGrilla();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            new FormPrincipal().Show();
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            _printY = 50;
            _printRowIndex = 0;
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(ImprimirBitacora);

            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = pd;
            if (Tema.Icono != null)
                ppd.Icon = Tema.Icono;
            ppd.ShowDialog();
        }

        private void ImprimirBitacora(object sender, PrintPageEventArgs e)
        {
            float x = 40;
            Font fontTitulo = new Font("Arial", 14, FontStyle.Bold);
            Font fontHeader = new Font("Arial", 9, FontStyle.Bold);
            Font fontData = new Font("Arial", 8);

            // Titulo y header solo en la primera pagina
            if (_printRowIndex == 0)
            {
                e.Graphics.DrawString("Bitácora de Eventos", fontTitulo, Brushes.Black, x, _printY);
                _printY += 30;
                e.Graphics.DrawString("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                    fontData, Brushes.Black, x, _printY);
                _printY += 25;

                e.Graphics.DrawString("Login", fontHeader, Brushes.Black, x, _printY);
                e.Graphics.DrawString("Fecha", fontHeader, Brushes.Black, x + 120, _printY);
                e.Graphics.DrawString("Hora", fontHeader, Brushes.Black, x + 200, _printY);
                e.Graphics.DrawString("Módulo", fontHeader, Brushes.Black, x + 260, _printY);
                e.Graphics.DrawString("Evento", fontHeader, Brushes.Black, x + 350, _printY);
                e.Graphics.DrawString("Criticidad", fontHeader, Brushes.Black, x + 500, _printY);
                _printY += 20;
                e.Graphics.DrawLine(Pens.Black, x, _printY, 760, _printY);
                _printY += 10;
            }

            // Imprimir filas desde donde quedamos
            while (_printRowIndex < dgvBitacora.Rows.Count)
            {
                if (_printY > e.PageBounds.Height - 50)
                {
                    e.HasMorePages = true;
                    _printY = 40; // reset Y para la nueva página
                    return;
                }

                var fila = dgvBitacora.Rows[_printRowIndex];
                e.Graphics.DrawString(fila.Cells["Login"].Value?.ToString() ?? "", fontData, Brushes.Black, x, _printY);
                e.Graphics.DrawString(fila.Cells["Fecha"].Value?.ToString() ?? "", fontData, Brushes.Black, x + 120, _printY);
                e.Graphics.DrawString(fila.Cells["Hora"].Value?.ToString() ?? "", fontData, Brushes.Black, x + 200, _printY);
                e.Graphics.DrawString(fila.Cells["Modulo"].Value?.ToString() ?? "", fontData, Brushes.Black, x + 260, _printY);
                e.Graphics.DrawString(fila.Cells["Evento"].Value?.ToString() ?? "", fontData, Brushes.Black, x + 350, _printY);
                e.Graphics.DrawString(fila.Cells["Criticidad"].Value?.ToString() ?? "", fontData, Brushes.Black, x + 500, _printY);
                _printY += 18;
                _printRowIndex++;
            }

            e.HasMorePages = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            try
            {
                cmbLogin.SelectedIndex = 0;
                cmbModulo.SelectedIndex = 0;
                cmbEvento.SelectedIndex = 0;
                cmbCriticidad.SelectedIndex = 0;
                dtpFechaIni.Checked = false;
                dtpFechaIni.Value = DateTime.Today;
                dtpFechaFin.Checked = false;
                dtpFechaFin.Value = DateTime.Today;
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(g.Obtener("FormBitacora", "msgErrorLimpiar") + GestorIdioma.Instancia.TraducirError(ex),
                 g.Obtener("FormBitacora", "msgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cmbCriticidad.Items.Clear();
            cmbCriticidad.Items.Add(GestorIdioma.Instancia.Obtener("FormBitacora", "cmbTodasCriticidades"));
            for (int i = 1; i <= 5; i++)
                cmbCriticidad.Items.Add(i.ToString());
            cmbCriticidad.SelectedIndex = 0;
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormBitacora"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblLogin.Text = t["lblLogin"]?.ToString();
            lblFechaIni.Text = t["lblFechaIni"]?.ToString();
            lblFechaFin.Text = t["lblFechaFin"]?.ToString();
            lblModulo.Text = t["lblModulo"]?.ToString();
            lblEvento.Text = t["lblEvento"]?.ToString();
            lblCriticidad.Text = t["lblCriticidad"]?.ToString();
            lblNombre.Text = t["lblNombre"]?.ToString();
            lblApellido.Text = t["lblApellido"]?.ToString();
            btnAplicar.Text = t["btnAplicar"]?.ToString();
            btnLimpiar.Text = t["btnLimpiar"]?.ToString();
            btnImprimir.Text = t["btnImprimir"]?.ToString();
            btnSalir.Text = t["btnSalir"]?.ToString();
            TraducirColumnas();

        }
        private void TraducirColumnas()
        {
            var g = GestorIdioma.Instancia;
            if (dgvBitacora.Columns.Contains("Login"))
                dgvBitacora.Columns["Login"].HeaderText = g.Obtener("FormBitacora", "colLogin");
            if (dgvBitacora.Columns.Contains("Fecha"))
                dgvBitacora.Columns["Fecha"].HeaderText = g.Obtener("FormBitacora", "colFecha");
            if (dgvBitacora.Columns.Contains("Hora"))
                dgvBitacora.Columns["Hora"].HeaderText = g.Obtener("FormBitacora", "colHora");
            if (dgvBitacora.Columns.Contains("Modulo"))
                dgvBitacora.Columns["Modulo"].HeaderText = g.Obtener("FormBitacora", "colModulo");
            if (dgvBitacora.Columns.Contains("Evento"))
                dgvBitacora.Columns["Evento"].HeaderText = g.Obtener("FormBitacora", "colEvento");
            if (dgvBitacora.Columns.Contains("Criticidad"))
                dgvBitacora.Columns["Criticidad"].HeaderText = g.Obtener("FormBitacora", "colCriticidad");
        }
    }
}
