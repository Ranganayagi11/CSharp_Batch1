using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.bean
{
    public class Account
    {
        private static long lastAccNo = 1000; // Static variable to generate account numbers
        public long AccountNumber { get; set; }
        public string AccountType { get; set; }
        public float AccountBalance { get; set; }
        public Customer AccountHolder { get; set; }

        public Account(string accountType, float initialBalance, Customer accountHolder)
        {
            AccountNumber = ++lastAccNo; // Increment and assign account number
            AccountType = accountType;
            AccountBalance = initialBalance;
            AccountHolder = accountHolder;
        }
        //Task13 
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Account other = (Account)obj;
            return AccountNumber == other.AccountNumber;
        }
        //Task 13(3)
        public override int GetHashCode()
        {
            return AccountNumber.GetHashCode();
        }
    }
}
