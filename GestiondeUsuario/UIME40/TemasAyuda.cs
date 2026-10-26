using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestiondeUsuario
{
    // Temas del manual de ayuda: a qué grupo del menú pertenecen, qué patente hace falta para verlos
    // (los mismos permisos que el menú) y qué pantallas abren ese tema con F1.
    // Los textos de cada tema están en la sección "Ayuda" del idioma.
    public class TemaAyuda
    {
        public string Id { get; }
        public string Grupo { get; }
        public string[] Patentes { get; }       // alcanza con una; vacío = todos los usuarios
        public string[] Formularios { get; }

        public TemaAyuda(string id, string grupo, string[] patentes, params string[] formularios)
        {
            Id = id;
            Grupo = grupo;
            Patentes = patentes;
            Formularios = formularios;
        }
    }

    public static class TemasAyuda
    {
        private static readonly string[] Todos_ = new string[0];

        public static readonly string[] Grupos = { "General", "Admin", "Maestro", "Venta", "Compras", "Reporte" };

        public static readonly List<TemaAyuda> Todos = new List<TemaAyuda>
        {
            new TemaAyuda("primerosPasos", "General", Todos_, "Form1", "FormPrincipal"),
            new TemaAyuda("cambiarContrasena", "General", Todos_, "FormRecuperar"),
            new TemaAyuda("cambiarIdioma", "General", Todos_, "FormCambiarIdioma"),

            new TemaAyuda("gestionUsuarios", "Admin", new[] { "GestionUsuarios" }, "FormGU"),
            new TemaAyuda("gestionFamilias", "Admin", new[] { "GestionPerfiles" }, "FormGestionFamilias"),
            new TemaAyuda("gestionRoles", "Admin", new[] { "GestionPerfiles" }, "FormGestionRoles"),
            new TemaAyuda("bitacora", "Admin", new[] { "VerBitacora" }, "FormBitacora"),
            new TemaAyuda("respaldo", "Admin", new[] { "GestionBackup" }, "FormGestionRespaldo", "FormRestore", "FormInconsistencia"),

            new TemaAyuda("gestionProductos", "Maestro", new[] { "GestionProductos" }, "FormGestionProductos"),
            new TemaAyuda("gestionClientes", "Maestro", new[] { "Facturar" }, "FormRegistrarCliente"),

            new TemaAyuda("cargarCarrito", "Venta", new[] { "CargarCarrito" }, "FormCargarCarrito", "FormSeleccionarProductos"),
            new TemaAyuda("generarFactura", "Venta", new[] { "Facturar" }, "FormGenerarFactura"),
            new TemaAyuda("cobrarVenta", "Venta", new[] { "Facturar" }, "FormCobrarVenta"),

            new TemaAyuda("generarSolicitud", "Compras", new[] { "ReponerProductos" }, "FormGenerarSolicitudCompra", "FormRealizarPreRegistro"),
            new TemaAyuda("generarOrden", "Compras", new[] { "GestionarCompras" }, "FormGenerarOrdenCompra", "FormRegistrarCotizacion", "FormCompletarRegistroProveedor"),
            new TemaAyuda("pagarOrden", "Compras", new[] { "GestionarCompras" }, "FormPagarOrdenCompra"),
            new TemaAyuda("recibirProductos", "Compras", new[] { "RecibirMercaderia" }, "FormRegistrarProductosRecibidos"),

            new TemaAyuda("reporteFacturas", "Reporte", new[] { "Reporte", "ReportesVentas", "Facturar" }, "FormReporteFacturas"),
            new TemaAyuda("reporteRecibos", "Reporte", new[] { "Reporte", "RecibirMercaderia", "GestionarCompras" }, "FormReporteRecibos"),
            new TemaAyuda("productosMasVendidos", "Reporte", new[] { "ReportesVentas" }, "FormProductosMasVendidos"),
        };

        // Los temas que el usuario activo puede usar (sin sesión, solo los generales)
        public static List<TemaAyuda> Disponibles()
        {
            string rol = SessionManager.Instancia.ObtenerUsuarioActivo()?.Rol;
            var permitidos = new Dictionary<string, bool>();
            Func<string, bool> tiene = patente =>
            {
                bool ok;
                if (!permitidos.TryGetValue(patente, out ok))
                    permitidos[patente] = ok = rol != null && PerfilBLL.Instancia.TienePermiso(rol, patente);
                return ok;
            };
            return Todos.Where(t => t.Patentes.Length == 0 || t.Patentes.Any(tiene)).ToList();
        }

        public static TemaAyuda DeFormulario(string formulario)
        {
            return Todos.FirstOrDefault(t => t.Formularios.Contains(formulario));
        }

        // Textos del tema en el idioma activo
        public static string Titulo(TemaAyuda tema)
        {
            return GestorIdioma.Instancia.Seccion("Ayuda")?[tema.Id]?["titulo"]?.ToString() ?? tema.Id;
        }

        public static List<string> Lineas(TemaAyuda tema)
        {
            var texto = GestorIdioma.Instancia.Seccion("Ayuda")?[tema.Id]?["texto"];
            return texto == null ? new List<string>() : texto.Select(l => l.ToString()).ToList();
        }

        public static string NombreGrupo(string grupo)
        {
            return GestorIdioma.Instancia.Obtener("FormAyuda", "grupo" + grupo);
        }
    }
}
