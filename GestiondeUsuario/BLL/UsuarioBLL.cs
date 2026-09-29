using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DAL;
using Servicios;

namespace BLL
{
    public class UsuarioBLL
    {
        private static UsuarioBLL _instancia;

        private UsuarioBLL() { } // constructor privado — corregido del original

        public static UsuarioBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new UsuarioBLL();
                return _instancia;
            }
        }

        public bool Login(string nombreUsuario, string contraseña)
        {
            string passEnc = Encriptador.Encriptar(contraseña);
            UsuarioDAL dal = new UsuarioDAL();
            Usuario usuario = dal.ObtenerPorNombreUsuario(nombreUsuario);

            // Usuario inexistente: no se registra en la bitácora (solo se auditan usuarios reales)
            if (usuario == null)
                return false;

            if (usuario.Bloqueado)
            {
                GestorEventosBLL.Instancia.Notificar(nombreUsuario, "Intento en cuenta bloqueada", "Usuarios", 1);
                throw new ErrorNegocio("USUARIO_BLOQUEADO");
            }

            if (usuario.Contraseña == passEnc)
            {
                usuario.IntentosFallidos = 0;
                dal.ActualizarIntentos(usuario);
                SessionManager.Instancia.IniciarSesion(usuario);
                GestorIdioma.Instancia.CambiarIdioma(usuario.Idioma ?? "español");
                GestorEventosBLL.Instancia.Notificar(nombreUsuario, "Login", "Usuarios", 1);

                // Verificamos integridad de datos
                if (!DigitoVerificadorBLL.Instancia.Verificar())
                {
                    if (usuario.Rol == "Admin")
                        throw new Exception("INCONSISTENCIA_DV"); // Admin ve FormInconsistencia
                    else
                        throw new Exception("SISTEMA_NO_DISPONIBLE"); // Usuario ve mensaje generico
                }

                return true;
            }

            else
            {
                usuario.IntentosFallidos++;
                if (usuario.IntentosFallidos >= 3)
                    usuario.Bloqueado = true;
                dal.ActualizarIntentos(usuario);
                GestorEventosBLL.Instancia.Notificar(nombreUsuario, "Login fallido", "Usuarios", 1);
                return false;
            }
        }

        public bool CambiarContraseña(int dni, string contraseñaActual, string nuevaPass)
        {
            // Política de contraseña segura: la hace cumplir la BLL, sin importar qué pantalla la use
            if (nuevaPass == null || !Encriptador.ContraseñaSegura(nuevaPass))
                throw new ErrorNegocio("CONTRASENA_INSEGURA");

            UsuarioDAL dal = new UsuarioDAL();
            Usuario usuario = dal.ObtenerPorDNI(dni);
            if (usuario == null) return false;

            if (usuario.Contraseña != Encriptador.Encriptar(contraseñaActual))
                return false;

            if (usuario.Contraseña == Encriptador.Encriptar(nuevaPass))
                throw new ErrorNegocio("CONTRASENA_IGUAL_ACTUAL");

            usuario.Contraseña = Encriptador.Encriptar(nuevaPass);
            usuario.PrimerIngreso = false;
            bool ok = dal.ActualizarContraseña(usuario);

            if (ok)
                GestorEventosBLL.Instancia.Notificar(usuario.NombreUsuario, "Cambiar Clave", "Usuarios", 3);
            return ok;
        }

        public bool CrearUsuario(Usuario nuevoUsuario)
        {
            if (string.IsNullOrEmpty(nuevoUsuario.Nombre)) return false;
            if (string.IsNullOrEmpty(nuevoUsuario.Email)) return false;

            // Reglas del alta: el nombre de usuario es Apellido + DNI y la contraseña inicial es el DNI
            nuevoUsuario.NombreUsuario = nuevoUsuario.Apellido + nuevoUsuario.DNI.ToString();
            nuevoUsuario.Contraseña = Encriptador.Encriptar(nuevoUsuario.DNI.ToString());

            UsuarioDAL dal = new UsuarioDAL();
            try
            {
                bool ok = dal.Insertar(nuevoUsuario);
                if (ok)
                {
                    GestorEventosBLL.Instancia.Notificar(
                        SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                        "Crear Usuario", "Administrador", 2);
                    DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
                }
                return ok;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    throw new ErrorNegocio("USUARIO_DUPLICADO");
                throw;
            }

        }
        public Usuario ObtenerPorNombreUsuario(string nombreUsuario)
        {
            UsuarioDAL dal = new UsuarioDAL();
            return dal.ObtenerPorNombreUsuario(nombreUsuario);
        }

        public List<Usuario> ObtenerTodos()
        {
            UsuarioDAL dal = new UsuarioDAL();
            return dal.ObtenerTodos();
        }

        public List<Usuario> ObtenerActivos()
        {
            UsuarioDAL dal = new UsuarioDAL();
            return dal.ObtenerActivos();
        }

        public bool Modificar(Usuario u)
        {
            UsuarioDAL dal = new UsuarioDAL();
            bool ok = dal.Modificar(u);
            if (ok)
            {
                GestorEventosBLL.Instancia.Notificar(
                    SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                    "Modificar Usuario", "Administrador", 3);
                DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            }
            return ok;
        }

        public bool Deshabilitar(int id)
        {
            UsuarioDAL dal = new UsuarioDAL();
            Usuario usuario = dal.ObtenerPorId(id);
            if (usuario == null) return false;

            // Al deshabilitar se resetea la contraseña al DNI (igual que al habilitar y desbloquear)
            string passReseteada = Encriptador.Encriptar(usuario.DNI.ToString());
            bool ok = dal.Deshabilitar(id, passReseteada);
            if (ok)
            {
                GestorEventosBLL.Instancia.Notificar(
                    SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                    "Deshabilitar Usuario", "Administrador", 3);
                DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            }
            return ok;
        }

        public bool Habilitar(int id)
        {
            UsuarioDAL dal = new UsuarioDAL();
            Usuario usuario = dal.ObtenerPorId(id);
            if (usuario == null) return false;

            // Reseteamos contraseña
            string passReseteada = Encriptador.Encriptar(usuario.DNI.ToString());

            bool ok = dal.Habilitar(id, passReseteada);
            if (ok)
            {
                GestorEventosBLL.Instancia.Notificar(
                    SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                    "Habilitar Usuario", "Administrador", 3);
                DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            }
            return ok;
        }

        public bool Desbloquear(int id)
        {
            UsuarioDAL dal = new UsuarioDAL();
            Usuario usuario = dal.ObtenerPorId(id);
            if (usuario == null) return false;

            string passReseteada = Encriptador.Encriptar(usuario.DNI.ToString());
            bool ok = dal.Desbloquear(id, passReseteada);

            if (ok)
            {
                GestorEventosBLL.Instancia.Notificar(
                    SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                    "Desbloquear Usuario", "Administrador", 4);
                DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            }
            return ok;
        }

        public void Logout()
        {
            string nombreUsuario = SessionManager.Instancia
                .ObtenerUsuarioActivo()?.NombreUsuario ?? "Desconocido";
            GestorEventosBLL.Instancia.Notificar(nombreUsuario, "Logout", "Usuarios", 1);
            SessionManager.Instancia.CerrarSesion();
        }
        public bool ActualizarIdioma(int id, string idioma)
        {
            UsuarioDAL dal = new UsuarioDAL();
            bool ok = dal.ActualizarIdioma(id, idioma);
            if (ok)
                GestorEventosBLL.Instancia.Notificar(
                    SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Desconocido",
                    "Cambiar Idioma", "Usuarios", 4);
            return ok;
        }
    }
}