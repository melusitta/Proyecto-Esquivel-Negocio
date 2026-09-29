using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class SessionManager
    {
        private static SessionManager _instancia; //guarda el unico objeto de esta clase (patron Singleton, igual que en UsuarioBLL)
        private Usuario _usuarioActivo;//guarda la informacion del usuario que ha iniciado sesion. Es privada para que solo pueda ser accedida a traves de los metodos publicos de esta clase.
        private string _idiomaActual = "español"; // idioma por defecto

        private SessionManager() { }
        public static SessionManager Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new SessionManager();
                return _instancia;
            }
        }

        public void IniciarSesion(Usuario usuario)
        {
            _usuarioActivo = usuario; //asigna el usuario que ha iniciado sesion a la variable _usuarioActivo. Esto permite que la aplicacion sepa quien es el usuario activo en todo momento, y pueda acceder a su informacion cuando sea necesario.
        }

        public void CerrarSesion()
        {
            _usuarioActivo = null;//"Olvida" al usuario activo poniendolo en null
        }

        public Usuario ObtenerUsuarioActivo()
        {
            return _usuarioActivo;
        }

        public bool HaySesionActiva()
        {
            return _usuarioActivo != null;
        }
        public string ObtenerIdioma()
        {
            return _idiomaActual;
        }

        public void CambiarIdioma(string idioma)
        {
            _idiomaActual = idioma;
        }
    }
}
