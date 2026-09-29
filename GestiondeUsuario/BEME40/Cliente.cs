using System;

namespace BE
{
    public class Cliente
    {
        public int Id { get; set; }
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }
        public DateTime FechaAlta { get; set; }

        public string NombreCompleto
        {
            get { return Nombre + " " + Apellido; }
        }

        public override string ToString()
        {
            return "DNI " + DNI + " - " + Apellido + ", " + Nombre + " - " + Email + " - " + Telefono +
                   " - " + Direccion + ", " + Localidad + " (" + CodigoPostal + ")";
        }
    }
}
