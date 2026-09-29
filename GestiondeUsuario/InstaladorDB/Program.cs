using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InstaladorDB
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = DetectarInstancia();

            if (connStr == null)
            {
                Console.WriteLine("ERROR: No se detectó ninguna instancia de SQL Server en ejecución.");
                Console.WriteLine("Instalá SQL Server Express y volvé a intentar.");
                Console.WriteLine("\nPresioná una tecla para salir...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Instancia de SQL Server detectada: " + connStr);

            try
            {
                string scriptPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Script_GestionUsuarios.sql");

                if (!File.Exists(scriptPath))
                {
                    Console.WriteLine("No se encontró el script SQL.");
                    Console.WriteLine("Presioná una tecla para salir...");
                    Console.ReadKey();
                    return;
                }

                string script = LeerScriptConEncodingCorrecto(scriptPath);

                string[] comandos = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                bool huboErrores = false;

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    int i = 0;
                    foreach (string cmd in comandos)
                    {
                        i++;
                        if (!string.IsNullOrWhiteSpace(cmd))
                        {
                            try
                            {
                                SqlCommand sqlCmd = new SqlCommand(cmd.Trim(), con);
                                sqlCmd.ExecuteNonQuery();
                            }
                            catch (Exception exBloque)
                            {
                                huboErrores = true;
                                Console.WriteLine($"Error en bloque {i}: {exBloque.Message}");
                                Console.WriteLine($"Contenido del bloque:\n{cmd.Trim()}\n");
                            }
                        }
                    }
                }

                if (huboErrores)
                {
                    Console.WriteLine("La base se creó pero con errores en algunos bloques. Revisá los mensajes de arriba.");
                }
                else
                {
                    Console.WriteLine("Base de datos creada exitosamente, sin errores.");
                }

                Console.WriteLine("\nIMPORTANTE: usá esta connection string en GestionNegocio.exe.config:");
                Console.WriteLine(connStr.Replace("Initial Catalog=master", "Initial Catalog=GestionUsuarios"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear la base de datos: " + ex.Message);
            }

            Console.WriteLine("\nPresioná una tecla para cerrar...");
            Console.ReadKey();
        }

        static string LeerScriptConEncodingCorrecto(string path)
        {
            // Primer intento: UTF-8
            string texto = File.ReadAllText(path, Encoding.UTF8);

            // Si aparecen caracteres de reemplazo (U+FFFD), el archivo NO era UTF-8 real,
            // sino ANSI/Windows-1252 (típico de ñ, tildes guardadas con Bloc de notas viejo o VS por defecto)
            if (texto.Contains('\uFFFD'))
            {
                texto = File.ReadAllText(path, Encoding.GetEncoding(1252));
            }

            // Sacamos BOM si quedó
            texto = texto.TrimStart('\uFEFF', '\u200B');

            return texto;
        }

        static string DetectarInstancia()
        {
            string[] candidatos = new string[]
            {
                @".\SQLEXPRESS",
                @".\SQLEXPRESS01",
                @".",
                Environment.MachineName + @"\SQLEXPRESS",
                Environment.MachineName + @"\SQLEXPRESS01",
                Environment.MachineName,
                @"localhost\SQLEXPRESS",
                @"localhost",
                @"(local)\SQLEXPRESS"
            };

            foreach (string dataSource in candidatos)
            {
                string cs = $"Data Source={dataSource};Initial Catalog=master;Integrated Security=True;Connection Timeout=3";
                if (Probar(cs))
                {
                    return cs;
                }
            }

            return null;
        }

        static bool Probar(string connStr)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}