using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Servicios
{
    // Serialización XML genérica: convierte objetos a un archivo XML y los vuelve a leer.
    // No conoce el negocio ni la base de datos.
    public static class Serializador
    {
        public static void SerializarXml<T>(T objeto, string ruta, string raiz)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(T), new XmlRootAttribute(raiz));
            using (StreamWriter escritor = new StreamWriter(ruta, false, Encoding.UTF8))
                serializador.Serialize(escritor, objeto);
        }

        // Devuelve el contenido del archivo como objetos. Si el XML no tiene el formato esperado tira ErrorNegocio.
        public static T DeserializarXml<T>(string ruta, string raiz)
        {
            if (!File.Exists(ruta))
                throw new ErrorNegocio("ARCHIVO_NO_ENCONTRADO", Path.GetFileName(ruta));

            XmlSerializer serializador = new XmlSerializer(typeof(T), new XmlRootAttribute(raiz));
            try
            {
                using (StreamReader lector = new StreamReader(ruta, Encoding.UTF8))
                    return (T)serializador.Deserialize(lector);
            }
            catch (InvalidOperationException)
            {
                throw new ErrorNegocio("XML_INVALIDO", Path.GetFileName(ruta));
            }
        }
    }
}
