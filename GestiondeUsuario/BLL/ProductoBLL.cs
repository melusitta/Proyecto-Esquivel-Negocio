using BE;
using DAL;
using Servicios;
using System.Collections.Generic;

namespace BLL
{
    // CUN-002 Seleccionar productos
    public class ProductoBLL
    {
        private static ProductoBLL _instancia;
        private ProductoBLL() { }
        public static ProductoBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new ProductoBLL();
                return _instancia;
            }
        }

        public List<Producto> ObtenerTodos()
        {
            ProductoDAL dal = new ProductoDAL();
            return dal.ObtenerActivos();
        }

        // Busca por código de producto, nombre o marca. Sin texto devuelve todos.
        public List<Producto> Buscar(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return ObtenerTodos();
            ProductoDAL dal = new ProductoDAL();
            return dal.Buscar(texto.Trim());
        }

        // Características del producto (nombre, marca, color, modelo, precio y existencia)
        public Producto ObtenerPorCodigo(int codigoProducto)
        {
            ProductoDAL dal = new ProductoDAL();
            Producto producto = dal.ObtenerPorCodigo(codigoProducto);
            if (producto == null)
                throw new ErrorNegocio("PRODUCTO_NO_ENCONTRADO", codigoProducto);
            return producto;
        }
    }
}
