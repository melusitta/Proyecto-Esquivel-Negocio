using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    // IGestorEventos.cs
    public interface IGestorEventos
    {
        void Suscribir(IObservador observador);
        void Desuscribir(IObservador observador);
        void Notificar(string usuario, string accion, string modulo, int criticidad);
    }

}
