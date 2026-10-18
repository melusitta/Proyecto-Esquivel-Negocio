using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    // PN2: CUN-012 Registrar productos recibidos (Responsable de depósito).
    // Paso 11: compara la orden con la factura y lo entregado, registra lo recibido y actualiza el stock.
    public class RecepcionBLL
    {
        private static RecepcionBLL _instancia;
        private RecepcionBLL() { }
        public static RecepcionBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new RecepcionBLL();
                return _instancia;
            }
        }

        // Órdenes pagadas que esperan la mercadería
        public List<OrdenCompra> ObtenerOrdenesARecibir()
        {
            return OrdenCompraBLL.Instancia.ObtenerPorEstado(OrdenCompra.EstadoPagada);
        }

        // items: un renglón por cada producto de la orden. Solo lo recibido en buen estado suma al stock.
        public void RegistrarRecepcion(int idOrden, List<ItemRecepcion> items)
        {
            OrdenCompra orden = OrdenCompraBLL.Instancia.ObtenerPorId(idOrden);
            if (orden.Estado != OrdenCompra.EstadoPagada)
                throw new ErrorNegocio("ORDEN_ESTADO_INVALIDO", orden.NroOrden, orden.Estado);
            if (items == null)
                items = new List<ItemRecepcion>();

            foreach (ItemRecepcion item in items)
                if (!orden.Items.Any(i => i.Producto.Id == item.Producto?.Id))
                    throw new ErrorNegocio("PRODUCTO_NO_EN_ORDEN", item.Producto?.Nombre);

            var validados = new List<ItemRecepcion>();
            foreach (ItemOrdenCompra pedido in orden.Items)
            {
                List<ItemRecepcion> renglones = items.Where(i => i.Producto.Id == pedido.Producto.Id).ToList();
                if (renglones.Count != 1)
                    throw new ErrorNegocio("RECEPCION_INCOMPLETA", pedido.Producto.Nombre);
                ItemRecepcion item = renglones[0];
                if (item.CantidadRecibida < 0 || item.CantidadRecibida > pedido.Cantidad)
                    throw new ErrorNegocio("CANTIDAD_RECIBIDA_INVALIDA", pedido.Producto.Nombre, pedido.Cantidad);
                if (!ItemRecepcion.Estados.Contains(item.Estado))
                    throw new ErrorNegocio("ESTADO_RECEPCION_INVALIDO");
                item.Ubicacion = item.Ubicacion?.Trim();
                if (item.CantidadRecibida > 0 && string.IsNullOrEmpty(item.Ubicacion))
                    throw new ErrorNegocio("UBICACION_REQUERIDA", pedido.Producto.Nombre);
                item.Producto = pedido.Producto;
                validados.Add(item);
            }

            var recepcion = new Recepcion
            {
                IdOrdenCompra = orden.Id,
                Fecha = DateTime.Now,
                Responsable = SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Desconocido",
                Items = validados
            };
            if (!new RecepcionDAL().Registrar(recepcion))
                throw new ErrorNegocio("ORDEN_ESTADO_INVALIDO", orden.NroOrden, orden.Estado);

            GestorEventosBLL.Instancia.Notificar(recepcion.Responsable, "Registrar productos recibidos", "Compras", 3);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
        }
    }
}
