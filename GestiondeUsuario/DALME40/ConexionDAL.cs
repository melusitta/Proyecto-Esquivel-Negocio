using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

// Cadena de conexión a la base GestionUsuarios.
// Se usa la del App.config; si ese servidor no responde (por ejemplo en otra compu donde SQL Server
// se instaló con otro nombre), se prueban los nombres de instancia más comunes y se usa el primero
// que tenga la base. Se resuelve una sola vez, la primera vez que alguna DAL la pide.
public static class ConexionDAL
{
    private static readonly Lazy<string> _cadena = new Lazy<string>(Detectar);

    public static string ConnectionString => _cadena.Value;

    private static string Detectar()
    {
        string configurada = ConfigurationManager.ConnectionStrings["GestionUsuarios"].ConnectionString;
        if (Responde(configurada))
            return configurada;

        var builder = new SqlConnectionStringBuilder(configurada);
        string servidorConfigurado = builder.DataSource;
        foreach (string servidor in Candidatos())
        {
            if (string.Equals(servidor, servidorConfigurado, StringComparison.OrdinalIgnoreCase))
                continue;
            builder.DataSource = servidor;
            if (Responde(builder.ConnectionString))
                return builder.ConnectionString;
        }
        // Ninguno respondió: se deja la configurada y el error de conexión se ve como siempre
        return configurada;
    }

    private static IEnumerable<string> Candidatos()
    {
        string pc = Environment.MachineName;
        return new[]
        {
            @".\SQLEXPRESS", ".", "localhost", pc + @"\SQLEXPRESS", pc,
            @".\SQLEXPRESS01", @"localhost\SQLEXPRESS", @"(localdb)\MSSQLLocalDB"
        };
    }

    // Conecta directo a la base: si el servidor existe pero no tiene GestionUsuarios, no sirve
    private static bool Responde(string cadena)
    {
        try
        {
            var prueba = new SqlConnectionStringBuilder(cadena)
            {
                // LocalDB puede tardar en arrancar la primera vez
                ConnectTimeout = cadena.IndexOf("(localdb)", StringComparison.OrdinalIgnoreCase) >= 0 ? 15 : 3,
                Pooling = false
            };
            using (var con = new SqlConnection(prueba.ConnectionString))
            {
                con.Open();
                return true;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
}
