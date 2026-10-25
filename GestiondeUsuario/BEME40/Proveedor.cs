using System;

namespace BE
{
    // PN2: lo pre-registra el encargado de compras (CUN-007 Realizar pre-registro) y el supervisor completa
    // su registro con los datos bancarios (CUN-009 Completar registro proveedor)
    public class Proveedor
    {
        public const string EstadoPreRegistrado = "PreRegistrado";
        public const string EstadoRegistrado = "Registrado";

        public int Id { get; set; }
        public string CUIT { get; set; }
        public string RazonSocial { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string CondicionesComerciales { get; set; }
        // Datos comerciales (opcionales en el pre-registro) que el encargado analiza al asignar productos
        public string Rubro { get; set; }
        public string CondicionesEntrega { get; set; }
        public int? PlazoEntregaDias { get; set; }
        public string FormaPago { get; set; }
        // Datos bancarios para pagarle; con ellos queda Registrado
        public string CBU { get; set; }
        public string Banco { get; set; }
        public string Estado { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }

        public bool EstaRegistrado
        {
            get { return Estado == EstadoRegistrado; }
        }

        public bool TieneDatosBancarios
        {
            get { return !string.IsNullOrEmpty(CBU) && !string.IsNullOrEmpty(Banco); }
        }

        public override string ToString()
        {
            return RazonSocial + " (" + CUIT + ")";
        }
    }
}
