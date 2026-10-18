using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    // PN2: CUN-009 Generar orden de compra (Supervisor): carga y compara las cotizaciones (paso 4),
    // genera la orden con el proveedor elegido (paso 5) y la envía (paso 7).
    // Si el proveedor no está registrado se extiende a CUN-010 Completar registro de proveedor (paso 6).
    public class OrdenCompraBLL
    {
        private static OrdenCompraBLL _instancia;
        private OrdenCompraBLL() { }
        public static OrdenCompraBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new OrdenCompraBLL();
                return _instancia;
            }
        }

        // Solicitudes con las que trabaja CUN-009: las enviadas (para cotizar y generar órdenes) y las cerradas
        // que todavía tienen órdenes sin enviar al proveedor (paso 7)
        public List<SolicitudCompra> ObtenerSolicitudesEnCurso()
        {
            SolicitudCompraDAL solicitudes = new SolicitudCompraDAL();
            OrdenCompraDAL ordenes = new OrdenCompraDAL();
            var lista = solicitudes.ObtenerPorEstado(SolicitudCompra.EstadoEnviada);
            lista.AddRange(solicitudes.ObtenerPorEstado(SolicitudCompra.EstadoCerrada)
                .Where(s => ordenes.ObtenerPorSolicitud(s.Id).Any(o => o.Estado == OrdenCompra.EstadoEmitida)));
            return lista.OrderBy(s => s.NroSolicitud).ToList();
        }

        // Id de producto -> proveedor al que ya se le pidió en esta solicitud
        public Dictionary<int, string> ProductosPedidos(int idSolicitud)
        {
            var pedidos = new Dictionary<int, string>();
            foreach (OrdenCompra o in ObtenerPorSolicitud(idSolicitud))
                foreach (ItemOrdenCompra i in o.Items)
                    pedidos[i.Producto.Id] = o.Proveedor.RazonSocial;
            return pedidos;
        }

        // ---------- Cotizaciones (paso 4) ----------

        public int RegistrarCotizacion(Cotizacion c)
        {
            SolicitudCompra s = SolicitudCompraBLL.Instancia.ObtenerPorId(c.IdSolicitud);
            if (s.Estado != SolicitudCompra.EstadoEnviada)
                throw new ErrorNegocio("SOLICITUD_NO_ENVIADA", s.NroSolicitud);
            if (c.Proveedor == null)
                throw new ErrorNegocio("PROVEEDOR_NO_ENCONTRADO");
            Proveedor proveedor = ProveedorBLL.Instancia.ObtenerPorId(c.Proveedor.Id);

            OrdenCompraDAL dal = new OrdenCompraDAL();
            if (dal.ObtenerCotizaciones(s.Id).Any(x => x.Proveedor.Id == proveedor.Id))
                throw new ErrorNegocio("COTIZACION_DUPLICADA", proveedor.RazonSocial);
            if (c.Items == null || c.Items.Count == 0)
                throw new ErrorNegocio("COTIZACION_SIN_PRODUCTOS");
            if (c.Items.Select(i => i.Producto?.Id).Distinct().Count() != c.Items.Count)
                throw new ErrorNegocio("PRODUCTO_REPETIDO_SOLICITUD");

            foreach (ItemCotizacion item in c.Items)
            {
                ItemSolicitud pedido = s.Items.FirstOrDefault(i => i.Producto.Id == item.Producto?.Id);
                if (pedido == null)
                    throw new ErrorNegocio("PRODUCTO_NO_EN_SOLICITUD", item.Producto?.Nombre);
                item.Producto = pedido.Producto;
                item.Marca = item.Marca?.Trim();
                item.Presentacion = item.Presentacion?.Trim();
                if (string.IsNullOrEmpty(item.Marca) || string.IsNullOrEmpty(item.Presentacion))
                    throw new ErrorNegocio("COTIZACION_DATOS_INCOMPLETOS");
                if (item.PrecioUnitario <= 0)
                    throw new ErrorNegocio("PRECIO_INVALIDO");
                if (item.CantidadDisponible < 0)
                    throw new ErrorNegocio("CANTIDAD_INVALIDA");
            }
            if (c.PlazoEntregaDias < 0)
                throw new ErrorNegocio("PLAZO_INVALIDO");
            if (!ProveedorBLL.FormasPago.Contains(c.FormaPago))
                throw new ErrorNegocio("FORMA_PAGO_INVALIDA");

            c.Proveedor = proveedor;
            if (c.FechaRecepcion == default(DateTime))
                c.FechaRecepcion = DateTime.Now;
            int id = dal.InsertarCotizacion(c);
            Registrar("Registrar cotización", 2);
            return id;
        }

        public List<Cotizacion> ObtenerCotizaciones(int idSolicitud)
        {
            return new OrdenCompraDAL().ObtenerCotizaciones(idSolicitud);
        }

        // Para comparar: el menor precio cotizado de cada producto (Id de producto -> precio)
        public Dictionary<int, decimal> MejoresPrecios(List<Cotizacion> cotizaciones)
        {
            return cotizaciones.SelectMany(c => c.Items)
                .Where(i => i.CantidadDisponible > 0)
                .GroupBy(i => i.Producto.Id)
                .ToDictionary(g => g.Key, g => g.Min(i => i.PrecioUnitario));
        }

        // ---------- Orden de compra (pasos 5 y 7) ----------

        // Productos de la cotización que se van a pedir: los que el proveedor tiene disponibles
        // y todavía no tienen orden. Cantidad = la solicitada, o lo que tenga el proveedor si es menos.
        public List<ItemOrdenCompra> ItemsAPedir(Cotizacion c, SolicitudCompra s)
        {
            HashSet<int> conOrden = new OrdenCompraDAL().ProductosConOrden(s.Id);
            var items = new List<ItemOrdenCompra>();
            foreach (ItemCotizacion ic in c.Items)
            {
                ItemSolicitud pedido = s.Items.FirstOrDefault(i => i.Producto.Id == ic.Producto.Id);
                if (pedido == null || ic.CantidadDisponible <= 0 || conOrden.Contains(ic.Producto.Id))
                    continue;
                items.Add(new ItemOrdenCompra
                {
                    Producto = ic.Producto,
                    Cantidad = Math.Min(pedido.Cantidad, ic.CantidadDisponible),
                    PrecioUnitario = ic.PrecioUnitario
                });
            }
            return items;
        }

        // Paso 5: el supervisor elige la cotización (proveedor) y genera la orden de compra.
        // Las condiciones vacías toman las del proveedor / cotización.
        public OrdenCompra GenerarOrden(int idCotizacion, string condicionesEntrega, string condicionesPago)
        {
            OrdenCompraDAL dal = new OrdenCompraDAL();
            Cotizacion c = dal.ObtenerCotizacion(idCotizacion);
            if (c == null)
                throw new ErrorNegocio("COTIZACION_NO_ENCONTRADA");
            if (c.Estado != Cotizacion.EstadoRecibida)
                throw new ErrorNegocio("COTIZACION_YA_ACEPTADA", c.Proveedor.RazonSocial);
            SolicitudCompra s = SolicitudCompraBLL.Instancia.ObtenerPorId(c.IdSolicitud);
            if (s.Estado != SolicitudCompra.EstadoEnviada)
                throw new ErrorNegocio("SOLICITUD_NO_ENVIADA", s.NroSolicitud);

            // Paso 6: con un proveedor sin registrar no se emite la orden (extiende a CUN-010)
            Proveedor proveedor = ProveedorBLL.Instancia.ObtenerPorId(c.Proveedor.Id);
            if (!proveedor.EstaRegistrado)
                throw new ErrorNegocio("PROVEEDOR_NO_REGISTRADO", proveedor.RazonSocial);

            List<ItemOrdenCompra> items = ItemsAPedir(c, s);
            if (items.Count == 0)
                throw new ErrorNegocio("ORDEN_SIN_PRODUCTOS", proveedor.RazonSocial);

            string entrega = string.IsNullOrWhiteSpace(condicionesEntrega) ? proveedor.CondicionesEntrega : condicionesEntrega.Trim();
            string pago = string.IsNullOrWhiteSpace(condicionesPago)
                ? c.FormaPago + (string.IsNullOrEmpty(proveedor.CondicionesComerciales) ? "" : " - " + proveedor.CondicionesComerciales)
                : condicionesPago.Trim();
            if (string.IsNullOrWhiteSpace(entrega) || string.IsNullOrWhiteSpace(pago))
                throw new ErrorNegocio("ORDEN_CONDICIONES_INCOMPLETAS");

            var orden = new OrdenCompra
            {
                IdSolicitud = s.Id,
                IdCotizacion = c.Id,
                Proveedor = proveedor,
                FechaEmision = DateTime.Now,
                CondicionesEntrega = entrega,
                CondicionesPago = pago,
                Total = items.Sum(i => i.Subtotal),
                Supervisor = UsuarioActivo(),
                Items = items
            };
            int id = dal.Generar(orden);
            if (id == 0)
                throw new ErrorNegocio("COTIZACION_YA_ACEPTADA", proveedor.RazonSocial);
            Registrar("Generar orden de compra", 3);
            return dal.ObtenerPorId(id);
        }

        // Paso 7: el supervisor envía la orden al proveedor
        public OrdenCompra EnviarOrden(int idOrden)
        {
            OrdenCompra o = ObtenerPorId(idOrden);
            if (o.Estado != OrdenCompra.EstadoEmitida || !new OrdenCompraDAL().MarcarEnviada(idOrden, DateTime.Now))
                throw new ErrorNegocio("ORDEN_ESTADO_INVALIDO", o.NroOrden, o.Estado);
            Registrar("Enviar orden de compra", 2);
            return ObtenerPorId(idOrden);
        }

        public OrdenCompra ObtenerPorId(int idOrden)
        {
            OrdenCompra o = new OrdenCompraDAL().ObtenerPorId(idOrden);
            if (o == null)
                throw new ErrorNegocio("ORDEN_NO_ENCONTRADA");
            return o;
        }

        public List<OrdenCompra> ObtenerPorEstado(string estado)
        {
            return new OrdenCompraDAL().ObtenerPorEstado(estado);
        }

        public List<OrdenCompra> ObtenerPorSolicitud(int idSolicitud)
        {
            return new OrdenCompraDAL().ObtenerPorSolicitud(idSolicitud);
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
