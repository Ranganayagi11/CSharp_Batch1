using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.exception
{
    public class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException() : base("Database connection failed.") { }

        public DatabaseConnectionException(string message) : base(message) { }
    }
}
