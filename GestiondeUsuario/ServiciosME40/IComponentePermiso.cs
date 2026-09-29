using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public interface IComponentePermiso
    {
        string Nombre { get; }
        bool TieneAcceso(string permiso);
    }
}
