using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GestorEventosBLL.Instancia.InicializarObservadores();

            // Si los DV están en cero (primera vez), los inicializamos
            DigitoVerificadorBLL.Instancia.InicializarSiEsNecesario();

            Application.Run(new Form1());
        }
    }
}
