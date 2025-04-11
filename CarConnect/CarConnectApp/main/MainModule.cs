using CarConnect.dao;
using CarConnect.entity;
using CarConnect.util;
using CarConnect.exception;

namespace CarConnect.main
{
    class MainModule
    {
        static ICustomerService customerService = new CustomerService();
        static IAdminService adminService = new AdminService();
        static IVehicleService vehicleService = new VehicleService();
        static IReservationService reservationService = new ReservationService();
        static ReportGenerator reportGenerator = new ReportGenerator();

        static void Main(string[] args)
        {
            Console.WriteLine("--- Welcome to CarConnect ---");
            Console.Write("Login as (1) Admin or (2) Customer: ");
            string choice = Console.ReadLine();

            if (choice == "1")
                AdminLogin();
            else if (choice == "2")
                CustomerLogin();
            else
                Console.WriteLine("Invalid choice.");
        }

        static void AdminLogin()
        {
            Console.Write("Enter admin username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            try
            {
                Admin admin = adminService.GetAdminByUsername(username);
                if (admin == null || !admin.Authenticate(password))
                    throw new AuthenticationException("Invalid admin credentials.");

                Console.WriteLine($"Welcome Admin {admin.FirstName}");
                AdminMenu();
            }
            catch (AuthenticationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Admin Menu ---");
                Console.WriteLine("1. Add Vehicle");
                Console.WriteLine("2. View Available Vehicles");
                Console.WriteLine("3. Remove Vehicle");
                Console.WriteLine("4. Generate Reports");
                Console.WriteLine("5. Add Admin");
                Console.WriteLine("6. View Admin by ID");
                Console.WriteLine("7. View Admin by Username");
                Console.WriteLine("8. Update Admin");
                Console.WriteLine("9. Delete Admin");
                Console.WriteLine("10. Logout");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddVehicle();
                        break;
                    case "2":
                        ViewAvailableVehicles();
                        break;
                    case "3":
                        RemoveVehicle();
                        break;
                    case "4":
                        GenerateReports();
                        break;
                    case "5":
                        AddAdmin();
                        break;
                    case "6":
                        ViewAdminById();
                        break;
                    case "7":
                        ViewAdminByUsername();
                        break;
                    case "8":
                        UpdateAdmin();
                        break;
                    case "9":
                        DeleteAdmin();
                        break;
                    case "10":
                        Console.WriteLine("Logging out...");
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
        static void AddAdmin()
        {
            Admin admin = new Admin();
            Console.Write("First Name: "); admin.FirstName = Console.ReadLine();
            Console.Write("Last Name: "); admin.LastName = Console.ReadLine();
            Console.Write("Email: "); admin.Email = Console.ReadLine();
            Console.Write("Phone Number: "); admin.PhoneNumber = Console.ReadLine();
            Console.Write("Username: "); admin.Username = Console.ReadLine();
            Console.Write("Password: "); admin.Password = Console.ReadLine();
            Console.Write("Role: "); admin.Role = Console.ReadLine();
            admin.JoinDate = DateTime.Now;

            adminService.RegisterAdmin(admin);
            Console.WriteLine("Admin added successfully.");
        }
        static void ViewAdminById()
        {
            Console.Write("Enter Admin ID: ");
            int id = int.Parse(Console.ReadLine());
            Admin admin = adminService.GetAdminById(id);

            if (admin != null)
            {
                Console.WriteLine($"{admin.AdminID}: {admin.FirstName} {admin.LastName}, {admin.Email}, {admin.Username}, {admin.Role}");
            }
            else
            {
                Console.WriteLine("Admin not found.");
            }
        }
        static void ViewAdminByUsername()
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Admin admin = adminService.GetAdminByUsername(username);

            if (admin != null)
            {
                Console.WriteLine($"{admin.AdminID}: {admin.FirstName} {admin.LastName}, {admin.Email}, {admin.Username}, {admin.Role}");
            }
            else
            {
                Console.WriteLine("Admin not found.");
            }
        }
        static void UpdateAdmin()
        {
            Console.Write("Enter Admin ID to update: ");
            int id = int.Parse(Console.ReadLine());
            Admin admin = adminService.GetAdminById(id);

            if (admin != null)
            {
                Console.Write("First Name: "); admin.FirstName = Console.ReadLine();
                Console.Write("Last Name: "); admin.LastName = Console.ReadLine();
                Console.Write("Email: "); admin.Email = Console.ReadLine();
                Console.Write("Phone Number: "); admin.PhoneNumber = Console.ReadLine();
                Console.Write("Password: "); admin.Password = Console.ReadLine();
                Console.Write("Role: "); admin.Role = Console.ReadLine();

                adminService.UpdateAdmin(admin);
                Console.WriteLine("Admin updated.");
            }
            else
            {
                Console.WriteLine("Admin not found.");
            }
        }
        static void DeleteAdmin()
        {
            Console.Write("Enter Admin ID to delete: ");
            int id = int.Parse(Console.ReadLine());
            adminService.DeleteAdmin(id);
            Console.WriteLine("Admin deleted successfully.");
        }


        static void AddVehicle()
        {
            Vehicle vehicle = new Vehicle();
            Console.Write("Model: "); vehicle.Model = Console.ReadLine();
            Console.Write("Make: "); vehicle.Make = Console.ReadLine();
            Console.Write("Year: "); vehicle.Year = int.Parse(Console.ReadLine());
            Console.Write("Color: "); vehicle.Color = Console.ReadLine();
            Console.Write("Registration Number: "); vehicle.RegistrationNumber = Console.ReadLine();
            Console.Write("Daily Rate: "); vehicle.DailyRate = decimal.Parse(Console.ReadLine());
            vehicle.Availability = true;

            vehicleService.AddVehicle(vehicle);
            Console.WriteLine("Vehicle added successfully.");
        }

        static void ViewAvailableVehicles()
        {
            var vehicles = vehicleService.GetAvailableVehicles();
            Console.WriteLine("\nAvailable Vehicles:");
            foreach (var v in vehicles)
            {
                Console.WriteLine($"{v.VehicleID} - {v.Make} {v.Model} ({v.Color}) - Rs. {v.DailyRate}/day");
            }
        }

        static void RemoveVehicle()
        {
            Console.Write("Enter Vehicle ID to remove: ");
            int id = int.Parse(Console.ReadLine());
            vehicleService.RemoveVehicle(id);
            Console.WriteLine("Vehicle removed.");
        }

        static void GenerateReports()
        {
            Console.WriteLine("\n--- Reports ---");
            reportGenerator.GenerateReservationHistory();
            reportGenerator.GenerateVehicleUtilizationReport();
            reportGenerator.GenerateRevenueReport();
        }

        static void CustomerLogin()
        {
            Console.Write("Enter customer username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            try
            {
                Customer customer = customerService.GetCustomerByUsername(username);
                if (customer == null || !customer.Authenticate(password))
                    throw new AuthenticationException("Invalid customer credentials.");

                Console.WriteLine($"Welcome {customer.FirstName}");
                CustomerMenu(customer);
            }
            catch (AuthenticationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void CustomerMenu(Customer customer)
        {
            while (true)
            {
                Console.WriteLine("\n--- Customer Menu ---");
                Console.WriteLine("1. View Available Vehicles");
                Console.WriteLine("2. Reserve a Vehicle");
                Console.WriteLine("3. View My Reservations");
                Console.WriteLine("4. View Reservation by ID");
                Console.WriteLine("5. Update My Reservation");
                Console.WriteLine("6. Cancel My Reservation");
                Console.WriteLine("7. Add Customer");
                Console.WriteLine("8. View Customer By Username");
                Console.WriteLine("9. Logout");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ViewAvailableVehicles();
                        break;
                    case "2":
                        ReserveVehicle(customer);
                        break;
                    case "3":
                        ViewMyReservations(customer.CustomerID);
                        break;
                    case "4":
                        ViewReservationById();
                        break;
                    case "5":
                        UpdateReservation();
                        break;
                    case "6":
                        CancelReservation();
                        break;
                    case "7":
                        AddCustomer();
                        break;
                    case "8":
                        ViewCustomerByUsername();
                        break;
                    case "9":
                        Console.WriteLine("Logging out...");
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
        static void AddCustomer()
        {
            Customer customer = new Customer();
            Console.Write("First Name: "); customer.FirstName = Console.ReadLine();
            Console.Write("Last Name: "); customer.LastName = Console.ReadLine();
            Console.Write("Email: "); customer.Email = Console.ReadLine();
            Console.Write("Phone Number: "); customer.PhoneNumber = Console.ReadLine();
            Console.Write("Address: "); customer.Address = Console.ReadLine();
            Console.Write("Username: "); customer.Username = Console.ReadLine();
            Console.Write("Password: "); customer.Password = Console.ReadLine();
            customer.RegistrationDate = DateTime.Now;

            customerService.RegisterCustomer(customer);
            Console.WriteLine("Customer registered successfully.");
        }
        static void ViewCustomerByUsername()
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Customer customer = customerService.GetCustomerByUsername(username);

            if (customer != null)
            {
                Console.WriteLine($"{customer.CustomerID}: {customer.FirstName} {customer.LastName}, {customer.Email}, {customer.Username}");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
        

        static void ReserveVehicle(Customer customer)
        {
            try
            {
                ViewAvailableVehicles();
                Console.Write("Enter Vehicle ID to reserve: ");
                int vehicleId = int.Parse(Console.ReadLine());

                Console.Write("Start Date (yyyy-MM-dd): ");
                DateTime start = DateTime.Parse(Console.ReadLine());
                Console.Write("End Date (yyyy-MM-dd): ");
                DateTime end = DateTime.Parse(Console.ReadLine());

                Vehicle v = vehicleService.GetVehicleById(vehicleId);
                if (v == null)
                    throw new VehicleNotFoundException("Vehicle not found.");

                Reservation r = new Reservation
                {
                    CustomerID = customer.CustomerID,
                    VehicleID = vehicleId,
                    StartDate = start,
                    EndDate = end,
                    Status = "Confirmed"
                };
                r.CalculateTotalCost(v.DailyRate);

                reservationService.CreateReservation(r);
                Console.WriteLine("Reservation successful!");
            }
            catch (ReservationException ex)
            {
                Console.WriteLine("Conflict: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void ViewMyReservations(int customerId)
        {
            var list = reservationService.GetReservationsByCustomerId(customerId);
            Console.WriteLine("\nYour Reservations:");
            foreach (var r in list)
            {
                Console.WriteLine($"Reservation #{r.ReservationID}: VehicleID {r.VehicleID} from {r.StartDate:yyyy-MM-dd} to {r.EndDate:yyyy-MM-dd}, Status: {r.Status}, Rs.{r.TotalCost}");
            }
        }
        static void UpdateReservation()
        {
            Console.Write("Enter Reservation ID to update: ");
            int resId = int.Parse(Console.ReadLine());

            Reservation existing = reservationService.GetReservationById(resId);

            if (existing == null)
            {
                Console.WriteLine("Reservation not found.");
                return;
            }

            Console.Write("New Start Date (yyyy-MM-dd): ");
            existing.StartDate = DateTime.Parse(Console.ReadLine());

            Console.Write("New End Date (yyyy-MM-dd): ");
            existing.EndDate = DateTime.Parse(Console.ReadLine());

            Vehicle vehicle = vehicleService.GetVehicleById(existing.VehicleID);
            if (vehicle == null)
            {
                Console.WriteLine("Vehicle not found.");
                return;
            }

            existing.CalculateTotalCost(vehicle.DailyRate);

            Console.Write("Status (Confirmed/Cancelled): ");
            existing.Status = Console.ReadLine();

            reservationService.UpdateReservation(existing);
            Console.WriteLine("Reservation updated successfully.");
        }
        static void CancelReservation()
        {
            Console.Write("Enter Reservation ID to cancel: ");
            int resId = int.Parse(Console.ReadLine());

            reservationService.CancelReservation(resId);
            Console.WriteLine("Reservation cancelled.");
        }
        static void ViewReservationById()
        {
            Console.Write("Enter Reservation ID: ");
            int resId = int.Parse(Console.ReadLine());

            Reservation res = reservationService.GetReservationById(resId);

            if (res != null)
            {
                Console.WriteLine($"Reservation #{res.ReservationID}: CustomerID {res.CustomerID}, VehicleID {res.VehicleID}");
                Console.WriteLine($"Start: {res.StartDate:yyyy-MM-dd}, End: {res.EndDate:yyyy-MM-dd}");
                Console.WriteLine($"Status: {res.Status}, Total: Rs.{res.TotalCost}");
            }
            else
            {
                Console.WriteLine("Reservation not found.");
            }
        }


    }
}
