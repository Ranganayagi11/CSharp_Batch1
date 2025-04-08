using System;

namespace BankApp
{
    public class Account
    {
        private static long nextAccountNumber = 1001;

        public long AccountNumber { get; set; }
        public string AccountType { get; set; }
        public float Balance { get; set; }
        public Customer Customer { get; set; }

        public Account(Customer customer, string accountType, float balance)
        {
            AccountNumber = nextAccountNumber++;
            Customer = customer;
            AccountType = accountType;
            Balance = balance;
        }

        public void Deposit(float amount)
        {
            Balance += amount;
        }

        public bool Withdraw(float amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }

        public void PrintAccountInfo()
        {
            Console.WriteLine($"Account Number: {AccountNumber}");
            Console.WriteLine($"Account Type: {AccountType}");
            Console.WriteLine($"Balance: {Balance}");
            Console.WriteLine("--- Customer Info ---");
            Customer.PrintCustomerInfo();
        }
    }
}
