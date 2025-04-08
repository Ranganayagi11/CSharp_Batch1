using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.bean
{
    public class SavingsAccount : Account
    {
        public float InterestRate { get; set; }

        public SavingsAccount(float initialBalance, Customer accountHolder, float interestRate)
            : base("Savings", Math.Max(initialBalance, 500), accountHolder) // Ensures min balance of 500
        {
            InterestRate = interestRate;
        }
    }
}
