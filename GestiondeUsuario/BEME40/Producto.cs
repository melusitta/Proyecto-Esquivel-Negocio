namespace BE
{
    public class Producto
    {
        public int Id { get; set; }
        public int CodigoProducto { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Color { get; set; }
        public string Modelo { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Existencia { get; set; }
        public bool Activo { get; set; }

        // PN2: datos de reposición
        public string Categoria { get; set; }
        public int StockMinimo { get; set; }
        public int? IdProveedorHabitual { get; set; }
        public string ProveedorHabitual { get; set; }   // razón social, solo para mostrar

        public bool NecesitaReposicion
        {
            get { return Activo && Existencia <= StockMinimo; }
        }

        public override string ToString()
        {
            return CodigoProducto + " - " + Nombre;
        }
    }
}
