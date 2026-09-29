using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public int DNI { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
        public int IntentosFallidos { get; set; }
        public bool Bloqueado { get; set; }
        public string Rol { get; set; }
        public int IdRol { get; set; }
        public bool PrimerIngreso { get; set; }

        public string Idioma { get; set; }
    }
}
