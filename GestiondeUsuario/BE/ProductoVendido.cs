namespace BE
{
    // Una fila del reporte "Productos más vendidos"
    public class ProductoVendido
    {
        public int Posicion { get; set; }
        public int CodigoProducto { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public int Unidades { get; set; }
        public decimal Monto { get; set; }
        // Porcentaje sobre el total del período, según el criterio de orden (unidades o monto)
        public decimal Porcentaje { get; set; }
    }
}
