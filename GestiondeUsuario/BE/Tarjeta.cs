namespace BE
{
    // Datos de la tarjeta para cobrar con débito o crédito.
    // No se guardan en la base: solo viajan de la pantalla a la BLL y al banco durante el cobro.
    public class Tarjeta
    {
        public string Numero { get; set; }
        public string Vencimiento { get; set; }   // MM/AA
        public string CVV { get; set; }
        public string Titular { get; set; }

        // Lo único que se puede mostrar o registrar de la tarjeta
        public string UltimosCuatro
        {
            get { return Numero != null && Numero.Length >= 4 ? Numero.Substring(Numero.Length - 4) : ""; }
        }
    }
}
