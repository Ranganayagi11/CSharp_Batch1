using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.bean
{
    public class CurrentAccount : Account
    {
        public float OverdraftLimit { get; set; }

        public CurrentAccount(float initialBalance, Customer accountHolder, float overdraftLimit)
            : base("Current", initialBalance, accountHolder)
        {
            OverdraftLimit = overdraftLimit;
        }

        public bool Withdraw(float amount)
        {
            if (AccountBalance + OverdraftLimit >= amount)
            {
                AccountBalance -= amount;
                return true;
            }
            return false;
        }
    }
}
