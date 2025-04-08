using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BankingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n--- Banking System ---");
                Console.WriteLine("1. Loan Eligibility Check");
                Console.WriteLine("2. ATM Transaction Simulation");
                Console.WriteLine("3. Compound Interest Calculation");
                Console.WriteLine("4. Account Balance Checker");
                Console.WriteLine("5. Password Validation");
                Console.WriteLine("6. Bank Transaction History");
                Console.WriteLine("7. Customer and Account Info (Task 7)");
                Console.WriteLine("8. Account OOP Operations (Task 8)");
                Console.WriteLine("9. Abstraction (Task 9)");
                Console.WriteLine("0. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        LoanEligibility();
                        break;
                    case "2":
                        AtmTransaction();
                        break;
                    case "3":
                        CompoundInterest();
                        break;
                    case "4":
                        BalanceChecker();
                        break;
                    case "5":
                        PasswordValidation();
                        break;
                    case "6":
                        BankTransactions();
                        break;
                    case "7":
                        Task7_AccountCustomerDetails();
                        break;
                    case "8":
                        Task8_AccountOperations();
                        break;
                    case "9":
                        Task9_AbstractionMenu();
                        break;

                    case "0":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        // Task 1: Loan Eligibility
        static void LoanEligibility()
        {
            Console.Write("Enter Credit Score: ");
            int creditScore = int.Parse(Console.ReadLine());

            Console.Write("Enter Annual Income: ");
            double income = double.Parse(Console.ReadLine());

            if (creditScore > 700 && income >= 50000)
                Console.WriteLine("You are eligible for loan.");
            else
                Console.WriteLine("You are not eligible for loan.");
        }

        // Task 2: ATM Transaction
        static void AtmTransaction()
        {
            Console.Write("Enter Current Balance: ");
            double balance = double.Parse(Console.ReadLine());

            Console.WriteLine("1. Check Balance\n2. Withdraw\n3. Deposit");
            Console.Write("Choose an option: ");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.WriteLine($"Your balance is: {balance}");
                    break;
                case 2:
                    Console.Write("Enter withdrawal amount: ");
                    double withdrawAmount = double.Parse(Console.ReadLine());
                    if ((withdrawAmount % 100 == 0 || withdrawAmount % 500 == 0) && withdrawAmount <= balance)
                    {
                        balance -= withdrawAmount;
                        Console.WriteLine($" Withdrawal successful. New Balance: {balance}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount or insufficient funds.");
                    }
                    break;
                case 3:
                    Console.Write("Enter deposit amount: ");
                    double deposit = double.Parse(Console.ReadLine());
                    balance += deposit;
                    Console.WriteLine($"Deposit successful. New Balance: {balance}");
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        // Task 3: Compound Interest
        static void CompoundInterest()
        {
            Console.Write("Enter number of customers: ");
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nCustomer {i + 1}:");
                Console.Write("Initial Balance: ");
                double principal = double.Parse(Console.ReadLine());
                Console.Write("Annual Interest Rate (%): ");
                double rate = double.Parse(Console.ReadLine());
                Console.Write("Number of Years: ");
                int years = int.Parse(Console.ReadLine());

                double futureBalance = principal * Math.Pow(1 + rate / 100, years);
                Console.WriteLine($"Future Balance: {futureBalance:F2}");
            }
        }

        // Task 4: Balance Checker with Account Number Validation
        static void BalanceChecker()
        {
            Dictionary<int, double> accounts = new Dictionary<int, double>()
            {
                {101, 25000.00},
                {102, 50000.50},
                {103, 100000.75}
            };

            while (true)
            {
                Console.Write("\nEnter your account number (or 0 to exit): ");
                int acc = int.Parse(Console.ReadLine());
                if (acc == 0) break;

                if (accounts.ContainsKey(acc))
                    Console.WriteLine($"Your balance is: {accounts[acc]}");
                else
                    Console.WriteLine("Invalid account number. Try again.");
            }
        }

        // Task 5: Password Validation
        static void PasswordValidation()
        {
            Console.Write("Create a password: ");
            string password = Console.ReadLine();

            bool hasUpper = false, hasDigit = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                if (char.IsDigit(c)) hasDigit = true;
            }

            if (password.Length >= 8 && hasUpper && hasDigit)
                Console.WriteLine("Password is valid.");
            else
                Console.WriteLine("Invalid password. Must be at least 8 characters, include one uppercase letter and one digit.");
        }

        // Task 6: Bank Transaction History
        static void BankTransactions()
        {
            List<string> history = new List<string>();
            double balance = 0;

            while (true)
            {
                Console.Write("\nChoose transaction (deposit/withdraw/exit): ");
                string action = Console.ReadLine().ToLower();

                if (action == "exit")
                    break;

                Console.Write("Enter amount: ");
                double amount = double.Parse(Console.ReadLine());

                if (action == "deposit")
                {
                    balance += amount;
                    history.Add($"Deposited: {amount}");
                }
                else if (action == "withdraw")
                {
                    if (amount <= balance)
                    {
                        balance -= amount;
                        history.Add($"Withdrawn: {amount}");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds.");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid transaction type.");
                }

                Console.WriteLine($"Current Balance: {balance}");
            }

            Console.WriteLine("\n--- Transaction History ---");
            foreach (string record in history)
            {
                Console.WriteLine(record);
            }
        }
        static void Task7_AccountCustomerDetails()
        {
            Console.WriteLine("\n--- Task 7: Customer and Account Information ---");

            Customer customer = new Customer(101, "Ravi", "Kumar", "ravi@example.com", "9876543210", "Chennai, India");
            customer.PrintCustomerDetails();

            Account account = new Account(123456, "Savings", 50000.0);
            account.PrintAccountDetails();

            account.Deposit(20000);
            account.Withdraw(10000);
            account.CalculateInterest();

            account.PrintAccountDetails();
        }
        static void Task8_AccountOperations()
        {
            Console.WriteLine("\n--- Account Creation ---");
            Console.WriteLine("1. Create Savings Account");
            Console.WriteLine("2. Create Current Account");
            Console.Write("Choose account type: ");
            string typeChoice = Console.ReadLine();

            Console.Write("Enter Account Number: ");
            int accNum = int.Parse(Console.ReadLine());

            Console.Write("Enter Initial Balance: ");
            double initBal = double.Parse(Console.ReadLine());

            Account acc = null;

            switch (typeChoice)
            {
                case "1":
                    acc = new SavingsAccount(accNum, initBal);
                    break;
                case "2":
                    acc = new CurrentAccount(accNum, initBal);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }

            while (true)
            {
                Console.WriteLine("\n1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Calculate Interest");
                Console.WriteLine("4. View Account Details");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");
                string action = Console.ReadLine();

                switch (action)
                {
                    case "1":
                        Console.Write("Enter amount to deposit: ");
                        double dep = double.Parse(Console.ReadLine());
                        acc.Deposit(dep);
                        break;
                    case "2":
                        Console.Write("Enter amount to withdraw: ");
                        double wd = double.Parse(Console.ReadLine());
                        acc.Withdraw(wd);
                        break;
                    case "3":
                        acc.CalculateInterest();
                        break;
                    case "4":
                        acc.PrintAccountDetails();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
        static void Task9_AbstractionMenu()
        {
            Console.WriteLine("Choose Account Type:\n1. Savings\n2. Current");
            string type = Console.ReadLine();
            Console.Write("Enter Account Number: ");
            string accNo = Console.ReadLine();
            Console.Write("Enter Customer Name: ");
            string custName = Console.ReadLine();
            Console.Write("Enter Initial Balance: ");
            double balance = Convert.ToDouble(Console.ReadLine());

            BankAccount account;
            switch (type)
            {
                case "1":
                    account = new AbstractSavingsAccount(accNo, custName, balance);
                    break;
                case "2":
                    account = new AbstractCurrentAccount(accNo, custName, balance);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    return;
            }

            while (true)
            {
                Console.WriteLine("\n1. Deposit\n2. Withdraw\n3. Calculate Interest\n0. Exit");
                string op = Console.ReadLine();
                switch (op)
                {
                    case "1":
                        Console.Write("Enter amount to deposit: ");
                        float dAmt = float.Parse(Console.ReadLine());
                        account.Deposit(dAmt);
                        break;
                    case "2":
                        Console.Write("Enter amount to withdraw: ");
                        float wAmt = float.Parse(Console.ReadLine());
                        account.Withdraw(wAmt);
                        break;
                    case "3":
                        account.CalculateInterest();
                        break;
                    case "0":
                        return;
                }
            }
        }


    }
}
