using System;

namespace BankApp
{
    internal class BankApp
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();

            while (true)
            {
                Console.WriteLine("\n--- Bank Application ---");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Get Balance");
                Console.WriteLine("5. Transfer");
                Console.WriteLine("6. Get Account Details");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("First Name: ");
                        string fname = Console.ReadLine();
                        Console.Write("Last Name: ");
                        string lname = Console.ReadLine();
                        Console.Write("Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Phone (10 digits): ");
                        string phone = Console.ReadLine();
                        Console.Write("Address: ");
                        string address = Console.ReadLine();

                        Customer customer = new Customer(0, fname, lname, email, phone, address);

                        Console.Write("Account Type (Savings/Current): ");
                        string type = Console.ReadLine();
                        Console.Write("Initial Balance: ");
                        float balance = float.Parse(Console.ReadLine());

                        var acc = bank.CreateAccount(customer, type, balance);
                        Console.WriteLine($"Account created. Account Number: {acc.AccountNumber}");
                        break;

                    case "2":
                        Console.Write("Account Number: ");
                        long accNo = long.Parse(Console.ReadLine());
                        Console.Write("Amount to Deposit: ");
                        float amt = float.Parse(Console.ReadLine());
                        var newBal = bank.Deposit(accNo, amt);
                        Console.WriteLine(newBal != null ? $"New Balance: {newBal}" : "Account not found.");
                        break;

                    case "3":
                        Console.Write("Account Number: ");
                        long wAcc = long.Parse(Console.ReadLine());
                        Console.Write("Amount to Withdraw: ");
                        float wAmt = float.Parse(Console.ReadLine());
                        var wBal = bank.Withdraw(wAcc, wAmt);
                        Console.WriteLine(wBal != null ? $"New Balance: {wBal}" : "Insufficient funds or Account not found.");
                        break;

                    case "4":
                        Console.Write("Account Number: ");
                        long bAcc = long.Parse(Console.ReadLine());
                        var balanceInfo = bank.GetAccountBalance(bAcc);
                        Console.WriteLine(balanceInfo != null ? $"Balance: {balanceInfo}" : "Account not found.");
                        break;

                    case "5":
                        Console.Write("From Account Number: ");
                        long from = long.Parse(Console.ReadLine());
                        Console.Write("To Account Number: ");
                        long to = long.Parse(Console.ReadLine());
                        Console.Write("Amount to Transfer: ");
                        float tAmt = float.Parse(Console.ReadLine());
                        bool success = bank.Transfer(from, to, tAmt);
                        Console.WriteLine(success ? "Transfer successful." : "Transfer failed.");
                        break;

                    case "6":
                        Console.Write("Account Number: ");
                        long detailAcc = long.Parse(Console.ReadLine());
                        bank.GetAccountDetails(detailAcc);
                        break;

                    case "0":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}
