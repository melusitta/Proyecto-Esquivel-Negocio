using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Bitacora
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Accion { get; set; }
        public DateTime Fecha { get; set; }
        public string Modulo { get; set; }
        public int Criticidad { get; set; }
        // Para mostrar en pantalla, no están en la tabla
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
