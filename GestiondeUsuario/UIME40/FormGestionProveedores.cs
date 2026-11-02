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
    // Gestión de proveedores (menú Maestro): se elige un proveedor de la grilla y se cambian sus condiciones
    // comerciales y de entrega, el plazo y las formas de pago que acepta. El resto de sus datos se ve pero no se edita.
    public partial class FormGestionProveedores : Form, IObservadorIdioma
    {
        private List<Proveedor> _proveedores = new List<Proveedor>();

        public FormGestionProveedores()
        {
            InitializeComponent();
        }

        private void FormGestionProveedores_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnGuardar);
            lblAyuda.ForeColor = Tema.PrincipalOscuro;
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            CargarProveedores(null);
        }

        private void FormGestionProveedores_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormGestionProveedores", clave);
        }

        // ---------- Proveedores ----------

        private void CargarProveedores(int? seleccionar)
        {
            try
            {
                int? actual = seleccionar ?? Seleccionado()?.Id;
                string buscar = txtBuscar.Text.Trim();
                _proveedores = ProveedorBLL.Instancia.ObtenerTodos()
                    .Where(p => buscar.Length == 0 ||
                                p.RazonSocial.IndexOf(buscar, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                p.CUIT.Replace("-", "").Contains(buscar.Replace("-", "")) ||
                                (p.Rubro ?? "").IndexOf(buscar, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
                dgvProveedores.SelectionChanged -= dgvProveedores_SelectionChanged;
                dgvProveedores.DataSource = null;
                dgvProveedores.DataSource = _proveedores.Select(p => new
                {
                    p.Id,
                    p.RazonSocial,
                    p.CUIT,
                    p.Contacto,
                    p.Telefono,
                    Rubro = p.Rubro ?? "",
                    FormaPago = TextosCompras.FormasPago(p.FormaPago),
                    Estado = TextosCompras.Estado(p.Estado)
                }).ToList();
                dgvProveedores.Columns["Id"].Visible = false;
                Tema.AnchoColumnas(dgvProveedores, ("RazonSocial", 190), ("CUIT", 110), ("Contacto", 130), ("Telefono", 110),
                    ("Rubro", 120), ("FormaPago", 170), ("Estado", 100));
                TraducirColumnas();
                dgvProveedores.ClearSelection();
                foreach (DataGridViewRow fila in dgvProveedores.Rows)
                    if (actual.HasValue && Convert.ToInt32(fila.Cells["Id"].Value) == actual.Value)
                    {
                        fila.Selected = true;
                        dgvProveedores.CurrentCell = fila.Cells["RazonSocial"];
                    }
                dgvProveedores.SelectionChanged += dgvProveedores_SelectionChanged;
                MostrarProveedor();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private Proveedor Seleccionado()
        {
            DataGridViewRow fila = dgvProveedores.SelectedRows.Count > 0 ? dgvProveedores.SelectedRows[0] : null;
            if (fila == null) return null;
            int id = Convert.ToInt32(fila.Cells["Id"].Value);
            return _proveedores.FirstOrDefault(p => p.Id == id);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarProveedores(null);
        }

        private void dgvProveedores_SelectionChanged(object sender, EventArgs e)
        {
            MostrarProveedor();
        }

        // Pasa los datos del proveedor elegido a los campos (vacíos y bloqueados si no hay ninguno).
        // Solo condiciones, entrega, plazo y formas de pago se pueden editar; el resto es de solo lectura.
        private void MostrarProveedor()
        {
            Proveedor p = Seleccionado();
            lblDatos.Text = p == null ? T("elegirProveedor") : string.Format(T("lblDatos"), p.RazonSocial);
            txtCUIT.Text = p?.CUIT ?? "";
            txtRazonSocial.Text = p?.RazonSocial ?? "";
            txtContacto.Text = p?.Contacto ?? "";
            txtTelefono.Text = p?.Telefono ?? "";
            txtEmail.Text = p?.Email ?? "";
            txtCondiciones.Text = p?.CondicionesComerciales ?? "";
            txtRubro.Text = p?.Rubro ?? "";
            txtEntrega.Text = p?.CondicionesEntrega ?? "";
            txtPlazo.Text = p?.PlazoEntregaDias?.ToString() ?? "";
            txtCBU.Text = p?.CBU ?? "";
            txtBanco.Text = p?.Banco ?? "";
            List<string> formas = ProveedorBLL.FormasAceptadas(p?.FormaPago);
            foreach (CheckBox c in flpFormasPago.Controls.OfType<CheckBox>())
                c.Checked = formas.Contains((string)c.Tag);

            bool hay = p != null;
            foreach (TextBox t in new[] { txtCondiciones, txtEntrega, txtPlazo })
                t.Enabled = hay;
            flpFormasPago.Enabled = hay;
            btnGuardar.Enabled = hay;
            lblAyuda.Text = hay ? T("lblAyuda") : "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Proveedor p = Seleccionado();
            if (p == null) return;
            // Plazo opcional: vacío no se carga; si no es un número queda negativo y la BLL lo rechaza
            int? plazo = null;
            if (!string.IsNullOrWhiteSpace(txtPlazo.Text))
            {
                int dias;
                plazo = int.TryParse(txtPlazo.Text.Trim(), out dias) ? dias : -1;
            }
            var cambios = new Proveedor
            {
                Id = p.Id,
                CondicionesComerciales = txtCondiciones.Text,
                CondicionesEntrega = txtEntrega.Text,
                PlazoEntregaDias = plazo,
                FormaPago = ProveedorBLL.UnirFormas(flpFormasPago.Controls.OfType<CheckBox>().Where(c => c.Checked).Select(c => (string)c.Tag))
            };
            try
            {
                ProveedorBLL.Instancia.Modificar(cambios);
                Aviso.Exito(string.Format(T("msgModificado"), p.RazonSocial));
                CargarProveedores(p.Id);
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

        // Una casilla por forma de pago (se vuelven a crear al cambiar el idioma, conservando las tildadas)
        private void CargarFormasPago()
        {
            var tildadas = flpFormasPago.Controls.OfType<CheckBox>().Where(c => c.Checked).Select(c => (string)c.Tag).ToList();
            flpFormasPago.Controls.Clear();
            foreach (string fp in ProveedorBLL.FormasPago)
                flpFormasPago.Controls.Add(new CheckBox
                {
                    Text = TextosCompras.FormaPago(fp),
                    Tag = fp,
                    AutoSize = true,
                    Checked = tildadas.Contains(fp),
                    Margin = new Padding(0, 3, 10, 0)
                });
        }

        // ---------- Idioma ----------

        private void TraducirColumnas()
        {
            foreach (DataGridViewColumn c in dgvProveedores.Columns)
                c.HeaderText = T("col" + c.Name);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormGestionProveedores"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblBuscar.Text = t["lblBuscar"]?.ToString();
            foreach (string campo in new[] { "CUIT", "RazonSocial", "Contacto", "Telefono", "Email", "Condiciones", "Rubro", "Entrega", "Plazo", "FormaPago", "CBU", "Banco" })
                Controls.Find("lbl" + campo, true)[0].Text = t["lbl" + campo]?.ToString();
            btnGuardar.Text = t["btnGuardar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            CargarFormasPago();
            if (dgvProveedores.DataSource != null)
                CargarProveedores(Seleccionado()?.Id);
        }
    }
}
