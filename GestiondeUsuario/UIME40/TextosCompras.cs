using Servicios;

namespace GestiondeUsuario
{
    // Textos traducidos de los valores del PN2 que se guardan en la base con un código fijo
    // (estados, prioridades, formas de pago). Están en la sección "Compras" de los JSON de idiomas.
    public static class TextosCompras
    {
        public static string Estado(string estado)
        {
            return Traducir("estado_" + estado, estado);
        }

        public static string Prioridad(string prioridad)
        {
            return Traducir("prioridad_" + prioridad, prioridad);
        }

        public static string FormaPago(string formaPago)
        {
            return Traducir("fp_" + (formaPago ?? "").Replace(" ", ""), formaPago);
        }

        public static string Texto(string clave)
        {
            return GestorIdioma.Instancia.Obtener("Compras", clave);
        }

        private static string Traducir(string clave, string siNoHay)
        {
            if (string.IsNullOrEmpty(siNoHay)) return "";
            string t = GestorIdioma.Instancia.Obtener("Compras", clave);
            return string.IsNullOrEmpty(t) || t == clave ? siNoHay : t;
        }
    }

    // Opción de un combo: código que va a la BLL y texto traducido que se muestra
    public class OpcionCodigo
    {
        public string Codigo { get; set; }
        public string Texto { get; set; }
        public override string ToString() { return Texto; }
    }
}
