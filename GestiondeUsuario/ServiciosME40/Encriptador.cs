using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Encriptador
    {
        public static string Encriptar(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(texto);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder resultado = new StringBuilder();
                foreach (byte b in hash)
                    resultado.Append(b.ToString("x2"));
                return resultado.ToString();
            }
        }

        public static bool ContraseñaSegura(string contraseña)
        {
            if (contraseña.Length < 8)
                return false;
            if (!contraseña.Any(char.IsUpper))
                return false;
            if (!contraseña.Any(char.IsDigit))
                return false;
            return true;
        }
    }
}

