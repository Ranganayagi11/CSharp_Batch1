using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.bean;

namespace Task11.service
{
    public class BankServiceProviderImpl : CustomerServiceProviderImpl, IBankServiceProvider
    {
        public List<Account> ListAccounts()
        {
            return accounts;
        }
    }
}
