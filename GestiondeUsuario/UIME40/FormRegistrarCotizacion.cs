using BE;
using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // Paso 4: los proveedores remiten su cotización (precio unitario, marca, presentación, cantidad disponible,
    // plazo de entrega y forma de pago). Diálogo de CUN-008 Generar orden de compra: el supervisor carga la que
    // mandó un proveedor, solo con los productos que se le pidieron en la solicitud.
    public partial class FormRegistrarCotizacion : Form, IObservadorIdioma
    {
        public int IdCotizacion { get; private set; }

        private class Opcion<TValor>
        {
            public TValor Valor { get; set; }
            public string Texto { get; set; }
            public override string ToString() { return Texto; }
        }

        public FormRegistrarCotizacion()
        {
            InitializeComponent();
        }

        private void FormRegistrarCotizacion_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.MensajeVacio(dgvItems, () => T("vacioItems"));
            Tema.EstiloPrincipal(btnRegistrarCotizacion);
            CrearColumnas();
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            CargarSolicitudes();
        }

        private void FormRegistrarCotizacion_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormRegistrarCotizacion", clave);
        }

        private void CrearColumnas()
        {
            dgvItems.AutoGenerateColumns = false;
            dgvItems.Columns.Clear();
            dgvItems.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Cotiza", FillWeight = 50 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Producto", FillWeight = 180, ReadOnly = true });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Pedido", FillWeight = 60, ReadOnly = true });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Precio", FillWeight = 85 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Marca", FillWeight = 100, ReadOnly = true });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Presentacion", FillWeight = 110 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Disponible", FillWeight = 75 });
        }

        // Solicitudes enviadas con proveedores que todavía no cotizaron
        private void CargarSolicitudes()
        {
            try
            {
                cmbSolicitud.Items.Clear();
                foreach (SolicitudCompra s in SolicitudCompraBLL.Instancia.ObtenerEnviadas())
                    if (OrdenCompraBLL.Instancia.ProveedoresPorCotizar(s.Id).Count > 0)
                        cmbSolicitud.Items.Add(new Opcion<SolicitudCompra>
                        {
                            Valor = s,
                            Texto = string.Format(T("opcionSolicitud"), s.NroSolicitud.ToString("D6"), s.FechaEnvio?.ToString("dd/MM/yyyy"))
                        });
                if (cmbSolicitud.Items.Count > 0)
                    cmbSolicitud.SelectedIndex = 0;
                else
                {
                    cmbProveedor.Items.Clear();
                    dgvItems.Rows.Clear();
                    btnRegistrarCotizacion.Enabled = false;
                    Aviso.Info(T("sinSolicitudes"));
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private SolicitudCompra SolicitudElegida()
        {
            return (cmbSolicitud.SelectedItem as Opcion<SolicitudCompra>)?.Valor;
        }

        private Proveedor ProveedorElegido()
        {
            return (cmbProveedor.SelectedItem as Opcion<Proveedor>)?.Valor;
        }

        private void cmbSolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {
            SolicitudCompra s = SolicitudElegida();
            cmbProveedor.Items.Clear();
            if (s == null) return;
            foreach (Proveedor p in OrdenCompraBLL.Instancia.ProveedoresPorCotizar(s.Id))
                cmbProveedor.Items.Add(new Opcion<Proveedor> { Valor = p, Texto = p.RazonSocial });
            if (cmbProveedor.Items.Count > 0)
                cmbProveedor.SelectedIndex = 0;
        }

        // La grilla arranca vacía: se carga lo que respondió el proveedor
        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvItems.Rows.Clear();
            SolicitudCompra s = SolicitudElegida();
            Proveedor p = ProveedorElegido();
            btnRegistrarCotizacion.Enabled = s != null && p != null;
            if (s == null || p == null) return;
            foreach (ItemSolicitud i in OrdenCompraBLL.Instancia.ItemsPedidosA(s.Id, p.Id))
            {
                // La marca es la del producto y no se edita
                int fila = dgvItems.Rows.Add(false, i.Producto.Nombre, i.Cantidad, "", i.Producto.Marca, "", "");
                dgvItems.Rows[fila].Tag = i.Producto;
            }
            nudPlazo.Value = 0;
            cmbFormaPago.SelectedIndex = 0;
        }

        private void dgvItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void btnRegistrarCotizacion_Click(object sender, EventArgs e)
        {
            SolicitudCompra s = SolicitudElegida();
            if (s == null) return;
            dgvItems.EndEdit();
            var cotizacion = new Cotizacion
            {
                IdSolicitud = s.Id,
                Proveedor = ProveedorElegido(),
                PlazoEntregaDias = (int)nudPlazo.Value,
                FormaPago = (cmbFormaPago.SelectedItem as OpcionCodigo)?.Codigo,
                FechaRecepcion = DateTime.Now
            };
            foreach (DataGridViewRow fila in dgvItems.Rows)
            {
                if (!Convert.ToBoolean(fila.Cells["Cotiza"].Value)) continue;
                decimal precio;
                int disponible;
                // Si no son números quedan en 0 / -1 y la BLL los rechaza con su mensaje
                decimal.TryParse(Convert.ToString(fila.Cells["Precio"].Value)?.Replace("$", "").Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out precio);
                if (!int.TryParse(Convert.ToString(fila.Cells["Disponible"].Value), out disponible))
                    disponible = -1;
                cotizacion.Items.Add(new ItemCotizacion
                {
                    Producto = (Producto)fila.Tag,
                    PrecioUnitario = precio,
                    Presentacion = Convert.ToString(fila.Cells["Presentacion"].Value),
                    CantidadDisponible = disponible
                });
            }
            try
            {
                IdCotizacion = OrdenCompraBLL.Instancia.RegistrarCotizacion(cotizacion);
                Aviso.Exito(string.Format(T("msgCotizacionRegistrada"), cotizacion.Proveedor?.RazonSocial));
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex), T("msgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ---------- Idioma ----------

        // Primera opción vacía: se elige la que informó el proveedor
        private void CargarFormasPago()
        {
            string elegida = (cmbFormaPago.SelectedItem as OpcionCodigo)?.Codigo;
            cmbFormaPago.Items.Clear();
            cmbFormaPago.Items.Add(new OpcionCodigo { Codigo = null, Texto = "" });
            foreach (string fp in ProveedorBLL.FormasPago)
                cmbFormaPago.Items.Add(new OpcionCodigo { Codigo = fp, Texto = TextosCompras.FormaPago(fp) });
            cmbFormaPago.SelectedIndex = Array.IndexOf(ProveedorBLL.FormasPago, elegida) + 1;
        }

        private void TraducirColumnas()
        {
            foreach (DataGridViewColumn c in dgvItems.Columns) c.HeaderText = T("col" + c.Name);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormRegistrarCotizacion"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblSolicitud.Text = t["lblSolicitud"]?.ToString();
            lblProveedor.Text = t["lblProveedor"]?.ToString();
            lblItems.Text = t["lblItems"]?.ToString();
            lblPlazo.Text = t["lblPlazo"]?.ToString();
            lblFormaPago.Text = t["lblFormaPago"]?.ToString();
            btnRegistrarCotizacion.Text = t["btnRegistrarCotizacion"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            CargarFormasPago();
            TraducirColumnas();
        }
    }
}
