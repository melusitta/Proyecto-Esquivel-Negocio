using BE;
using DAL;
using Servicios;
using System.Linq;

namespace BLL
{
    // CUN-001 Cargar carrito (Vendedor)
    public class CarritoBLL
    {
        private static CarritoBLL _instancia;
        private CarritoBLL() { }
        public static CarritoBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new CarritoBLL();
                return _instancia;
            }
        }

        private string UsuarioActivo()
        {
            return SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Desconocido";
        }

        // Crea un carrito vacío para el vendedor que tiene la sesión iniciada
        public Carrito Crear()
        {
            CarritoDAL dal = new CarritoDAL();
            int id = dal.Crear(UsuarioActivo());
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return dal.ObtenerPorId(id);
        }

        public Carrito ObtenerPorId(int idCarrito)
        {
            CarritoDAL dal = new CarritoDAL();
            Carrito carrito = dal.ObtenerPorId(idCarrito);
            if (carrito == null)
                throw new ErrorNegocio("CARRITO_NO_ENCONTRADO");
            return carrito;
        }

        // Carga un producto al carrito. Si ya estaba, suma la cantidad.
        // Valida que la cantidad total no supere la existencia del producto.
        public bool AgregarProducto(int idCarrito, int codigoProducto, int cantidad)
        {
            if (cantidad <= 0)
                throw new ErrorNegocio("CANTIDAD_INVALIDA");

            Carrito carrito = ObtenerAbierto(idCarrito);
            Producto producto = ProductoBLL.Instancia.ObtenerPorCodigo(codigoProducto);

            ItemCarrito existente = carrito.Items.FirstOrDefault(i => i.Producto.Id == producto.Id);
            int cantidadTotal = cantidad + (existente?.Cantidad ?? 0);
            if (cantidadTotal > producto.Existencia)
                throw new ErrorNegocio("STOCK_INSUFICIENTE", producto.Nombre, producto.Existencia);

            // Si el producto ya estaba se respeta el precio que se le comunicó al cliente
            decimal precio = existente?.PrecioUnitario ?? producto.PrecioUnitario;

            CarritoDAL dal = new CarritoDAL();
            bool ok = dal.GuardarItem(idCarrito, producto.Id, cantidadTotal, precio);
            if (ok)
                GestorEventosBLL.Instancia.Notificar(UsuarioActivo(), "Cargar producto al carrito", "Ventas", 1);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return ok;
        }

        public bool QuitarProducto(int idCarrito, int idProducto)
        {
            ObtenerAbierto(idCarrito);
            CarritoDAL dal = new CarritoDAL();
            bool ok = dal.EliminarItem(idCarrito, idProducto);
            if (ok)
                GestorEventosBLL.Instancia.Notificar(UsuarioActivo(), "Quitar producto del carrito", "Ventas", 1);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return ok;
        }

        // Resta una unidad del producto. Si era la última, el producto sale del carrito.
        // Devuelve la cantidad que queda (0 si se quitó).
        public int QuitarUnidad(int idCarrito, int idProducto)
        {
            Carrito carrito = ObtenerAbierto(idCarrito);
            ItemCarrito item = carrito.Items.FirstOrDefault(i => i.Producto.Id == idProducto);
            if (item == null)
                throw new ErrorNegocio("PRODUCTO_NO_EN_CARRITO");

            CarritoDAL dal = new CarritoDAL();
            int queda = item.Cantidad - 1;
            if (queda > 0)
                dal.GuardarItem(idCarrito, idProducto, queda, item.PrecioUnitario);   // mantiene el precio informado
            else
                dal.EliminarItem(idCarrito, idProducto);
            GestorEventosBLL.Instancia.Notificar(UsuarioActivo(), "Quitar unidad del carrito", "Ventas", 1);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return queda;
        }

        // Paso 7: el vendedor asocia el carrito al DNI del cliente y el cliente pasa a la caja
        public bool AsociarCliente(int idCarrito, int dni)
        {
            ClienteBLL.ValidarDNI(dni);
            Carrito carrito = ObtenerAbierto(idCarrito);
            if (carrito.Items.Count == 0)
                throw new ErrorNegocio("CARRITO_VACIO");

            CarritoDAL dal = new CarritoDAL();
            bool ok = dal.Asociar(idCarrito, dni);
            if (!ok)
                throw new ErrorNegocio("CARRITO_NO_EDITABLE");
            GestorEventosBLL.Instancia.Notificar(UsuarioActivo(), "Asociar carrito al cliente", "Ventas", 2);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return ok;
        }

        // Paso 9: el cajero consulta el carrito asociado al DNI del cliente
        public Carrito ObtenerAsociadoPorDNI(int dni)
        {
            ClienteBLL.ValidarDNI(dni);
            CarritoDAL dal = new CarritoDAL();
            Carrito carrito = dal.ObtenerAsociadoPorDNI(dni);
            if (carrito == null)
                throw new ErrorNegocio("CARRITO_NO_ASOCIADO", dni);
            return carrito;
        }

        // Solo se puede modificar un carrito que todavía no se asoció al cliente
        private Carrito ObtenerAbierto(int idCarrito)
        {
            Carrito carrito = ObtenerPorId(idCarrito);
            if (carrito.Estado != Carrito.EstadoAbierto)
                throw new ErrorNegocio("CARRITO_NO_EDITABLE");
            return carrito;
        }
    }
}
