using CarConnect.dao;
using CarConnect.entity;
using CarConnect.util;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using CarConnect.exception;

namespace CarConnect.dao
{
    public class ReservationService : IReservationService
    {
        public Reservation GetReservationById(int reservationId)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Reservation WHERE ReservationID = @ReservationID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Reservation
                    {
                        ReservationID = (int)reader["ReservationID"],
                        CustomerID = (int)reader["CustomerID"],
                        VehicleID = (int)reader["VehicleID"],
                        StartDate = (DateTime)reader["StartDate"],
                        EndDate = (DateTime)reader["EndDate"],
                        TotalCost = (decimal)reader["TotalCost"],
                        Status = reader["Status"].ToString()
                    };
                }
                return null;
            }
        }

        public List<Reservation> GetReservationsByCustomerId(int customerId)
        {
            List<Reservation> reservations = new List<Reservation>();

            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Reservation WHERE CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    reservations.Add(new Reservation
                    {
                        ReservationID = (int)reader["ReservationID"],
                        CustomerID = (int)reader["CustomerID"],
                        VehicleID = (int)reader["VehicleID"],
                        StartDate = (DateTime)reader["StartDate"],
                        EndDate = (DateTime)reader["EndDate"],
                        TotalCost = (decimal)reader["TotalCost"],
                        Status = reader["Status"].ToString()
                    });
                }
            }

            return reservations;
        }

        public void CreateReservation(Reservation reservation)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string checkConflict = @"
            SELECT COUNT(*) FROM Reservation
            WHERE VehicleID = @VehicleID
            AND NOT (
                EndDate <= @StartDate OR StartDate >= @EndDate
            )";

                SqlCommand checkCmd = new SqlCommand(checkConflict, con);
                checkCmd.Parameters.AddWithValue("@VehicleID", reservation.VehicleID);
                checkCmd.Parameters.AddWithValue("@StartDate", reservation.StartDate);
                checkCmd.Parameters.AddWithValue("@EndDate", reservation.EndDate);

                int conflictCount = (int)checkCmd.ExecuteScalar();
                if (conflictCount > 0)
                {
                    throw new ReservationException("This vehicle is already reserved in the selected time range.");
                }

                string insert = @"
            INSERT INTO Reservation (CustomerID, VehicleID, StartDate, EndDate, TotalCost, Status)
            VALUES (@CustomerID, @VehicleID, @StartDate, @EndDate, @TotalCost, @Status)";
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.Parameters.AddWithValue("@CustomerID", reservation.CustomerID);
                cmd.Parameters.AddWithValue("@VehicleID", reservation.VehicleID);
                cmd.Parameters.AddWithValue("@StartDate", reservation.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", reservation.EndDate);
                cmd.Parameters.AddWithValue("@TotalCost", reservation.TotalCost);
                cmd.Parameters.AddWithValue("@Status", reservation.Status);
                cmd.ExecuteNonQuery();
            }
        }


        public void UpdateReservation(Reservation reservation)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = @"UPDATE Reservation 
                                 SET CustomerID = @CustomerID, VehicleID = @VehicleID, 
                                     StartDate = @StartDate, EndDate = @EndDate, 
                                     TotalCost = @TotalCost, Status = @Status
                                 WHERE ReservationID = @ReservationID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CustomerID", reservation.CustomerID);
                cmd.Parameters.AddWithValue("@VehicleID", reservation.VehicleID);
                cmd.Parameters.AddWithValue("@StartDate", reservation.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", reservation.EndDate);
                cmd.Parameters.AddWithValue("@TotalCost", reservation.TotalCost);
                cmd.Parameters.AddWithValue("@Status", reservation.Status);
                cmd.Parameters.AddWithValue("@ReservationID", reservation.ReservationID);
                cmd.ExecuteNonQuery();
            }
        }

        public void CancelReservation(int reservationId)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "DELETE FROM Reservation WHERE ReservationID = @ReservationID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
