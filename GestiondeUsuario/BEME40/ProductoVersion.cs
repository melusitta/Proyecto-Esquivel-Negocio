using System;

namespace BE
{
    // Control de cambios del maestro de productos: cada alta, modificación, baja, reactivación o restauración
    // guarda una versión con todos los datos del producto en ese momento.
    public class ProductoVersion
    {
        public const string TipoAlta = "Alta";
        public const string TipoModificacion = "Modificacion";
        public const string TipoBaja = "Baja";
        public const string TipoReactivacion = "Reactivacion";
        public const string TipoRestauracion = "Restauracion";

        public int Id { get; set; }
        public int NroVersion { get; set; }
        public string TipoCambio { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
        // Por ejemplo, en una restauración: qué versión se restauró
        public string Detalle { get; set; }
        // Los datos del producto en esta versión (Id = el del producto)
        public Producto Datos { get; set; }
    }
}
