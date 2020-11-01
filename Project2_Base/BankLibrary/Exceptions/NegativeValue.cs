using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary.Exceptions
{
    public class NegativeValue : Exception
    {
        public NegativeValue() : base($"Negative Value is not Allowed")
        {

        }
    }
}



