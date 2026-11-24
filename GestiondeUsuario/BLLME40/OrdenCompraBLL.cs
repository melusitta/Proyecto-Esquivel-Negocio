using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BLL
{
    // PN2: CUN-008 Generar orden de compra (Supervisor): registra las cotizaciones que remiten los proveedores
    // (paso 4), genera la orden a partir de la cotización elegida (paso 5) y se la envía (paso 7).
    // Si el proveedor no tiene datos bancarios se extiende a CUN-009 Completar registro proveedor (paso 6).
    // También CUN-010 Pagar orden de compra (incluido en CUN-008): paso 8, el proveedor entrega la factura;
    // paso 9, el supervisor paga la orden y registra la factura con el comprobante.
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
                // La marca es siempre la del producto: el proveedor cotiza ese mismo producto
                item.Marca = pedido.Producto.Marca;
                item.Presentacion = item.Presentacion?.Trim();
                if (string.IsNullOrEmpty(item.Presentacion))
                    throw new ErrorNegocio("COTIZACION_DATOS_INCOMPLETOS");
                if (item.PrecioUnitario <= 0)
                    throw new ErrorNegocio("PRECIO_INVALIDO");
                // Si cotiza el producto tiene que tener unidades; si no tiene stock, se destilda
                if (item.CantidadDisponible <= 0)
                    throw new ErrorNegocio("COTIZACION_SIN_UNIDADES", pedido.Producto.Nombre);
            }
            if (c.PlazoEntregaDias < 0)
                throw new ErrorNegocio("PLAZO_INVALIDO");

            // Las formas de pago que acepta el proveedor quedan con la cotización (al pagar se elige una de ellas)
            c.FormaPago = proveedor.FormaPago ?? "";
            c.Proveedor = proveedor;
            if (c.FechaRecepcion == default(DateTime))
                c.FechaRecepcion = DateTime.Now;
            int id = dal.InsertarCotizacion(c);
            Registrar("Registrar cotización", 2);
            return id;
        }

        // Paso 4: el proveedor responde que no tiene stock de nada de lo que se le pidió. Queda registrada su
        // respuesta (como cotización rechazada, sin productos) y esos productos se pueden volver a pedir a otro.
        public void RegistrarSinStock(int idSolicitud, int idProveedor)
        {
            SolicitudCompra s = SolicitudCompraBLL.Instancia.ObtenerPorId(idSolicitud);
            if (s.Estado != SolicitudCompra.EstadoEnviada)
                throw new ErrorNegocio("SOLICITUD_NO_ENVIADA", s.NroSolicitud);
            Proveedor proveedor = ProveedorBLL.Instancia.ObtenerPorId(idProveedor);
            if (!s.Items.Any(i => i.Proveedor.Id == proveedor.Id))
                throw new ErrorNegocio("PROVEEDOR_NO_EN_SOLICITUD", proveedor.RazonSocial, s.NroSolicitud.ToString("D6"));
            OrdenCompraDAL dal = new OrdenCompraDAL();
            if (dal.ObtenerCotizaciones(s.Id).Any(x => x.Proveedor.Id == proveedor.Id))
                throw new ErrorNegocio("COTIZACION_DUPLICADA", proveedor.RazonSocial);

            dal.InsertarCotizacion(new Cotizacion
            {
                IdSolicitud = s.Id,
                Proveedor = proveedor,
                FechaRecepcion = DateTime.Now,
                PlazoEntregaDias = 0,
                FormaPago = proveedor.FormaPago ?? "",
                Estado = Cotizacion.EstadoRechazada
            });
            Registrar("Registrar cotización sin stock", 2);
        }

        public Cotizacion ObtenerCotizacion(int idCotizacion)
        {
            return new OrdenCompraDAL().ObtenerCotizacion(idCotizacion);
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

        // Paso 5: si la cotización no le conviene (por ejemplo, es cara) el supervisor la descarta en vez de
        // generar la orden. Sus productos quedan sin pedir y vuelven a estar disponibles para otra solicitud.
        public void DescartarCotizacion(int idCotizacion)
        {
            OrdenCompraDAL dal = new OrdenCompraDAL();
            Cotizacion c = dal.ObtenerCotizacion(idCotizacion);
            if (c == null)
                throw new ErrorNegocio("COTIZACION_NO_ENCONTRADA");
            if (c.Estado != Cotizacion.EstadoRecibida || !dal.Descartar(c.Id, c.IdSolicitud))
                throw new ErrorNegocio("COTIZACION_YA_RESUELTA", c.Proveedor.RazonSocial);
            Registrar("Descartar cotización", 2);
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
            if (c.Estado == Cotizacion.EstadoRechazada)
                throw new ErrorNegocio("COTIZACION_DESCARTADA", c.Proveedor.RazonSocial);
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

        // ---------- Pago de la orden (CUN-010, pasos 8 y 9) ----------

        // Formas en que se le puede pagar la orden: las que aceptaba el proveedor al cotizar (si no tenía
        // ninguna cargada, cualquiera)
        public List<string> FormasPagoDeOrden(OrdenCompra orden)
        {
            Cotizacion c = ObtenerCotizacion(orden.IdCotizacion);
            List<string> formas = ProveedorBLL.FormasAceptadas(c?.FormaPago).Where(f => ProveedorBLL.FormasPago.Contains(f)).ToList();
            return formas.Count > 0 ? formas : ProveedorBLL.FormasPago.ToList();
        }

        // Paso 9: importe es el de la factura sin impuestos (tiene que coincidir con el total de la orden);
        // formaPago es la que elige el supervisor entre las que acepta el proveedor
        public OrdenCompra PagarOrden(int idOrden, DateTime fechaFactura, decimal importe, decimal impuestos,
                                      decimal montoAbonado, DateTime fechaPago, string nroComprobante, string formaPago)
        {
            OrdenCompra orden = ObtenerPorId(idOrden);
            if (orden.Estado != OrdenCompra.EstadoEnviada)
                throw new ErrorNegocio("ORDEN_ESTADO_INVALIDO", orden.NroOrden, orden.Estado);

            nroComprobante = nroComprobante?.Trim();
            if (string.IsNullOrEmpty(nroComprobante))
                throw new ErrorNegocio("FACTURA_PROVEEDOR_DATOS_INCOMPLETOS");
            if (string.IsNullOrEmpty(formaPago))
                throw new ErrorNegocio("FORMA_PAGO_INVALIDA");
            if (!FormasPagoDeOrden(orden).Contains(formaPago))
                throw new ErrorNegocio("FORMA_PAGO_NO_ACEPTADA", orden.Proveedor.RazonSocial, formaPago);
            if (fechaFactura.Date > DateTime.Today || fechaPago.Date > DateTime.Today)
                throw new ErrorNegocio("FECHA_FUTURA");
            if (fechaPago.Date < fechaFactura.Date)
                throw new ErrorNegocio("PAGO_ANTERIOR_A_FACTURA");
            if (importe <= 0 || impuestos < 0)
                throw new ErrorNegocio("IMPORTE_INVALIDO");
            // El importe de la factura (sin impuestos) tiene que coincidir con lo acordado en la orden
            if (importe != orden.Total)
                throw new ErrorNegocio("FACTURA_IMPORTE_NO_COINCIDE", orden.Total.ToString("N2"));
            if (montoAbonado != importe + impuestos)
                throw new ErrorNegocio("MONTO_ABONADO_INCORRECTO", (importe + impuestos).ToString("N2"));

            // El número de factura lo asigna el sistema: el siguiente al último de ese proveedor
            OrdenCompraDAL dal = new OrdenCompraDAL();
            orden.NroFacturaProveedor = SiguienteNroFactura(orden.Proveedor.Id);
            if (dal.ExisteNroFactura(orden.Proveedor.Id, orden.NroFacturaProveedor))
                throw new ErrorNegocio("FACTURA_PROVEEDOR_DUPLICADA", orden.NroFacturaProveedor);

            orden.FechaFactura = fechaFactura.Date;
            orden.Impuestos = impuestos;
            orden.MontoAbonado = montoAbonado;
            orden.FechaPago = fechaPago.Date;
            orden.NroComprobante = nroComprobante;
            orden.PagadoPor = UsuarioActivo();
            if (!dal.RegistrarPago(orden, formaPago))
                throw new ErrorNegocio("ORDEN_ESTADO_INVALIDO", orden.NroOrden, orden.Estado);

            Registrar("Pagar orden de compra", 3);
            return dal.ObtenerPorId(idOrden);
        }

        // Paso 8: las facturas de cada proveedor se numeran en forma correlativa; el sistema asigna la siguiente
        // a la última que se le registró (A-0001-00001234 -> A-0001-00001235). La primera es 0001-00000001.
        public string SiguienteNroFactura(int idProveedor)
        {
            string ultimo = new OrdenCompraDAL().UltimoNroFactura(idProveedor);
            Match numero = Regex.Match(ultimo ?? "", @"(\d+)$");
            if (!numero.Success)
                return string.IsNullOrEmpty(ultimo) ? "0001-00000001" : ultimo + "-1";
            string siguiente = (long.Parse(numero.Value) + 1).ToString().PadLeft(numero.Length, '0');
            return ultimo.Substring(0, numero.Index) + siguiente;
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
