using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.service
{
    public interface ICustomerServiceProvider
    {
        float GetAccountBalance(long accountNumber);
        void Deposit(long accountNumber, float amount);
        void Withdraw(long accountNumber, float amount);
        void Transfer(long fromAccountNumber, long toAccountNumber, float amount);
        string GetAccountDetails(long accountNumber);
    }
}
