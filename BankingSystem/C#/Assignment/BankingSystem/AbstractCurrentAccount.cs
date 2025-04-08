namespace BankingSystem
{
    public class AbstractCurrentAccount : BankAccount
    {
        private const double OverdraftLimit = 1000;

        public AbstractCurrentAccount(string accountNumber, string customerName, double balance)
            : base(accountNumber, customerName, balance)
        {
        }

        public override void Deposit(float amount)
        {
            Balance += amount;
            Console.WriteLine($"Deposited {amount}. New Balance: {Balance}");
        }

        public override void Withdraw(float amount)
        {
            if (amount > Balance + OverdraftLimit)
            {
                Console.WriteLine("Overdraft limit exceeded.");
            }
            else
            {
                Balance -= amount;
                Console.WriteLine($"Withdrew {amount}. Remaining Balance: {Balance}");
            }
        }

        public override void CalculateInterest()
        {
            Console.WriteLine("No interest for Current Account.");
        }
    }
}
