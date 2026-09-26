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
    // CUN-003 Generar factura (Cajero).
    // Lo extiende CUN-004 Registrar cliente (si el DNI no está registrado) e incluye CUN-005 Cobrar venta.
    public partial class FormGenerarFactura : Form, IObservadorIdioma
    {
        private int? _dni;
        private Cliente _cliente;     // null si el DNI no está registrado
        private Carrito _carrito;     // carrito asociado al DNI, pendiente de facturar
        private Factura _factura;     // factura generada y todavía no cobrada
        private string _mensajeCarrito = "";

        public FormGenerarFactura()
        {
            InitializeComponent();
        }

        private void FormGenerarFactura_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnRegistrarCliente);
            Tema.EstiloPrincipal(btnGenerar);
            Tema.EstiloPrincipal(btnCobrar);
            lblTotal.ForeColor = Tema.PrincipalOscuro;
            lblFactura.ForeColor = Tema.PrincipalOscuro;

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            NuevaVenta();
        }

        private void FormGenerarFactura_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        // ---------- Paso 8: consultar el DNI ----------

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int dni;
            if (!int.TryParse(txtDNI.Text.Trim(), out dni))
            {
                MessageBox.Show(GestorIdioma.Instancia.Obtener("Errores", "DNI_INVALIDO"),
                    GestorIdioma.Instancia.Obtener("FormGenerarFactura", "msgAtencion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Buscar(dni);
        }

        private void Buscar(int dni)
        {
            try
            {
                _dni = dni;
                _cliente = ClienteBLL.Instancia.ObtenerPorDNI(dni);
                // Si ya se facturó y no se cobró, se retoma esa factura
                _factura = FacturaBLL.Instancia.ObtenerPendientePorDNI(dni);
                _carrito = null;
                _mensajeCarrito = "";
                if (_factura == null)
                {
                    try
                    {
                        _carrito = CarritoBLL.Instancia.ObtenerAsociadoPorDNI(dni);
                    }
                    catch (ErrorNegocio ex) when (ex.Codigo == "CARRITO_NO_ASOCIADO")
                    {
                        _mensajeCarrito = GestorIdioma.Instancia.TraducirError(ex);
                    }
                }
                MostrarCliente();
                MostrarVenta();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void MostrarCliente()
        {
            var g = GestorIdioma.Instancia;
            if (_dni == null)
            {
                lblEstadoCliente.Text = "";
                lblDatosCliente.Text = "";
                btnRegistrarCliente.Enabled = false;
                return;
            }

            if (_cliente == null)
            {
                lblEstadoCliente.Text = g.Obtener("FormGenerarFactura", "estClienteNuevo");
                lblEstadoCliente.ForeColor = Tema.Acento;
                lblDatosCliente.Text = "";
                btnRegistrarCliente.Enabled = true;
            }
            else
            {
                lblEstadoCliente.Text = g.Obtener("FormGenerarFactura", "estClienteRegistrado");
                lblEstadoCliente.ForeColor = Tema.PrincipalOscuro;
                lblDatosCliente.Text =
                    g.Obtener("FormGenerarFactura", "datNombre") + " " + _cliente.NombreCompleto + "\n" +
                    g.Obtener("FormGenerarFactura", "datDNI") + " " + _cliente.DNI + "\n\n" +
                    g.Obtener("FormGenerarFactura", "datTelefono") + " " + _cliente.Telefono + "\n" +
                    g.Obtener("FormGenerarFactura", "datEmail") + " " + _cliente.Email + "\n" +
                    g.Obtener("FormGenerarFactura", "datDireccion") + " " + _cliente.Direccion + ", " +
                        _cliente.Localidad + " (" + _cliente.CodigoPostal + ")";
                btnRegistrarCliente.Enabled = false;
            }
        }

        // Extend CUN-004: el cliente no está registrado, el cajero carga sus datos
        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            if (_dni == null) return;
            using (var registrar = new FormRegistrarCliente(_dni.Value))
            {
                if (registrar.ShowDialog(this) == DialogResult.OK)
                    Buscar(_dni.Value);
            }
        }

        // ---------- Carrito y factura ----------

        private void MostrarVenta()
        {
            var g = GestorIdioma.Instancia;

            // Si ya hay factura pendiente se muestra su detalle; si no, el del carrito
            var filas = _factura != null
                ? _factura.Items.Select(i => new { Producto = i.Producto.Nombre, i.Cantidad, Precio = i.PrecioUnitario, i.Subtotal }).ToList()
                : (_carrito?.Items ?? new List<ItemCarrito>())
                    .Select(i => new { Producto = i.Producto.Nombre, i.Cantidad, Precio = i.PrecioUnitario, i.Subtotal }).ToList();
            dgvCarrito.DataSource = null;
            dgvCarrito.DataSource = filas;
            dgvCarrito.Columns["Precio"].DefaultCellStyle.Format = "N2";
            dgvCarrito.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
            Tema.AnchoColumnas(dgvCarrito, ("Producto", 220), ("Cantidad", 70), ("Precio", 90), ("Subtotal", 90));
            TraducirColumnas();

            decimal total = _factura?.Total ?? _carrito?.Total ?? 0;
            lblTotal.Text = g.Obtener("FormGenerarFactura", "lblTotal") + " $ " + total.ToString("N2");

            lblFactura.Text = _factura != null
                ? string.Format(g.Obtener("FormGenerarFactura", "lblFacturaNro"), _factura.NroFactura,
                    g.Obtener("FormGenerarFactura", "estadoPendiente"))
                : _mensajeCarrito;

            btnGenerar.Enabled = _cliente != null && _carrito != null && _factura == null;
            btnCobrar.Enabled = _factura != null;
        }

        // Paso 9: genera la factura y pasa directo al cobro (include CUN-005)
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (_dni == null) return;
            try
            {
                _factura = FacturaBLL.Instancia.Generar(_dni.Value);
                _carrito = null;
                MostrarVenta();
                MessageBox.Show(string.Format(g.Obtener("FormGenerarFactura", "msgFacturaGenerada"),
                        _factura.NroFactura, _factura.Total.ToString("N2")),
                    g.Obtener("FormGenerarFactura", "msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                CobrarVenta();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            CobrarVenta();
        }

        // Include CUN-005: si se cobra, la venta termina; si no, la factura queda pendiente
        private void CobrarVenta()
        {
            if (_factura == null) return;
            using (var cobrar = new FormCobrarVenta(_factura.Id))
            {
                if (cobrar.ShowDialog(this) == DialogResult.OK)
                    NuevaVenta();
            }
        }

        // ---------- Otros ----------

        private void NuevaVenta()
        {
            _dni = null;
            _cliente = null;
            _carrito = null;
            _factura = null;
            _mensajeCarrito = "";
            txtDNI.Clear();
            MostrarCliente();
            MostrarVenta();
            txtDNI.Focus();
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnBuscar.PerformClick();
            }
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
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
                GestorIdioma.Instancia.Obtener("FormGenerarFactura", "msgError"),
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
                    dgvCarrito.Columns[c.Key].HeaderText = g.Obtener("FormGenerarFactura", c.Value);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormGenerarFactura"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblDNI.Text = t["lblDNI"]?.ToString();
            btnBuscar.Text = t["btnBuscar"]?.ToString();
            grpCliente.Text = t["grpCliente"]?.ToString();
            btnRegistrarCliente.Text = t["btnRegistrarCliente"]?.ToString();
            grpCarrito.Text = t["grpCarrito"]?.ToString();
            btnGenerar.Text = t["btnGenerar"]?.ToString();
            btnCobrar.Text = t["btnCobrar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            MostrarCliente();
            MostrarVenta();
        }
    }
}
