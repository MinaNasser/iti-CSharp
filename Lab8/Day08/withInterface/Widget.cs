using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D8
{
    public class Widget : IWidget
    {
        public virtual void First()
        {
            Console.WriteLine("Widget's First method");
        }

        public void Second()
        {
            Console.WriteLine("Widget's Second method");
        }

    }
}


