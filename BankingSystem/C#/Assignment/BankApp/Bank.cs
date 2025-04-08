using System;
using System.Collections.Generic;
using System.Linq;

namespace BankApp
{
    public class Bank
    {
        private List<Account> accounts = new List<Account>();

        public Account CreateAccount(Customer customer, string accType, float balance)
        {
            Account account = new Account(customer, accType, balance);
            accounts.Add(account);
            return account;
        }

        public float? GetAccountBalance(long accountNumber)
        {
            Account account = null;

            foreach (Account a in accounts)
            {
                if (a.AccountNumber == accountNumber)
                {
                    account = a;
                    break;
                }
            }

            return account?.Balance;
        }


        public float? Deposit(long accountNumber, float amount)
        {
            Account account = null;

            foreach (Account a in accounts)
            {
                if (a.AccountNumber == accountNumber)
                {
                    account = a;
                    break;
                }
            }

            if (account != null)
            {
                account.Deposit(amount);
                return account.Balance;
            }
            return null;
        }

        public float? Withdraw(long accountNumber, float amount)
        {
            Account account = null;

            foreach (Account a in accounts)
            {
                if (a.AccountNumber == accountNumber)
                {
                    account = a;
                    break;
                }
            }

            if (account != null && account.Withdraw(amount))
            {
                return account.Balance;
            }

            return null;
        }

        public bool Transfer(long fromAcc, long toAcc, float amount)
        {
            Account fromAccount = null;
            Account toAccount = null;

            foreach (Account a in accounts)
            {
                if (a.AccountNumber == fromAcc)
                {
                    fromAccount = a;
                }
                else if (a.AccountNumber == toAcc)
                {
                    toAccount = a;
                }

                if (fromAccount != null && toAccount != null)
                    break;
            }

            if (fromAccount != null && toAccount != null && fromAccount.Withdraw(amount))
            {
                toAccount.Deposit(amount);
                return true;
            }

            return false;
        }

        public void GetAccountDetails(long accountNumber)
        {
            Account account = null;

            foreach (Account a in accounts)
            {
                if (a.AccountNumber == accountNumber)
                {
                    account = a;
                    break;
                }
            }

            if (account != null)
            {
                account.PrintAccountInfo();
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

    }
}
