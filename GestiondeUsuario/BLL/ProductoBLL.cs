using BE;
using DAL;
using Servicios;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BLL
{
    // CUN-002 Seleccionar productos y ABM de productos (menú Maestro)
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

        // ---------- ABM de productos (menú Maestro) ----------

        // Activos e inactivos, filtrados por código, nombre o marca
        public List<Producto> ObtenerParaABM(bool incluirInactivos, string texto)
        {
            ProductoDAL dal = new ProductoDAL();
            IEnumerable<Producto> lista = dal.ObtenerTodos();
            if (!incluirInactivos)
                lista = lista.Where(p => p.Activo);
            if (!string.IsNullOrWhiteSpace(texto))
            {
                string t = texto.Trim().ToLower();
                lista = lista.Where(p => p.CodigoProducto.ToString() == t ||
                                         p.Nombre.ToLower().Contains(t) || p.Marca.ToLower().Contains(t));
            }
            return lista.ToList();
        }

        public bool Crear(Producto p)
        {
            Validar(p);
            ProductoDAL dal = new ProductoDAL();
            if (dal.ExisteCodigo(p.CodigoProducto, 0))
                throw new ErrorNegocio("PRODUCTO_CODIGO_DUPLICADO", p.CodigoProducto);
            try
            {
                bool ok = dal.Insertar(p);
                if (ok)
                    Registrar("Crear Producto", 2);
                return ok;
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                throw new ErrorNegocio("PRODUCTO_CODIGO_DUPLICADO", p.CodigoProducto);
            }
        }

        // El precio nuevo no cambia los carritos ya cargados: cada ítem guarda su precio
        public bool Modificar(Producto p)
        {
            Validar(p);
            ProductoDAL dal = new ProductoDAL();
            if (dal.ExisteCodigo(p.CodigoProducto, p.Id))
                throw new ErrorNegocio("PRODUCTO_CODIGO_DUPLICADO", p.CodigoProducto);
            try
            {
                bool ok = dal.Modificar(p);
                if (ok)
                    Registrar("Modificar Producto", 3);
                return ok;
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                throw new ErrorNegocio("PRODUCTO_CODIGO_DUPLICADO", p.CodigoProducto);
            }
        }

        public bool DarDeBaja(int id)
        {
            ProductoDAL dal = new ProductoDAL();
            bool ok = dal.CambiarActivo(id, false);
            if (ok)
                Registrar("Baja Producto", 3);
            return ok;
        }

        public bool Reactivar(int id)
        {
            ProductoDAL dal = new ProductoDAL();
            bool ok = dal.CambiarActivo(id, true);
            if (ok)
                Registrar("Reactivar Producto", 3);
            return ok;
        }

        private void Validar(Producto p)
        {
            if (p.CodigoProducto <= 0)
                throw new ErrorNegocio("CODIGO_PRODUCTO_INVALIDO");
            if (string.IsNullOrWhiteSpace(p.Nombre) || string.IsNullOrWhiteSpace(p.Marca) ||
                string.IsNullOrWhiteSpace(p.Color) || string.IsNullOrWhiteSpace(p.Modelo))
                throw new ErrorNegocio("PRODUCTO_DATOS_INCOMPLETOS");
            if (p.PrecioUnitario <= 0)
                throw new ErrorNegocio("PRECIO_INVALIDO");
            if (p.Existencia < 0)
                throw new ErrorNegocio("EXISTENCIA_INVALIDA");

            p.Nombre = p.Nombre.Trim();
            p.Marca = p.Marca.Trim();
            p.Color = p.Color.Trim();
            p.Modelo = p.Modelo.Trim();
        }

        private void Registrar(string accion, int criticidad)
        {
            GestorEventosBLL.Instancia.Notificar(
                SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Desconocido",
                accion, "Productos", criticidad);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
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
