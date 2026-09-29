using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BitacoraBLL
    {
        private static BitacoraBLL _instancia;

        private BitacoraBLL() { }

        public static BitacoraBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new BitacoraBLL();
                return _instancia;
            }
        }

        // Regla: el Admin ve toda la bitácora; cualquier otro usuario, solo sus propios eventos
        public bool PuedeVerTodaLaBitacora()
        {
            return SessionManager.Instancia.ObtenerUsuarioActivo()?.Rol == "Admin";
        }

        // Login al que queda restringida la consulta (null = sin restricción)
        private string LoginRestringido()
        {
            return PuedeVerTodaLaBitacora() ? null : SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "";
        }

        public List<Bitacora> ObtenerFiltrado(string login, DateTime? fechaIni,
            DateTime? fechaFin, string modulo, string evento, int? criticidad)
        {
            // Aunque la pantalla pida otro login, un usuario que no es Admin solo recibe lo suyo
            string restringido = LoginRestringido();
            if (restringido != null)
                login = restringido;

            if ((fechaIni.HasValue && fechaIni.Value.Date > DateTime.Today) ||
                (fechaFin.HasValue && fechaFin.Value.Date > DateTime.Today))
                throw new ErrorNegocio("FECHA_FUTURA");
            if (fechaIni.HasValue && fechaFin.HasValue && fechaIni.Value.Date > fechaFin.Value.Date)
                throw new ErrorNegocio("FECHAS_INVALIDAS");

            BitacoraDAL dal = new BitacoraDAL();
            return dal.ObtenerFiltrado(login, fechaIni, fechaFin, modulo, evento, criticidad);
        }

        public List<string> ObtenerLogins()
        {
            string restringido = LoginRestringido();
            if (restringido != null)
                return new List<string> { restringido };
            BitacoraDAL dal = new BitacoraDAL();
            return dal.ObtenerLogins();
        }

        // Módulos y eventos que realmente hay registrados (se actualizan solos al sumar funciones nuevas)
        public List<string> ObtenerModulos()
        {
            BitacoraDAL dal = new BitacoraDAL();
            return dal.ObtenerValoresDistintos("Modulo", LoginRestringido());
        }

        public List<string> ObtenerEventos()
        {
            BitacoraDAL dal = new BitacoraDAL();
            return dal.ObtenerValoresDistintos("Accion", LoginRestringido());
        }
    }
}
