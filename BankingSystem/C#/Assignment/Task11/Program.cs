using System;
using Task11.bean;
using Task11.service;
using Task11.Task12.Exceptions;

namespace Task11
{
    class Program
    {
        static BankServiceProviderImpl bankService = new BankServiceProviderImpl();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n--- BANK SYSTEM MENU ---");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Get Balance");
                Console.WriteLine("5. Transfer");
                Console.WriteLine("6. Get Account Details");
                Console.WriteLine("7. List All Accounts");
                Console.WriteLine("8. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
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
                            ListAllAccounts();
                            break;
                        case "8":
                            Console.WriteLine("Exiting...");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                }
                catch (InvalidAccountException ex)
                {
                    Console.WriteLine($"[Invalid Account] {ex.Message}");
                }
                catch (InsufficientFundException ex)
                {
                    Console.WriteLine($"[Insufficient Funds] {ex.Message}");
                }
                catch (OverDraftLimitExceededException ex)
                {
                    Console.WriteLine($"[Overdraft Limit] {ex.Message}");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("[Error] Some required information is missing. Please try again.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("[Input Error] Invalid format. Please enter numeric values where required.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Unexpected Error] {ex.Message}");
                }
            }
        }

        static void CreateAccount()
        {
            Console.WriteLine("\nSelect Account Type: 1. Savings  2. Current  3. ZeroBalance");
            string accChoice = Console.ReadLine();

            Console.Write("Enter Customer ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter First Name: ");
            string fName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lName = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Enter Address: ");
            string address = Console.ReadLine();

            float balance = 0;
            if (accChoice != "3")
            {
                Console.Write("Enter Initial Balance: ");
                balance = float.Parse(Console.ReadLine());
            }

            string type = accChoice switch
            {
                "1" => "Savings",
                "2" => "Current",
                "3" => "ZeroBalance",
                _ => "Invalid"
            };

            if (type != "Invalid")
            {
                var customer = new Customer(id, fName, lName, email, phone, address);
                bankService.CreateAccount(customer, type, balance);
            }
            else
            {
                Console.WriteLine("Invalid account type selected.");
            }
        }

        static void Deposit()
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine());

            Console.Write("Enter Amount to Deposit: ");
            float amount = float.Parse(Console.ReadLine());

            bankService.Deposit(accNo, amount);
        }

        static void Withdraw()
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine());

            Console.Write("Enter Amount to Withdraw: ");
            float amount = float.Parse(Console.ReadLine());

            bankService.Withdraw(accNo, amount);
        }

        static void GetBalance()
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine());

            float balance = bankService.GetAccountBalance(accNo);
            Console.WriteLine($"Balance: Rs.{balance}");
        }

        static void Transfer()
        {
            Console.Write("Enter Sender Account Number: ");
            long fromAcc = long.Parse(Console.ReadLine());

            Console.Write("Enter Receiver Account Number: ");
            long toAcc = long.Parse(Console.ReadLine());

            Console.Write("Enter Amount to Transfer: ");
            float amount = float.Parse(Console.ReadLine());

            bankService.Transfer(fromAcc, toAcc, amount);
        }

        static void GetAccountDetails()
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine());

            string details = bankService.GetAccountDetails(accNo);
            Console.WriteLine(details);
        }

        static void ListAllAccounts()
        {
            foreach (var acc in bankService.ListAccounts())
            {
                Console.WriteLine($"Account No: {acc.AccountNumber}, Type: {acc.AccountType}, Balance: Rs.{acc.AccountBalance}, Holder: {acc.AccountHolder.FirstName} {acc.AccountHolder.LastName}");
            }
        }
    }
}
