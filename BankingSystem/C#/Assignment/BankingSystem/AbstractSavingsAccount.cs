namespace BankingSystem
{
    public class AbstractSavingsAccount : BankAccount
    {
        private double interestRate = 4.5;

        public AbstractSavingsAccount(string accountNumber, string customerName, double balance)
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
            if (amount > Balance)
            {
                Console.WriteLine("Insufficient balance.");
            }
            else
            {
                Balance -= amount;
                Console.WriteLine($"Withdrew {amount}. Remaining Balance: {Balance}");
            }
        }

        public override void CalculateInterest()
        {
            double interest = Balance * interestRate / 100;
            Balance += interest;
            Console.WriteLine($"Interest of {interest} added. New Balance: {Balance}");
        }
    }
}
