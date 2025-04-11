using CarConnect.dao;
using CarConnect.entity;
using CarConnect.util;
using Microsoft.Data.SqlClient;

namespace CarConnect.dao
{
    public class VehicleService : IVehicleService
    {
        public Vehicle GetVehicleById(int vehicleId)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Vehicle WHERE VehicleID = @VehicleID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Vehicle
                    {
                        VehicleID = (int)reader["VehicleID"],
                        Model = reader["Model"].ToString(),
                        Make = reader["Make"].ToString(),
                        Year = (int)reader["Year"],
                        Color = reader["Color"].ToString(),
                        RegistrationNumber = reader["RegistrationNumber"].ToString(),
                        Availability = (bool)reader["Availability"],
                        DailyRate = (decimal)reader["DailyRate"]
                    };
                }
                return null;
            }
        }

        public List<Vehicle> GetAvailableVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Vehicle WHERE Availability = 1";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    vehicles.Add(new Vehicle
                    {
                        VehicleID = (int)reader["VehicleID"],
                        Model = reader["Model"].ToString(),
                        Make = reader["Make"].ToString(),
                        Year = (int)reader["Year"],
                        Color = reader["Color"].ToString(),
                        RegistrationNumber = reader["RegistrationNumber"].ToString(),
                        Availability = (bool)reader["Availability"],
                        DailyRate = (decimal)reader["DailyRate"]
                    });
                }
            }

            return vehicles;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = @"INSERT INTO Vehicle (Model, Make, Year, Color, RegistrationNumber, Availability, DailyRate) 
                                 VALUES (@Model, @Make, @Year, @Color, @RegistrationNumber, @Availability, @DailyRate)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                cmd.Parameters.AddWithValue("@Make", vehicle.Make);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@Color", vehicle.Color);
                cmd.Parameters.AddWithValue("@RegistrationNumber", vehicle.RegistrationNumber);
                cmd.Parameters.AddWithValue("@Availability", vehicle.Availability);
                cmd.Parameters.AddWithValue("@DailyRate", vehicle.DailyRate);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = @"UPDATE Vehicle 
                                 SET Model = @Model, Make = @Make, Year = @Year, Color = @Color,
                                     RegistrationNumber = @RegistrationNumber, Availability = @Availability, DailyRate = @DailyRate 
                                 WHERE VehicleID = @VehicleID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                cmd.Parameters.AddWithValue("@Make", vehicle.Make);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@Color", vehicle.Color);
                cmd.Parameters.AddWithValue("@RegistrationNumber", vehicle.RegistrationNumber);
                cmd.Parameters.AddWithValue("@Availability", vehicle.Availability);
                cmd.Parameters.AddWithValue("@DailyRate", vehicle.DailyRate);
                cmd.Parameters.AddWithValue("@VehicleID", vehicle.VehicleID);
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveVehicle(int vehicleId)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "DELETE FROM Vehicle WHERE VehicleID = @VehicleID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
