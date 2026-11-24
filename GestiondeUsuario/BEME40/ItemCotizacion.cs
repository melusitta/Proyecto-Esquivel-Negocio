namespace BE
{
    public class ItemCotizacion
    {
        public Producto Producto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string Marca { get; set; }
        public string Presentacion { get; set; }
        public int CantidadDisponible { get; set; }
    }
}
