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
    // Desde Maestro es "Gestión de clientes": lista de clientes, alta y serialización XML (A03).
    // Desde Generar factura es "Registrar cliente": diálogo del cajero con el DNI que buscó.
    public partial class FormRegistrarCliente : Form, IObservadorIdioma
    {
        private readonly int _dni;
        private readonly bool _desdeFactura;

        public FormRegistrarCliente()
        {
            InitializeComponent();
        }

        public FormRegistrarCliente(int dni) : this()
        {
            _dni = dni;
            _desdeFactura = true;
        }

        private void FormRegistrarCliente_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnRegistrar);
            Tema.EstiloPrincipal(btnSerializar);

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            if (_desdeFactura)
            {
                // El DNI viene de Generar factura y no se cambia
                txtDNI.Text = _dni.ToString();
                txtDNI.ReadOnly = true;
                this.AcceptButton = btnRegistrar;
                txtNombre.Focus();
            }
            else
            {
                txtDNI.Focus();
            }
            CargarClientes();
        }

        private void FormRegistrarCliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private void CargarClientes()
        {
            try
            {
                dgvClientes.DataSource = null;
                dgvClientes.DataSource = ClienteBLL.Instancia.ObtenerTodos();
                foreach (var oculta in new[] { "Id", "NombreCompleto" })
                    if (dgvClientes.Columns.Contains(oculta)) dgvClientes.Columns[oculta].Visible = false;
                if (dgvClientes.Columns.Contains("FechaAlta"))
                    dgvClientes.Columns["FechaAlta"].DefaultCellStyle.Format = "dd/MM/yyyy";
                Tema.AnchoColumnas(dgvClientes, ("DNI", 95), ("Nombre", 80), ("Apellido", 80), ("Telefono", 100), ("Email", 130),
                    ("Direccion", 100), ("Localidad", 90), ("CodigoPostal", 50), ("FechaAlta", 95));
                TraducirColumnas();
                dgvClientes.ClearSelection();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // Alta del cliente
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            try
            {
                int dni;
                int.TryParse(txtDNI.Text.Trim(), out dni);   // si no es un número queda 0 y la BLL lo rechaza
                var cliente = new Cliente
                {
                    DNI = dni,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Telefono = txtTelefono.Text,
                    Email = txtEmail.Text,
                    Direccion = txtDireccion.Text,
                    Localidad = txtLocalidad.Text,
                    CodigoPostal = txtCodigoPostal.Text
                };
                ClienteBLL.Instancia.Registrar(cliente);
                MessageBox.Show(g.Obtener("FormRegistrarCliente", "msgClienteRegistrado"),
                    g.Obtener("FormRegistrarCliente", "msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (_desdeFactura)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                LimpiarCampos();
                CargarClientes();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void LimpiarCampos()
        {
            foreach (var t in new[] { txtDNI, txtNombre, txtApellido, txtTelefono, txtEmail, txtDireccion, txtLocalidad, txtCodigoPostal })
                t.Clear();
            txtDNI.Focus();
        }

        // ---------- A03 Serialización ----------

        // Pasos 3 a 7: serializa los clientes seleccionados en la grilla
        private void btnSerializar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            List<Cliente> seleccionados = dgvClientes.SelectedRows.Cast<DataGridViewRow>()
                .OrderBy(f => f.Index)
                .Select(f => f.DataBoundItem as Cliente)
                .Where(c => c != null)
                .ToList();
            if (seleccionados.Count == 0)
            {
                MostrarError(new ErrorNegocio("SIN_CLIENTES_SELECCIONADOS"));
                return;
            }

            using (SaveFileDialog dialogo = new SaveFileDialog())
            {
                dialogo.Filter = g.Obtener("FormRegistrarCliente", "filtroXml") + " (*.xml)|*.xml";
                dialogo.FileName = "Clientes.xml";
                if (dialogo.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    ClienteBLL.Instancia.Serializar(seleccionados, dialogo.FileName);
                    MessageBox.Show(g.Obtener("FormRegistrarCliente", "msgSerializado"),
                        g.Obtener("FormRegistrarCliente", "msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MostrarError(ex);
                }
            }
        }

        // Escenario alternativo 4.1 a 7.1: muestra en el listbox lo que tiene el archivo (no lo guarda en la base)
        private void btnDeserializar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Filter = g.Obtener("FormRegistrarCliente", "filtroXml") + " (*.xml)|*.xml";
                if (dialogo.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    List<Cliente> clientes = ClienteBLL.Instancia.Deserializar(dialogo.FileName);
                    lstDeserializado.Items.Clear();
                    foreach (Cliente c in clientes)
                        lstDeserializado.Items.Add(c);
                    if (clientes.Count == 0)
                        lstDeserializado.Items.Add(g.Obtener("FormRegistrarCliente", "msgArchivoVacio"));
                }
                catch (Exception ex)
                {
                    MostrarError(ex);
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            if (_desdeFactura)
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
            var g = GestorIdioma.Instancia;
            MessageBox.Show(g.TraducirError(ex), g.Obtener("FormRegistrarCliente", "msgError"),
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ---------- Idioma ----------

        private void TraducirColumnas()
        {
            var g = GestorIdioma.Instancia;
            var columnas = new Dictionary<string, string>
            {
                { "DNI", "colDNI" }, { "Nombre", "colNombre" }, { "Apellido", "colApellido" }, { "Telefono", "colTelefono" },
                { "Email", "colEmail" }, { "Direccion", "colDireccion" }, { "Localidad", "colLocalidad" },
                { "CodigoPostal", "colCodigoPostal" }, { "FechaAlta", "colFechaAlta" }
            };
            foreach (var c in columnas)
                if (dgvClientes.Columns.Contains(c.Key))
                    dgvClientes.Columns[c.Key].HeaderText = g.Obtener("FormRegistrarCliente", c.Value);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormRegistrarCliente"];
            if (t == null) return;

            string titulo = (_desdeFactura ? t["tituloForm"] : t["tituloGestion"])?.ToString();
            this.Text = titulo;
            lblTitulo.Text = titulo;
            lblDatos.Text = t["lblDatos"]?.ToString();
            lblDNI.Text = t["lblDNI"]?.ToString();
            lblNombre.Text = t["lblNombre"]?.ToString();
            lblApellido.Text = t["lblApellido"]?.ToString();
            lblTelefono.Text = t["lblTelefono"]?.ToString();
            lblEmail.Text = t["lblEmail"]?.ToString();
            lblDireccion.Text = t["lblDireccion"]?.ToString();
            lblLocalidad.Text = t["lblLocalidad"]?.ToString();
            lblCodigoPostal.Text = t["lblCodigoPostal"]?.ToString();
            lblClientes.Text = t["lblClientes"]?.ToString();
            lblDeserializado.Text = t["lblDeserializado"]?.ToString();
            btnRegistrar.Text = t["btnRegistrar"]?.ToString();
            btnSerializar.Text = t["btnSerializar"]?.ToString();
            btnDeserializar.Text = t["btnDeserializar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            TraducirColumnas();
        }
    }
}
