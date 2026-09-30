using BE;
using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // CUN-001 Cargar carrito (Vendedor). Incluye CUN-002 Seleccionar productos.
    public partial class FormCargarCarrito : Form, IObservadorIdioma
    {
        private Carrito _carrito;   // null hasta que se carga el primer producto

        public FormCargarCarrito()
        {
            InitializeComponent();
        }

        private void FormCargarCarrito_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnSeleccionarProductos);
            Tema.EstiloPrincipal(btnAsociar);
            lblTotal.ForeColor = Tema.PrincipalOscuro;
            lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            MostrarCarrito();
        }

        private void FormCargarCarrito_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        // Include CUN-002: el vendedor selecciona productos y los va cargando (N veces)
        private void btnSeleccionarProductos_Click(object sender, EventArgs e)
        {
            using (var seleccionar = new FormSeleccionarProductos())
            {
                seleccionar.AlAgregar = AgregarProducto;
                seleccionar.ShowDialog(this);
            }
        }

        // Paso 5: se carga en el carrito el producto que el cliente confirmó
        private void AgregarProducto(Producto producto, int cantidad)
        {
            // El carrito se crea recién con el primer producto, así no quedan carritos vacíos
            if (_carrito == null)
                _carrito = CarritoBLL.Instancia.Crear();
            CarritoBLL.Instancia.AgregarProducto(_carrito.Id, producto.CodigoProducto, cantidad);
            MostrarCarrito();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (_carrito == null || dgvCarrito.CurrentRow == null)
            {
                MessageBox.Show(g.Obtener("FormCargarCarrito", "msgSeleccionarItem"),
                    g.Obtener("FormCargarCarrito", "msgAtencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int idProducto = Convert.ToInt32(dgvCarrito.CurrentRow.Cells["IdProducto"].Value);
                CarritoBLL.Instancia.QuitarProducto(_carrito.Id, idProducto);
                MostrarCarrito();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // Resta de a una unidad; el producto sigue seleccionado para poder seguir restando
        private void btnQuitarUnidad_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (_carrito == null || dgvCarrito.CurrentRow == null)
            {
                MessageBox.Show(g.Obtener("FormCargarCarrito", "msgSeleccionarItem"),
                    g.Obtener("FormCargarCarrito", "msgAtencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int idProducto = Convert.ToInt32(dgvCarrito.CurrentRow.Cells["IdProducto"].Value);
                int queda = CarritoBLL.Instancia.QuitarUnidad(_carrito.Id, idProducto);
                MostrarCarrito();
                if (queda > 0)
                    SeleccionarProducto(idProducto);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void SeleccionarProducto(int idProducto)
        {
            foreach (DataGridViewRow fila in dgvCarrito.Rows)
            {
                if (Convert.ToInt32(fila.Cells["IdProducto"].Value) == idProducto)
                {
                    dgvCarrito.CurrentCell = fila.Cells["Producto"];
                    fila.Selected = true;
                    return;
                }
            }
        }

        private void MostrarCarrito()
        {
            if (_carrito != null)
                _carrito = CarritoBLL.Instancia.ObtenerPorId(_carrito.Id);

            var items = _carrito?.Items ?? new List<ItemCarrito>();
            dgvCarrito.DataSource = null;
            dgvCarrito.DataSource = items.Select(i => new
            {
                IdProducto = i.Producto.Id,
                Producto = i.Producto.Nombre,
                i.Cantidad,
                Precio = i.PrecioUnitario,
                i.Subtotal
            }).ToList();
            dgvCarrito.Columns["IdProducto"].Visible = false;
            dgvCarrito.Columns["Precio"].DefaultCellStyle.Format = "N2";
            dgvCarrito.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
            Tema.AnchoColumnas(dgvCarrito, ("Producto", 220), ("Cantidad", 70), ("Precio", 90), ("Subtotal", 90));
            TraducirColumnas();
            MostrarTotal();
        }

        private void MostrarTotal()
        {
            lblTotal.Text = GestorIdioma.Instancia.Obtener("FormCargarCarrito", "lblTotal") + " $ " + (_carrito?.Total ?? 0).ToString("N2");
        }

        // Paso 7: se asocia el carrito al DNI y el cliente pasa a la caja
        private void btnAsociar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            int dni;
            if (!int.TryParse(txtDNI.Text.Trim(), out dni))
            {
                MessageBox.Show(g.Obtener("Errores", "DNI_INVALIDO"),
                    g.Obtener("FormCargarCarrito", "msgAtencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_carrito == null)
            {
                MessageBox.Show(g.Obtener("Errores", "CARRITO_VACIO"),
                    g.Obtener("FormCargarCarrito", "msgAtencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                CarritoBLL.Instancia.AsociarCliente(_carrito.Id, dni);
                MessageBox.Show(string.Format(g.Obtener("FormCargarCarrito", "msgAsociado"), dni),
                    g.Obtener("FormCargarCarrito", "msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Listo para atender al próximo cliente
                _carrito = null;
                txtDNI.Clear();
                MostrarCarrito();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;   // solo números
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            new FormPrincipal().Show();
            this.Close();
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex),
                GestorIdioma.Instancia.Obtener("FormCargarCarrito", "msgError"),
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ---------- Idioma ----------

        private void TraducirColumnas()
        {
            var g = GestorIdioma.Instancia;
            var columnas = new Dictionary<string, string>
            {
                { "Producto", "colProducto" }, { "Cantidad", "colCantidad" },
                { "Precio", "colPrecio" }, { "Subtotal", "colSubtotal" }
            };
            foreach (var c in columnas)
                if (dgvCarrito.Columns.Contains(c.Key))
                    dgvCarrito.Columns[c.Key].HeaderText = g.Obtener("FormCargarCarrito", c.Value);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormCargarCarrito"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            btnSeleccionarProductos.Text = t["btnSeleccionarProductos"]?.ToString();
            lblCarrito.Text = t["lblCarrito"]?.ToString();
            btnQuitar.Text = t["btnQuitar"]?.ToString();
            btnQuitarUnidad.Text = t["btnQuitarUnidad"]?.ToString();
            lblDNI.Text = t["lblDNI"]?.ToString();
            btnAsociar.Text = t["btnAsociar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            TraducirColumnas();
            MostrarTotal();
        }
    }
}
