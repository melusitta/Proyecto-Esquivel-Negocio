namespace BLL
{
    // Qué módulos tiene instalados la base. Plumy se instala en dos versiones:
    //   versión 1: solo los servicios (usuarios, perfiles, bitácora, respaldo, idiomas, ayuda)
    //   versión 2: el reinstalador agrega el negocio (maestro, ventas, compras, reportes y bitácora de cambios)
    // La aplicación es la misma: oculta lo que la base todavía no tiene.
    public static class ModulosBLL
    {
        private static bool? _negocio;

        // Se consulta una sola vez por ejecución (instalar la versión 2 implica volver a abrir la aplicación)
        public static bool NegocioInstalado
        {
            get
            {
                if (_negocio == null)
                    _negocio = ConexionDAL.ExisteTabla("Producto");
                return _negocio.Value;
            }
        }
    }
}
