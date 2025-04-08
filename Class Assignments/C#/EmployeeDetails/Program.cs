using Microsoft.Data.SqlClient;
namespace EmployeeDetails
{
    internal class Program
    {
        private static SqlConnection con;
        private static SqlCommand cmd;
        private static SqlDataReader dr;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            SelectData();
        }
        
        public static SqlConnection getConnection()
        {
            con = new SqlConnection("data source=LAPTOP-PLAECRO0\\SQLEXPRESS;initial catalog=Assignment_2;integrated security=true;TrustServerCertificate=true;");
            con.Open();
            return con;
        }
        public static void SelectData()
        {
            con = getConnection();
            cmd = new SqlCommand("select * from emp", con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Console.WriteLine(dr[0] + " " + dr[1]);
                Console.WriteLine("Employee Number = " + dr["empno"]);
                Console.WriteLine("Employee Name = " + dr["ename"]);
                Console.WriteLine("Employee Job = " + dr[2]);
                Console.WriteLine("Manager ID = " + dr["mgr_id"]);
                Console.WriteLine("Employee Salary = " + dr[5]);
            }
        }

    }
}
