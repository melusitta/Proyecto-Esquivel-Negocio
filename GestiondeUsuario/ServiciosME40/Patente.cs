using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Patente : IComponentePermiso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public Patente() { }
        public Patente(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public bool TieneAcceso(string permiso)
        {
            return Nombre == permiso;
        }

        public override string ToString() => Nombre;
    }
}
