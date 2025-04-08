using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.bean
{
    public class ZeroBalanceAccount : Account
    {
        public ZeroBalanceAccount(Customer accountHolder)
            : base("ZeroBalance", 0, accountHolder)
        {
        }
    }
}
