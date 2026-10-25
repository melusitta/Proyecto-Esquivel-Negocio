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
    // CUN-008 Generar orden de compra (Supervisor).
    // Elige una cotización recibida (paso 4; las que mandan los proveedores se cargan con "Registrar cotización"),
    // ve sus productos con cantidades y precios acordados, completa fecha de emisión y condiciones y genera la
    // orden (paso 5), que se le envía al proveedor en PDF (paso 7). Después se abre "Pagar" (include CUN-010).
    // 3.1: si el proveedor no tiene datos bancarios se avisa y con "Registrar" se extiende a CUN-009 (paso 6).
    // Si la cotización no conviene, se descarta y sus productos se pueden pedir en otra solicitud.
    public partial class FormGenerarOrdenCompra : Form, IObservadorIdioma
    {
        private List<Cotizacion> _cotizaciones = new List<Cotizacion>();
        private readonly Dictionary<int, SolicitudCompra> _solicitudes = new Dictionary<int, SolicitudCompra>();
        private Proveedor _proveedor;

        private class OpcionCotizacion
        {
            public Cotizacion Valor { get; set; }
            public string Texto { get; set; }
            public override string ToString() { return Texto; }
        }

        public FormGenerarOrdenCompra()
        {
            InitializeComponent();
        }

        private void FormGenerarOrdenCompra_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnGenerar);
            lblInfoCotizacion.ForeColor = Tema.PrincipalOscuro;
            lblInfoCotizacion.AutoEllipsis = true;
            lblTotalOrden.ForeColor = Tema.PrincipalOscuro;
            dtpFechaEmision.MaxDate = DateTime.Today;

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            CargarCotizaciones(null);
        }

        private void FormGenerarOrdenCompra_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormGenerarOrdenCompra", clave);
        }

        private Cotizacion CotizacionElegida()
        {
            return (cmbCotizacion.SelectedItem as OpcionCotizacion)?.Valor;
        }

        private SolicitudCompra SolicitudDe(Cotizacion c)
        {
            SolicitudCompra s;
            if (!_solicitudes.TryGetValue(c.IdSolicitud, out s))
                _solicitudes[c.IdSolicitud] = s = SolicitudCompraBLL.Instancia.ObtenerPorId(c.IdSolicitud);
            return s;
        }

        // ---------- Cotizaciones recibidas ----------

        private void CargarCotizaciones(int? seleccionar)
        {
            try
            {
                int? actual = seleccionar ?? CotizacionElegida()?.Id;
                _solicitudes.Clear();
                _cotizaciones = OrdenCompraBLL.Instancia.ObtenerCotizacionesRecibidas();
                cmbCotizacion.Items.Clear();
                foreach (Cotizacion c in _cotizaciones)
                    cmbCotizacion.Items.Add(new OpcionCotizacion
                    {
                        Valor = c,
                        Texto = string.Format(T("opcionCotizacion"), c.Proveedor.RazonSocial,
                            SolicitudDe(c).NroSolicitud.ToString("D6"), c.FechaRecepcion.ToString("dd/MM/yyyy"))
                    });
                int indice = _cotizaciones.FindIndex(c => c.Id == actual);
                if (cmbCotizacion.Items.Count > 0)
                    cmbCotizacion.SelectedIndex = indice >= 0 ? indice : 0;
                else
                    MostrarCotizacion();
                ActualizarOrdenesSinPagar();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void cmbCotizacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarCotizacion();
        }

        // Productos que se van a pedir con lo acordado en la cotización, y condiciones propuestas
        private void MostrarCotizacion()
        {
            Cotizacion c = CotizacionElegida();
            dgvItems.DataSource = null;
            txtEntrega.Clear();
            txtPago.Clear();
            dtpFechaEmision.Value = DateTime.Today;
            if (c == null)
            {
                _proveedor = null;
                lblInfoCotizacion.Text = T("sinCotizaciones");
                lblEstadoProveedor.Text = lblTotalOrden.Text = "";
                btnRegistrar.Visible = false;
                btnGenerar.Enabled = btnDescartar.Enabled = false;
                return;
            }
            try
            {
                SolicitudCompra s = SolicitudDe(c);
                _proveedor = ProveedorBLL.Instancia.ObtenerPorId(c.Proveedor.Id);
                lblInfoCotizacion.Text = string.Format(T("lblInfoCotizacion"), s.NroSolicitud.ToString("D6"),
                    TextosCompras.Prioridad(s.Prioridad), c.PlazoEntregaDias, TextosCompras.FormaPago(c.FormaPago));

                // 3.1: sin datos bancarios no se le puede pagar
                bool registrado = _proveedor.EstaRegistrado;
                lblEstadoProveedor.Text = registrado ? string.Format(T("provRegistrado"), _proveedor.Banco)
                                                     : string.Format(T("provNoRegistrado"), _proveedor.RazonSocial);
                lblEstadoProveedor.ForeColor = registrado ? Tema.PrincipalOscuro : Tema.Acento;
                btnRegistrar.Visible = !registrado;

                List<ItemOrdenCompra> items = OrdenCompraBLL.Instancia.ItemsAPedir(c, s);
                dgvItems.DataSource = items.Select(i =>
                {
                    ItemCotizacion cotizado = c.Items.First(x => x.Producto.Id == i.Producto.Id);
                    return new
                    {
                        Codigo = i.Producto.CodigoProducto,
                        Producto = i.Producto.Nombre,
                        cotizado.Marca,
                        cotizado.Presentacion,
                        i.Cantidad,
                        Precio = i.PrecioUnitario.ToString("N2"),
                        Subtotal = i.Subtotal.ToString("N2")
                    };
                }).ToList();
                Tema.AnchoColumnas(dgvItems, ("Codigo", 70), ("Producto", 250), ("Marca", 130), ("Presentacion", 150),
                    ("Cantidad", 80), ("Precio", 110), ("Subtotal", 120));
                TraducirColumnas();
                lblTotalOrden.Text = items.Count == 0 ? T("nadaParaPedir") : string.Format(T("lblTotalOrden"), items.Sum(i => i.Subtotal).ToString("N2"));

                txtEntrega.Text = string.Format(T("entregaPropuesta"), c.PlazoEntregaDias)
                    + (string.IsNullOrEmpty(_proveedor.CondicionesEntrega) ? "" : " - " + _proveedor.CondicionesEntrega);
                txtPago.Text = TextosCompras.FormaPago(c.FormaPago)
                    + (string.IsNullOrEmpty(_proveedor.CondicionesComerciales) ? "" : " - " + _proveedor.CondicionesComerciales);
                btnGenerar.Enabled = registrado && items.Count > 0;
                btnDescartar.Enabled = true;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // Paso 4: se carga la cotización que mandó un proveedor
        private void btnRegistrarCotizacion_Click(object sender, EventArgs e)
        {
            using (var registrar = new FormRegistrarCotizacion())
            {
                if (registrar.ShowDialog(this) == DialogResult.OK)
                    CargarCotizaciones(registrar.IdCotizacion);
            }
        }

        // 3.2 (extend CUN-009): completar los datos bancarios del proveedor de la cotización
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (_proveedor == null) return;
            using (var registro = new FormCompletarRegistroProveedor(_proveedor.Id))
                registro.ShowDialog(this);
            MostrarCotizacion();
        }

        // Pasos 5 y 7: genera la orden, se le envía al proveedor y se abre el pago (include CUN-010)
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            Cotizacion c = CotizacionElegida();
            if (c == null) return;
            OrdenCompra orden;
            try
            {
                orden = OrdenCompraBLL.Instancia.GenerarOrden(c.Id, dtpFechaEmision.Value, txtEntrega.Text, txtPago.Text);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
                return;
            }
            ImpresionCompras.InformarEnvio(this,
                string.Format(T("msgOrdenGenerada"), orden.NroOrden.ToString("D6"), orden.Proveedor.RazonSocial, orden.Total.ToString("N2")),
                T("msgExito"),
                () => new List<string> { ImpresionCompras.Orden(orden)
                    .GuardarPdf(ImpresionCompras.NombreArchivo("archivoOrden", orden.NroOrden.ToString("D6"), orden.Proveedor.RazonSocial)) });
            CargarCotizaciones(null);
            using (var pago = new FormGestionarPago(orden.Id))
                pago.ShowDialog(this);
            ActualizarOrdenesSinPagar();
        }

        // La cotización no conviene: se descarta y no se genera la orden
        private void btnDescartar_Click(object sender, EventArgs e)
        {
            Cotizacion c = CotizacionElegida();
            if (c == null) return;
            if (MessageBox.Show(string.Format(T("msgPreguntaDescartar"), c.Proveedor.RazonSocial), T("msgAtencion"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                OrdenCompraBLL.Instancia.DescartarCotizacion(c.Id);
                MessageBox.Show(string.Format(T("msgDescartada"), c.Proveedor.RazonSocial), T("msgExito"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCotizaciones(null);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // Órdenes enviadas cuyo pago quedó pendiente (se cerró "Pagar" sin pagar)
        private void ActualizarOrdenesSinPagar()
        {
            int pendientes = OrdenCompraBLL.Instancia.ObtenerPorEstado(OrdenCompra.EstadoEnviada).Count;
            btnOrdenesSinPagar.Text = string.Format(T("btnOrdenesSinPagar"), pendientes);
            btnOrdenesSinPagar.Enabled = pendientes > 0;
        }

        private void btnOrdenesSinPagar_Click(object sender, EventArgs e)
        {
            using (var pago = new FormGestionarPago())
                pago.ShowDialog(this);
            ActualizarOrdenesSinPagar();
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
            foreach (DataGridViewColumn c in dgvItems.Columns) c.HeaderText = T("col" + c.Name);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormGenerarOrdenCompra"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblCotizacion.Text = t["lblCotizacion"]?.ToString();
            btnRegistrarCotizacion.Text = t["btnRegistrarCotizacion"]?.ToString();
            btnRegistrar.Text = t["btnRegistrar"]?.ToString();
            lblItems.Text = t["lblItems"]?.ToString();
            lblFechaEmision.Text = t["lblFechaEmision"]?.ToString();
            lblEntrega.Text = t["lblEntrega"]?.ToString();
            lblPago.Text = t["lblPago"]?.ToString();
            btnGenerar.Text = t["btnGenerar"]?.ToString();
            btnDescartar.Text = t["btnDescartar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            TraducirColumnas();
            if (IsHandleCreated)
                CargarCotizaciones(null);   // textos de prioridades, formas de pago y estados
        }
    }
}
