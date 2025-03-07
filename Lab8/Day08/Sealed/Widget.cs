using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D8
{
   sealed class Widget : IWidget
    {
        void IWidget.First()
        {
            Console.WriteLine("Widget's First method From  Sealed");
        }

        public void Second()
        {
            Console.WriteLine("Widget's Second method From  Sealed");
        }

       
    }
}


