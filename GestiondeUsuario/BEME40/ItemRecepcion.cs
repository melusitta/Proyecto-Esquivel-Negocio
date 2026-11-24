namespace BE
{
    public class ItemRecepcion
    {
        public const string EstadoBueno = "Bueno";
        public const string EstadoDanado = "Dañado";

        public static readonly string[] Estados = { EstadoBueno, EstadoDanado };

        public Producto Producto { get; set; }
        public int CantidadRecibida { get; set; }
        public string Estado { get; set; }
        public string Ubicacion { get; set; }
    }
}
