using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.exception
{
    public class AdminNotFoundException : Exception
    {
        public AdminNotFoundException() : base("Admin not found.") { }

        public AdminNotFoundException(string message) : base(message) { }
    }
}
