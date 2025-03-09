using Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inhertance
{
    public class PersonalAccount : BankAccount
    {
        public PersonalAccount(decimal initialBalance) : base(initialBalance)
        {

        }
    }
}

