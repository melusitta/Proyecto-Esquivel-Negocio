using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace InstaladorDB
{
    // Crea la base GestionUsuarios con Script_GestionUsuarios.sql en la instancia de SQL Server que se elija.
    // Sin --silencioso funciona en consola: lista las instancias, se elige una y pregunta si reemplazar una base existente.
    // El instalador de Plumy lo usa así:
    //   --listar [--salida <archivo>]     instancias encontradas, una por línea: "fuente|descripción" (en el archivo, en UTF-8)
    //   --existe <instancia>             0 = conecta y no hay base, 2 = conecta y la base existe, 1 = no conecta
    //   --silencioso --instancia <inst> [--reemplazar] --script <.sql> --config <GestionNegocio.exe.config> [--log <archivo>] [--base <nombre>]
    // Si el servicio de la instancia está detenido, intenta iniciarlo. Antes de reemplazar una base guarda una copia (.bak).
    // Códigos de salida de la instalación: 0 base creada, 2 la base ya existía y se dejó como estaba, 3 base reemplazada,
    // 10 no se pudo conectar, 11 no está el script, 12 la base se creó con errores, 13 error inesperado.
    class Program
    {
        const int Creada = 0, YaExistia = 2, Reemplazada = 3, SinConexion = 10, SinScript = 11, ConErrores = 12, ErrorInesperado = 13;

        static bool _silencioso;
        static string _log;

        static int Main(string[] args)
        {
            _silencioso = args.Contains("--silencioso");
            _log = Argumento(args, "--log");
            string baseDatos = Argumento(args, "--base") ?? "GestionUsuarios";

            if (args.Contains("--listar"))
            {
                string[] lineas = BuscarInstancias().Select(i => i.Fuente + "|" + i.Descripcion).ToArray();
                string salida = Argumento(args, "--salida");
                if (salida != null)
                    File.WriteAllLines(salida, lineas, Encoding.UTF8);
                else
                    foreach (string l in lineas) Console.WriteLine(l);
                return 0;
            }
            string aProbar = Argumento(args, "--existe");
            if (aProbar != null)
                return Probar(aProbar, baseDatos);

            string script = Argumento(args, "--script") ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Script_GestionUsuarios.sql");
            string config = Argumento(args, "--config");
            int resultado;
            if (_silencioso)
                resultado = Instalar(Argumento(args, "--instancia"), args.Contains("--reemplazar"), script, config, baseDatos);
            else
                resultado = InstalarEnConsola(script, config, baseDatos);
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

        // ---------- Instancias de SQL Server ----------

        class Instancia
        {
            public string Fuente;        // lo que va en "Data Source"
            public string Servicio;      // servicio de Windows, o null (LocalDB)
            public string Descripcion;
        }

        // Instancias registradas en Windows (64 y 32 bits), con el estado de su servicio, y LocalDB si está instalado
        static List<Instancia> BuscarInstancias()
        {
            var lista = new List<Instancia>();
            foreach (RegistryView vista in new[] { RegistryView.Registry64, RegistryView.Registry32 })
            {
                try
                {
                    using (var raiz = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, vista))
                    using (var clave = raiz.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL"))
                    {
                        if (clave == null) continue;
                        foreach (string nombre in clave.GetValueNames())
                        {
                            bool predeterminada = nombre.Equals("MSSQLSERVER", StringComparison.OrdinalIgnoreCase);
                            string fuente = predeterminada ? "." : @".\" + nombre;
                            if (lista.Any(i => i.Fuente.Equals(fuente, StringComparison.OrdinalIgnoreCase))) continue;
                            string servicio = predeterminada ? "MSSQLSERVER" : "MSSQL$" + nombre;
                            lista.Add(new Instancia
                            {
                                Fuente = fuente,
                                Servicio = servicio,
                                Descripcion = (predeterminada ? "Instancia predeterminada (MSSQLSERVER)" : "Instancia " + nombre) + " · " + EstadoServicio(servicio)
                            });
                        }
                    }
                }
                catch { /* sin acceso a esa vista del registro */ }
            }
            try
            {
                using (var raiz = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                using (var clave = raiz.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server Local DB\Installed Versions"))
                    if (clave != null && clave.GetSubKeyNames().Length > 0)
                        lista.Add(new Instancia { Fuente = @"(localdb)\MSSQLLocalDB", Descripcion = "SQL Server LocalDB (el de Visual Studio)" });
            }
            catch { }
            return lista;
        }

        static string EstadoServicio(string servicio)
        {
            try
            {
                using (var sc = new ServiceController(servicio))
                    return sc.Status == ServiceControllerStatus.Running ? "en ejecución" : "detenida";
            }
            catch
            {
                return "sin servicio";
            }
        }

        // Si el servicio de la instancia está detenido, se intenta iniciar (el instalador corre como administrador)
        static void IniciarSiHaceFalta(string fuente)
        {
            string servicio = ServicioDe(fuente);
            if (servicio == null) return;
            try
            {
                using (var sc = new ServiceController(servicio))
                {
                    if (sc.Status == ServiceControllerStatus.Running) return;
                    Mostrar("El servicio " + servicio + " está detenido: se intenta iniciarlo...");
                    if (sc.Status != ServiceControllerStatus.StartPending)
                        sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(40));
                    Mostrar("Servicio " + servicio + " iniciado.");
                }
            }
            catch (Exception ex)
            {
                Mostrar("No se pudo iniciar el servicio " + servicio + ": " + ex.Message);
            }
        }

        static string ServicioDe(string fuente)
        {
            string f = fuente.Trim();
            if (f.StartsWith("(localdb)", StringComparison.OrdinalIgnoreCase)) return null;
            string equipo = Environment.MachineName;
            Match m = Regex.Match(f, @"^(\.|\(local\)|localhost|" + Regex.Escape(equipo) + @")(\\(?<nombre>.+))?$", RegexOptions.IgnoreCase);
            if (!m.Success) return null;   // instancia de otra computadora
            string nombre = m.Groups["nombre"].Value;
            return nombre.Length == 0 || nombre.Equals("MSSQLSERVER", StringComparison.OrdinalIgnoreCase) ? "MSSQLSERVER" : "MSSQL$" + nombre;
        }

        static string Conexion(string fuente, string catalogo)
        {
            int espera = fuente.StartsWith("(localdb)", StringComparison.OrdinalIgnoreCase) ? 30 : 8;   // LocalDB tarda en arrancar
            return new SqlConnectionStringBuilder { DataSource = fuente, InitialCatalog = catalogo, IntegratedSecurity = true, ConnectTimeout = espera }.ConnectionString;
        }

        // 0 = conecta y no hay base, 2 = conecta y la base existe, 1 = no conecta
        static int Probar(string fuente, string baseDatos)
        {
            IniciarSiHaceFalta(fuente);
            try
            {
                bool existe = ExisteBase(Conexion(fuente, "master"), baseDatos);
                Console.WriteLine(existe ? "existe" : "no existe");
                return existe ? 2 : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.Replace("\r", " ").Replace("\n", " "));
                return 1;
            }
        }

        // ---------- Instalación ----------

        static int InstalarEnConsola(string script, string config, string baseDatos)
        {
            List<Instancia> instancias = BuscarInstancias();
            if (instancias.Count == 0)
                Console.WriteLine("No se encontró ninguna instancia de SQL Server instalada.");
            else
            {
                Console.WriteLine("Instancias de SQL Server encontradas:");
                for (int i = 0; i < instancias.Count; i++)
                    Console.WriteLine("  " + (i + 1) + ". " + instancias[i].Fuente + "   (" + instancias[i].Descripcion + ")");
            }
            Console.Write("\nElegí el número de la instancia, o escribí su nombre (por ejemplo .\\SQLEXPRESS): ");
            string respuesta = (Console.ReadLine() ?? "").Trim();
            int numero;
            string fuente = int.TryParse(respuesta, out numero) && numero >= 1 && numero <= instancias.Count ? instancias[numero - 1].Fuente : respuesta;
            if (fuente.Length == 0)
                return SinConexion;

            bool reemplazar = false;
            if (Probar(fuente, baseDatos) == 2)
            {
                Console.Write("La base " + baseDatos + " ya existe. ¿Reemplazarla por una nueva? Se guarda una copia antes y se pierden los datos actuales (S/N): ");
                reemplazar = (Console.ReadLine() ?? "").Trim().StartsWith("s", StringComparison.OrdinalIgnoreCase);
            }
            return Instalar(fuente, reemplazar, script, config, baseDatos);
        }

        static int Instalar(string fuente, bool reemplazar, string scriptPath, string configPath, string baseDatos)
        {
            if (string.IsNullOrWhiteSpace(fuente))
            {
                Mostrar("ERROR: no se indicó la instancia de SQL Server.");
                return SinConexion;
            }
            IniciarSiHaceFalta(fuente);
            string connMaster = Conexion(fuente, "master");
            string connApp = new SqlConnectionStringBuilder(Conexion(fuente, baseDatos)) { ConnectTimeout = 15 }.ConnectionString;
            Mostrar("Instancia de SQL Server: " + fuente);

            try
            {
                bool existe;
                try
                {
                    existe = ExisteBase(connMaster, baseDatos);
                }
                catch (Exception ex)
                {
                    Mostrar("ERROR: no se pudo conectar a " + fuente + ": " + ex.Message);
                    return SinConexion;
                }

                if (existe && !reemplazar)
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

                if (existe)
                    Reemplazar(connMaster, baseDatos);

                bool huboErrores = EjecutarScript(connMaster, scriptPath, baseDatos);
                ConfigurarAplicacion(configPath, connApp);
                if (huboErrores)
                {
                    Mostrar("La base se creó pero con errores en algunos bloques. Revisá los mensajes de arriba.");
                    return ConErrores;
                }
                Mostrar(existe ? "Base de datos reemplazada por una nueva, sin errores." : "Base de datos creada exitosamente, sin errores.");
                if (configPath == null)
                {
                    Mostrar("\nIMPORTANTE: usá esta connection string en GestionNegocio.exe.config:");
                    Mostrar(connApp);
                }
                return existe ? Reemplazada : Creada;
            }
            catch (Exception ex)
            {
                Mostrar("Error al crear la base de datos: " + ex.Message);
                return ErrorInesperado;
            }
        }

        // Copia de seguridad en la carpeta de backups de SQL Server y después se borra la base vieja
        static void Reemplazar(string connMaster, string baseDatos)
        {
            string archivo = baseDatos + "_antes_de_instalar_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bak";
            using (var con = new SqlConnection(connMaster))
            {
                con.Open();
                new SqlCommand("BACKUP DATABASE [" + baseDatos + "] TO DISK = N'" + archivo + "' WITH COPY_ONLY, INIT", con) { CommandTimeout = 600 }.ExecuteNonQuery();
                // Dónde la guardó SQL Server (depende de la instancia: en LocalDB va a la carpeta del usuario)
                object ruta = null;
                try
                {
                    var cmdRuta = new SqlCommand(@"SELECT TOP 1 m.physical_device_name FROM msdb.dbo.backupmediafamily m
                        INNER JOIN msdb.dbo.backupset s ON s.media_set_id = m.media_set_id
                        WHERE s.database_name = @Base ORDER BY s.backup_finish_date DESC", con);
                    cmdRuta.Parameters.AddWithValue("@Base", baseDatos);
                    ruta = cmdRuta.ExecuteScalar();
                }
                catch { }
                Mostrar("Copia de seguridad de la base anterior: " + (ruta as string ?? archivo + " (en la carpeta de backups de SQL Server)"));
                new SqlCommand("ALTER DATABASE [" + baseDatos + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [" + baseDatos + "];", con) { CommandTimeout = 120 }.ExecuteNonQuery();
                Mostrar("Base anterior borrada.");
            }
        }

        static bool EjecutarScript(string connMaster, string scriptPath, string baseDatos)
        {
            string script = LeerScriptConEncodingCorrecto(scriptPath);
            if (baseDatos != "GestionUsuarios")
                script = script.Replace("DB_ID('GestionUsuarios')", "DB_ID('" + baseDatos + "')")
                               .Replace("CREATE DATABASE GestionUsuarios ", "CREATE DATABASE " + baseDatos + " ")
                               .Replace("USE GestionUsuarios\r", "USE " + baseDatos + "\r")
                               .Replace("USE GestionUsuarios\n", "USE " + baseDatos + "\n");

            string[] comandos = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            bool huboErrores = false;
            using (SqlConnection con = new SqlConnection(connMaster))
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
            return huboErrores;
        }

        static bool ExisteBase(string connMaster, string baseDatos)
        {
            using (SqlConnection con = new SqlConnection(connMaster))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT DB_ID(@Nombre)", con);
                cmd.Parameters.AddWithValue("@Nombre", baseDatos);
                return cmd.ExecuteScalar() != DBNull.Value;
            }
        }

        // Deja la connection string "GestionUsuarios" de la aplicación apuntando a la instancia elegida.
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
    }
}
