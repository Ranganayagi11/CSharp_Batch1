using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    public class CurrentAccount : Account
    {
        private const double overdraftLimit = 5000;

        public CurrentAccount(int accNum, double bal)
            : base(accNum, "Current", bal)
        {
        }

        public override void Withdraw(double amount)
        {
            if (amount <= balance + overdraftLimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdrawn: {amount}, New balance: {balance}");
            }
            else
            {
                Console.WriteLine("Withdrawal exceeds overdraft limit.");
            }
        }

        public override void CalculateInterest()
        {
            Console.WriteLine("No interest for current account.");
        }
    }

}
