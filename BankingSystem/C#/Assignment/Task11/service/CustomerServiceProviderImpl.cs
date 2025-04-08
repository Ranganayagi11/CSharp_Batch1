using System;
using System.Collections.Generic;
using System.Linq;
using Task11.bean;
using Task11.Task12.Exceptions;

namespace Task11.service
{
    public class CustomerServiceProviderImpl : ICustomerServiceProvider
    {
        public List<Account> accounts = new List<Account>();
        //Task 13
        //private HashSet<Account> accounts = new HashSet<Account>();
        //private Dictionary<long, Account> accounts = new Dictionary<long, Account>();

        public void CreateAccount(Customer customer, string accountType, float initialBalance)
        {
            Account account = null;

            switch (accountType.ToLower())
            {
                case "savings":
                    if (initialBalance < 500)
                    {
                        Console.WriteLine("Minimum balance required for Savings Account is Rs.500.");
                        return;
                    }
                    account = new SavingsAccount(initialBalance, customer, 4.0f); // 4% interest
                    break;

                case "current":
                    account = new CurrentAccount(initialBalance, customer, 1000); // ₹1000 overdraft
                    break;

                case "zerobalance":
                    account = new ZeroBalanceAccount(customer);
                    break;

                default:
                    Console.WriteLine("Invalid account type. Please choose Savings, Current, or ZeroBalance.");
                    return;
            }

            accounts.Add(account);
            Console.WriteLine($"Account created successfully. Account Number: {account.AccountNumber}");
        }

        public float GetAccountBalance(long accountNumber)
        {
            var account = FindAccount(accountNumber);
            if (account == null)
                throw new InvalidAccountException("Account not found.");

            return account.AccountBalance;
        }

        public void Deposit(long accountNumber, float amount)
        {
            var account = FindAccount(accountNumber);
            if (account == null)
                throw new InvalidAccountException("Account not found.");

            account.AccountBalance += amount;
            Console.WriteLine($"Deposited Rs.{amount}. New Balance: Rs.{account.AccountBalance}");
        }

        public void Withdraw(long accountNumber, float amount)
        {
            var account = FindAccount(accountNumber);
            if (account == null)
                throw new InvalidAccountException("Account not found.");

            if (account is CurrentAccount currentAcc)
            {
                if (amount <= currentAcc.AccountBalance + currentAcc.OverdraftLimit)
                {
                    currentAcc.AccountBalance -= amount;
                    Console.WriteLine($"Withdrawn Rs.{amount}. New Balance: Rs.{currentAcc.AccountBalance}");
                }
                else
                {
                    throw new OverDraftLimitExceededException("Withdrawal exceeds overdraft limit.");
                }
            }
            else if (account.AccountBalance >= amount)
            {
                account.AccountBalance -= amount;
                Console.WriteLine($"Withdrawn Rs.{amount}. New Balance: Rs.{account.AccountBalance}");
            }
            else
            {
                throw new InsufficientFundException("Insufficient balance.");
            }
        }

        public void Transfer(long fromAccountNumber, long toAccountNumber, float amount)
        {
            var from = FindAccount(fromAccountNumber);
            var to = FindAccount(toAccountNumber);

            if (from == null)
                throw new InvalidAccountException("Sender account not found.");
            if (to == null)
                throw new InvalidAccountException("Receiver account not found.");

            if (from is CurrentAccount currentAcc)
            {
                if (amount <= from.AccountBalance + currentAcc.OverdraftLimit)
                {
                    from.AccountBalance -= amount;
                    to.AccountBalance += amount;
                    Console.WriteLine($"Transferred Rs.{amount} from {from.AccountNumber} to {to.AccountNumber}");
                }
                else
                {
                    throw new OverDraftLimitExceededException("Transfer exceeds overdraft limit.");
                }
            }
            else if (from.AccountBalance >= amount)
            {
                from.AccountBalance -= amount;
                to.AccountBalance += amount;
                Console.WriteLine($"Transferred Rs.{amount} from {from.AccountNumber} to {to.AccountNumber}");
            }
            else
            {
                throw new InsufficientFundException("Insufficient balance for transfer.");
            }
        }

        public string GetAccountDetails(long accountNumber)
        {
            var acc = FindAccount(accountNumber);
            if (acc == null)
                throw new InvalidAccountException("Account not found.");

            var c = acc.AccountHolder;
            return $"Account No: {acc.AccountNumber}, Type: {acc.AccountType}, Balance: ₹{acc.AccountBalance}, Customer: {c.FirstName} {c.LastName}, Email: {c.Email}, Phone: {c.Phone}";
        }
        //Task 13: Collections
        public List<Account> ListAccounts()
        {
            //return new List<Account>(accounts);
            return accounts.OrderBy(a => a.AccountHolder.FirstName).ThenBy(a => a.AccountHolder.LastName).ToList();
            //return accounts.Values.OrderBy(a => a.AccountHolder.FirstName).ThenBy(a => a.AccountHolder.LastName).ToList();

        }

        private Account FindAccount(long accNumber)
        {
            foreach (Account acc in accounts)
            {
                if (acc.AccountNumber == accNumber)
                {
                    return acc;
                }
            }
            return null;
        }

    }
}
