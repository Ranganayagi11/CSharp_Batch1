using System;
using System.Collections.Generic;
using HospitalManagement.util;
using HospitalManagement.dao;
using HospitalManagement.entity;
using HospitalManagement.exception;

namespace HospitalManagement.mainmod
{
    class MainModule
    {
        static IHospitalService service = new HospitalServiceImpl();

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n--- Hospital Management ---");
                Console.WriteLine("0. View All Patients, Doctors, and Appointments");
                Console.WriteLine("1. Get Appointment by ID");
                Console.WriteLine("2. Get Appointments for Patient");
                Console.WriteLine("3. Get Appointments for Doctor");
                Console.WriteLine("4. Schedule Appointment");
                Console.WriteLine("5. Update Appointment");
                Console.WriteLine("6. Cancel Appointment");
                Console.WriteLine("7. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "0":
                            DisplayAllData();
                            break;
                        case "1": GetAppointmentById(); break;
                        case "2": GetAppointmentsForPatient(); break;
                        case "3": GetAppointmentsForDoctor(); break;
                        case "4": ScheduleAppointment(); break;
                        case "5": UpdateAppointment(); break;
                        case "6": CancelAppointment(); break;
                        case "7":
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (PatientNumberNotFoundException ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        static void DisplayAllData()
        {
            Console.WriteLine("\n--- All Patients ---");
            var patients = service.GetAllPatients();
            patients.ForEach(p => Console.WriteLine(p));

            Console.WriteLine("\n--- All Doctors ---");
            var doctors = service.GetAllDoctors();
            doctors.ForEach(d => Console.WriteLine(d));

            Console.WriteLine("\n--- All Appointments ---");
            var appointments = service.GetAllAppointments();
            appointments.ForEach(a => Console.WriteLine(a));
        }
        static void GetAppointmentById()
        {
            Console.Write("Enter Appointment ID: ");
            int aid = int.Parse(Console.ReadLine());
            Appointment a = service.GetAppointmentById(aid);
            Console.WriteLine(a != null ? a.ToString() : "Appointment not found.");
        }

        static void GetAppointmentsForPatient()
        {
            Console.Write("Enter Patient ID: ");
            int pid = int.Parse(Console.ReadLine());
            List<Appointment> appts = service.GetAppointmentsForPatient(pid);
            appts.ForEach(a => Console.WriteLine(a));
        }

        static void GetAppointmentsForDoctor()
        {
            Console.Write("Enter Doctor ID: ");
            int did = int.Parse(Console.ReadLine());
            List<Appointment> appts = service.GetAppointmentsForDoctor(did);
            appts.ForEach(a => Console.WriteLine(a));
        }

        static void ScheduleAppointment()
        {
            Console.Write("Enter Appointment ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter Patient ID: ");
            int pid = int.Parse(Console.ReadLine());
            Console.Write("Enter Doctor ID: ");
            int did = int.Parse(Console.ReadLine());
            Console.Write("Enter Appointment Date (yyyy-MM-dd HH:mm): ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter Description: ");
            string desc = Console.ReadLine();

            Appointment appt = new Appointment(id, pid, did, date, desc);
            bool added = service.ScheduleAppointment(appt);
            Console.WriteLine(added ? "Appointment scheduled successfully!" : "Failed to schedule appointment.");
        }

        static void UpdateAppointment()
        {
            Console.Write("Enter Appointment ID to Update: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter New Patient ID: ");
            int pid = int.Parse(Console.ReadLine());
            Console.Write("Enter New Doctor ID: ");
            int did = int.Parse(Console.ReadLine());
            Console.Write("Enter New Appointment Date (yyyy-MM-dd HH:mm): ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter New Description: ");
            string desc = Console.ReadLine();

            Appointment appt = new Appointment(id, pid, did, date, desc);
            bool updated = service.UpdateAppointment(appt);
            Console.WriteLine(updated ? "Appointment updated successfully!" : "Failed to update appointment.");
        }

        static void CancelAppointment()
        {
            Console.Write("Enter Appointment ID to Cancel: ");
            int id = int.Parse(Console.ReadLine());
            bool deleted = service.CancelAppointment(id);
            Console.WriteLine(deleted ? "Appointment cancelled." : "Failed to cancel.");
        }
    }
}
