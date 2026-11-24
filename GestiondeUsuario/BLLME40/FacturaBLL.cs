using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

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

        // Reporte de facturas (PN1): todas las facturas para consultarlas e imprimirlas
        public List<Factura> ObtenerTodas()
        {
            return new FacturaDAL().ObtenerTodas();
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
        public DataTable ObtenerPendientesDeCaja()
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

        // Revisa que la factura se pueda cobrar con ese monto y forma de pago (sin cobrarla).
        // La usa Cobrar y también AutorizarPagoConTarjeta, antes de pedirle la aprobación al banco.
        public Factura ValidarCobro(int idFactura, decimal monto, string formaPago)
        {
            Factura factura = ObtenerPorId(idFactura);
            if (factura.Estado == Factura.EstadoPagada)
                throw new ErrorNegocio("FACTURA_YA_PAGADA", factura.NroFactura);
            if (!FormasPago.Contains(formaPago))
                throw new ErrorNegocio("FORMA_PAGO_INVALIDA");
            if (monto != factura.Total)
                throw new ErrorNegocio("MONTO_INCORRECTO", factura.Total.ToString("N2"));
            return factura;
        }

        // Pasos 10 a 15: cobra la factura. pagoAcreditado es la confirmación del banco
        // (con tarjeta la da AutorizarPagoConTarjeta; con transferencia, el cajero). El efectivo no la necesita.
        // El monto tiene que ser igual al total. Al cobrar se descuenta el stock (todo o nada).
        public bool Cobrar(int idFactura, decimal monto, string formaPago, bool pagoAcreditado)
        {
            Factura factura = ValidarCobro(idFactura, monto, formaPago);
            if (RequiereConfirmacionBanco(formaPago) && !pagoAcreditado)
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

        // ---------- Pago con tarjeta de débito o crédito (CUN-005) ----------

        public static bool EsPagoConTarjeta(string formaPago)
        {
            return formaPago == "Debito" || formaPago == "Credito";
        }

        // El efectivo se cobra en mano y no pasa por el banco. La transferencia la confirma el banco
        // (el cajero lo verifica) y la tarjeta la autoriza el banco con AutorizarPagoConTarjeta.
        public static bool RequiereConfirmacionBanco(string formaPago)
        {
            return formaPago != "Efectivo";
        }

        // Los datos de la tarjeta no se guardan: solo viajan de la pantalla al banco durante el cobro
        public void ValidarTarjeta(string numero, string vencimiento, string cvv, string titular)
        {
            if (!Regex.IsMatch(numero ?? "", @"^\d{16}$"))
                throw new ErrorNegocio("TARJETA_NUMERO_INVALIDO");
            if (!Regex.IsMatch(cvv ?? "", @"^\d{3}$"))
                throw new ErrorNegocio("TARJETA_CVV_INVALIDO");

            // MM/AA leído a mano: con año de 2 dígitos, .NET Framework toma "30" como 1930
            Match venc = Regex.Match(vencimiento ?? "", @"^(0[1-9]|1[0-2])/(\d{2})$");
            if (!venc.Success)
                throw new ErrorNegocio("TARJETA_VENCIMIENTO_INVALIDO");
            int mes = int.Parse(venc.Groups[1].Value);
            int anio = 2000 + int.Parse(venc.Groups[2].Value);
            // La tarjeta sirve hasta el último día de su mes de vencimiento
            if (new DateTime(anio, mes, 1).AddMonths(1) <= DateTime.Now)
                throw new ErrorNegocio("TARJETA_VENCIDA");

            if (string.IsNullOrWhiteSpace(titular))
                throw new ErrorNegocio("TARJETA_TITULAR_VACIO");
        }

        // Pasos 11 a 13 con tarjeta: se valida el cobro y la tarjeta, y el banco confirma la acreditación.
        // Primero se revisan la factura y el monto, para no pedirle al banco un pago que después no se registra.
        // Devuelve true si el banco aprobó; si lo rechaza, ErrorNegocio PAGO_RECHAZADO.
        public bool AutorizarPagoConTarjeta(int idFactura, decimal monto, string formaPago,
                                            string numero, string vencimiento, string cvv, string titular)
        {
            ValidarCobro(idFactura, monto, formaPago);
            ValidarTarjeta(numero, vencimiento, cvv, titular);

            BancoDAL banco = new BancoDAL();
            if (!banco.AutorizarPago(numero, monto))
                throw new ErrorNegocio("PAGO_RECHAZADO");

            // Solo los últimos 4 dígitos: el número completo no se registra en ningún lado
            GestorEventosBLL.Instancia.Notificar(UsuarioActivo(),
                "Pago con tarjeta aprobado (terminada en " + numero.Substring(numero.Length - 4) + ")", "Ventas", 2);
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
