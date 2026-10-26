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
        // Qué puede hacer el usuario: define los consejos de la mascota
        private bool _reponer, _gestionarCompras, _recibir;

        public FormPrincipal()
        {
            InitializeComponent();
            // La primera vez que el usuario entra se le ofrece la visita guiada
            Shown += (s, e) =>
            {
                if (!VisitaGuiada.YaLaVio(SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario))
                    BeginInvoke(new Action(IniciarVisita));
            };
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

            Tema.Aplicar(this);
            pnlLateral.BackColor = Tema.Principal;
            Tema.EstiloMenu(menuStrip1);
            picLogo.Image = Tema.Logo;

            GestorIdioma.Instancia.Suscribir(this);
            Usuario usuario = SessionManager.Instancia.ObtenerUsuarioActivo();
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
            bool tieneReponerProductos = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "ReponerProductos");
            bool tieneGestionarCompras = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "GestionarCompras");
            bool tieneRecibirMercaderia = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "RecibirMercaderia");
            bool tieneReporte = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "Reporte");
            bool tieneReportesVentas = PerfilBLL.Instancia.TienePermiso(usuario.Rol, "ReportesVentas");

            gestionDeUsuariosToolStripMenuItem.Enabled = tieneGestionUsuarios;
            gestionDePerfilesToolStripMenuItem.Enabled = tieneGestionPerfiles;
            gestionDeRolesToolStripMenuItem.Enabled = tieneGestionPerfiles;
            bitacoraEventosToolStripMenuItem.Enabled = tieneVerBitacora;
            PerfilBLL.Instancia.TienePermiso(usuario.Rol, "GestionBackup");

            gestionProductosToolStripMenuItem.Enabled = tieneGestionProductos;
            gestionClientesToolStripMenuItem.Enabled = tieneFacturar;   // Gestión de clientes: Administrador y Cajero
            menuMaestro.Enabled = tieneMaestro || tieneGestionProductos || tieneFacturar;
            // PN1: el Vendedor carga el carrito y el Cajero factura y cobra
            cargarCarritoToolStripMenuItem.Enabled = tieneCargarCarrito;
            generarFacturaToolStripMenuItem.Enabled = tieneFacturar;
            menuVenta.Enabled = tieneVentas || tieneCargarCarrito || tieneFacturar;
            generarSolicitudCompraToolStripMenuItem.Enabled = tieneReponerProductos;
            generarOrdenCompraToolStripMenuItem.Enabled = tieneGestionarCompras;
            registrarProductosRecibidosToolStripMenuItem.Enabled = tieneRecibirMercaderia;
            _reponer = tieneReponerProductos;
            _gestionarCompras = tieneGestionarCompras;
            _recibir = tieneRecibirMercaderia;
            menuCompras.Enabled = tieneCompras || tieneReponerProductos || tieneGestionarCompras || tieneRecibirMercaderia;
            productosMasVendidosToolStripMenuItem.Enabled = tieneReportesVentas;
            // Reportes de cada proceso: facturas (PN1) y recibos de mercadería (PN2)
            reporteFacturasToolStripMenuItem.Enabled = tieneReporte || tieneReportesVentas || tieneFacturar;
            reporteRecibosToolStripMenuItem.Enabled = tieneReporte || tieneRecibirMercaderia || tieneGestionarCompras;
            menuReporte.Enabled = true;   // queda en gris si no tiene ninguna opción habilitada (ver abajo)
            // La ayuda es para todos: cada uno ve solo los temas de lo que puede usar
            menuAyuda.Enabled = true;
            gestionRespaldoToolStripMenuItem1.Enabled =
             PerfilBLL.Instancia.TienePermiso(usuario.Rol, "GestionBackup");

            // Un menú sin ninguna opción habilitada queda en gris y no se puede abrir
            // (por ejemplo, Admin para un Vendedor)
            foreach (ToolStripMenuItem menu in menuStrip1.Items.OfType<ToolStripMenuItem>())
                if (menu.DropDownItems.Count > 0)
                    menu.Enabled = menu.Enabled && menu.DropDownItems.OfType<ToolStripItem>().Any(i => i.Enabled);

            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
        }

        private void iniciarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var g = GestorIdioma.Instancia;
            if (SessionManager.Instancia.HaySesionActiva())
                Aviso.Info(g.Obtener("FormPrincipal", "msgSesionActiva") + SessionManager.Instancia.ObtenerUsuarioActivo().NombreUsuario);
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

        private void gestionClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormRegistrarCliente().Show();
            this.Hide();
        }

        private void gestionProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormGestionProductos().Show();
            this.Hide();
        }
        private void generarSolicitudCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormGenerarSolicitudCompra().Show();
            this.Hide();
        }

        private void generarOrdenCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormGenerarOrdenCompra().Show();
            this.Hide();
        }

        private void registrarProductosRecibidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormRegistrarProductosRecibidos().Show();
            this.Hide();
        }

        private void productosMasVendidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormProductosMasVendidos().Show();
            this.Hide();
        }
        private void reporteFacturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormReporteFacturas().Show();
            this.Hide();
        }

        private void reporteRecibosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormReporteRecibos().Show();
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
        private void verAyudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAyuda.Abrir(Name);
        }

        private void visitaGuiadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IniciarVisita();
        }

        // ---------- Visita guiada ----------

        // Pasos: Plumy, el menú, Usuario, cada menú que el rol tiene habilitado, Ayuda y el final
        private void IniciarVisita()
        {
            var pasos = new List<VisitaGuiada.Paso>
            {
                new VisitaGuiada.Paso { Clave = "Bienvenida", Zona = () => Zona(mascota) },
                new VisitaGuiada.Paso { Clave = "Menu", Zona = () => Zona(menuStrip1) },
                new VisitaGuiada.Paso { Clave = "Usuario", Zona = () => Zona(usuarioToolStripMenuItem) },
            };
            foreach (var menu in new[] { (menuAdmin, "Admin"), (menuMaestro, "Maestro"), (menuVenta, "Venta"), (menuCompras, "Compras"), (menuReporte, "Reporte") })
                if (menu.Item1.Enabled)
                {
                    ToolStripMenuItem item = menu.Item1;
                    pasos.Add(new VisitaGuiada.Paso { Clave = menu.Item2, Zona = () => Zona(item) });
                }
            pasos.Add(new VisitaGuiada.Paso { Clave = "Ayuda", Zona = () => Zona(menuAyuda) });
            pasos.Add(new VisitaGuiada.Paso { Clave = "Fin", Zona = () => Zona(mascota) });
            VisitaGuiada.Iniciar(this, pasos);
        }

        // Rectángulo de un control o de un ítem del menú, en coordenadas de esta ventana
        private Rectangle Zona(Control control)
        {
            return RectangleToClient(control.RectangleToScreen(control.ClientRectangle));
        }

        private Rectangle Zona(ToolStripItem item)
        {
            return RectangleToClient(menuStrip1.RectangleToScreen(item.Bounds));
        }

        private void manualPdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImpresionAyuda.GenerarManual();
        }

        private void cambiarIdiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormCambiarIdioma().Show();
            this.Hide();
        }

        // ---------- Mascota ----------

        // Saludo según la hora y consejos según lo que tiene pendiente el usuario en Compras
        private void MostrarMascota()
        {
            var g = GestorIdioma.Instancia;
            int hora = DateTime.Now.Hour;
            string clave = hora >= 5 && hora < 12 ? "saludoManana" : hora >= 12 && hora < 20 ? "saludoTarde" : "saludoNoche";
            string saludo = string.Format(g.Obtener("FormPrincipal", clave), SessionManager.Instancia.ObtenerUsuarioActivo()?.Nombre);

            var consejos = new List<string>();
            try
            {
                if (_reponer)
                {
                    int aReponer = SolicitudCompraBLL.Instancia.ObtenerProductosAReponer().Count;
                    consejos.Add(aReponer == 0 ? g.Obtener("FormPrincipal", "tipReponerCero") : Contar("tipReponer", aReponer));
                }
                if (_gestionarCompras)
                {
                    int cotizaciones = OrdenCompraBLL.Instancia.ObtenerCotizacionesRecibidas().Count;
                    int sinPagar = OrdenCompraBLL.Instancia.ObtenerPorEstado(BE.OrdenCompra.EstadoEnviada).Count;
                    if (cotizaciones > 0) consejos.Add(Contar("tipCotizaciones", cotizaciones));
                    if (sinPagar > 0) consejos.Add(Contar("tipSinPagar", sinPagar));
                }
                if (_recibir)
                {
                    int aRecibir = RecepcionBLL.Instancia.ObtenerOrdenesARecibir().Count;
                    if (aRecibir > 0) consejos.Add(Contar("tipRecibir", aRecibir));
                }
            }
            catch
            {
                // Si no se pueden contar los pendientes, Plumy igual saluda con los consejos generales
            }
            foreach (string tip in new[] { "tipGeneral1", "tipGeneral2", "tipGeneral3" })
                consejos.Add(g.Obtener("FormPrincipal", tip));
            mascota.Mostrar(saludo, consejos, g.Obtener("FormPrincipal", "pistaMascota"));
        }

        // Con uno solo se usa el texto en singular (clave terminada en "Uno")
        private static string Contar(string clave, int cantidad)
        {
            return string.Format(GestorIdioma.Instancia.Obtener("FormPrincipal", cantidad == 1 ? clave + "Uno" : clave), cantidad);
        }

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormPrincipal"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            MostrarMascota();
            usuarioToolStripMenuItem.Text = t["menuUsuario"]?.ToString();
            menuAdmin.Text = t["menuAdmin"]?.ToString();
            menuMaestro.Text = t["menuMaestro"]?.ToString();
            gestionProductosToolStripMenuItem.Text = t["gestionProductos"]?.ToString();
            gestionClientesToolStripMenuItem.Text = t["gestionClientes"]?.ToString();
            menuVenta.Text = t["menuVenta"]?.ToString();
            cargarCarritoToolStripMenuItem.Text = t["cargarCarrito"]?.ToString();
            generarFacturaToolStripMenuItem.Text = t["generarFactura"]?.ToString();
            menuCompras.Text = t["menuCompras"]?.ToString();
            generarSolicitudCompraToolStripMenuItem.Text = t["generarSolicitudCompra"]?.ToString();
            generarOrdenCompraToolStripMenuItem.Text = t["generarOrdenCompra"]?.ToString();
            registrarProductosRecibidosToolStripMenuItem.Text = t["registrarProductosRecibidos"]?.ToString();
            menuReporte.Text = t["menuReporte"]?.ToString();
            productosMasVendidosToolStripMenuItem.Text = t["productosMasVendidos"]?.ToString();
            reporteFacturasToolStripMenuItem.Text = t["reporteFacturas"]?.ToString();
            reporteRecibosToolStripMenuItem.Text = t["reporteRecibos"]?.ToString();
            menuAyuda.Text = t["menuAyuda"]?.ToString();
            verAyudaToolStripMenuItem.Text = t["verAyuda"]?.ToString();
            manualPdfToolStripMenuItem.Text = t["manualPdf"]?.ToString();
            visitaGuiadaToolStripMenuItem.Text = t["visitaGuiada"]?.ToString();
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
