using CarConnect.dao;
using CarConnect.entity;
using CarConnect.util;
using Microsoft.Data.SqlClient;

namespace CarConnect.dao
{
    public class AdminService : IAdminService
    {
        public Admin GetAdminById(int adminId)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Admin WHERE AdminID = @AdminID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AdminID", adminId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Admin
                    {
                        AdminID = (int)reader["AdminID"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Role = reader["Role"].ToString(),
                        JoinDate = (DateTime)reader["JoinDate"]
                    };
                }
                return null;
            }
        }

        public Admin GetAdminByUsername(string username)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Admin WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", username);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Admin
                    {
                        AdminID = (int)reader["AdminID"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Role = reader["Role"].ToString(),
                        JoinDate = (DateTime)reader["JoinDate"]
                    };
                }
                return null;
            }
        }

        public void RegisterAdmin(Admin admin)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = @"INSERT INTO Admin 
                (FirstName, LastName, Email, PhoneNumber, Username, Password, Role, JoinDate) 
                VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Username, @Password, @Role, @JoinDate)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FirstName", admin.FirstName);
                cmd.Parameters.AddWithValue("@LastName", admin.LastName);
                cmd.Parameters.AddWithValue("@Email", admin.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", admin.PhoneNumber);
                cmd.Parameters.AddWithValue("@Username", admin.Username);
                cmd.Parameters.AddWithValue("@Password", admin.Password);
                cmd.Parameters.AddWithValue("@Role", admin.Role);
                cmd.Parameters.AddWithValue("@JoinDate", admin.JoinDate);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateAdmin(Admin admin)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = @"UPDATE Admin 
                                 SET FirstName = @FirstName, LastName = @LastName, Email = @Email,
                                     PhoneNumber = @PhoneNumber, Password = @Password, Role = @Role
                                 WHERE AdminID = @AdminID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FirstName", admin.FirstName);
                cmd.Parameters.AddWithValue("@LastName", admin.LastName);
                cmd.Parameters.AddWithValue("@Email", admin.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", admin.PhoneNumber);
                cmd.Parameters.AddWithValue("@Password", admin.Password);
                cmd.Parameters.AddWithValue("@Role", admin.Role);
                cmd.Parameters.AddWithValue("@AdminID", admin.AdminID);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAdmin(int adminId)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "DELETE FROM Admin WHERE AdminID = @AdminID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AdminID", adminId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
