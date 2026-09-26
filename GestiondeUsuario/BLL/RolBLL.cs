using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RolBLL
    {
        private static RolBLL _instancia;
        private RolBLL() { }
        public static RolBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new RolBLL();
                return _instancia;
            }
        }

        public List<Rol> ObtenerTodos()
        {
            RolDAL dal = new RolDAL();
            return dal.ObtenerTodos();
        }

        public bool Insertar(Rol r)
        {
            if (string.IsNullOrEmpty(r.Nombre)) return false;
            RolDAL dal = new RolDAL();
            bool ok = dal.Insertar(r);
            if (ok)
                GestorEventosBLL.Instancia.Notificar(
                    SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                    "Crear Rol", "Roles", 2);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return ok;
        }

        public bool Modificar(Rol r)
        {
            if (string.IsNullOrEmpty(r.Nombre)) return false;
            RolDAL dal = new RolDAL();
            bool ok = dal.Modificar(r);
            if (ok)
                GestorEventosBLL.Instancia.Notificar(
                    SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                    "Modificar Rol", "Roles", 3);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return ok;
        }

        public bool Eliminar(int id)
        {
            RolDAL dal = new RolDAL();
            if (dal.EstaEnUso(id))
                throw new ErrorNegocio("ROL_EN_USO");
            foreach (var p in dal.ObtenerPatentes(id))
                dal.EliminarPatente(id, p.Id);
            foreach (var f in dal.ObtenerFamilias(id))
                dal.EliminarFamilia(id, f.Id);
            bool ok = dal.Eliminar(id);
            if (ok)
                GestorEventosBLL.Instancia.Notificar(
                    SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                    "Eliminar Rol", "Roles", 2);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return ok;
        }

        public bool AgregarPatente(int idRol, int idPatente)
        {
            RolDAL dal = new RolDAL();
            FamiliaDAL familiaDAL = new FamiliaDAL();

            // Verificamos que no esté ya directo en el rol
            var patentes = dal.ObtenerPatentes(idRol);
            if (patentes.Any(p => p.Id == idPatente))
                throw new ErrorNegocio("PATENTE_YA_EN_ROL");

            // Verificamos que ninguna familia del rol ya tenga esa patente (recursivo)
            var familias = dal.ObtenerFamilias(idRol);
            foreach (var familia in familias)
            {
                if (TienePatenteRecursivo(familia.Id, idPatente, familiaDAL))
                {
                    var patente = PatenteBLL.Instancia.ObtenerTodos()
                        .FirstOrDefault(p => p.Id == idPatente);
                    throw new ErrorNegocio("PATENTE_EN_FAMILIA_DEL_ROL", patente?.Nombre, familia.Nombre);
                }
            }

            bool ok = dal.AgregarPatente(idRol, idPatente);
            if (ok)
                GestorEventosBLL.Instancia.Notificar(
                    SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                    "Agregar Patente a Rol", "Roles", 3);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return ok;
        }
        private bool TienePatenteRecursivo(int idFamilia, int idPatente, FamiliaDAL familiaDAL)
        {
            var patentes = familiaDAL.ObtenerPatentes(idFamilia);
            if (patentes.Any(p => p.Id == idPatente))
                return true;

            var familiasIntegradas = familiaDAL.ObtenerFamiliasIntegradas(idFamilia);
            foreach (var f in familiasIntegradas)
            {
                if (TienePatenteRecursivo(f.Id, idPatente, familiaDAL))
                    return true;
            }
            return false;
        }
        public bool AgregarFamilia(int idRol, int idFamilia)
        {
            RolDAL dal = new RolDAL();
            var familias = dal.ObtenerFamilias(idRol);
            if (familias.Any(f => f.Id == idFamilia))
                throw new ErrorNegocio("FAMILIA_YA_EN_ROL");

            bool ok = dal.AgregarFamilia(idRol, idFamilia);
            if (ok)
                GestorEventosBLL.Instancia.Notificar(
                    SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                    "Agregar Familia a Rol", "Roles", 3);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return ok;
        }

        public bool EliminarPatente(int idRol, int idPatente)
        {
            RolDAL dal = new RolDAL();
            return dal.EliminarPatente(idRol, idPatente);
        }

        public bool EliminarFamilia(int idRol, int idFamilia)
        {
            RolDAL dal = new RolDAL();
            return dal.EliminarFamilia(idRol, idFamilia);
        }

        public List<Patente> ObtenerPatentes(int idRol)
        {
            RolDAL dal = new RolDAL();
            return dal.ObtenerPatentes(idRol);
        }

        public List<Familia> ObtenerFamilias(int idRol)
        {
            RolDAL dal = new RolDAL();
            return dal.ObtenerFamilias(idRol);
        }
    }
}

