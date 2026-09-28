using BE;
using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // Serializar productos (menú Maestro): los productos de la base se guardan en un archivo XML.
    // Deserializar solo muestra lo que tiene el archivo, no lo guarda en la base.
    // Limpiar descarta lo mostrado y vuelve a mostrar lo que hay en la base.
    public partial class FormSerializarProductos : Form, IObservadorIdioma
    {
        private string _archivoMostrado;   // null = se muestra la base de datos

        public FormSerializarProductos()
        {
            InitializeComponent();
        }

        private void FormSerializarProductos_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            Tema.EstiloPrincipal(btnSerializar);
            txtXml.BackColor = Tema.PrincipalSuave;

            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            MostrarBase();
        }

        private void FormSerializarProductos_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }

        // ---------- Qué se muestra ----------

        private void MostrarBase()
        {
            try
            {
                _archivoMostrado = null;
                txtXml.Clear();
                MostrarEnGrilla(ProductoBLL.Instancia.ObtenerParaABM(true, null));
                MostrarOrigen();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void MostrarEnGrilla(List<Producto> productos)
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = productos;
            if (dgvProductos.Columns.Contains("Id")) dgvProductos.Columns["Id"].Visible = false;
            if (dgvProductos.Columns.Contains("PrecioUnitario"))
                dgvProductos.Columns["PrecioUnitario"].DefaultCellStyle.Format = "N2";
            Tema.AnchoColumnas(dgvProductos, ("CodigoProducto", 60), ("Nombre", 170), ("Marca", 90), ("Color", 75),
                ("Modelo", 110), ("PrecioUnitario", 80), ("Existencia", 75), ("Activo", 55));
            TraducirColumnas();
        }

        private void MostrarOrigen()
        {
            var g = GestorIdioma.Instancia;
            if (_archivoMostrado == null)
            {
                lblOrigen.Text = g.Obtener("FormSerializarProductos", "lblOrigenBase");
                lblOrigen.ForeColor = Tema.PrincipalOscuro;
            }
            else
            {
                lblOrigen.Text = string.Format(g.Obtener("FormSerializarProductos", "lblOrigenArchivo"),
                    Path.GetFileName(_archivoMostrado));
                lblOrigen.ForeColor = Tema.Acento;
            }
        }

        // ---------- Botones ----------

        private void btnSerializar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            using (SaveFileDialog dialogo = new SaveFileDialog())
            {
                dialogo.Filter = g.Obtener("FormSerializarProductos", "filtroXml") + " (*.xml)|*.xml";
                dialogo.FileName = "Productos.xml";
                if (dialogo.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    int cantidad = ProductoBLL.Instancia.Serializar(dialogo.FileName);
                    MostrarBase();
                    txtXml.Text = File.ReadAllText(dialogo.FileName);
                    MessageBox.Show(string.Format(g.Obtener("FormSerializarProductos", "msgSerializado"),
                            cantidad, Path.GetFileName(dialogo.FileName)),
                        g.Obtener("FormSerializarProductos", "msgExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MostrarError(ex);
                }
            }
        }

        private void btnDeserializar_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Filter = g.Obtener("FormSerializarProductos", "filtroXml") + " (*.xml)|*.xml";
                if (dialogo.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    List<Producto> delArchivo = ProductoBLL.Instancia.Deserializar(dialogo.FileName);
                    _archivoMostrado = dialogo.FileName;
                    MostrarEnGrilla(delArchivo);
                    txtXml.Text = File.ReadAllText(dialogo.FileName);
                    MostrarOrigen();
                }
                catch (Exception ex)
                {
                    MostrarError(ex);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            MostrarBase();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            new FormGestionProductos().Show();
            this.Close();
        }

        private void MostrarError(Exception ex)
        {
            var g = GestorIdioma.Instancia;
            MessageBox.Show(g.TraducirError(ex), g.Obtener("FormSerializarProductos", "msgError"),
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // ---------- Idioma ----------

        private void TraducirColumnas()
        {
            var g = GestorIdioma.Instancia;
            var columnas = new Dictionary<string, string>
            {
                { "CodigoProducto", "colCodigo" }, { "Nombre", "colNombre" }, { "Marca", "colMarca" },
                { "Color", "colColor" }, { "Modelo", "colModelo" }, { "PrecioUnitario", "colPrecio" },
                { "Existencia", "colExistencia" }, { "Activo", "colActivo" }
            };
            foreach (var c in columnas)
                if (dgvProductos.Columns.Contains(c.Key))
                    dgvProductos.Columns[c.Key].HeaderText = g.Obtener("FormGestionProductos", c.Value);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormSerializarProductos"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            grpXml.Text = t["grpXml"]?.ToString();
            btnSerializar.Text = t["btnSerializar"]?.ToString();
            btnDeserializar.Text = t["btnDeserializar"]?.ToString();
            btnLimpiar.Text = t["btnLimpiar"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            TraducirColumnas();
            MostrarOrigen();
        }
    }
}
