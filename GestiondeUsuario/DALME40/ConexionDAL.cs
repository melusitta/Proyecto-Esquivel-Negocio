using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

public static class ConexionDAL
{
    public static string ConnectionString =
        ConfigurationManager.ConnectionStrings["GestionUsuarios"].ConnectionString;
}
