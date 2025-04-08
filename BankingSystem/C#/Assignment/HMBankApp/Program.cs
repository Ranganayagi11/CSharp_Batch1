using System;
using Microsoft.Data.SqlClient;
//Task 14
namespace HMBankApp
{
    internal class BankApp
    {
        private static SqlConnection con;
        private static SqlCommand cmd;
        private static SqlDataReader dr;

        static void Main(string[] args)
        {
            string choice = "";
            do
            {
                Console.WriteLine("\n===== HMBank Menu =====");
                Console.WriteLine("1. Create_account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Get_balance");
                Console.WriteLine("5. Transfer");
                Console.WriteLine("6. GetAccountDetails");
                Console.WriteLine("7. ListAccounts");
                Console.WriteLine("8. GetTransactions");
                Console.WriteLine("9. Exit");
                Console.Write("Enter your choice: ");
                choice = Console.ReadLine();

                switch (choice.ToLower())
                {
                    case "1":                
                        CreateAccount();
                        break;
                    case "2":                  
                        Deposit();
                        break;
                    case "3":                    
                        Withdraw();
                        break;
                    case "4":                  
                        GetBalance();
                        break;
                    case "5":                
                        Transfer();
                        break;
                    case "6":
                        GetAccountDetails();
                        break;
                    case "7":                    
                       ListAccounts();
                        break;
                    case "8":                   
                        GetTransactions();
                        break;
                    case "9":
                    case "exit":
                        Console.WriteLine("Exiting the app...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

            } while (choice.ToLower() != "9" && choice.ToLower() != "exit");
        }

        public static SqlConnection GetConnection()
        {
            return DBUtil.GetDBConn();
        }

        public static void CreateAccount()
        {
            while (true)
            {
                Console.WriteLine("\nSelect Account Type:");
                Console.WriteLine("1. savings");
                Console.WriteLine("2. current");
                Console.WriteLine("3. zero_balance");
                Console.WriteLine("4. back");
                Console.Write("Enter choice: ");
                string option = Console.ReadLine().ToLower();

                string accType = "";
                if (option == "1" || option == "savings") accType = "savings";
                else if (option == "2" || option == "current") accType = "current";
                else if (option == "3" || option == "zero_balance") accType = "zero_balance";
                else if (option == "4" || option == "back") break;
                else
                {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                Console.Write("Enter Customer ID: ");
                int custId = int.Parse(Console.ReadLine());

                // Check if customer exists
                con = GetConnection();
                cmd = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE customer_id = @cid", con);
                cmd.Parameters.AddWithValue("@cid", custId);
                int count = (int)cmd.ExecuteScalar();
                con.Close();

                if (count == 0)
                {
                    Console.WriteLine("Customer not found. Please enter new customer details:");

                    Console.Write("First Name: ");
                    string fname = Console.ReadLine();
                    Console.Write("Last Name: ");
                    string lname = Console.ReadLine();
                    Console.Write("DOB (yyyy-mm-dd): ");
                    DateTime dob = DateTime.Parse(Console.ReadLine());
                    Console.Write("Email: ");
                    string email = Console.ReadLine();
                    Console.Write("Phone: ");
                    string phone = Console.ReadLine();
                    Console.Write("Address: ");
                    string address = Console.ReadLine();

                    con = GetConnection();
                    cmd = new SqlCommand(@"INSERT INTO Customers 
                    (first_name, last_name, DOB, email, phone_number, address) 
                    VALUES (@fname, @lname, @dob, @mail, @phone, @addr);
                    SELECT SCOPE_IDENTITY();", con);

                    cmd.Parameters.AddWithValue("@fname", fname);
                    cmd.Parameters.AddWithValue("@lname", lname);
                    cmd.Parameters.AddWithValue("@dob", dob);
                    cmd.Parameters.AddWithValue("@mail", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@addr", address);

                    // Get the newly inserted customer_id
                    object result = cmd.ExecuteScalar();
                    custId = Convert.ToInt32(result);
                    con.Close();

                    Console.WriteLine("Customer created with ID: " + custId);
                }

                // Now insert account
                Console.Write("Enter Initial Balance: ");
                decimal balance = decimal.Parse(Console.ReadLine());

                con = GetConnection();
                cmd = new SqlCommand("INSERT INTO Accounts (customer_id, account_type, balance) VALUES (@cid, @type, @bal)", con);
                cmd.Parameters.AddWithValue("@cid", custId);
                cmd.Parameters.AddWithValue("@type", accType);
                cmd.Parameters.AddWithValue("@bal", balance);
                int rows = cmd.ExecuteNonQuery();
                con.Close();

                Console.WriteLine(rows > 0 ? "Account created successfully!" : "Failed to create account.");
            }
        }


        public static int AddNewCustomer()
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter Date of Birth (yyyy-MM-dd): ");
            string dob = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Phone Number: ");
            string phone = Console.ReadLine();
            Console.Write("Enter Address: ");
            string address = Console.ReadLine();

            con = GetConnection();
            cmd = new SqlCommand(@"INSERT INTO Customers 
        (first_name, last_name, DOB, email, phone_number, address) 
        OUTPUT INSERTED.customer_id 
        VALUES (@fname, @lname, @dob, @mail, @phone, @addr)", con);

            cmd.Parameters.AddWithValue("@fname", firstName);
            cmd.Parameters.AddWithValue("@lname", lastName);
            cmd.Parameters.AddWithValue("@dob", DateTime.Parse(dob));
            cmd.Parameters.AddWithValue("@mail", email);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@addr", address);

            int newCustomerId = (int)cmd.ExecuteScalar();
            Console.WriteLine($"Customer created successfully! New Customer ID: {newCustomerId}");
            con.Close();
            return newCustomerId;
        }



        public static void Deposit()
        {
            Console.Write("Enter Account ID: ");
            int accId = int.Parse(Console.ReadLine());
            Console.Write("Enter Amount to Deposit: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            con = GetConnection();
            cmd = new SqlCommand("UPDATE Accounts SET balance = balance + @amt WHERE account_id = @accid", con);
            cmd.Parameters.AddWithValue("@amt", amount);
            cmd.Parameters.AddWithValue("@accid", accId);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("INSERT INTO Transactions (account_id, transaction_type, amount, transaction_date) VALUES (@aid, 'deposit', @amt, GETDATE())", con);
            cmd.Parameters.AddWithValue("@aid", accId);
            cmd.Parameters.AddWithValue("@amt", amount);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Amount deposited successfully!");
            con.Close();
        }

        public static void Withdraw()
        {
            Console.Write("Enter Account ID: ");
            int accId = int.Parse(Console.ReadLine());
            Console.Write("Enter Amount to Withdraw: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            con = GetConnection();

            cmd = new SqlCommand("SELECT balance FROM Accounts WHERE account_id = @accid", con);
            cmd.Parameters.AddWithValue("@accid", accId);
            decimal balance = (decimal)cmd.ExecuteScalar();

            if (balance >= amount)
            {
                cmd = new SqlCommand("UPDATE Accounts SET balance = balance - @amt WHERE account_id = @accid", con);
                cmd.Parameters.AddWithValue("@amt", amount);
                cmd.Parameters.AddWithValue("@accid", accId);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO Transactions (account_id, transaction_type, amount, transaction_date) VALUES (@aid, 'withdrawal', @amt, GETDATE())", con);
                cmd.Parameters.AddWithValue("@aid", accId);
                cmd.Parameters.AddWithValue("@amt", amount);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Withdrawal successful.");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }

            con.Close();
        }

        public static void GetBalance()
        {
            Console.Write("Enter Account ID: ");
            int accId = int.Parse(Console.ReadLine());

            con = GetConnection();
            cmd = new SqlCommand("SELECT balance FROM Accounts WHERE account_id = @accid", con);
            cmd.Parameters.AddWithValue("@accid", accId);
            object result = cmd.ExecuteScalar();

            if (result != null)
                Console.WriteLine("Current Balance: " + result);
            else
                Console.WriteLine("Account not found.");

            con.Close();
        }

        public static void Transfer()
        {
            Console.Write("Enter Source Account ID: ");
            int fromAcc = int.Parse(Console.ReadLine());

            Console.Write("Enter Destination Account ID: ");
            int toAcc = int.Parse(Console.ReadLine());

            Console.Write("Enter Amount to Transfer: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            con = GetConnection();

            SqlTransaction transaction = con.BeginTransaction();

            try
            {
                // Withdraw from source
                cmd = new SqlCommand("UPDATE Accounts SET balance = balance - @amt WHERE account_id = @accid", con, transaction);
                cmd.Parameters.AddWithValue("@amt", amount);
                cmd.Parameters.AddWithValue("@accid", fromAcc);
                cmd.ExecuteNonQuery();

                // Deposit to destination
                cmd = new SqlCommand("UPDATE Accounts SET balance = balance + @amt WHERE account_id = @accid", con, transaction);
                cmd.Parameters.AddWithValue("@amt", amount);
                cmd.Parameters.AddWithValue("@accid", toAcc);
                cmd.ExecuteNonQuery();

                // Insert into Transactions for source (withdrawal)
                cmd = new SqlCommand("INSERT INTO Transactions (account_id, transaction_type, amount, transaction_date) VALUES (@accid, 'withdrawal', @amt, GETDATE())", con, transaction);
                cmd.Parameters.AddWithValue("@accid", fromAcc);
                cmd.Parameters.AddWithValue("@amt", amount);
                cmd.ExecuteNonQuery();

                // Insert into Transactions for destination (deposit)
                cmd = new SqlCommand("INSERT INTO Transactions (account_id, transaction_type, amount, transaction_date) VALUES (@accid, 'deposit', @amt, GETDATE())", con, transaction);
                cmd.Parameters.AddWithValue("@accid", toAcc);
                cmd.Parameters.AddWithValue("@amt", amount);
                cmd.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("Transaction successful!");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine("Transaction failed: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        public static void GetAccountDetails()
        {
            Console.Write("Enter Account ID: ");
            int accId = int.Parse(Console.ReadLine());

            con = GetConnection();
            cmd = new SqlCommand(@"SELECT a.account_id, a.customer_id, a.account_type, a.balance, 
                                  c.first_name, c.last_name, c.email 
                           FROM Accounts a 
                           JOIN Customers c ON a.customer_id = c.customer_id 
                           WHERE a.account_id = @accid", con);

            cmd.Parameters.AddWithValue("@accid", accId);
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Console.WriteLine("\n--- Account Details ---");
                Console.WriteLine("Account ID   : " + dr["account_id"]);
                Console.WriteLine("Customer ID  : " + dr["customer_id"]);
                Console.WriteLine("Type         : " + dr["account_type"]);
                Console.WriteLine("Balance      : " + dr["balance"]);
                Console.WriteLine("First Name   : " + dr["first_name"]);
                Console.WriteLine("Last Name    : " + dr["last_name"]);
                Console.WriteLine("Email        : " + dr["email"]);
            }
            else
            {
                Console.WriteLine("Account not found.");
            }

            dr.Close();
            con.Close();
        }


        public static void ListAccounts()
        {
            con = GetConnection();
            string query = @"SELECT a.account_id, a.customer_id, a.account_type, a.balance,
                            c.first_name, c.last_name
                     FROM Accounts a
                     JOIN Customers c ON a.customer_id = c.customer_id";

            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();

            Console.WriteLine("\n--- List of All Accounts ---");

            bool hasData = false;
            while (dr.Read())
            {
                hasData = true;
                Console.WriteLine("---------------------------");
                Console.WriteLine("Account ID     : " + dr["account_id"]);
                Console.WriteLine("Customer ID    : " + dr["customer_id"]);
                Console.WriteLine("Name           : " + dr["first_name"] + " " + dr["last_name"]);
                Console.WriteLine("Account Type   : " + dr["account_type"]);
                Console.WriteLine("Balance        : " + dr["balance"]);
            }

            if (!hasData)
            {
                Console.WriteLine("No accounts found.");
            }

            dr.Close();
            con.Close();
        }


        public static void GetTransactions()
        {
            Console.Write("Enter Account ID: ");
            int accId = int.Parse(Console.ReadLine());

            con = GetConnection();
            string query = @"SELECT transaction_id, account_id, transaction_type, amount, transaction_date
                     FROM Transactions
                     WHERE account_id = @accid
                     ORDER BY transaction_date DESC";

            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@accid", accId);
            dr = cmd.ExecuteReader();

            Console.WriteLine("\n--- Transactions ---");
            bool hasData = false;

            while (dr.Read())
            {
                hasData = true;
                Console.WriteLine("-----------------------");
                Console.WriteLine("Transaction ID   : " + dr["transaction_id"]);
                Console.WriteLine("Account ID       : " + dr["account_id"]);
                Console.WriteLine("Type             : " + dr["transaction_type"]);
                Console.WriteLine("Amount           : " + dr["amount"]);
                Console.WriteLine("Date             : " + dr["transaction_date"]);
            }

            if (!hasData)
            {
                Console.WriteLine("No transactions found for this account.");
            }

            dr.Close();
            con.Close();
        }

    }
}
