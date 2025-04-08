using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.bean;

namespace Task11.service
{
    public interface IBankServiceProvider
    {
        void CreateAccount(Customer customer, string accountType, float initialBalance);
        List<Account> ListAccounts();
    }
}
