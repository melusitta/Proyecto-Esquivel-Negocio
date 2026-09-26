using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FamiliaBLL
    {
        private static FamiliaBLL _instancia;
        private FamiliaBLL() { }
        public static FamiliaBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new FamiliaBLL();
                return _instancia;
            }
        }

        public List<Familia> ObtenerTodos()
        {
            FamiliaDAL dal = new FamiliaDAL();
            return dal.ObtenerTodos();
        }

        public bool Insertar(Familia f)
        {
            if (string.IsNullOrEmpty(f.Nombre)) return false;
            FamiliaDAL dal = new FamiliaDAL();
            bool ok = dal.Insertar(f);
            if (ok)
                GestorEventosBLL.Instancia.Notificar(
                    SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                    "Crear Familia", "Familias", 2);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return ok;
        }

        public bool Modificar(Familia f)
        {
            if (string.IsNullOrEmpty(f.Nombre)) return false;
            FamiliaDAL dal = new FamiliaDAL();
            bool ok = dal.Modificar(f);
            if (ok)
                GestorEventosBLL.Instancia.Notificar(
                    SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                    "Modificar Familia", "Familias", 3);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return ok;
        }

        public bool Eliminar(int id)
        {
            FamiliaDAL dal = new FamiliaDAL();
            if (dal.EstaEnUso(id))
                throw new ErrorNegocio("FAMILIA_EN_USO");
            bool ok = dal.Eliminar(id);
            if (ok)
                GestorEventosBLL.Instancia.Notificar(
                    SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                    "Eliminar Familia", "Familias", 2);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return ok;
        }

        public List<Patente> ObtenerPatentes(int idFamilia)
        {
            FamiliaDAL dal = new FamiliaDAL();
            return dal.ObtenerPatentes(idFamilia);
        }

        public List<Familia> ObtenerFamiliasIntegradas(int idFamilia)
        {
            FamiliaDAL dal = new FamiliaDAL();
            return dal.ObtenerFamiliasIntegradas(idFamilia);
        }

        public bool AgregarPatente(int idFamilia, int idPatente)
        {
            FamiliaDAL dal = new FamiliaDAL();

            // Verificamos que no esté ya directo en esta familia
            var patentes = dal.ObtenerPatentes(idFamilia);
            if (patentes.Any(p => p.Id == idPatente))
                throw new ErrorNegocio("PATENTE_YA_EN_FAMILIA");

            // Verificamos que ninguna familia integrada (recursivamente) ya la tenga
            var familiasIntegradas = dal.ObtenerFamiliasIntegradas(idFamilia);
            foreach (var familiaIntegrada in familiasIntegradas)
            {
                if (TienePatenteRecursivo(familiaIntegrada.Id, idPatente, dal))
                {
                    var patente = PatenteBLL.Instancia.ObtenerTodos()
                        .FirstOrDefault(p => p.Id == idPatente);
                    throw new ErrorNegocio("PATENTE_EN_FAMILIA_INTEGRADA", patente?.Nombre, familiaIntegrada.Nombre);
                }
            }

            bool ok = dal.AgregarPatente(idFamilia, idPatente);
            if (ok)
                GestorEventosBLL.Instancia.Notificar(
                    SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                    "Agregar Patente a Familia", "Familias", 3);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return ok;
        }

        private bool TienePatenteRecursivo(int idFamilia, int idPatente, FamiliaDAL dal)
        {
            var patentes = dal.ObtenerPatentes(idFamilia);
            if (patentes.Any(p => p.Id == idPatente))
                return true;

            var familiasIntegradas = dal.ObtenerFamiliasIntegradas(idFamilia);
            foreach (var f in familiasIntegradas)
            {
                if (TienePatenteRecursivo(f.Id, idPatente, dal))
                    return true;
            }
            return false;
        }

        public bool AgregarFamilia(int idFamilia, int idFamiliaIntegrada)
        {
            if (idFamilia == idFamiliaIntegrada)
                throw new ErrorNegocio("FAMILIA_SE_CONTIENE");

            FamiliaDAL dal = new FamiliaDAL();
            var familias = dal.ObtenerFamiliasIntegradas(idFamilia);
            if (familias.Any(f => f.Id == idFamiliaIntegrada))
                throw new ErrorNegocio("FAMILIA_YA_INTEGRADA");

            bool ok = dal.AgregarFamilia(idFamilia, idFamiliaIntegrada);
            if (ok)
                GestorEventosBLL.Instancia.Notificar(
                    SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                    "Agregar Familia a Familia", "Familias", 3);
            DigitoVerificadorBLL.Instancia.RecalcularYGuardar();
            return ok;
        }

        public bool EliminarPatente(int idFamilia, int idPatente)
        {
            FamiliaDAL dal = new FamiliaDAL();
            return dal.EliminarPatente(idFamilia, idPatente);
        }

        public bool EliminarFamiliaIntegrada(int idFamilia, int idFamiliaIntegrada)
        {
            FamiliaDAL dal = new FamiliaDAL();
            return dal.EliminarFamiliaIntegrada(idFamilia, idFamiliaIntegrada);
        }

        public int ObtenerUltimoId()
        {
            FamiliaDAL dal = new FamiliaDAL();
            return dal.ObtenerUltimoId();
        }
    }
}

