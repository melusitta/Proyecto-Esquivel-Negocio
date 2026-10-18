using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    // PN2: CUN-006 Seleccionar productos a reponer (Encargado de compras)
    //      CUN-008 Generar solicitud de compra (Supervisor)
    public class SolicitudCompraBLL
    {
        private static SolicitudCompraBLL _instancia;
        private SolicitudCompraBLL() { }
        public static SolicitudCompraBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new SolicitudCompraBLL();
                return _instancia;
            }
        }

        // Paso 1: productos con stock en el mínimo o por debajo que todavía no están en reposición
        public List<Producto> ObtenerProductosAReponer()
        {
            HashSet<int> enCurso = new SolicitudCompraDAL().ProductosEnCurso();
            return new ProductoDAL().ObtenerAReponer().Where(p => !enCurso.Contains(p.Id)).ToList();
        }

        // Se repone hasta el doble del stock mínimo
        public static int CantidadSugerida(Producto p)
        {
            return Math.Max(1, p.StockMinimo * 2 - p.Existencia);
        }

        // Paso 1: el encargado de compras selecciona los productos y le asigna un proveedor a cada uno.
        // Queda una solicitud en Borrador para que el supervisor la genere.
        public SolicitudCompra CrearSolicitud(List<ItemSolicitud> items)
        {
            if (items == null || items.Count == 0)
                throw new ErrorNegocio("SOLICITUD_SIN_PRODUCTOS");
            if (items.Select(i => i.Producto?.Id).Distinct().Count() != items.Count)
                throw new ErrorNegocio("PRODUCTO_REPETIDO_SOLICITUD");

            SolicitudCompraDAL dal = new SolicitudCompraDAL();
            HashSet<int> enCurso = dal.ProductosEnCurso();
            ProductoDAL productos = new ProductoDAL();
            ProveedorDAL proveedores = new ProveedorDAL();
            var validados = new List<ItemSolicitud>();
            foreach (ItemSolicitud item in items)
            {
                Producto producto = item.Producto == null ? null : productos.ObtenerPorId(item.Producto.Id);
                if (producto == null || !producto.Activo)
                    throw new ErrorNegocio("PRODUCTO_NO_ENCONTRADO", item.Producto?.CodigoProducto);
                if (!producto.NecesitaReposicion)
                    throw new ErrorNegocio("PRODUCTO_NO_REQUIERE_REPOSICION", producto.Nombre, producto.Existencia, producto.StockMinimo);
                if (enCurso.Contains(producto.Id))
                    throw new ErrorNegocio("PRODUCTO_YA_EN_REPOSICION", producto.Nombre);
                if (item.Proveedor == null)
                    throw new ErrorNegocio("PRODUCTO_SIN_PROVEEDOR", producto.Nombre);
                Proveedor proveedor = proveedores.ObtenerPorId(item.Proveedor.Id);
                if (proveedor == null || !proveedor.Activo)
                    throw new ErrorNegocio("PROVEEDOR_NO_ENCONTRADO");
                if (item.Cantidad <= 0)
                    throw new ErrorNegocio("CANTIDAD_INVALIDA");
                validados.Add(new ItemSolicitud { Producto = producto, Proveedor = proveedor, Cantidad = item.Cantidad });
            }

            var solicitud = new SolicitudCompra
            {
                FechaCreacion = DateTime.Now,
                CreadaPor = UsuarioActivo(),
                Items = validados
            };
            int id = dal.Crear(solicitud);
            Registrar("Seleccionar productos a reponer", 2);
            return dal.ObtenerPorId(id);
        }

        public SolicitudCompra ObtenerPorId(int id)
        {
            SolicitudCompra s = new SolicitudCompraDAL().ObtenerPorId(id);
            if (s == null)
                throw new ErrorNegocio("SOLICITUD_NO_ENCONTRADA");
            return s;
        }

        public List<SolicitudCompra> ObtenerBorradores()
        {
            return new SolicitudCompraDAL().ObtenerPorEstado(SolicitudCompra.EstadoBorrador);
        }

        public List<SolicitudCompra> ObtenerEnviadas()
        {
            return new SolicitudCompraDAL().ObtenerPorEstado(SolicitudCompra.EstadoEnviada);
        }

        // Paso 3: el supervisor fija las cantidades y la prioridad y la envía a los proveedores.
        // cantidades: Id de producto -> cantidad (los productos que no estén mantienen la cantidad sugerida)
        public SolicitudCompra GenerarSolicitud(int idSolicitud, Dictionary<int, int> cantidades, string prioridad)
        {
            SolicitudCompra s = ObtenerPorId(idSolicitud);
            if (s.Estado != SolicitudCompra.EstadoBorrador)
                throw new ErrorNegocio("SOLICITUD_NO_BORRADOR", s.NroSolicitud);
            if (!SolicitudCompra.Prioridades.Contains(prioridad))
                throw new ErrorNegocio("PRIORIDAD_INVALIDA");

            foreach (ItemSolicitud item in s.Items)
            {
                int cantidad;
                if (cantidades != null && cantidades.TryGetValue(item.Producto.Id, out cantidad))
                    item.Cantidad = cantidad;
                if (item.Cantidad <= 0)
                    throw new ErrorNegocio("CANTIDAD_INVALIDA");
            }

            s.Prioridad = prioridad;
            s.FechaEnvio = DateTime.Now;
            s.Supervisor = UsuarioActivo();
            if (!new SolicitudCompraDAL().Enviar(s))
                throw new ErrorNegocio("SOLICITUD_NO_BORRADOR", s.NroSolicitud);
            Registrar("Generar solicitud de compra", 3);
            return ObtenerPorId(idSolicitud);
        }

        // Proveedores a los que se envía la solicitud (cada uno recibe sus productos)
        public List<Proveedor> ProveedoresDe(SolicitudCompra s)
        {
            return s.Items.Select(i => i.Proveedor).GroupBy(p => p.Id).Select(g => g.First()).ToList();
        }

        private static string UsuarioActivo()
        {
            return SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Desconocido";
        }

        private void Registrar(string accion, int criticidad)
        {
            GestorEventosBLL.Instancia.Notificar(UsuarioActivo(), accion, "Compras", criticidad);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
        }
    }
}
