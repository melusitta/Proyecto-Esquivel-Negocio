using BE;
using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // CUS014 Bitácora de cambios del maestro de productos (menú Admin). Al abrir muestra los cambios del último mes
    // de todos los productos; se filtran por producto, usuario, tipo de cambio y fechas, y "Limpiar" vuelve a eso.
    // En cada cambio se resalta en rosa lo que cambió respecto de la versión anterior del producto.
    // De las versiones de un producto una sola es la activa; "Activar esta versión" activa una vieja y el producto
    // vuelve a esos datos (menos la existencia). Las versiones las guarda el trigger trg_Producto_Audit.
    public partial class FormControlCambios : Form, IObservadorIdioma
    {
        private List<ProductoVersion> _cambios = new List<ProductoVersion>();

        // Campos que se comparan con la versión anterior (nombre de columna de la grilla)
        private static readonly string[] Campos = { "CodigoProducto", "Nombre", "Marca", "Color", "Modelo", "PrecioUnitario",
                                                    "Existencia", "Categoria", "StockMinimo", "Proveedor", "Activo" };
        private static readonly string[] Tipos = { ProductoVersion.TipoAlta, ProductoVersion.TipoModificacion, ProductoVersion.TipoBaja,
                                                   ProductoVersion.TipoReactivacion, ProductoVersion.TipoRestauracion };

        // Ítem de un combo: valor que se filtra y texto en el idioma activo (valor null = todos)
        private class Opcion
        {
            public string Valor { get; set; }
            public string Texto { get; set; }
            public override string ToString() => Texto;
        }

        public FormControlCambios()
        {
            InitializeComponent();
        }

        private void FormControlCambios_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnRestaurar);
            Tema.EstiloPrincipal(btnBuscar);
            Tema.EstiloSecundario(btnLimpiar);
            Tema.MensajeVacio(dgvVersiones, () => T("sinCambios"));
            lblAyudaRestaurar.ForeColor = Tema.PrincipalOscuro;
            CrearColumnas();
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            Limpiar();
        }

        private void FormControlCambios_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormControlCambios", clave);
        }

        // ---------- Filtros ----------

        private void CargarCombos()
        {
            string usuario = (cmbUsuario.SelectedItem as Opcion)?.Valor;
            string tipo = (cmbTipo.SelectedItem as Opcion)?.Valor;

            cmbUsuario.Items.Clear();
            cmbUsuario.Items.Add(new Opcion { Valor = null, Texto = T("todos") });
            try
            {
                foreach (string u in ProductoBLL.Instancia.ObtenerUsuariosCambios())
                    cmbUsuario.Items.Add(new Opcion { Valor = u, Texto = u });
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
            cmbTipo.Items.Clear();
            cmbTipo.Items.Add(new Opcion { Valor = null, Texto = T("todos") });
            foreach (string t in Tipos)
                cmbTipo.Items.Add(new Opcion { Valor = t, Texto = T("tipo" + t) });

            Elegir(cmbUsuario, usuario);
            Elegir(cmbTipo, tipo);
        }

        private static void Elegir(ComboBox cmb, string valor)
        {
            cmb.SelectedIndex = 0;
            for (int i = 0; i < cmb.Items.Count; i++)
                if (((Opcion)cmb.Items[i]).Valor == valor)
                    cmb.SelectedIndex = i;
        }

        // Datos iniciales: sin filtros, los cambios del último mes
        private void Limpiar()
        {
            txtBuscar.Clear();
            cmbUsuario.SelectedIndex = -1;
            cmbTipo.SelectedIndex = -1;
            CargarCombos();
            dtpDesde.Value = ProductoBLL.DesdeInicial;
            dtpHasta.Value = DateTime.Today;
            CargarCambios(null);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarCambios(null);
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            CargarCambios(null);
        }

        // ---------- Cambios ----------

        private void CrearColumnas()
        {
            dgvVersiones.AutoGenerateColumns = false;
            dgvVersiones.Columns.Clear();
            foreach (var c in new[] { ("VersionActiva", 60f), ("CodigoProducto", 60f), ("Nombre", 170f), ("Version", 40f), ("Fecha", 120f),
                                      ("Usuario", 95f), ("Cambio", 100f), ("Marca", 95f), ("Color", 60f), ("Modelo", 90f),
                                      ("PrecioUnitario", 80f), ("Existencia", 70f), ("Categoria", 80f), ("StockMinimo", 70f),
                                      ("Proveedor", 140f), ("Activo", 55f) })
                dgvVersiones.Columns.Add(new DataGridViewTextBoxColumn { Name = c.Item1, FillWeight = c.Item2, MinimumWidth = 40 });
        }

        private void CargarCambios(int? seleccionar)
        {
            try
            {
                _cambios = ProductoBLL.Instancia.ObtenerCambios(dtpDesde.Value, dtpHasta.Value, txtBuscar.Text,
                    (cmbUsuario.SelectedItem as Opcion)?.Valor, (cmbTipo.SelectedItem as Opcion)?.Valor);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
                return;
            }

            dgvVersiones.Rows.Clear();
            foreach (ProductoVersion v in _cambios)
            {
                int fila = dgvVersiones.Rows.Add(Fila(v, v.Datos, v.VersionActiva ? T("si") : T("no"), v.NroVersion,
                    v.Fecha.ToString("dd/MM/yyyy HH:mm"), v.Usuario, TextoCambio(v)));
                DataGridViewRow r = dgvVersiones.Rows[fila];
                r.Tag = v;
                if (v.VersionActiva)
                {
                    // La versión activa del producto se ve en violeta
                    DataGridViewCell activa = r.Cells["VersionActiva"];
                    activa.Style.BackColor = Tema.Principal;
                    activa.Style.ForeColor = Color.White;
                    activa.Style.SelectionBackColor = Tema.PrincipalHover;
                    activa.Style.SelectionForeColor = Color.White;
                    activa.Style.Font = new Font(dgvVersiones.Font, FontStyle.Bold);
                }
                // Lo que cambió respecto de la versión anterior del producto
                if (v.DatosAnteriores == null) continue;
                object[] anterior = Fila(v, v.DatosAnteriores, "", 0, "", "", "");
                foreach (string campo in Campos)
                {
                    DataGridViewCell celda = r.Cells[campo];
                    if (Equals(Convert.ToString(celda.Value), Convert.ToString(anterior[celda.ColumnIndex])))
                        continue;
                    // Rosa fuerte, que se vea aun con la fila seleccionada
                    celda.Style.BackColor = Tema.AcentoFuerte;
                    celda.Style.ForeColor = Color.White;
                    celda.Style.SelectionBackColor = Tema.AcentoOscuro;
                    celda.Style.SelectionForeColor = Color.White;
                    celda.Style.Font = new Font(dgvVersiones.Font, FontStyle.Bold);
                }
            }
            lblVersiones.Text = string.Format(T("lblVersiones"), _cambios.Count);
            dgvVersiones.ClearSelection();
            if (seleccionar.HasValue)
                foreach (DataGridViewRow r in dgvVersiones.Rows)
                    if (((ProductoVersion)r.Tag).Id == seleccionar.Value)
                        r.Selected = true;
            dgvVersiones_SelectionChanged(null, EventArgs.Empty);
        }

        // Valores de una fila de la grilla, en el orden de las columnas
        private object[] Fila(ProductoVersion v, Producto d, string activa, int nro, string fecha, string usuario, string cambio)
        {
            return new object[] { activa, d.CodigoProducto, d.Nombre, nro == 0 ? (object)"" : nro, fecha, usuario, cambio,
                d.Marca, d.Color, d.Modelo, d.PrecioUnitario.ToString("N2"), d.Existencia, d.Categoria, d.StockMinimo,
                d.ProveedorHabitual ?? "", d.Activo ? T("activo") : T("inactivo") };
        }

        private string TextoCambio(ProductoVersion v)
        {
            string texto = T("tipo" + v.TipoCambio);
            if (v.TipoCambio == ProductoVersion.TipoRestauracion && v.Detalle != null)
                texto += " (" + string.Format(T("deVersion"), v.Detalle) + ")";
            return texto;
        }

        private ProductoVersion VersionElegida()
        {
            DataGridViewRow fila = dgvVersiones.SelectedRows.Count > 0 ? dgvVersiones.SelectedRows[0] : null;
            return fila?.Tag as ProductoVersion;
        }

        // Se puede activar cualquier versión que no sea la activa de su producto
        private void dgvVersiones_SelectionChanged(object sender, EventArgs e)
        {
            ProductoVersion v = VersionElegida();
            btnRestaurar.Enabled = v != null && !v.VersionActiva;
            lblAyudaRestaurar.Text = v == null ? T("ayudaElegir") : v.VersionActiva ? T("ayudaActual") : T("ayudaRestaurar");
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            ProductoVersion v = VersionElegida();
            if (v == null) return;
            if (MessageBox.Show(string.Format(T("msgConfirmar"), v.Datos.Nombre, v.NroVersion, v.Fecha.ToString("dd/MM/yyyy HH:mm")),
                    T("tituloForm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                ProductoBLL.Instancia.RestaurarVersion(v.Id);
                Aviso.Exito(string.Format(T("msgRestaurada"), v.Datos.Nombre, v.NroVersion));
                CargarCambios(v.Id);
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

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormControlCambios"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblBuscar.Text = t["lblBuscar"]?.ToString();
            lblUsuario.Text = t["lblUsuario"]?.ToString();
            lblTipo.Text = t["lblTipo"]?.ToString();
            lblDesde.Text = t["lblDesde"]?.ToString();
            lblHasta.Text = t["lblHasta"]?.ToString();
            btnBuscar.Text = t["btnBuscar"]?.ToString();
            btnLimpiar.Text = t["btnLimpiar"]?.ToString();
            btnRestaurar.Text = t["btnRestaurar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            foreach (DataGridViewColumn c in dgvVersiones.Columns) c.HeaderText = t["col" + c.Name]?.ToString();
            if (cmbTipo.Items.Count > 0)
            {
                CargarCombos();
                CargarCambios(VersionElegida()?.Id);
            }
        }
    }
}
