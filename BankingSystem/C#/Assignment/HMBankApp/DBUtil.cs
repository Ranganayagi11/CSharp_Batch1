using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HMBankApp
{
    public static class DBUtil
    {
        private static readonly string connectionString = "Data Source=LAPTOP-PLAECRO0\\SQLEXPRESS;Initial Catalog=HMBank;Integrated Security=True";

        public static SqlConnection GetDBConn()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                return conn;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database connection error: " + ex.Message);
                return null;
            }
        }
    }
}
