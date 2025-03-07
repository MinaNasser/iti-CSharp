using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public  class NegativeOprandExption:Exception
    {
        public NegativeOprandExption():base("Var Must be greater than 0 "){
        
        
        
        }
    }
}
