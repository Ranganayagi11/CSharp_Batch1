using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HospitalManagement.util
{
    public class DBConnUtil
    {
        // Static variable for the connection (as per instructions)
        private static SqlConnection connection;

        // Connection string
        private static string connectionString = "data source=LAPTOP-PLAECRO0\\SQLEXPRESS;initial catalog=HospitalDB;integrated security=true;TrustServerCertificate=true;";

        // Static method to return a connection
        public static SqlConnection GetConnection()
        {
            if (connection == null || connection.State == System.Data.ConnectionState.Closed)
            {
                connection = new SqlConnection(connectionString);
            }
            return connection;
        }
    }
}
