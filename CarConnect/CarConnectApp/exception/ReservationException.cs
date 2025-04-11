using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.exception
{
    
        public class ReservationException : Exception
        {
            public ReservationException() : base("Reservation error occurred.") { }

            public ReservationException(string message) : base(message) { }
        }
    }


