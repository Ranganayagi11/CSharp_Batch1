using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Task12.Exceptions
{
    public class NullPointerException : Exception
    {
        public NullPointerException(string message) : base(message) { }
    }
}
