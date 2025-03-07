using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D8
{
    public abstract class Widget 
    {
        protected abstract void First();


        public void Second()
        {
            Console.WriteLine("Widget's Second method");
        }
    }
}


