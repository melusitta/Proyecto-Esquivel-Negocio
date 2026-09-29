using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PerfilBLL
    {
        private static PerfilBLL _instancia;

        private PerfilBLL() { }

        public static PerfilBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new PerfilBLL();
                return _instancia;
            }
        }

        public bool TienePermiso(string nombreRol, string nombrePatente)
        {
            var roles = RolBLL.Instancia.ObtenerTodos();
            var rol = roles.FirstOrDefault(r => r.Nombre == nombreRol);
            if (rol == null) return false;

            var patentes = RolBLL.Instancia.ObtenerPatentes(rol.Id);
            if (patentes.Any(p => p.Nombre == nombrePatente))
                return true;

            var familias = RolBLL.Instancia.ObtenerFamilias(rol.Id);
            foreach (var familia in familias)
            {
                if (TienePermisoEnFamilia(familia.Id, nombrePatente))
                    return true;
            }

            return false;
        }

        private bool TienePermisoEnFamilia(int idFamilia, string nombrePatente)
        {
            FamiliaDAL dal = new FamiliaDAL();

            var patentes = dal.ObtenerPatentes(idFamilia);
            if (patentes.Any(p => p.Nombre == nombrePatente))
                return true;

            var familiasIntegradas = dal.ObtenerFamiliasIntegradas(idFamilia);
            foreach (var familia in familiasIntegradas)
            {
                if (TienePermisoEnFamilia(familia.Id, nombrePatente))
                    return true;
            }

            return false;
        }
    }
}
