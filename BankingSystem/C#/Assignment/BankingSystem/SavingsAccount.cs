using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    public class SavingsAccount : Account
    {
        private double interestRate;

        public SavingsAccount(int accNum, double bal, double rate = 4.5)
            : base(accNum, "Savings", bal)
        {
            interestRate = rate;
        }

        public override void CalculateInterest()
        {
            double interest = balance * interestRate / 100;
            balance += interest;
            Console.WriteLine($"Interest added: {interest}, New balance: {balance}");
        }
    }

}
