using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D8
{
    interface IWidget
    {
          void First();

         void Second();
        void Third()
        {
            Console.WriteLine("Hello From Third Function");
        }

    }
}
