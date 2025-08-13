using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace DB_connection.Data_Access_Layer
{
    public static  class DbConnectionFactory
    {
        private static string _connectionString = "Server=BSD-BOOBATHIA01\\SQLEXPRESS;Database=Student;Integrated Security=SSPI;TrustServerCertificate=True;";
        
        public static IDbConnection GetOpenConnection()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;

        }
    }
}
