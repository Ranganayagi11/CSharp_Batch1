namespace BankingSystem
{
    public class Account
    {
        private int accountNumber;
        protected string accountType;
        protected double balance;


        public Account(int accNum, string accType, double bal)
        {
            accountNumber = accNum;
            accountType = accType;
            balance = bal;
        }

        public virtual void Deposit(float amount) => balance += amount;
        public virtual void Deposit(int amount) => balance += amount;
        public virtual void Deposit(double amount) => balance += amount;

        public virtual void Withdraw(float amount)
        {
            if (amount <= balance)
                balance -= amount;
            else
                Console.WriteLine("Insufficient balance.");
        }
        public virtual void Withdraw(int amount)
        {
            if (amount <= balance)
                balance -= amount;
            else
                Console.WriteLine("Insufficient balance.");
        }
        public virtual void Withdraw(double amount)
        {
            if (amount <= balance)
                balance -= amount;
            else
                Console.WriteLine("Insufficient balance.");
        }

        public virtual void CalculateInterest()
        {
            double interest = balance * 0.045;
            balance += interest;
            Console.WriteLine($"Interest added: {interest}, New balance: {balance}");
        }

        public void PrintAccountDetails()
        {
            Console.WriteLine($"Account Number: {accountNumber}, Type: {accountType}, Balance: {balance}");
        }
    }

}

