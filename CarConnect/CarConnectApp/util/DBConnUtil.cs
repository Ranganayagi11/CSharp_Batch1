using System;
using Microsoft.Data.SqlClient;

namespace CarConnect.util
{
    public static class DBConnUtil
    {
        private static readonly string connectionString = "data source=LAPTOP-PLAECRO0\\SQLEXPRESS;initial catalog=CarConnectDB;integrated security=true;TrustServerCertificate=true;";

        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                return con;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database connection failed: " + ex.Message);
                throw;
            }
        }
    }
}
