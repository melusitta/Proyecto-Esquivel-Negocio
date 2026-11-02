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
    // CUN-010 Pagar orden de compra (Supervisor). Lo incluye CUN-008 Generar orden de compra, que lo abre como
    // diálogo ("Pagar") apenas genera la orden; también desde "Órdenes sin pagar" si quedó pendiente.
    // Paso 8: el proveedor entrega la factura; paso 9: el supervisor paga la orden y registra la factura
    // con el comprobante (estado Pagado), que se le envía al proveedor en PDF.
    public partial class FormPagarOrdenCompra : Form, IObservadorIdioma
    {
        private readonly int? _idOrden;
        private List<OrdenCompra> _ordenes = new List<OrdenCompra>();

        public FormPagarOrdenCompra()
        {
            InitializeComponent();
        }

        public FormPagarOrdenCompra(int idOrden) : this()
        {
            _idOrden = idOrden;
        }

        private void FormPagarOrdenCompra_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.MensajeVacio(dgvOrdenes, () => T("vacioOrdenes"));
            Tema.EstiloPrincipal(btnPagar);
            lblTotalPagar.ForeColor = Tema.PrincipalOscuro;
            dtpFechaFactura.MaxDate = DateTime.Today;
            dtpFechaOperacion.MaxDate = DateTime.Today;
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            CargarOrdenes();
        }

        private void FormPagarOrdenCompra_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormPagarOrdenCompra", clave);
        }

        private void CargarOrdenes()
        {
            try
            {
                _ordenes = OrdenCompraBLL.Instancia.ObtenerPorEstado(OrdenCompra.EstadoEnviada)
                    .Where(o => !_idOrden.HasValue || o.Id == _idOrden.Value).ToList();
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
            grpFactura.Enabled = grpPago.Enabled = btnPagar.Enabled = o != null;
            dgvItems.DataSource = o?.Items.Select(i => new
            {
                Producto = i.Producto.Nombre,
                i.Cantidad,
                Precio = i.PrecioUnitario.ToString("N2"),
                Subtotal = i.Subtotal.ToString("N2")
            }).ToList();
            Tema.AnchoColumnas(dgvItems, ("Producto", 200), ("Cantidad", 70), ("Precio", 90), ("Subtotal", 100));
            TraducirColumnas();

            // El número de factura lo asigna el sistema (solo se muestra); se propone el importe de la orden
            txtNroFactura.Text = o == null ? "" : PagoProveedorBLL.Instancia.SiguienteNroFactura(o.Proveedor.Id);
            txtComprobante.Clear();
            CargarFormasPago(o, null);
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

        private void btnPagar_Click(object sender, EventArgs e)
        {
            OrdenCompra o = Seleccionada();
            if (o == null) return;
            var factura = new FacturaProveedor
            {
                IdOrdenCompra = o.Id,
                Fecha = dtpFechaFactura.Value.Date,
                Importe = nudImporte.Value,
                Impuestos = nudImpuestos.Value,
                MontoAbonado = nudMonto.Value,
                FechaOperacion = dtpFechaOperacion.Value.Date,
                NroComprobante = txtComprobante.Text
            };
            string formaPago = (cmbFormaPago.SelectedItem as OpcionCodigo)?.Codigo;
            try
            {
                FacturaProveedor registrada = PagoProveedorBLL.Instancia.PagarOrden(factura, formaPago);
                // Paso 9: se envía el comprobante de pago al proveedor
                ImpresionCompras.InformarEnvio(this,
                    string.Format(T("msgPagoRegistrado"), o.NroOrden.ToString("D6"), o.Proveedor.RazonSocial,
                        registrada.MontoAbonado.ToString("N2"), registrada.NroComprobante), T("msgExito"),
                    () => new List<string> { ImpresionCompras.Comprobante(o, registrada, formaPago)
                        .GuardarPdf(ImpresionCompras.NombreArchivo("archivoComprobante", o.NroOrden.ToString("D6"), o.Proveedor.RazonSocial)) });
                if (_idOrden.HasValue)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                CargarOrdenes();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            if (this.Modal)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }
            new FormPrincipal().Show();
            this.Close();
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex), T("msgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ---------- Idioma ----------

        // Solo las formas de pago que acepta el proveedor de la orden. Si acepta una sola viene elegida; si
        // acepta varias, la primera opción queda vacía y el supervisor elige (la BLL exige que elija una)
        private void CargarFormasPago(OrdenCompra o, string elegida)
        {
            cmbFormaPago.Items.Clear();
            if (o == null) return;
            List<string> formas = PagoProveedorBLL.Instancia.FormasPagoDeOrden(o);
            if (formas.Count > 1)
                cmbFormaPago.Items.Add(new OpcionCodigo { Codigo = null, Texto = "" });
            foreach (string fp in formas)
                cmbFormaPago.Items.Add(new OpcionCodigo { Codigo = fp, Texto = TextosCompras.FormaPago(fp) });
            int indice = cmbFormaPago.Items.Cast<OpcionCodigo>().ToList().FindIndex(x => x.Codigo == elegida);
            cmbFormaPago.SelectedIndex = Math.Max(indice, 0);
        }

        private void TraducirColumnas()
        {
            foreach (DataGridViewColumn c in dgvOrdenes.Columns) c.HeaderText = T("colOrd" + c.Name);
            foreach (DataGridViewColumn c in dgvItems.Columns) c.HeaderText = T("col" + c.Name);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormPagarOrdenCompra"];
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
            lblFormaPago.Text = t["lblFormaPago"]?.ToString();
            CargarFormasPago(Seleccionada(), (cmbFormaPago.SelectedItem as OpcionCodigo)?.Codigo);
            btnPagar.Text = t["btnPagar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            TraducirColumnas();
            ActualizarTotal();
        }
    }
}
