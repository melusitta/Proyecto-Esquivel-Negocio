using System;

namespace Servicios
{
    // Error de negocio identificado por un código. El texto de cada código está en la
    // sección "Errores" de los JSON de idiomas, así el mensaje se muestra en el idioma activo.
    // Datos son los valores que reemplazan {0}, {1}... en el texto traducido.
    public class ErrorNegocio : Exception
    {
        public string Codigo { get; }
        public object[] Datos { get; }

        public ErrorNegocio(string codigo, params object[] datos) : base(codigo)
        {
            Codigo = codigo;
            Datos = datos ?? new object[0];
        }
    }
}
