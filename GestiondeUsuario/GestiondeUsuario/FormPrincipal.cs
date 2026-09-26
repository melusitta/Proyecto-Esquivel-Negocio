using BLL;
using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    public partial class FormPrincipal : Form, IObservadorIdioma
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

            Tema.Aplicar(this);
            pnlLateral.BackColor = Tema.Principal;
            Tema.EstiloMenu(menuStrip1);
            picLogo.Image = Tema.Logo;
            lblBienvenida.ForeColor = Tema.PrincipalOscuro;

            GestorIdioma.Instancia.Suscribir(this);
            Usuario usuario = SessionManager.Instancia.ObtenerUsuarioActivo();
            lblBienvenida.Text = "Bienvenido, " + usuario.Nombre + "!";
            // Cada submenú depende de SU propia patente, no de un permiso genérico
            bool tieneGestionUsuarios = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "GestionUsuarios");
            bool tieneGestionPerfiles = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "GestionPerfiles");
            bool tieneVerBitacora = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "VerBitacora");
            bool tieneMaestro = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "Maestro");
            bool tieneVentas = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "Ventas");
            bool tieneCargarCarrito = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "CargarCarrito");
            bool tieneFacturar = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "Facturar");
            bool tieneGestionProductos = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "GestionProductos");
            bool tieneCompras = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "Compras");
            bool tieneReporte = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "Reporte");
            bool tieneReportesVentas = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "ReportesVentas");
            bool tieneAyuda = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "Ayuda");

            gestionDeUsuariosToolStripMenuItem.Enabled = tieneGestionUsuarios;
            gestionDePerfilesToolStripMenuItem.Enabled = tieneGestionPerfiles;
            gestionDeRolesToolStripMenuItem.Enabled = tieneGestionPerfiles;
            bitacoraEventosToolStripMenuItem.Enabled = tieneVerBitacora;
            PerfilBLL.Instancia.TienePermiso(usuario.Rol, "GestionBackup");

            gestionProductosToolStripMenuItem.Enabled = tieneGestionProductos;
            menuMaestro.Enabled = tieneMaestro || tieneGestionProductos;
            // PN1: el Vendedor carga el carrito y el Cajero factura y cobra
            cargarCarritoToolStripMenuItem.Enabled = tieneCargarCarrito;
            generarFacturaToolStripMenuItem.Enabled = tieneFacturar;
            menuVenta.Enabled = tieneVentas || tieneCargarCarrito || tieneFacturar;
            menuCompras.Enabled = tieneCompras;
            productosMasVendidosToolStripMenuItem.Enabled = tieneReportesVentas;
            menuReporte.Enabled = tieneReporte || tieneReportesVentas;
            menuAyuda.Enabled = tieneAyuda;
            gestionRespaldoToolStripMenuItem1.Enabled =
             PerfilBLL.Instancia.TienePermiso(usuario.Rol, "GestionBackup");

            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
        }

        private void iniciarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (SessionManager.Instancia.HaySesionActiva())
                MessageBox.Show(
                    g.Obtener("FormPrincipal", "msgSesionActiva") +
                    SessionManager.Instancia.ObtenerUsuarioActivo().NombreUsuario,
                    g.Obtener("FormPrincipal", "msgSesionActivaTitulo"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormRecuperar().Show();
            this.Hide();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            DialogResult confirm = MessageBox.Show(
                g.Obtener("FormPrincipal", "msgCerrarSesion"),
                g.Obtener("FormPrincipal", "msgCerrarSesionTitulo"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                UsuarioBLL.Instancia.Logout();
                new Form1().Show();
                this.Close();
            }
        }

        private void gestionDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormGU().Show();
            this.Hide();
        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bitacoraEventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormBitacora().Show();
            this.Hide();
        }

        private void gestionDePerfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormGestionFamilias().Show();
            this.Hide();
        }

        private void gestionDeRolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormGestionRoles().Show();
            this.Hide();
        }

        private void gestionProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormGestionProductos().Show();
            this.Hide();
        }
        private void productosMasVendidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormProductosMasVendidos().Show();
            this.Hide();
        }
        private void cargarCarritoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormCargarCarrito().Show();
            this.Hide();
        }
        private void generarFacturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormGenerarFactura().Show();
            this.Hide();
        }
        private void cambiarIdiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormCambiarIdioma().Show();
            this.Hide();
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormPrincipal"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();

            lblBienvenida.Text = t["lblBienvenida"]?.ToString() + ", " +
            SessionManager.Instancia.ObtenerUsuarioActivo()?.Nombre + "!";
            usuarioToolStripMenuItem.Text = t["menuUsuario"]?.ToString();
            menuAdmin.Text = t["menuAdmin"]?.ToString();
            menuMaestro.Text = t["menuMaestro"]?.ToString();
            gestionProductosToolStripMenuItem.Text = t["gestionProductos"]?.ToString();
            menuVenta.Text = t["menuVenta"]?.ToString();
            cargarCarritoToolStripMenuItem.Text = t["cargarCarrito"]?.ToString();
            generarFacturaToolStripMenuItem.Text = t["generarFactura"]?.ToString();
            menuCompras.Text = t["menuCompras"]?.ToString();
            menuReporte.Text = t["menuReporte"]?.ToString();
            productosMasVendidosToolStripMenuItem.Text = t["productosMasVendidos"]?.ToString();
            menuAyuda.Text = t["menuAyuda"]?.ToString();
            iniciarSesionToolStripMenuItem.Text = t["iniciarSesion"]?.ToString();
            cambiarContraseñaToolStripMenuItem.Text = t["cambiarContraseña"]?.ToString();
            cambiarIdiomaToolStripMenuItem.Text = t["cambiarIdioma"]?.ToString();
            cerrarSesionToolStripMenuItem.Text = t["cerrarSesion"]?.ToString();
            gestionDeUsuariosToolStripMenuItem.Text = t["gestionUsuarios"]?.ToString();
            bitacoraEventosToolStripMenuItem.Text = t["bitacoraEventos"]?.ToString();
            gestionDePerfilesToolStripMenuItem.Text = t["gestionPerfiles"]?.ToString();
            gestionDeRolesToolStripMenuItem.Text = t["gestionRoles"]?.ToString();
            gestionRespaldoToolStripMenuItem1.Text = t["gestionRespaldo"]?.ToString();
        }
        private void FormPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Desuscribirse al cerrar
            GestorIdioma.Instancia.Desuscribir(this);
        }

        private void gestionRespaldoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new FormGestionRespaldo().Show();
            this.Hide();
        }
    }
}
