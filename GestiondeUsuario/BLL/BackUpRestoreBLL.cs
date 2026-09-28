using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BackUpRestoreBLL
    {
        private static BackUpRestoreBLL _instancia;
        private BackUpRestoreBLL() { }
        public static BackUpRestoreBLL Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new BackUpRestoreBLL();
                return _instancia;
            }
        }

        public void RealizarBackUp(string carpeta)
        {
            if (string.IsNullOrEmpty(carpeta))
            {
                carpeta = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "GestionUsuarios", "Backups");
            }

            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string nombreArchivo = $"GestionUsuarios_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
            string rutaCompleta = Path.Combine(carpeta, nombreArchivo);
            BackUpRestoreDAL dal = new BackUpRestoreDAL();
            dal.RealizarBackUp(rutaCompleta);
        }

        public void RealizarRestore(string rutaArchivo)
        {
            BackUpRestoreDAL dal = new BackUpRestoreDAL();
            dal.RealizarRestore(rutaArchivo);
            GestorEventosBLL.Instancia.Notificar(
                SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario ?? "Admin",
                "Restore BD", "Administrador", 5);
        }
    }
}

