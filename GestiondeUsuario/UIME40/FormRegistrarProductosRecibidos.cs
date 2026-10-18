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
    // CUN-012 Registrar productos recibidos (Responsable de depósito).
    // Paso 11: compara la orden con la factura y lo entregado, registra cantidad, estado y ubicación
    // de cada producto y se actualiza el stock con lo recibido en buen estado.
    public partial class FormRegistrarProductosRecibidos : Form, IObservadorIdioma
    {
        private List<OrdenCompra> _ordenes = new List<OrdenCompra>();

        public FormRegistrarProductosRecibidos()
        {
            InitializeComponent();
        }

        private void FormRegistrarProductosRecibidos_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnRegistrar);
            lblComparacion.ForeColor = Tema.PrincipalOscuro;
            CrearColumnas();
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            CargarOrdenes();
        }

        private void FormRegistrarProductosRecibidos_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormRegistrarProductosRecibidos", clave);
        }

        private void CrearColumnas()
        {
            dgvItems.AutoGenerateColumns = false;
            dgvItems.Columns.Clear();
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Codigo", FillWeight = 55, ReadOnly = true });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Producto", FillWeight = 200, ReadOnly = true });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Pedido", FillWeight = 60, ReadOnly = true });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Recibido", FillWeight = 70 });
            dgvItems.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "Estado", FillWeight = 90, DisplayMember = "Texto", ValueMember = "Codigo",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox, FlatStyle = FlatStyle.Flat
            });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ubicacion", FillWeight = 140 });
        }

        private void CargarOrdenes()
        {
            try
            {
                _ordenes = RecepcionBLL.Instancia.ObtenerOrdenesARecibir();
                dgvOrdenes.DataSource = null;
                dgvOrdenes.DataSource = _ordenes.Select(o =>
                {
                    FacturaProveedor f = PagoProveedorBLL.Instancia.ObtenerFacturaDeOrden(o.Id);
                    return new
                    {
                        o.Id,
                        Nro = o.NroOrden.ToString("D6"),
                        Proveedor = o.Proveedor.RazonSocial,
                        Emision = o.FechaEmision.ToString("dd/MM/yyyy"),
                        Productos = o.Items.Count,
                        Total = o.Total.ToString("N2"),
                        Factura = f?.NroFactura ?? "",
                        Importe = f?.Importe.ToString("N2") ?? ""
                    };
                }).ToList();
                dgvOrdenes.Columns["Id"].Visible = false;
                Tema.AnchoColumnas(dgvOrdenes, ("Nro", 70), ("Proveedor", 180), ("Emision", 85), ("Productos", 70), ("Total", 100), ("Factura", 120), ("Importe", 100));
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

        // Se propone lo pedido, en buen estado; el responsable corrige según lo que llegó
        private void MostrarOrden()
        {
            dgvItems.Rows.Clear();
            OrdenCompra o = Seleccionada();
            btnRegistrar.Enabled = o != null;
            if (o == null)
            {
                lblComparacion.Text = T("sinOrdenes");
                return;
            }
            FacturaProveedor f = PagoProveedorBLL.Instancia.ObtenerFacturaDeOrden(o.Id);
            lblComparacion.Text = string.Format(T("lblComparacion"), o.NroOrden.ToString("D6"), o.Total.ToString("N2"),
                f?.NroFactura ?? "-", f?.Importe.ToString("N2") ?? "-", o.Items.Sum(i => i.Cantidad));
            foreach (ItemOrdenCompra i in o.Items)
            {
                int fila = dgvItems.Rows.Add(i.Producto.CodigoProducto, i.Producto.Nombre, i.Cantidad, i.Cantidad, ItemRecepcion.EstadoBueno, "");
                dgvItems.Rows[fila].Tag = i.Producto;
            }
        }

        private void dgvItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            OrdenCompra o = Seleccionada();
            if (o == null) return;
            dgvItems.EndEdit();
            var items = new List<ItemRecepcion>();
            foreach (DataGridViewRow fila in dgvItems.Rows)
            {
                int recibido;
                if (!int.TryParse(Convert.ToString(fila.Cells["Recibido"].Value), out recibido))
                    recibido = -1;   // la BLL lo rechaza con su mensaje
                items.Add(new ItemRecepcion
                {
                    Producto = (Producto)fila.Tag,
                    CantidadRecibida = recibido,
                    Estado = Convert.ToString(fila.Cells["Estado"].Value),
                    Ubicacion = Convert.ToString(fila.Cells["Ubicacion"].Value)
                });
            }
            try
            {
                RecepcionBLL.Instancia.RegistrarRecepcion(o.Id, items);
                int alStock = items.Where(i => i.Estado == ItemRecepcion.EstadoBueno).Sum(i => i.CantidadRecibida);
                MessageBox.Show(string.Format(T("msgRecepcionRegistrada"), o.NroOrden.ToString("D6"), alStock),
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
            var t = traducciones["FormRegistrarProductosRecibidos"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblOrdenes.Text = t["lblOrdenes"]?.ToString();
            lblItems.Text = t["lblItems"]?.ToString();
            btnRegistrar.Text = t["btnRegistrar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            ((DataGridViewComboBoxColumn)dgvItems.Columns["Estado"]).DataSource =
                ItemRecepcion.Estados.Select(s => new OpcionCodigo { Codigo = s, Texto = TextosCompras.Estado(s) }).ToList();
            TraducirColumnas();
            if (dgvOrdenes.DataSource != null)
                MostrarOrden();
        }
    }
}
