using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    // CUN-003 Generar factura y CUN-005 Cobrar venta (Cajero)
    public class FacturaBLL
    {
        private static FacturaBLL _instancia;
        private FacturaBLL() { }
        public static FacturaBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new FacturaBLL();
                return _instancia;
            }
        }

        // Códigos de forma de pago (la UI los muestra traducidos)
        public static readonly string[] FormasPago = { "Efectivo", "Debito", "Credito", "Transferencia" };

        private string UsuarioActivo()
        {
            return SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Desconocido";
        }

        public Factura ObtenerPorId(int idFactura)
        {
            FacturaDAL dal = new FacturaDAL();
            Factura factura = dal.ObtenerPorId(idFactura);
            if (factura == null)
                throw new ErrorNegocio("FACTURA_NO_ENCONTRADA");
            return factura;
        }

        public Factura ObtenerPorNro(int nroFactura)
        {
            FacturaDAL dal = new FacturaDAL();
            Factura factura = dal.ObtenerPorNro(nroFactura);
            if (factura == null)
                throw new ErrorNegocio("FACTURA_NO_ENCONTRADA");
            return factura;
        }

        // Clientes esperando en la caja (carritos asociados y facturas sin cobrar)
        public List<PendienteCaja> ObtenerPendientesDeCaja()
        {
            FacturaDAL dal = new FacturaDAL();
            return dal.ObtenerPendientesDeCaja();
        }

        // Factura generada y todavía no cobrada para ese DNI (o null). Sirve para retomar el cobro
        // si el cajero cerró la pantalla después de facturar.
        public Factura ObtenerPendientePorDNI(int dni)
        {
            ClienteBLL.ValidarDNI(dni);
            FacturaDAL dal = new FacturaDAL();
            return dal.ObtenerPendientePorDNI(dni);
        }

        // Paso 9: genera la factura del carrito asociado al DNI. El cliente tiene que estar registrado
        // (paso 8). Queda Pendiente de pago hasta que se cobre.
        public Factura Generar(int dni)
        {
            Cliente cliente = ClienteBLL.Instancia.ObtenerPorDNI(dni);
            if (cliente == null)
                throw new ErrorNegocio("CLIENTE_NO_REGISTRADO", dni);

            Carrito carrito = CarritoBLL.Instancia.ObtenerAsociadoPorDNI(dni);
            // La factura respeta los precios con los que se cargó el carrito
            var items = carrito.Items.Select(i => new ItemFactura
            {
                Producto = i.Producto,
                Cantidad = i.Cantidad,
                PrecioUnitario = i.PrecioUnitario
            }).ToList();
            ValidarStock(items.ToArray());

            Factura factura = new Factura
            {
                IdCarrito = carrito.Id,
                IdCliente = cliente.Id,
                DNI = cliente.DNI,
                NombreCliente = cliente.NombreCompleto,
                FechaHora = DateTime.Now,
                Total = carrito.Total,
                Estado = Factura.EstadoPendiente,
                Cajero = UsuarioActivo(),
                Items = items
            };

            FacturaDAL dal = new FacturaDAL();
            int idFactura = dal.Generar(factura);
            if (idFactura == 0)
                throw new ErrorNegocio("CARRITO_NO_ASOCIADO", dni); // otro cajero lo facturó primero

            GestorEventosBLL.Instancia.Notificar(UsuarioActivo(), "Generar factura", "Ventas", 2);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return dal.ObtenerPorId(idFactura);
        }

        // Pasos 10 a 15: cobra la factura. pagoAcreditado simula la confirmación del banco.
        // El monto tiene que ser igual al total. Al cobrar se descuenta el stock (todo o nada).
        public bool Cobrar(int idFactura, decimal monto, string formaPago, bool pagoAcreditado)
        {
            Factura factura = ObtenerPorId(idFactura);
            if (factura.Estado == Factura.EstadoPagada)
                throw new ErrorNegocio("FACTURA_YA_PAGADA", factura.NroFactura);
            if (!FormasPago.Contains(formaPago))
                throw new ErrorNegocio("FORMA_PAGO_INVALIDA");
            if (monto != factura.Total)
                throw new ErrorNegocio("MONTO_INCORRECTO", factura.Total.ToString("N2"));
            if (!pagoAcreditado)
                throw new ErrorNegocio("PAGO_NO_ACREDITADO");
            ValidarStock(factura.Items.ToArray());

            factura.FormaPago = formaPago;
            factura.MontoPagado = monto;
            factura.FechaPago = DateTime.Now;

            FacturaDAL dal = new FacturaDAL();
            int idProductoSinStock;
            if (!dal.Cobrar(factura, out idProductoSinStock))
            {
                if (idProductoSinStock == 0)
                    throw new ErrorNegocio("FACTURA_YA_PAGADA", factura.NroFactura);
                // Alguien vendió ese producto entre la validación y el cobro
                Producto producto = new ProductoDAL().ObtenerPorId(idProductoSinStock);
                throw new ErrorNegocio("STOCK_INSUFICIENTE", producto?.Nombre, producto?.Existencia ?? 0);
            }

            GestorEventosBLL.Instancia.Notificar(UsuarioActivo(), "Cobrar venta", "Ventas", 3);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return true;
        }

        // Revisa contra el stock actual de la base (puede haber cambiado desde que se cargó el carrito)
        private void ValidarStock(ItemFactura[] items)
        {
            ProductoDAL dal = new ProductoDAL();
            foreach (ItemFactura item in items)
            {
                Producto actual = dal.ObtenerPorId(item.Producto.Id);
                int disponible = actual?.Existencia ?? 0;
                if (item.Cantidad > disponible)
                    throw new ErrorNegocio("STOCK_INSUFICIENTE", item.Producto.Nombre, disponible);
            }
        }
    }
}
