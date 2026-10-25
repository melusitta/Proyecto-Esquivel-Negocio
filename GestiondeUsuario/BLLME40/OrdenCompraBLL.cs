using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    // PN2: CUN-008 Generar orden de compra (Supervisor): registra las cotizaciones que remiten los proveedores
    // (paso 4), genera la orden a partir de la cotización elegida (paso 5) y se la envía (paso 7).
    // Si el proveedor no tiene datos bancarios se extiende a CUN-009 Completar registro proveedor (paso 6).
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

        // ---------- Cotizaciones (paso 4) ----------

        // Proveedores a los que se les envió la solicitud y todavía no remitieron su cotización
        public List<Proveedor> ProveedoresPorCotizar(int idSolicitud)
        {
            SolicitudCompra s = SolicitudCompraBLL.Instancia.ObtenerPorId(idSolicitud);
            var cotizaron = new OrdenCompraDAL().ObtenerCotizaciones(s.Id).Select(c => c.Proveedor.Id).ToList();
            return SolicitudCompraBLL.Instancia.ProveedoresDe(s).Where(p => !cotizaron.Contains(p.Id)).ToList();
        }

        // Lo que se le pidió a un proveedor en la solicitud: es lo único que puede cotizar
        public List<ItemSolicitud> ItemsPedidosA(int idSolicitud, int idProveedor)
        {
            return SolicitudCompraBLL.Instancia.ObtenerPorId(idSolicitud).Items.Where(i => i.Proveedor.Id == idProveedor).ToList();
        }

        public int RegistrarCotizacion(Cotizacion c)
        {
            SolicitudCompra s = SolicitudCompraBLL.Instancia.ObtenerPorId(c.IdSolicitud);
            if (s.Estado != SolicitudCompra.EstadoEnviada)
                throw new ErrorNegocio("SOLICITUD_NO_ENVIADA", s.NroSolicitud);
            if (c.Proveedor == null)
                throw new ErrorNegocio("PROVEEDOR_NO_ENCONTRADO");
            Proveedor proveedor = ProveedorBLL.Instancia.ObtenerPorId(c.Proveedor.Id);
            if (!s.Items.Any(i => i.Proveedor.Id == proveedor.Id))
                throw new ErrorNegocio("PROVEEDOR_NO_EN_SOLICITUD", proveedor.RazonSocial, s.NroSolicitud.ToString("D6"));

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
                if (pedido.Proveedor.Id != proveedor.Id)
                    throw new ErrorNegocio("PRODUCTO_NO_PEDIDO_A_PROVEEDOR", pedido.Producto.Nombre, proveedor.RazonSocial);
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

        // Las cotizaciones recibidas que todavía no tienen orden: las que el supervisor elige en CUN-008
        public List<Cotizacion> ObtenerCotizacionesRecibidas()
        {
            return new OrdenCompraDAL().ObtenerCotizacionesRecibidas();
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

        // Paso 5: el supervisor elige la cotización y genera la orden con la fecha de emisión y las condiciones
        // de entrega y de pago. Paso 7: al generarla se le envía al proveedor (queda Enviada, lista para pagar).
        public OrdenCompra GenerarOrden(int idCotizacion, DateTime fechaEmision, string condicionesEntrega, string condicionesPago)
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

            // Paso 6: sin datos bancarios no se le puede pagar, así que no se emite la orden (extiende a CUN-009)
            Proveedor proveedor = ProveedorBLL.Instancia.ObtenerPorId(c.Proveedor.Id);
            if (!proveedor.EstaRegistrado)
                throw new ErrorNegocio("PROVEEDOR_NO_REGISTRADO", proveedor.RazonSocial);

            List<ItemOrdenCompra> items = ItemsAPedir(c, s);
            if (items.Count == 0)
                throw new ErrorNegocio("ORDEN_SIN_PRODUCTOS", proveedor.RazonSocial);

            string entrega = condicionesEntrega?.Trim();
            string pago = condicionesPago?.Trim();
            if (string.IsNullOrEmpty(entrega) || string.IsNullOrEmpty(pago))
                throw new ErrorNegocio("ORDEN_CONDICIONES_INCOMPLETAS");
            if (fechaEmision.Date > DateTime.Today)
                throw new ErrorNegocio("FECHA_FUTURA");
            if (fechaEmision.Date < c.FechaRecepcion.Date)
                throw new ErrorNegocio("FECHA_EMISION_ANTERIOR_COTIZACION", c.FechaRecepcion.ToString("dd/MM/yyyy"));

            var orden = new OrdenCompra
            {
                IdSolicitud = s.Id,
                IdCotizacion = c.Id,
                Proveedor = proveedor,
                FechaEmision = fechaEmision.Date == DateTime.Today ? DateTime.Now : fechaEmision.Date,
                FechaEnvio = DateTime.Now,
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
