using BE;
using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // ABM de productos (menú Maestro): alta, modificación y baja lógica / reactivación
    public partial class FormGestionProductos : Form, IObservadorIdioma
    {
        private enum Modo { Inicial, Agregar, Modificar }
        private Modo _modo = Modo.Inicial;

        public FormGestionProductos()
        {
            InitializeComponent();
        }

        private void FormGestionProductos_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            lblModo.ForeColor = Tema.PrincipalOscuro;

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            CargarGrilla();
            CambiarModo(Modo.Inicial);
        }

        private void FormGestionProductos_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        // ---------- Grilla ----------

        private void CargarGrilla()
        {
            try
            {
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = ProductoBLL.Instancia.ObtenerParaABM(rbTodos.Checked, txtBuscar.Text);
                if (dgvProductos.Columns.Contains("Id")) dgvProductos.Columns["Id"].Visible = false;
                if (dgvProductos.Columns.Contains("PrecioUnitario"))
                    dgvProductos.Columns["PrecioUnitario"].DefaultCellStyle.Format = "N2";
                Tema.AnchoColumnas(dgvProductos, ("CodigoProducto", 60), ("Nombre", 180), ("Marca", 90), ("Color", 80),
                    ("Modelo", 120), ("PrecioUnitario", 85), ("Existencia", 80), ("Activo", 55));
                TraducirColumnas();
                MostrarSeleccionado();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private Producto ProductoSeleccionado()
        {
            DataGridViewRow fila = dgvProductos.SelectedRows.Count > 0 ? dgvProductos.SelectedRows[0] : dgvProductos.CurrentRow;
            return fila?.DataBoundItem as Producto;
        }

        private void MostrarSeleccionado()
        {
            if (_modo != Modo.Inicial) return;
            Producto p = ProductoSeleccionado();
            if (p == null)
            {
                LimpiarCampos();
                return;
            }
            nudCodigo.Value = p.CodigoProducto;
            txtNombre.Text = p.Nombre;
            txtMarca.Text = p.Marca;
            txtColor.Text = p.Color;
            txtModelo.Text = p.Modelo;
            nudPrecio.Value = Math.Min(p.PrecioUnitario, nudPrecio.Maximum);
            nudExistencia.Value = Math.Min(p.Existencia, nudExistencia.Maximum);
            ActualizarBotonBaja(p);
        }

        // Un producto activo se da de baja; uno inactivo se reactiva
        private void ActualizarBotonBaja(Producto p)
        {
            var g = GestorIdioma.Instancia;
            btnBaja.Text = p != null && !p.Activo
                ? g.Obtener("FormGestionProductos", "btnReactivar")
                : g.Obtener("FormGestionProductos", "btnBaja");
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            MostrarSeleccionado();
        }

        private void rbFiltro_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                CargarGrilla();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                CargarGrilla();
            }
        }

        // ---------- Modos ----------

        private void CambiarModo(Modo modo)
        {
            _modo = modo;
            bool editando = modo != Modo.Inicial;

            nudCodigo.Enabled = editando;
            txtNombre.Enabled = editando;
            txtMarca.Enabled = editando;
            txtColor.Enabled = editando;
            txtModelo.Enabled = editando;
            nudPrecio.Enabled = editando;
            nudExistencia.Enabled = editando;

            btnAgregar.Enabled = !editando;
            btnModificar.Enabled = !editando;
            btnBaja.Enabled = !editando;
            btnAplicar.Enabled = editando;
            btnCancelar.Enabled = editando;
            dgvProductos.Enabled = !editando;

            MostrarModo();
            if (modo == Modo.Inicial)
                MostrarSeleccionado();
        }

        private void MostrarModo()
        {
            var g = GestorIdioma.Instancia;
            string clave = _modo == Modo.Agregar ? "lblModoAgregar"
                         : _modo == Modo.Modificar ? "lblModoModificar"
                         : "lblModoInicial";
            lblModo.Text = g.Obtener("FormGestionProductos", clave);
        }

        private void LimpiarCampos()
        {
            nudCodigo.Value = 0;
            txtNombre.Clear();
            txtMarca.Clear();
            txtColor.Clear();
            txtModelo.Clear();
            nudPrecio.Value = 0;
            nudExistencia.Value = 0;
        }

        // ---------- Botonera ----------

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CambiarModo(Modo.Agregar);
            LimpiarCampos();
            nudCodigo.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (!HaySeleccion()) return;
            CambiarModo(Modo.Modificar);
            txtNombre.Focus();
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            if (!HaySeleccion()) return;
            var g = GestorIdioma.Instancia;
            Producto p = ProductoSeleccionado();
            bool reactivar = !p.Activo;

            DialogResult confirmar = MessageBox.Show(
                string.Format(g.Obtener("FormGestionProductos", reactivar ? "msgConfirmarReactivar" : "msgConfirmarBaja"), p.Nombre),
                g.Obtener("FormGestionProductos", "msgConfirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar != DialogResult.Yes) return;

            try
            {
                bool ok = reactivar ? ProductoBLL.Instancia.Reactivar(p.Id) : ProductoBLL.Instancia.DarDeBaja(p.Id);
                if (ok)
                {
                    MessageBox.Show(g.Obtener("FormGestionProductos", reactivar ? "msgReactivado" : "msgBaja"),
                        g.Obtener("FormGestionProductos", "msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            var producto = new Producto
            {
                CodigoProducto = (int)nudCodigo.Value,
                Nombre = txtNombre.Text,
                Marca = txtMarca.Text,
                Color = txtColor.Text,
                Modelo = txtModelo.Text,
                PrecioUnitario = nudPrecio.Value,
                Existencia = (int)nudExistencia.Value
            };
            try
            {
                bool ok;
                if (_modo == Modo.Agregar)
                    ok = ProductoBLL.Instancia.Crear(producto);
                else
                {
                    producto.Id = ProductoSeleccionado().Id;
                    ok = ProductoBLL.Instancia.Modificar(producto);
                }
                if (ok)
                {
                    MessageBox.Show(g.Obtener("FormGestionProductos", _modo == Modo.Agregar ? "msgCreado" : "msgModificado"),
                        g.Obtener("FormGestionProductos", "msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CambiarModo(Modo.Inicial);
                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CambiarModo(Modo.Inicial);
        }

        private bool HaySeleccion()
        {
            if (ProductoSeleccionado() != null) return true;
            var g = GestorIdioma.Instancia;
            MessageBox.Show(g.Obtener("FormGestionProductos", "msgSeleccionarPrimero"),
                g.Obtener("FormGestionProductos", "msgAtencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        // Serializar / deserializar productos en XML (pantalla propia del caso de uso)
        private void btnSerializar_Click(object sender, EventArgs e)
        {
            new FormSerializarProductos().Show();
            this.Close();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            new FormPrincipal().Show();
            this.Close();
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex),
                GestorIdioma.Instancia.Obtener("FormGestionProductos", "msgError"),
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ---------- Idioma ----------

        private void TraducirColumnas()
        {
            var g = GestorIdioma.Instancia;
            var columnas = new Dictionary<string, string>
            {
                { "CodigoProducto", "colCodigo" }, { "Nombre", "colNombre" }, { "Marca", "colMarca" },
                { "Color", "colColor" }, { "Modelo", "colModelo" }, { "PrecioUnitario", "colPrecio" },
                { "Existencia", "colExistencia" }, { "Activo", "colActivo" }
            };
            foreach (var c in columnas)
                if (dgvProductos.Columns.Contains(c.Key))
                    dgvProductos.Columns[c.Key].HeaderText = g.Obtener("FormGestionProductos", c.Value);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormGestionProductos"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblBuscar.Text = t["lblBuscar"]?.ToString();
            btnBuscar.Text = t["btnBuscar"]?.ToString();
            rbActivos.Text = t["rbActivos"]?.ToString();
            rbTodos.Text = t["rbTodos"]?.ToString();
            lblCodigo.Text = t["lblCodigo"]?.ToString();
            lblNombre.Text = t["lblNombre"]?.ToString();
            lblMarca.Text = t["lblMarca"]?.ToString();
            lblColor.Text = t["lblColor"]?.ToString();
            lblModelo.Text = t["lblModelo"]?.ToString();
            lblPrecio.Text = t["lblPrecio"]?.ToString();
            lblExistencia.Text = t["lblExistencia"]?.ToString();
            btnAgregar.Text = t["btnAgregar"]?.ToString();
            btnModificar.Text = t["btnModificar"]?.ToString();
            btnAplicar.Text = t["btnAplicar"]?.ToString();
            btnCancelar.Text = t["btnCancelar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            btnSerializar.Text = t["btnSerializar"]?.ToString();
            ActualizarBotonBaja(ProductoSeleccionado());
            TraducirColumnas();
            MostrarModo();
        }
    }
}
