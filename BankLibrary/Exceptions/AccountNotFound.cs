using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary.Exceptions
{
    public sealed class  AccountNotFound : Exception
    {
        public AccountNotFound (string accountNumber)
            :base($"Can't find account with this number: {accountNumber}")
        {

        }
        public AccountNotFound()
            : base("404, Account not Found!")
        {
        }
    }
}
