using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace InstaladorDB
{
    // Crea la base GestionUsuarios con Script_GestionUsuarios.sql en la instancia de SQL Server que encuentre.
    // Sin argumentos funciona como antes, en consola. El instalador de Plumy lo usa así:
    //   InstaladorDB.exe --silencioso --script <ruta del .sql> --config <ruta de GestionNegocio.exe.config> [--log <archivo>] [--base <nombre>]
    // y además deja configurada la conexión de la aplicación. Si la base ya existe no la toca.
    // Con --log guarda en ese archivo todo lo que muestra (para revisar si algo falló en otra compu).
    // Códigos de salida: 0 base creada, 2 la base ya existía, 10 no hay SQL Server, 11 no está el script,
    // 12 la base se creó con errores, 13 error inesperado.
    class Program
    {
        const int Creada = 0, YaExistia = 2, SinSqlServer = 10, SinScript = 11, ConErrores = 12, ErrorInesperado = 13;

        static bool _silencioso;
        static string _log;

        static int Main(string[] args)
        {
            _silencioso = args.Contains("--silencioso");
            string script = Argumento(args, "--script") ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Script_GestionUsuarios.sql");
            string config = Argumento(args, "--config");
            string baseDatos = Argumento(args, "--base") ?? "GestionUsuarios";
            _log = Argumento(args, "--log");

            int resultado = Instalar(script, config, baseDatos);
            if (!_silencioso)
            {
                Console.WriteLine("\nPresioná una tecla para cerrar...");
                Console.ReadKey();
            }
            return resultado;
        }

        static string Argumento(string[] args, string nombre)
        {
            int i = Array.IndexOf(args, nombre);
            return i >= 0 && i + 1 < args.Length ? args[i + 1] : null;
        }

        static void Mostrar(string texto)
        {
            Console.WriteLine(texto);
            if (_log == null) return;
            try { File.AppendAllText(_log, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + texto + Environment.NewLine, Encoding.UTF8); }
            catch { /* sin permiso para el registro: se sigue igual */ }
        }

        static int Instalar(string scriptPath, string configPath, string baseDatos)
        {
            string connStr = DetectarInstancia();
            if (connStr == null)
            {
                Mostrar("ERROR: No se detectó ninguna instancia de SQL Server en ejecución.");
                Mostrar("Instalá SQL Server Express y volvé a intentar.");
                return SinSqlServer;
            }
            Mostrar("Instancia de SQL Server detectada: " + connStr);
            string connApp = new SqlConnectionStringBuilder(connStr) { InitialCatalog = baseDatos, ConnectTimeout = 15 }.ConnectionString;

            try
            {
                if (ExisteBase(connStr, baseDatos))
                {
                    Mostrar("La base " + baseDatos + " ya existe: se usa tal cual, sin modificarla.");
                    ConfigurarAplicacion(configPath, connApp);
                    return YaExistia;
                }

                if (!File.Exists(scriptPath))
                {
                    Mostrar("No se encontró el script SQL: " + scriptPath);
                    return SinScript;
                }

                string script = LeerScriptConEncodingCorrecto(scriptPath);
                if (baseDatos != "GestionUsuarios")
                    script = script.Replace("DB_ID('GestionUsuarios')", "DB_ID('" + baseDatos + "')")
                                   .Replace("CREATE DATABASE GestionUsuarios ", "CREATE DATABASE " + baseDatos + " ")
                                   .Replace("USE GestionUsuarios\r", "USE " + baseDatos + "\r")
                                   .Replace("USE GestionUsuarios\n", "USE " + baseDatos + "\n");

                string[] comandos = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                bool huboErrores = false;

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    int i = 0;
                    foreach (string cmd in comandos)
                    {
                        i++;
                        if (string.IsNullOrWhiteSpace(cmd)) continue;
                        try
                        {
                            new SqlCommand(cmd.Trim(), con) { CommandTimeout = 120 }.ExecuteNonQuery();
                        }
                        catch (Exception exBloque)
                        {
                            huboErrores = true;
                            Mostrar($"Error en bloque {i}: {exBloque.Message}");
                            Mostrar($"Contenido del bloque:\n{cmd.Trim()}\n");
                        }
                    }
                }

                ConfigurarAplicacion(configPath, connApp);
                if (huboErrores)
                {
                    Mostrar("La base se creó pero con errores en algunos bloques. Revisá los mensajes de arriba.");
                    return ConErrores;
                }
                Mostrar("Base de datos creada exitosamente, sin errores.");
                if (configPath == null)
                {
                    Mostrar("\nIMPORTANTE: usá esta connection string en GestionNegocio.exe.config:");
                    Mostrar(connApp);
                }
                return Creada;
            }
            catch (Exception ex)
            {
                Mostrar("Error al crear la base de datos: " + ex.Message);
                return ErrorInesperado;
            }
        }

        static bool ExisteBase(string connStr, string baseDatos)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT DB_ID(@Nombre)", con);
                cmd.Parameters.AddWithValue("@Nombre", baseDatos);
                return cmd.ExecuteScalar() != DBNull.Value;
            }
        }

        // Deja la connection string "GestionUsuarios" de la aplicación apuntando a la instancia encontrada.
        // Si no se puede escribir (por ejemplo, sin permisos de administrador) la app igual busca sola el SQL Server.
        static void ConfigurarAplicacion(string configPath, string connApp)
        {
            if (configPath == null || !File.Exists(configPath)) return;
            try
            {
                var xml = new XmlDocument { PreserveWhitespace = true };
                xml.Load(configPath);
                var nodo = xml.SelectSingleNode("/configuration/connectionStrings/add[@name='GestionUsuarios']") as XmlElement;
                if (nodo == null) return;
                nodo.SetAttribute("connectionString", connApp);
                xml.Save(configPath);
                Mostrar("Conexión de la aplicación configurada en " + configPath);
            }
            catch (Exception ex)
            {
                Mostrar("No se pudo actualizar la configuración (" + ex.Message + "). La aplicación va a buscar sola el SQL Server.");
            }
        }

        static string LeerScriptConEncodingCorrecto(string path)
        {
            // Primer intento: UTF-8
            string texto = File.ReadAllText(path, Encoding.UTF8);

            // Si aparecen caracteres de reemplazo (U+FFFD), el archivo NO era UTF-8 real,
            // sino ANSI/Windows-1252 (típico de ñ, tildes guardadas con Bloc de notas viejo o VS por defecto)
            if (texto.Contains('�'))
            {
                texto = File.ReadAllText(path, Encoding.GetEncoding(1252));
            }

            // Sacamos BOM si quedó
            texto = texto.TrimStart('﻿', '​');

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
