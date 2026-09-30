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
            var visitadas = new HashSet<int>();
            foreach (var familia in familias)
            {
                if (TienePermisoEnFamilia(familia.Id, nombrePatente, visitadas))
                    return true;
            }

            return false;
        }

        // "visitadas" evita recorrer dos veces la misma familia: si hubiera un ciclo en los datos
        // (ej.: Ventas -> Vendedor -> Ventas) el recorrido termina en vez de desbordar la pila
        private bool TienePermisoEnFamilia(int idFamilia, string nombrePatente, HashSet<int> visitadas)
        {
            if (!visitadas.Add(idFamilia))
                return false;

            FamiliaDAL dal = new FamiliaDAL();

            var patentes = dal.ObtenerPatentes(idFamilia);
            if (patentes.Any(p => p.Nombre == nombrePatente))
                return true;

            var familiasIntegradas = dal.ObtenerFamiliasIntegradas(idFamilia);
            foreach (var familia in familiasIntegradas)
            {
                if (TienePermisoEnFamilia(familia.Id, nombrePatente, visitadas))
                    return true;
            }

            return false;
        }
    }
}
