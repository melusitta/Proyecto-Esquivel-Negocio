using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GestorEventosBLL : IGestorEventos
    {
        private static GestorEventosBLL _instancia;
        private List<IObservador> _observadores = new List<IObservador>();

        private GestorEventosBLL() { }

        public static GestorEventosBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new GestorEventosBLL();
                return _instancia;
            }
        }

        public void Suscribir(IObservador obs) => _observadores.Add(obs);
        public void Desuscribir(IObservador obs) => _observadores.Remove(obs);

        public void Notificar(string usuario, string accion, string modulo, int criticidad)
        {
            foreach (var obs in _observadores)
                obs.Actualizar(usuario, accion, modulo, criticidad);
        }

        public void InicializarObservadores()
        {
            Suscribir(new BitacoraDAL());
        }
    }
}
