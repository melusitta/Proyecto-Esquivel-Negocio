using System;

namespace BE
{
    // Bitácora de cambios del maestro de productos: cada alta, modificación, baja o reactivación guarda una versión
    // con todos los datos del producto en ese momento (lo hace el trigger trg_Producto_Audit). De las versiones de un
    // producto una sola es la activa; restaurar es activar una versión vieja.
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
        // ¿Es la versión vigente del producto? (una sola por producto)
        public bool VersionActiva { get; set; }
        // Los datos del producto en esta versión (Id = el del producto)
        public Producto Datos { get; set; }
        // Los datos de la versión anterior (null en la primera), para mostrar qué cambió
        public Producto DatosAnteriores { get; set; }
    }
}
