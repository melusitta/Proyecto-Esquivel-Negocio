using BE;
using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // CUN-009 Generar orden de compra (Supervisor).
    // 1. Cotizaciones: carga las que remitieron los proveedores (paso 4).
    // 2. Comparar y generar orden: elige el proveedor y genera la orden (paso 5). Si el proveedor está
    //    pre-registrado se extiende a CUN-010 Completar registro de proveedor (paso 6).
    // 3. Órdenes de compra: envía la orden al proveedor (paso 7) e incluye CUN-011 Gestionar pago (pasos 8 y 9).
    public partial class FormGenerarOrdenCompra : Form, IObservadorIdioma
    {
        private SolicitudCompra _solicitud;
        private List<Cotizacion> _cotizaciones = new List<Cotizacion>();
        private List<Proveedor> _proveedores = new List<Proveedor>();

        private class Opcion<TValor>
        {
            public TValor Valor { get; set; }
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
            Tema.EstiloPrincipal(btnRegistrarCotizacion);
            Tema.EstiloPrincipal(btnGenerarOrden);
            Tema.EstiloPrincipal(btnEnviarOrden);
            Tema.EstiloPrincipal(btnGestionarPago);
            foreach (TabPage pagina in tabCompra.TabPages)
            {
                pagina.UseVisualStyleBackColor = false;
                pagina.BackColor = Tema.Fondo;
            }
            lblInfoSolicitud.ForeColor = Tema.PrincipalOscuro;
            lblInfoSolicitud.AutoEllipsis = true;
            lblTotalOrden.ForeColor = Tema.PrincipalOscuro;
            CrearColumnasCotizacion();
            cmbProveedorCot.SelectedIndexChanged += cmbProveedorCot_SelectedIndexChanged;

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            CargarSolicitudes(null);
        }

        private void FormGenerarOrdenCompra_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormGenerarOrdenCompra", clave);
        }

        private bool SolicitudAbierta
        {
            get { return _solicitud != null && _solicitud.Estado == SolicitudCompra.EstadoEnviada; }
        }

        // ---------- Solicitud ----------

        private void CargarSolicitudes(int? seleccionar)
        {
            try
            {
                int? actual = seleccionar ?? (cmbSolicitud.SelectedItem as Opcion<int>)?.Valor;
                cmbSolicitud.Items.Clear();
                foreach (SolicitudCompra s in OrdenCompraBLL.Instancia.ObtenerSolicitudesEnCurso())
                    cmbSolicitud.Items.Add(new Opcion<int>
                    {
                        Valor = s.Id,
                        Texto = string.Format(T("opcionSolicitud"), s.NroSolicitud.ToString("D6"), TextosCompras.Estado(s.Estado))
                    });
                int indice = cmbSolicitud.Items.Cast<Opcion<int>>().ToList().FindIndex(o => o.Valor == actual);
                if (cmbSolicitud.Items.Count > 0)
                    cmbSolicitud.SelectedIndex = indice >= 0 ? indice : 0;
                else
                    MostrarSolicitud(null);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void cmbSolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarSolicitud((cmbSolicitud.SelectedItem as Opcion<int>)?.Valor);
        }

        private void MostrarSolicitud(int? idSolicitud)
        {
            try
            {
                _solicitud = idSolicitud.HasValue ? SolicitudCompraBLL.Instancia.ObtenerPorId(idSolicitud.Value) : null;
                tabCompra.Enabled = _solicitud != null;
                lblInfoSolicitud.Text = _solicitud == null ? T("sinSolicitudes") : string.Format(T("lblInfoSolicitud"),
                    TextosCompras.Prioridad(_solicitud.Prioridad), _solicitud.FechaEnvio?.ToString("dd/MM/yyyy"),
                    _solicitud.Items.Count, string.Join(", ", SolicitudCompraBLL.Instancia.ProveedoresDe(_solicitud).Select(p => p.RazonSocial)));
                _cotizaciones = _solicitud == null ? new List<Cotizacion>() : OrdenCompraBLL.Instancia.ObtenerCotizaciones(_solicitud.Id);
                MostrarCotizaciones();
                PrepararNuevaCotizacion();
                MostrarComparativa();
                MostrarOrdenes();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ---------- 1. Cotizaciones ----------

        private int CantidadPedida(int idProducto)
        {
            return _solicitud?.Items.FirstOrDefault(i => i.Producto.Id == idProducto)?.Cantidad ?? 0;
        }

        private void MostrarCotizaciones()
        {
            dgvCotizaciones.DataSource = null;
            dgvCotizaciones.DataSource = _cotizaciones.Select(c => new
            {
                Proveedor = c.Proveedor.RazonSocial,
                EstadoProveedor = TextosCompras.Estado(c.Proveedor.Estado),
                Plazo = c.PlazoEntregaDias,
                FormaPago = TextosCompras.FormaPago(c.FormaPago),
                Productos = c.Items.Count,
                Total = c.Items.Sum(i => i.PrecioUnitario * Math.Min(CantidadPedida(i.Producto.Id), i.CantidadDisponible)).ToString("N2"),
                Estado = TextosCompras.Estado(c.Estado)
            }).ToList();
            Tema.AnchoColumnas(dgvCotizaciones, ("Proveedor", 180), ("EstadoProveedor", 100), ("Plazo", 60), ("FormaPago", 100),
                ("Productos", 70), ("Total", 100), ("Estado", 80));
            TraducirColumnas();
        }

        private void CrearColumnasCotizacion()
        {
            dgvItemsCot.AutoGenerateColumns = false;
            dgvItemsCot.Columns.Clear();
            dgvItemsCot.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Cotiza", FillWeight = 45 });
            dgvItemsCot.Columns.Add(new DataGridViewTextBoxColumn { Name = "Producto", FillWeight = 170, ReadOnly = true });
            dgvItemsCot.Columns.Add(new DataGridViewTextBoxColumn { Name = "Pedido", FillWeight = 55, ReadOnly = true });
            dgvItemsCot.Columns.Add(new DataGridViewTextBoxColumn { Name = "Precio", FillWeight = 80 });
            dgvItemsCot.Columns.Add(new DataGridViewTextBoxColumn { Name = "Marca", FillWeight = 90 });
            dgvItemsCot.Columns.Add(new DataGridViewTextBoxColumn { Name = "Presentacion", FillWeight = 100 });
            dgvItemsCot.Columns.Add(new DataGridViewTextBoxColumn { Name = "Disponible", FillWeight = 70 });
        }

        private void PrepararNuevaCotizacion()
        {
            grpNuevaCotizacion.Enabled = SolicitudAbierta;
            cmbProveedorCot.Items.Clear();
            dgvItemsCot.Rows.Clear();
            if (_solicitud == null) return;

            // Proveedores que todavía no cotizaron esta solicitud (los asignados aparecen primero)
            _proveedores = ProveedorBLL.Instancia.ObtenerTodos();
            var asignados = SolicitudCompraBLL.Instancia.ProveedoresDe(_solicitud).Select(p => p.Id).ToList();
            foreach (Proveedor p in _proveedores.Where(p => !_cotizaciones.Any(c => c.Proveedor.Id == p.Id))
                                                .OrderBy(p => asignados.Contains(p.Id) ? 0 : 1).ThenBy(p => p.RazonSocial))
                cmbProveedorCot.Items.Add(new Opcion<Proveedor> { Valor = p, Texto = p.RazonSocial + " - " + TextosCompras.Estado(p.Estado) });
            if (cmbProveedorCot.Items.Count > 0)
                cmbProveedorCot.SelectedIndex = 0;

            foreach (ItemSolicitud i in _solicitud.Items)
            {
                int fila = dgvItemsCot.Rows.Add(true, i.Producto.Nombre, i.Cantidad, "", i.Producto.Marca, "", i.Cantidad);
                dgvItemsCot.Rows[fila].Tag = i.Producto;
            }
        }

        // Al elegir el proveedor se proponen su plazo y su forma de pago habituales
        private void cmbProveedorCot_SelectedIndexChanged(object sender, EventArgs e)
        {
            Proveedor p = (cmbProveedorCot.SelectedItem as Opcion<Proveedor>)?.Valor;
            if (p == null) return;
            nudPlazo.Value = Math.Min(p.PlazoEntregaDias ?? 0, nudPlazo.Maximum);
            int indice = cmbFormaPagoCot.Items.Cast<OpcionCodigo>().ToList().FindIndex(o => o.Codigo == p.FormaPago);
            if (indice >= 0)
                cmbFormaPagoCot.SelectedIndex = indice;
        }

        private void dgvItemsCot_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void btnRegistrarCotizacion_Click(object sender, EventArgs e)
        {
            if (_solicitud == null) return;
            dgvItemsCot.EndEdit();
            var cotizacion = new Cotizacion
            {
                IdSolicitud = _solicitud.Id,
                Proveedor = (cmbProveedorCot.SelectedItem as Opcion<Proveedor>)?.Valor,
                PlazoEntregaDias = (int)nudPlazo.Value,
                FormaPago = (cmbFormaPagoCot.SelectedItem as OpcionCodigo)?.Codigo,
                FechaRecepcion = DateTime.Now
            };
            foreach (DataGridViewRow fila in dgvItemsCot.Rows)
            {
                if (!Convert.ToBoolean(fila.Cells["Cotiza"].Value)) continue;
                decimal precio;
                int disponible;
                // Si no son números quedan en 0 y la BLL los rechaza con su mensaje
                decimal.TryParse(Convert.ToString(fila.Cells["Precio"].Value)?.Replace("$", "").Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out precio);
                int.TryParse(Convert.ToString(fila.Cells["Disponible"].Value), out disponible);
                cotizacion.Items.Add(new ItemCotizacion
                {
                    Producto = (Producto)fila.Tag,
                    PrecioUnitario = precio,
                    Marca = Convert.ToString(fila.Cells["Marca"].Value),
                    Presentacion = Convert.ToString(fila.Cells["Presentacion"].Value),
                    CantidadDisponible = disponible
                });
            }
            try
            {
                OrdenCompraBLL.Instancia.RegistrarCotizacion(cotizacion);
                MessageBox.Show(string.Format(T("msgCotizacionRegistrada"), cotizacion.Proveedor?.RazonSocial),
                    T("msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarSolicitud(_solicitud.Id);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ---------- 2. Comparar y generar orden ----------

        private void MostrarComparativa()
        {
            dgvComparativa.DataSource = null;
            dgvComparativa.Columns.Clear();
            dgvComparativa.Rows.Clear();
            cmbCotizacion.Items.Clear();
            dgvItemsOrden.DataSource = null;
            lblEstadoProveedor.Text = "";
            lblTotalOrden.Text = "";
            txtEntrega.Clear();
            txtPago.Clear();
            btnGenerarOrden.Enabled = false;
            if (_solicitud == null) return;

            Dictionary<int, string> pedidos = OrdenCompraBLL.Instancia.ProductosPedidos(_solicitud.Id);
            Dictionary<int, decimal> mejores = OrdenCompraBLL.Instancia.MejoresPrecios(_cotizaciones);
            dgvComparativa.Columns.Add("Producto", T("colProducto"));
            dgvComparativa.Columns.Add("Pedido", T("colPedido"));
            foreach (Cotizacion c in _cotizaciones)
                dgvComparativa.Columns.Add("Cot" + c.Id, c.Proveedor.RazonSocial);
            dgvComparativa.Columns.Add("YaPedido", T("colYaPedido"));
            dgvComparativa.Columns["Producto"].FillWeight = 170;

            foreach (ItemSolicitud item in _solicitud.Items)
            {
                var valores = new List<object> { item.Producto.Nombre, item.Cantidad };
                foreach (Cotizacion c in _cotizaciones)
                {
                    ItemCotizacion ic = c.Items.FirstOrDefault(i => i.Producto.Id == item.Producto.Id);
                    valores.Add(ic == null ? "—" : ic.CantidadDisponible == 0 ? T("sinStock")
                        : "$ " + ic.PrecioUnitario.ToString("N2") + (ic.CantidadDisponible < item.Cantidad ? " (" + ic.CantidadDisponible + ")" : ""));
                }
                string pedidoA;
                valores.Add(pedidos.TryGetValue(item.Producto.Id, out pedidoA) ? pedidoA : "");
                DataGridViewRow fila = dgvComparativa.Rows[dgvComparativa.Rows.Add(valores.ToArray())];

                // Mejor precio en verde
                decimal mejor;
                if (!mejores.TryGetValue(item.Producto.Id, out mejor)) continue;
                foreach (Cotizacion c in _cotizaciones)
                {
                    ItemCotizacion ic = c.Items.FirstOrDefault(i => i.Producto.Id == item.Producto.Id);
                    if (ic != null && ic.CantidadDisponible > 0 && ic.PrecioUnitario == mejor)
                    {
                        DataGridViewCell celda = fila.Cells["Cot" + c.Id];
                        celda.Style.BackColor = Color.FromArgb(214, 240, 216);
                        celda.Style.Font = new Font(dgvComparativa.Font, FontStyle.Bold);
                    }
                }
            }

            dgvComparativa.ClearSelection();   // la selección taparía el verde del mejor precio

            foreach (Cotizacion c in _cotizaciones.Where(c => c.Estado == Cotizacion.EstadoRecibida))
                cmbCotizacion.Items.Add(new Opcion<Cotizacion> { Valor = c, Texto = c.Proveedor.RazonSocial });
            if (cmbCotizacion.Items.Count > 0)
                cmbCotizacion.SelectedIndex = 0;
        }

        private void cmbCotizacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cotizacion c = (cmbCotizacion.SelectedItem as Opcion<Cotizacion>)?.Valor;
            if (c == null || _solicitud == null) return;
            Proveedor p = c.Proveedor;
            lblEstadoProveedor.Text = p.EstaRegistrado ? T("provRegistrado") : T("provNoRegistrado");
            lblEstadoProveedor.ForeColor = p.EstaRegistrado ? Tema.PrincipalOscuro : Tema.Acento;

            List<ItemOrdenCompra> items = OrdenCompraBLL.Instancia.ItemsAPedir(c, _solicitud);
            dgvItemsOrden.DataSource = items.Select(i => new
            {
                Producto = i.Producto.Nombre,
                i.Cantidad,
                Precio = i.PrecioUnitario.ToString("N2"),
                Subtotal = i.Subtotal.ToString("N2")
            }).ToList();
            Tema.AnchoColumnas(dgvItemsOrden, ("Producto", 220), ("Cantidad", 70), ("Precio", 90), ("Subtotal", 100));
            TraducirColumnas();
            lblTotalOrden.Text = items.Count == 0 ? T("nadaParaPedir") : string.Format(T("lblTotalOrden"), items.Sum(i => i.Subtotal).ToString("N2"));
            txtEntrega.Text = p.CondicionesEntrega ?? "";
            txtPago.Text = TextosCompras.FormaPago(c.FormaPago) + (string.IsNullOrEmpty(p.CondicionesComerciales) ? "" : " - " + p.CondicionesComerciales);
            btnGenerarOrden.Enabled = SolicitudAbierta && items.Count > 0;
        }

        private void btnGenerarOrden_Click(object sender, EventArgs e)
        {
            Cotizacion c = (cmbCotizacion.SelectedItem as Opcion<Cotizacion>)?.Valor;
            if (c == null) return;
            try
            {
                OrdenCompra orden = OrdenCompraBLL.Instancia.GenerarOrden(c.Id, txtEntrega.Text, txtPago.Text);
                OrdenGenerada(orden);
            }
            catch (ErrorNegocio ex) when (ex.Codigo == "PROVEEDOR_NO_REGISTRADO")
            {
                // Paso 6 (extend): completar el registro del proveedor y volver a intentar
                DialogResult completar = MessageBox.Show(
                    GestorIdioma.Instancia.TraducirError(ex) + "\n\n" + T("msgPreguntaCompletar"),
                    T("msgAtencion"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (completar != DialogResult.Yes) return;
                using (var registro = new FormCompletarRegistroProveedor(c.Proveedor.Id))
                {
                    if (registro.ShowDialog(this) != DialogResult.OK) return;
                }
                try
                {
                    OrdenGenerada(OrdenCompraBLL.Instancia.GenerarOrden(c.Id, txtEntrega.Text, txtPago.Text));
                }
                catch (Exception ex2)
                {
                    MostrarError(ex2);
                    MostrarSolicitud(_solicitud.Id);
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void OrdenGenerada(OrdenCompra orden)
        {
            MessageBox.Show(string.Format(T("msgOrdenGenerada"), orden.NroOrden.ToString("D6"), orden.Proveedor.RazonSocial, orden.Total.ToString("N2")),
                T("msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarSolicitudes(_solicitud.Id);
            tabCompra.SelectedTab = tabOrdenes;
        }

        // ---------- 3. Órdenes de compra ----------

        private void MostrarOrdenes()
        {
            dgvOrdenes.DataSource = null;
            btnEnviarOrden.Enabled = false;
            btnGestionarPago.Enabled = false;
            if (_solicitud == null) return;
            dgvOrdenes.DataSource = OrdenCompraBLL.Instancia.ObtenerPorSolicitud(_solicitud.Id).Select(o => new
            {
                o.Id,
                Nro = o.NroOrden.ToString("D6"),
                Proveedor = o.Proveedor.RazonSocial,
                Emision = o.FechaEmision.ToString("dd/MM/yyyy HH:mm"),
                Productos = o.Items.Count,
                Total = o.Total.ToString("N2"),
                Estado = TextosCompras.Estado(o.Estado),
                Envio = o.FechaEnvio?.ToString("dd/MM/yyyy HH:mm") ?? "",
                CodigoEstado = o.Estado
            }).ToList();
            dgvOrdenes.Columns["Id"].Visible = false;
            dgvOrdenes.Columns["CodigoEstado"].Visible = false;
            Tema.AnchoColumnas(dgvOrdenes, ("Nro", 70), ("Proveedor", 180), ("Emision", 110), ("Productos", 70), ("Total", 100), ("Estado", 80), ("Envio", 110));
            TraducirColumnas();
            dgvOrdenes.SelectionChanged -= dgvOrdenes_SelectionChanged;
            dgvOrdenes.SelectionChanged += dgvOrdenes_SelectionChanged;
            dgvOrdenes_SelectionChanged(null, EventArgs.Empty);
        }

        private DataGridViewRow OrdenSeleccionada()
        {
            return dgvOrdenes.SelectedRows.Count > 0 ? dgvOrdenes.SelectedRows[0] : dgvOrdenes.CurrentRow;
        }

        private void dgvOrdenes_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow fila = OrdenSeleccionada();
            string estado = fila == null ? null : Convert.ToString(fila.Cells["CodigoEstado"].Value);
            btnEnviarOrden.Enabled = estado == OrdenCompra.EstadoEmitida;
            btnGestionarPago.Enabled = estado == OrdenCompra.EstadoEnviada;
        }

        // Include CUN-011 Gestionar pago: se paga la orden enviada elegida
        private void btnGestionarPago_Click(object sender, EventArgs e)
        {
            DataGridViewRow fila = OrdenSeleccionada();
            if (fila == null) return;
            using (var pago = new FormGestionarPago(Convert.ToInt32(fila.Cells["Id"].Value)))
            {
                if (pago.ShowDialog(this) != DialogResult.OK) return;
            }
            CargarSolicitudes(_solicitud.Id);
            tabCompra.SelectedTab = tabOrdenes;
        }

        private void btnEnviarOrden_Click(object sender, EventArgs e)
        {
            DataGridViewRow fila = OrdenSeleccionada();
            if (fila == null) return;
            try
            {
                OrdenCompra orden = OrdenCompraBLL.Instancia.EnviarOrden(Convert.ToInt32(fila.Cells["Id"].Value));
                ImpresionCompras.InformarEnvio(this,
                    string.Format(T("msgOrdenEnviada"), orden.NroOrden.ToString("D6"), orden.Proveedor.RazonSocial), T("msgExito"),
                    () => new List<string> { ImpresionCompras.Orden(orden)
                        .GuardarPdf(ImpresionCompras.NombreArchivo("archivoOrden", orden.NroOrden.ToString("D6"), orden.Proveedor.RazonSocial)) });
                CargarSolicitudes(_solicitud.Id);
                tabCompra.SelectedTab = tabOrdenes;
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

        private void CargarFormasPago()
        {
            string elegida = (cmbFormaPagoCot.SelectedItem as OpcionCodigo)?.Codigo;
            cmbFormaPagoCot.Items.Clear();
            foreach (string fp in ProveedorBLL.FormasPago)
                cmbFormaPagoCot.Items.Add(new OpcionCodigo { Codigo = fp, Texto = TextosCompras.FormaPago(fp) });
            int indice = Array.IndexOf(ProveedorBLL.FormasPago, elegida);
            cmbFormaPagoCot.SelectedIndex = indice >= 0 ? indice : 0;
        }

        private void TraducirColumnas()
        {
            foreach (DataGridViewColumn c in dgvCotizaciones.Columns) c.HeaderText = T("colCot" + c.Name);
            foreach (DataGridViewColumn c in dgvItemsCot.Columns) c.HeaderText = T("col" + c.Name);
            foreach (DataGridViewColumn c in dgvItemsOrden.Columns) c.HeaderText = T("col" + c.Name);
            foreach (DataGridViewColumn c in dgvOrdenes.Columns) c.HeaderText = T("colOrd" + c.Name);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormGenerarOrdenCompra"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblSolicitud.Text = t["lblSolicitud"]?.ToString();
            tabCotizaciones.Text = t["tabCotizaciones"]?.ToString();
            tabComparar.Text = t["tabComparar"]?.ToString();
            tabOrdenes.Text = t["tabOrdenes"]?.ToString();
            lblCotizaciones.Text = t["lblCotizaciones"]?.ToString();
            grpNuevaCotizacion.Text = t["grpNuevaCotizacion"]?.ToString();
            lblProveedorCot.Text = t["lblProveedorCot"]?.ToString();
            lblPlazo.Text = t["lblPlazo"]?.ToString();
            lblFormaPagoCot.Text = t["lblFormaPagoCot"]?.ToString();
            btnRegistrarCotizacion.Text = t["btnRegistrarCotizacion"]?.ToString();
            lblComparativa.Text = t["lblComparativa"]?.ToString();
            lblCotizacionElegida.Text = t["lblCotizacionElegida"]?.ToString();
            lblEntrega.Text = t["lblEntrega"]?.ToString();
            lblPago.Text = t["lblPago"]?.ToString();
            btnGenerarOrden.Text = t["btnGenerarOrden"]?.ToString();
            lblOrdenes.Text = t["lblOrdenes"]?.ToString();
            btnEnviarOrden.Text = t["btnEnviarOrden"]?.ToString();
            btnGestionarPago.Text = t["btnGestionarPago"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            CargarFormasPago();
            TraducirColumnas();
            if (cmbSolicitud.Items.Count > 0)
                CargarSolicitudes(null);   // textos de estados y prioridades
        }
    }
}
