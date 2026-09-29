using BE;

namespace DAL
{
    // Comunicación con el banco (sistema externo), igual que las otras DAL se comunican con la base.
    public class BancoDAL
    {
        // Simulación de la aprobación bancaria: por ahora el banco siempre aprueba.
        // En producción esto llamaría a la API de una pasarela de pago real con los datos de la tarjeta
        // y el monto, y devolvería si el pago fue aprobado. Los datos de la tarjeta no se guardan.
        public bool AutorizarPago(Tarjeta tarjeta, decimal monto)
        {
            return true;
        }
    }
}
