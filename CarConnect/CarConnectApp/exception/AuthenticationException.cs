﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.exception
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException() : base("Authentication failed.") { }

        public AuthenticationException(string message) : base(message) { }
    }
}
