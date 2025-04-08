namespace BankingSystem
{
    public class Customer
    {
        private int customerId;
        private string firstName;
        private string lastName;
        private string email;
        private string phone;
        private string address;

        

        public Customer(int id, string first, string last, string email, string phone, string address)
        {
            this.customerId = id;
            this.firstName = first;
            this.lastName = last;
            this.email = email;
            this.phone = phone;
            this.address = address;
        }

        public int CustomerId { get => customerId; set => customerId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        /*private int customerId;
        private string firstName;
        private string lastName;
        private string email;
        private string phone;
        private string address;

        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }*/


        public void PrintCustomerDetails()
        {
            Console.WriteLine("\n--- Customer Information ---");
            Console.WriteLine($"Customer ID: {customerId}");
            Console.WriteLine($"Name: {firstName} {lastName}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Phone: {phone}");
            Console.WriteLine($"Address: {address}");
        }
    }
}

