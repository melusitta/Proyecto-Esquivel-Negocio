using BE;
using DAL;
using Servicios;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BLL
{
    // CUN-002 Seleccionar productos y ABM de productos (menú Maestro).
    // Control de cambios: cada alta, modificación, baja o reactivación guarda una versión del producto,
    // que se puede consultar y restaurar desde Admin → Control de cambios.
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

        // Categorías que ya se usan (para sugerirlas en el ABM)
        public List<string> ObtenerCategorias()
        {
            return new ProductoDAL().ObtenerTodos()
                .Select(p => p.Categoria).Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct().OrderBy(c => c).ToList();
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
                {
                    GuardarVersion(dal.ObtenerIdPorCodigo(p.CodigoProducto), ProductoVersion.TipoAlta, null);
                    Registrar("Crear Producto", 2);
                }
                return ok;
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                throw new ErrorNegocio("PRODUCTO_CODIGO_DUPLICADO", p.CodigoProducto);
            }
        }

        // El precio nuevo no cambia los carritos ya cargados: cada ítem guarda su precio
        // Compara con lo guardado: si no cambió ningún dato no hay nada que modificar
        public bool HayCambios(Producto p)
        {
            Validar(p);
            Producto actual = new ProductoDAL().ObtenerPorId(p.Id);
            if (actual == null)
                throw new ErrorNegocio("PRODUCTO_NO_ENCONTRADO", p.CodigoProducto);
            return actual.CodigoProducto != p.CodigoProducto || actual.Nombre != p.Nombre || actual.Marca != p.Marca
                || actual.Color != p.Color || actual.Modelo != p.Modelo || actual.PrecioUnitario != p.PrecioUnitario
                || actual.Existencia != p.Existencia || actual.Categoria != p.Categoria || actual.StockMinimo != p.StockMinimo
                || actual.IdProveedorHabitual != p.IdProveedorHabitual;
        }

        // Devuelve false sin guardar (ni registrar en la bitácora) si no cambió nada
        public bool Modificar(Producto p)
        {
            if (!HayCambios(p))
                return false;
            ProductoDAL dal = new ProductoDAL();
            if (dal.ExisteCodigo(p.CodigoProducto, p.Id))
                throw new ErrorNegocio("PRODUCTO_CODIGO_DUPLICADO", p.CodigoProducto);
            try
            {
                bool ok = dal.Modificar(p);
                if (ok)
                {
                    GuardarVersion(p.Id, ProductoVersion.TipoModificacion, null);
                    Registrar("Modificar Producto", 3);
                }
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
            {
                GuardarVersion(id, ProductoVersion.TipoBaja, null);
                Registrar("Baja Producto", 3);
            }
            return ok;
        }

        public bool Reactivar(int id)
        {
            ProductoDAL dal = new ProductoDAL();
            bool ok = dal.CambiarActivo(id, true);
            if (ok)
            {
                GuardarVersion(id, ProductoVersion.TipoReactivacion, null);
                Registrar("Reactivar Producto", 3);
            }
            return ok;
        }

        // ---------- Control de cambios ----------

        private static void GuardarVersion(int idProducto, string tipoCambio, string detalle)
        {
            if (idProducto <= 0) return;
            new ProductoVersionDAL().Guardar(idProducto, tipoCambio,
                SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Desconocido", detalle);
        }

        // Versiones de un producto, de la más nueva a la más vieja
        public List<ProductoVersion> ObtenerVersiones(int idProducto)
        {
            return new ProductoVersionDAL().ObtenerPorProducto(idProducto);
        }

        // Vuelve el producto a los datos de esa versión (incluido si estaba activo o dado de baja) y eso queda
        // como una versión nueva. La existencia no se restaura: el stock cambia con las ventas y las recepciones.
        public void RestaurarVersion(int idVersion)
        {
            ProductoVersionDAL versiones = new ProductoVersionDAL();
            ProductoVersion v = versiones.ObtenerPorId(idVersion);
            if (v == null)
                throw new ErrorNegocio("VERSION_NO_ENCONTRADA");
            ProductoDAL dal = new ProductoDAL();
            Producto actual = dal.ObtenerPorId(v.Datos.Id);
            if (actual == null)
                throw new ErrorNegocio("PRODUCTO_NO_ENCONTRADO", v.Datos.CodigoProducto);

            Producto p = v.Datos;
            p.Existencia = actual.Existencia;
            if (p.CodigoProducto == actual.CodigoProducto && p.Nombre == actual.Nombre && p.Marca == actual.Marca
                && p.Color == actual.Color && p.Modelo == actual.Modelo && p.PrecioUnitario == actual.PrecioUnitario
                && p.Categoria == actual.Categoria && p.StockMinimo == actual.StockMinimo
                && p.IdProveedorHabitual == actual.IdProveedorHabitual && p.Activo == actual.Activo)
                throw new ErrorNegocio("VERSION_YA_ACTUAL", v.NroVersion);
            Validar(p);
            if (dal.ExisteCodigo(p.CodigoProducto, p.Id))
                throw new ErrorNegocio("PRODUCTO_CODIGO_DUPLICADO", p.CodigoProducto);

            dal.Modificar(p);
            if (p.Activo != actual.Activo)
                dal.CambiarActivo(p.Id, p.Activo);
            GuardarVersion(p.Id, ProductoVersion.TipoRestauracion, v.NroVersion.ToString());
            Registrar("Restaurar versión de producto", 4);
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

            // Datos de reposición (PN2)
            p.Categoria = string.IsNullOrWhiteSpace(p.Categoria) ? "General" : p.Categoria.Trim();
            if (p.StockMinimo < 0)
                throw new ErrorNegocio("STOCK_MINIMO_INVALIDO");
            if (p.IdProveedorHabitual.HasValue)
            {
                Proveedor proveedor = new ProveedorDAL().ObtenerPorId(p.IdProveedorHabitual.Value);
                if (proveedor == null || !proveedor.Activo)
                    throw new ErrorNegocio("PROVEEDOR_NO_ENCONTRADO");
            }

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
