using Microsoft.Data.SqlClient;

namespace CarConnect.util
{
    public class ReportGenerator
    {
        public void GenerateReservationHistory()
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Reservation ORDER BY StartDate DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Reservation History:");
                while (reader.Read())
                {
                    Console.WriteLine($"ReservationID: {reader["ReservationID"]}, VehicleID: {reader["VehicleID"]}, CustomerID: {reader["CustomerID"]}, Status: {reader["Status"]}");
                }
            }
        }

        public void GenerateVehicleUtilizationReport()
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = @"
                    SELECT V.VehicleID, V.Model, V.Make, COUNT(R.ReservationID) AS TotalReservations
                    FROM Vehicle V
                    LEFT JOIN Reservation R ON V.VehicleID = R.VehicleID
                    GROUP BY V.VehicleID, V.Model, V.Make";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Vehicle Utilization:");
                while (reader.Read())
                {
                    Console.WriteLine($"VehicleID: {reader["VehicleID"]}, Model: {reader["Model"]}, Total Bookings: {reader["TotalReservations"]}");
                }
            }
        }

        public void GenerateRevenueReport()
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "SELECT SUM(TotalCost) AS TotalRevenue FROM Reservation ";
                SqlCommand cmd = new SqlCommand(query, con);
                object result = cmd.ExecuteScalar();
                decimal revenue = result != DBNull.Value ? (decimal)result : 0;
                Console.WriteLine($"Total Revenue: Rs.{revenue}");
                
            }
        }
    }
}
