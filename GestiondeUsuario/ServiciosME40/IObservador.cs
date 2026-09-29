using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public interface IObservador
    {
        void Actualizar(string usuario, string accion, string modulo, int criticidad);
    }

}
