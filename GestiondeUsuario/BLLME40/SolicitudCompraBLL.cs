using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    // PN2 (Encargado de compras):
    //   CUN-006 Generar solicitud de compra: arma la solicitud, fija la prioridad y la envía a los proveedores (pasos 1 y 3)
    //   CU-007 Seleccionar productos stock mínimo (include): productos a reponer con su proveedor asignado (paso 1)
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

        // CU-007: productos con stock en el mínimo o por debajo que todavía no están en reposición
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

        // CU-007: revisa la selección (productos a reponer, cada uno con proveedor y cantidad) y la devuelve
        // con los datos actuales de la base. CUN-006 la vuelve a revisar al generar la solicitud.
        public List<ItemSolicitud> ValidarSeleccion(List<ItemSolicitud> items)
        {
            if (items == null || items.Count == 0)
                throw new ErrorNegocio("SOLICITUD_SIN_PRODUCTOS");
            if (items.Select(i => i.Producto?.Id).Distinct().Count() != items.Count)
                throw new ErrorNegocio("PRODUCTO_REPETIDO_SOLICITUD");

            HashSet<int> enCurso = new SolicitudCompraDAL().ProductosEnCurso();
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
            return validados;
        }

        // CUN-006 (pasos 1 y 3): genera la solicitud con la prioridad elegida y queda enviada a los proveedores
        public SolicitudCompra GenerarSolicitud(List<ItemSolicitud> items, string prioridad)
        {
            List<ItemSolicitud> validados = ValidarSeleccion(items);
            if (!SolicitudCompra.Prioridades.Contains(prioridad))
                throw new ErrorNegocio("PRIORIDAD_INVALIDA");

            DateTime ahora = DateTime.Now;
            var solicitud = new SolicitudCompra
            {
                FechaCreacion = ahora,
                FechaEnvio = ahora,
                CreadaPor = UsuarioActivo(),
                Prioridad = prioridad,
                Items = validados
            };
            SolicitudCompraDAL dal = new SolicitudCompraDAL();
            int id = dal.Crear(solicitud);
            Registrar("Generar solicitud de compra", 3);
            return dal.ObtenerPorId(id);
        }

        public SolicitudCompra ObtenerPorId(int id)
        {
            SolicitudCompra s = new SolicitudCompraDAL().ObtenerPorId(id);
            if (s == null)
                throw new ErrorNegocio("SOLICITUD_NO_ENCONTRADA");
            return s;
        }

        public List<SolicitudCompra> ObtenerEnviadas()
        {
            return new SolicitudCompraDAL().ObtenerPorEstado(SolicitudCompra.EstadoEnviada);
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
