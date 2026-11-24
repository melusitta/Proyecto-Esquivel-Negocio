namespace BE
{
    // Producto a reponer con el proveedor al que se le asignó
    public class ItemSolicitud
    {
        public Producto Producto { get; set; }
        public Proveedor Proveedor { get; set; }
        public int Cantidad { get; set; }
    }
}
