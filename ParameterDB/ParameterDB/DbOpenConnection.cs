using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Parameter
{
    public class DbOpenConnection
    {

        public static SqlConnection GetOpenConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            return conn;
        }
       
    }
}
