using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.entity;
using HospitalManagement.util;
using Microsoft.Data.SqlClient;

namespace HospitalManagement.dao
{
    public class HospitalServiceImpl : IHospitalService
    {
        public Appointment GetAppointmentById(int appointmentId)
        {
            Appointment appointment = null;
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Appointment WHERE appointmentId = @id", conn);
                cmd.Parameters.AddWithValue("@id", appointmentId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    appointment = new Appointment(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        reader.GetDateTime(3),
                        reader.GetString(4)
                    );
                }
            }
            return appointment;
        }

        public List<Appointment> GetAppointmentsForPatient(int patientId)
        {
            List<Appointment> appointments = new List<Appointment>();
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Appointment WHERE patientId = @pid", conn);
                cmd.Parameters.AddWithValue("@pid", patientId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    appointments.Add(new Appointment(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        reader.GetDateTime(3),
                        reader.GetString(4)
                    ));
                }
            }
            return appointments;
        }

        public List<Appointment> GetAppointmentsForDoctor(int doctorId)
        {
            List<Appointment> appointments = new List<Appointment>();
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Appointment WHERE doctorId = @did", conn);
                cmd.Parameters.AddWithValue("@did", doctorId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    appointments.Add(new Appointment(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        reader.GetDateTime(3),
                        reader.GetString(4)
                    ));
                }
            }
            return appointments;
        }

        public bool ScheduleAppointment(Appointment appointment)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Appointment VALUES (@aid, @pid, @did, @date, @desc)", conn);
                cmd.Parameters.AddWithValue("@aid", appointment.AppointmentId);
                cmd.Parameters.AddWithValue("@pid", appointment.PatientId);
                cmd.Parameters.AddWithValue("@did", appointment.DoctorId);
                cmd.Parameters.AddWithValue("@date", appointment.AppointmentDate);
                cmd.Parameters.AddWithValue("@desc", appointment.Description);

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Appointment SET patientId=@pid, doctorId=@did, appointmentDate=@date, description=@desc WHERE appointmentId=@aid", conn);
                cmd.Parameters.AddWithValue("@aid", appointment.AppointmentId);
                cmd.Parameters.AddWithValue("@pid", appointment.PatientId);
                cmd.Parameters.AddWithValue("@did", appointment.DoctorId);
                cmd.Parameters.AddWithValue("@date", appointment.AppointmentDate);
                cmd.Parameters.AddWithValue("@desc", appointment.Description);

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public bool CancelAppointment(int appointmentId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Appointment WHERE appointmentId = @aid", conn);
                cmd.Parameters.AddWithValue("@aid", appointmentId);

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }

        }
        public List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM patient", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Patient p = new Patient
                    {
                        PatientId = (int)reader["patientid"],
                        FirstName = reader["firstname"].ToString(),
                        Gender = reader["gender"].ToString(),
                        ContactNumber = (string)reader["contactnumber"],
                        Address = (string)reader["address"]
                    };
                    patients.Add(p);
                }
            }
            return patients;
        }

        public List<Doctor> GetAllDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM doctor", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Doctor d = new Doctor
                    {
                        DoctorId = (int)reader["doctorid"],
                        FirstName = reader["firstname"].ToString(),
                        Specialization = reader["specialization"].ToString(),
                        ContactNumber = (string)reader["contactnumber"]
                    };
                    doctors.Add(d);
                }
            }
            return doctors;
        }

        public List<Appointment> GetAllAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM appointment", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Appointment a = new Appointment
                    {
                        AppointmentId = (int)reader["appointmentid"],
                        PatientId = (int)reader["patientid"],
                        DoctorId = (int)reader["doctorid"],
                        AppointmentDate = (DateTime)reader["appointmentdate"],
                        Description = reader["description"].ToString()
                    };
                    appointments.Add(a);
                }
            }
            return appointments;
        }


    }
}
