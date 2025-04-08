using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankApp
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private string email;
        public string Email
        {
            get {  return email; }
            set
            {
                if (value.Contains("@") && value.Contains("."))
                    email = value;
                else
                    throw new ArgumentException("Invalid email address.");
            }
        }


        private string phone;
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                if (value.Length == 10 && long.TryParse(value, out _))
                    phone = value;
                else
                    throw new ArgumentException("Phone number must be exactly 10 digits.");
            }
        }


        public string Address { get; set; }

        public Customer(long cid, string? fname) { }

        public Customer(int id, string fname, string lname, string email, string phone, string address)
        {
            CustomerId = id;
            FirstName = fname;
            LastName = lname;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public void PrintCustomerInfo()
        {
            Console.WriteLine($"Customer ID: {CustomerId}");
            Console.WriteLine($"Name: {FirstName} {LastName}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Phone: {Phone}");
            Console.WriteLine($"Address: {Address}");
        }
    }
}
