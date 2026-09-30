using BE;
using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // CUN-005 Cobrar venta. Lo incluye CUN-003 Generar factura, que lo abre como diálogo con la factura.
    public partial class FormCobrarVenta : Form, IObservadorIdioma
    {
        private readonly int _idFactura;
        private Factura _factura;

        // Opción del combo de forma de pago: código para la BLL, texto traducido para mostrar
        private class OpcionPago
        {
            public string Codigo { get; set; }
            public string Texto { get; set; }
            public override string ToString() { return Texto; }
        }

        public FormCobrarVenta()
        {
            InitializeComponent();
        }

        public FormCobrarVenta(int idFactura) : this()
        {
            _idFactura = idFactura;
        }

        private void FormCobrarVenta_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnCobrar);
            lblFactura.ForeColor = Tema.PrincipalOscuro;
            lblTotal.ForeColor = Tema.PrincipalOscuro;

            try
            {
                _factura = FacturaBLL.Instancia.ObtenerPorId(_idFactura);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
                this.Close();
                return;
            }
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());

            dgvItems.DataSource = _factura.Items
                .Select(i => new { Producto = i.Producto.Nombre, i.Cantidad, Precio = i.PrecioUnitario, i.Subtotal }).ToList();
            dgvItems.Columns["Precio"].DefaultCellStyle.Format = "N2";
            dgvItems.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
            Tema.AnchoColumnas(dgvItems, ("Producto", 220), ("Cantidad", 70), ("Precio", 90), ("Subtotal", 90));
            TraducirColumnas();
            txtMonto.Text = _factura.Total.ToString("N2");
        }

        private void FormCobrarVenta_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
            // Los datos de la tarjeta no se guardan en ningún lado: se borran de la pantalla al cerrar
            txtNumeroTarjeta.Clear();
            txtCVV.Clear();
        }

        private void MostrarFactura()
        {
            if (_factura == null) return;
            var g = GestorIdioma.Instancia;
            lblFactura.Text = string.Format(g.Obtener("FormCobrarVenta", "lblFacturaNro"),
                _factura.NroFactura, _factura.NombreCliente, _factura.DNI);
            lblFecha.Text = string.Format(g.Obtener("FormCobrarVenta", "lblFecha"), _factura.FechaHora.ToString("dd/MM/yyyy HH:mm"));
            lblTotal.Text = g.Obtener("FormCobrarVenta", "lblTotal") + " $ " + _factura.Total.ToString("N2");
        }

        // Pasos 10 a 15: pago del total, confirmación del banco y cobro (el stock se actualiza solo)
        private void btnCobrar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            decimal monto;
            if (!decimal.TryParse(txtMonto.Text.Replace("$", "").Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out monto))
            {
                MessageBox.Show(g.Obtener("FormCobrarVenta", "msgMontoInvalido"),
                    g.Obtener("FormCobrarVenta", "msgAtencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string formaPago = (cmbFormaPago.SelectedItem as OpcionPago)?.Codigo ?? "";
            try
            {
                // Débito / Crédito: la BLL valida la tarjeta y le pide la aprobación al banco.
                // Transferencia: el cajero confirma que el banco la acreditó. Efectivo: no pasa por el banco.
                bool pagoAcreditado = PagoBLL.EsPagoConTarjeta(formaPago)
                    ? PagoBLL.Instancia.AutorizarPagoConTarjeta(_factura.Id, monto, formaPago, LeerTarjeta())
                    : chkAcreditado.Checked;

                FacturaBLL.Instancia.Cobrar(_factura.Id, monto, formaPago, pagoAcreditado);

                // Paso 15: el cajero entrega la factura. Se ofrece imprimirla (ya figura como pagada)
                DialogResult imprimir = MessageBox.Show(
                    string.Format(g.Obtener("FormCobrarVenta", "msgVentaCobrada"), _factura.NroFactura) + "\n\n" +
                    g.Obtener("FormCobrarVenta", "msgPreguntaImprimir"),
                    g.Obtener("FormCobrarVenta", "msgExito"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (imprimir == DialogResult.Yes)
                    new ImpresionFactura(FacturaBLL.Instancia.ObtenerPorId(_factura.Id)).MostrarVistaPrevia(this);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ---------- Pago con tarjeta ----------

        // Con tarjeta se muestran sus datos; con transferencia, el tilde de "el banco confirmó el pago".
        // Con efectivo no se muestra nada extra.
        private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            string formaPago = (cmbFormaPago.SelectedItem as OpcionPago)?.Codigo;
            bool tarjeta = PagoBLL.EsPagoConTarjeta(formaPago);
            pnlTarjeta.Visible = tarjeta;
            chkAcreditado.Visible = !tarjeta && PagoBLL.RequiereConfirmacionBanco(formaPago);
            chkAcreditado.Checked = false;
        }

        // Datos que cargó el cajero; las validaciones las hace PagoBLL
        private Tarjeta LeerTarjeta()
        {
            return new Tarjeta
            {
                Numero = txtNumeroTarjeta.Text.Trim(),
                Vencimiento = txtVencimiento.Text.Trim(),
                CVV = txtCVV.Text.Trim(),
                Titular = txtTitular.Text.Trim()
            };
        }

        private void txtSoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtVencimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '/')
                e.Handled = true;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            // La factura queda pendiente: se puede cobrar después buscando el DNI
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CargarFormasPago()
        {
            var g = GestorIdioma.Instancia;
            string seleccionada = (cmbFormaPago.SelectedItem as OpcionPago)?.Codigo;
            cmbFormaPago.Items.Clear();
            foreach (string codigo in FacturaBLL.FormasPago)
                cmbFormaPago.Items.Add(new OpcionPago { Codigo = codigo, Texto = g.Obtener("FormCobrarVenta", "fp" + codigo) });
            int indice = cmbFormaPago.Items.Cast<OpcionPago>().ToList().FindIndex(o => o.Codigo == seleccionada);
            cmbFormaPago.SelectedIndex = indice >= 0 ? indice : 0;
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex),
                GestorIdioma.Instancia.Obtener("FormCobrarVenta", "msgError"),
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ---------- Idioma ----------

        private void TraducirColumnas()
        {
            var g = GestorIdioma.Instancia;
            foreach (var c in new[] { "Producto", "Cantidad", "Precio", "Subtotal" })
                if (dgvItems.Columns.Contains(c))
                    dgvItems.Columns[c].HeaderText = g.Obtener("FormCobrarVenta", "col" + c);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormCobrarVenta"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            grpPago.Text = t["grpPago"]?.ToString();
            lblFormaPago.Text = t["lblFormaPago"]?.ToString();
            lblMonto.Text = t["lblMonto"]?.ToString();
            chkAcreditado.Text = t["chkAcreditado"]?.ToString();
            lblNumeroTarjeta.Text = t["lblNumeroTarjeta"]?.ToString();
            lblVencimiento.Text = t["lblVencimiento"]?.ToString();
            lblCVV.Text = t["lblCVV"]?.ToString();
            lblTitular.Text = t["lblTitular"]?.ToString();
            btnCobrar.Text = t["btnCobrar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            CargarFormasPago();
            TraducirColumnas();
            MostrarFactura();
        }
    }
}
