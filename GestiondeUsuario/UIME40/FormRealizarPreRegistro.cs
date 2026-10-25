using BE;
using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // CUN-007 Realizar pre-registro (Encargado de compras). Extiende CUN-006 Generar solicitud de compra:
    // se abre como diálogo con "Pre-registrar" cuando el proveedor que se quiere asignar no está.
    // El supervisor completa después sus datos bancarios (CUN-009).
    public partial class FormRealizarPreRegistro : Form, IObservadorIdioma
    {
        public int IdProveedor { get; private set; }

        public FormRealizarPreRegistro()
        {
            InitializeComponent();
        }

        private void FormRealizarPreRegistro_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnGuardar);
            lblAyuda.ForeColor = Tema.PrincipalOscuro;
            this.AcceptButton = btnGuardar;
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            txtCUIT.Focus();
        }

        private void FormRealizarPreRegistro_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormRealizarPreRegistro", clave);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Plazo opcional: vacío no se carga; si no es un número queda negativo y la BLL lo rechaza
            int? plazo = null;
            if (!string.IsNullOrWhiteSpace(txtPlazo.Text))
            {
                int dias;
                plazo = int.TryParse(txtPlazo.Text.Trim(), out dias) ? dias : -1;
            }
            var proveedor = new Proveedor
            {
                CUIT = txtCUIT.Text,
                RazonSocial = txtRazonSocial.Text,
                Contacto = txtContacto.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                CondicionesComerciales = txtCondiciones.Text,
                Rubro = txtRubro.Text,
                CondicionesEntrega = txtEntrega.Text,
                PlazoEntregaDias = plazo,
                FormaPago = (cmbFormaPago.SelectedItem as OpcionCodigo)?.Codigo
            };
            try
            {
                IdProveedor = ProveedorBLL.Instancia.PreRegistrar(proveedor);
                MessageBox.Show(string.Format(T("msgPreRegistrado"), proveedor.RazonSocial),
                    T("msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(GestorIdioma.Instancia.TraducirError(ex), T("msgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Primera opción vacía: la forma de pago es opcional en el pre-registro
        private void CargarFormasPago()
        {
            string elegida = (cmbFormaPago.SelectedItem as OpcionCodigo)?.Codigo;
            cmbFormaPago.Items.Clear();
            cmbFormaPago.Items.Add(new OpcionCodigo { Codigo = null, Texto = "" });
            foreach (string fp in ProveedorBLL.FormasPago)
                cmbFormaPago.Items.Add(new OpcionCodigo { Codigo = fp, Texto = TextosCompras.FormaPago(fp) });
            cmbFormaPago.SelectedIndex = Array.IndexOf(ProveedorBLL.FormasPago, elegida) + 1;
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormRealizarPreRegistro"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            foreach (string campo in new[] { "CUIT", "RazonSocial", "Contacto", "Telefono", "Email", "Condiciones", "Rubro", "Entrega", "Plazo", "FormaPago" })
                Controls.Find("lbl" + campo, true)[0].Text = t["lbl" + campo]?.ToString();
            lblAyuda.Text = t["lblAyuda"]?.ToString();
            lblOpcionales.Text = t["lblOpcionales"]?.ToString();
            btnGuardar.Text = t["btnGuardar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            CargarFormasPago();
        }
    }
}
