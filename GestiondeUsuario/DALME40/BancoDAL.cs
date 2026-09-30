using BE;

namespace DAL
{
    // Comunicación con el banco (sistema externo), igual que las otras DAL se comunican con la base.
    public class BancoDAL
    {
        // Simulación de la aprobación bancaria: por ahora el banco siempre aprueba.
        public bool AutorizarPago(Tarjeta tarjeta, decimal monto)
        {
            return true;
        }
    }
}
