using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Familia : IComponentePermiso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        private List<IComponentePermiso> _componentes = new List<IComponentePermiso>();

        public Familia() { }
        public Familia(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public void Agregar(IComponentePermiso componente)
        {
            _componentes.Add(componente);
        }

        public void Quitar(IComponentePermiso componente)
        {
            _componentes.Remove(componente);
        }

        public bool TieneAcceso(string permiso)
        {
            return _componentes.Any(c => c.TieneAcceso(permiso));
        }

        public List<IComponentePermiso> ObtenerComponentes()
        {
            return _componentes;
        }

        public override string ToString() => Nombre;
    }
}
