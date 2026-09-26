namespace BE
{
    public class ItemCarrito
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        // Precio al momento de cargarlo al carrito (el que se le comunicó al cliente)
        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal
        {
            get { return Cantidad * PrecioUnitario; }
        }
    }
}
