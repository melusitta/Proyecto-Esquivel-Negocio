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
    // CUN-011 Gestionar pago (Supervisor).
    // Paso 8: el proveedor entrega la factura; paso 9: el supervisor paga la orden y registra la factura
    // con el comprobante (estado Pagado).
    public partial class FormGestionarPago : Form, IObservadorIdioma
    {
        private List<OrdenCompra> _ordenes = new List<OrdenCompra>();

        public FormGestionarPago()
        {
            InitializeComponent();
        }

        private void FormGestionarPago_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnRegistrarPago);
            lblTotalPagar.ForeColor = Tema.PrincipalOscuro;
            dtpFechaFactura.MaxDate = DateTime.Today;
            dtpFechaOperacion.MaxDate = DateTime.Today;
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            CargarOrdenes();
        }

        private void FormGestionarPago_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormGestionarPago", clave);
        }

        private void CargarOrdenes()
        {
            try
            {
                _ordenes = OrdenCompraBLL.Instancia.ObtenerPorEstado(OrdenCompra.EstadoEnviada);
                dgvOrdenes.DataSource = null;
                dgvOrdenes.DataSource = _ordenes.Select(o => new
                {
                    o.Id,
                    Nro = o.NroOrden.ToString("D6"),
                    Proveedor = o.Proveedor.RazonSocial,
                    o.Proveedor.CUIT,
                    Emision = o.FechaEmision.ToString("dd/MM/yyyy"),
                    Total = o.Total.ToString("N2"),
                    o.CondicionesPago
                }).ToList();
                dgvOrdenes.Columns["Id"].Visible = false;
                Tema.AnchoColumnas(dgvOrdenes, ("Nro", 70), ("Proveedor", 170), ("CUIT", 100), ("Emision", 85), ("Total", 100), ("CondicionesPago", 200));
                TraducirColumnas();
                MostrarOrden();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private OrdenCompra Seleccionada()
        {
            DataGridViewRow fila = dgvOrdenes.SelectedRows.Count > 0 ? dgvOrdenes.SelectedRows[0] : dgvOrdenes.CurrentRow;
            if (fila == null) return null;
            int id = Convert.ToInt32(fila.Cells["Id"].Value);
            return _ordenes.FirstOrDefault(o => o.Id == id);
        }

        private void dgvOrdenes_SelectionChanged(object sender, EventArgs e)
        {
            MostrarOrden();
        }

        private void MostrarOrden()
        {
            OrdenCompra o = Seleccionada();
            grpFactura.Enabled = grpPago.Enabled = btnRegistrarPago.Enabled = o != null;
            dgvItems.DataSource = o?.Items.Select(i => new
            {
                Producto = i.Producto.Nombre,
                i.Cantidad,
                Precio = i.PrecioUnitario.ToString("N2"),
                Subtotal = i.Subtotal.ToString("N2")
            }).ToList();
            Tema.AnchoColumnas(dgvItems, ("Producto", 200), ("Cantidad", 70), ("Precio", 90), ("Subtotal", 100));
            TraducirColumnas();

            // Se propone el importe de la orden; el supervisor carga lo que dice la factura
            txtNroFactura.Clear();
            txtComprobante.Clear();
            dtpFechaFactura.Value = DateTime.Today;
            dtpFechaOperacion.Value = DateTime.Today;
            nudImpuestos.Value = 0;
            nudImporte.Value = Math.Min(o?.Total ?? 0, nudImporte.Maximum);
            ActualizarTotal();
        }

        private void Montos_ValueChanged(object sender, EventArgs e)
        {
            ActualizarTotal();
        }

        // El monto a abonar es el importe más los impuestos (la BLL lo vuelve a validar)
        private void ActualizarTotal()
        {
            decimal total = nudImporte.Value + nudImpuestos.Value;
            lblTotalPagar.Text = string.Format(T("lblTotalPagar"), total.ToString("N2"));
            nudMonto.Value = Math.Min(total, nudMonto.Maximum);
        }

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            OrdenCompra o = Seleccionada();
            if (o == null) return;
            var factura = new FacturaProveedor
            {
                IdOrdenCompra = o.Id,
                NroFactura = txtNroFactura.Text,
                Fecha = dtpFechaFactura.Value.Date,
                Importe = nudImporte.Value,
                Impuestos = nudImpuestos.Value,
                MontoAbonado = nudMonto.Value,
                FechaOperacion = dtpFechaOperacion.Value.Date,
                NroComprobante = txtComprobante.Text
            };
            try
            {
                FacturaProveedor registrada = PagoProveedorBLL.Instancia.GestionarPago(factura);
                MessageBox.Show(string.Format(T("msgPagoRegistrado"), o.NroOrden.ToString("D6"), o.Proveedor.RazonSocial,
                        registrada.MontoAbonado.ToString("N2"), registrada.NroComprobante),
                    T("msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarOrdenes();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            new FormPrincipal().Show();
            this.Close();
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex), T("msgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ---------- Idioma ----------

        private void TraducirColumnas()
        {
            foreach (DataGridViewColumn c in dgvOrdenes.Columns) c.HeaderText = T("colOrd" + c.Name);
            foreach (DataGridViewColumn c in dgvItems.Columns) c.HeaderText = T("col" + c.Name);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormGestionarPago"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblOrdenes.Text = t["lblOrdenes"]?.ToString();
            lblItems.Text = t["lblItems"]?.ToString();
            grpFactura.Text = t["grpFactura"]?.ToString();
            grpPago.Text = t["grpPago"]?.ToString();
            lblNroFactura.Text = t["lblNroFactura"]?.ToString();
            lblFechaFactura.Text = t["lblFechaFactura"]?.ToString();
            lblImporte.Text = t["lblImporte"]?.ToString();
            lblImpuestos.Text = t["lblImpuestos"]?.ToString();
            lblMonto.Text = t["lblMonto"]?.ToString();
            lblFechaOperacion.Text = t["lblFechaOperacion"]?.ToString();
            lblComprobante.Text = t["lblComprobante"]?.ToString();
            btnRegistrarPago.Text = t["btnRegistrarPago"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            TraducirColumnas();
            ActualizarTotal();
        }
    }
}
