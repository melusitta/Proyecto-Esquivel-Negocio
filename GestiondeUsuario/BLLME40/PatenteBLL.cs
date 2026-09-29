using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PatenteBLL
    {
        private static PatenteBLL _instancia;
        private PatenteBLL() { }
        public static PatenteBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new PatenteBLL();
                return _instancia;
            }
        }

        public List<Patente> ObtenerTodos()
        {
            PatenteDAL dal = new PatenteDAL();
            return dal.ObtenerTodos();
        }
    }
}

