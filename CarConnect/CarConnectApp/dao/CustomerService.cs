using System;
using Microsoft.Data.SqlClient;
using CarConnect.dao;
using CarConnect.entity;
using CarConnect.util;

namespace CarConnect.dao
{
    public class CustomerService : ICustomerService
    {
        public Customer GetCustomerById(int customerId)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Customer WHERE CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Customer
                    {
                        CustomerID = (int)reader["CustomerID"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Address = reader["Address"].ToString(),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        RegistrationDate = (DateTime)reader["RegistrationDate"]
                    };
                }
                return null;
            }
        }

        public Customer GetCustomerByUsername(string username)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Customer WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", username);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Customer
                    {
                        CustomerID = (int)reader["CustomerID"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Address = reader["Address"].ToString(),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        RegistrationDate = (DateTime)reader["RegistrationDate"]
                    };
                }
                return null;
            }
        }

        public void RegisterCustomer(Customer customer)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = @"INSERT INTO Customer 
                (FirstName, LastName, Email, PhoneNumber, Address, Username, Password, RegistrationDate)
                VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Address, @Username, @Password, @RegistrationDate)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@Username", customer.Username);
                cmd.Parameters.AddWithValue("@Password", customer.Password);
                cmd.Parameters.AddWithValue("@RegistrationDate", customer.RegistrationDate);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = @"UPDATE Customer 
                                 SET FirstName = @FirstName, LastName = @LastName, Email = @Email, 
                                     PhoneNumber = @PhoneNumber, Address = @Address, Password = @Password 
                                 WHERE CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@Password", customer.Password);
                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                cmd.ExecuteNonQuery();
            }
        }

        
    }
}
