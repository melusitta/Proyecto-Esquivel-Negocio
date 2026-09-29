using BE;
using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // CUN-002 Seleccionar productos. Lo incluye CUN-001 Cargar carrito, que lo abre como diálogo.
    public partial class FormSeleccionarProductos : Form, IObservadorIdioma
    {
        // Lo define Cargar carrito: qué hacer con el producto y la cantidad que confirmó el cliente
        public Action<Producto, int> AlAgregar { get; set; }

        public FormSeleccionarProductos()
        {
            InitializeComponent();
        }

        private void FormSeleccionarProductos_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnAgregar);
            lblAgregado.ForeColor = Tema.PrincipalOscuro;

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            CargarProductos("");
        }

        private void FormSeleccionarProductos_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private void CargarProductos(string texto)
        {
            try
            {
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = ProductoBLL.Instancia.Buscar(texto);
                if (dgvProductos.Columns.Contains("Id")) dgvProductos.Columns["Id"].Visible = false;
                if (dgvProductos.Columns.Contains("Activo")) dgvProductos.Columns["Activo"].Visible = false;
                if (dgvProductos.Columns.Contains("PrecioUnitario"))
                    dgvProductos.Columns["PrecioUnitario"].DefaultCellStyle.Format = "N2";
                Tema.AnchoColumnas(dgvProductos, ("CodigoProducto", 60), ("Nombre", 180), ("Marca", 90), ("Color", 80),
                    ("Modelo", 120), ("PrecioUnitario", 85), ("Existencia", 90));
                TraducirColumnas();
                MostrarDetalle();
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

        // Paso 3: características y precio que el vendedor le comunica al cliente
        private void MostrarDetalle()
        {
            var g = GestorIdioma.Instancia;
            Producto p = ProductoSeleccionado();
            if (p == null)
            {
                lblDetalle.Text = "";
                return;
            }
            lblDetalle.Text =
                g.Obtener("FormSeleccionarProductos", "detNombre") + " " + p.Nombre + "\n\n" +
                g.Obtener("FormSeleccionarProductos", "detMarca") + " " + p.Marca + "\n" +
                g.Obtener("FormSeleccionarProductos", "detColor") + " " + p.Color + "\n" +
                g.Obtener("FormSeleccionarProductos", "detModelo") + " " + p.Modelo + "\n\n" +
                g.Obtener("FormSeleccionarProductos", "detPrecio") + " $ " + p.PrecioUnitario.ToString("N2") + "\n" +
                g.Obtener("FormSeleccionarProductos", "detExistencia") + " " + p.Existencia;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarProductos(txtBuscar.Text);
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                CargarProductos(txtBuscar.Text);
            }
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            MostrarDetalle();
        }

        private void dgvProductos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            MostrarDetalle();
        }

        // Pasos 4 y 5: el cliente confirma y el producto se carga al carrito
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            Producto p = ProductoSeleccionado();
            if (p == null)
            {
                MessageBox.Show(g.Obtener("FormSeleccionarProductos", "msgSeleccionarProducto"),
                    g.Obtener("FormSeleccionarProductos", "msgAtencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int cantidad = (int)nudCantidad.Value;
                AlAgregar?.Invoke(p, cantidad);
                lblAgregado.Text = string.Format(g.Obtener("FormSeleccionarProductos", "msgAgregado"), cantidad, p.Nombre);
                nudCantidad.Value = 1;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex),
                GestorIdioma.Instancia.Obtener("FormSeleccionarProductos", "msgError"),
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
                { "Existencia", "colExistencia" }
            };
            foreach (var c in columnas)
                if (dgvProductos.Columns.Contains(c.Key))
                    dgvProductos.Columns[c.Key].HeaderText = g.Obtener("FormSeleccionarProductos", c.Value);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormSeleccionarProductos"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblBuscar.Text = t["lblBuscar"]?.ToString();
            btnBuscar.Text = t["btnBuscar"]?.ToString();
            grpDetalle.Text = t["grpDetalle"]?.ToString();
            lblCantidad.Text = t["lblCantidad"]?.ToString();
            btnAgregar.Text = t["btnAgregar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            TraducirColumnas();
            MostrarDetalle();
        }
    }
}
