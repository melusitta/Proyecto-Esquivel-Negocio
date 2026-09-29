using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BackUpRestoreDAL
    {
        private string connectionString = ConexionDAL.ConnectionString;
        private string masterConnection = ConexionDAL.ConnectionString
            .Replace("Initial Catalog=GestionUsuarios", "Initial Catalog=master");

        public void RealizarBackUp(string rutaArchivo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"BACKUP DATABASE GestionUsuarios TO DISK = '{rutaArchivo}'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandTimeout = 300;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void RealizarRestore(string rutaArchivo)
        {
            using (SqlConnection con = new SqlConnection(masterConnection))
            {
                con.Open();
                string querySingle = "ALTER DATABASE GestionUsuarios SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                new SqlCommand(querySingle, con).ExecuteNonQuery();

                string queryRestore = $"RESTORE DATABASE GestionUsuarios FROM DISK = '{rutaArchivo}' WITH REPLACE";
                SqlCommand cmd = new SqlCommand(queryRestore, con);
                cmd.CommandTimeout = 300;
                cmd.ExecuteNonQuery();

                string queryMulti = "ALTER DATABASE GestionUsuarios SET MULTI_USER";
                new SqlCommand(queryMulti, con).ExecuteNonQuery();
            }
        }
    }
}
