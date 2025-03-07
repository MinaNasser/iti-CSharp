using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D8
{
    public class Widget
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


/*
 
interface IWidget
{
    void First();
    void Second();
}

class Widget : IWidget
{
    public virtual void First()
    {
        Console.WriteLine("Widget's First method");
    }

    void IWidget.Second()
    {
        Console.WriteLine("Widget's explicitly implemented Second method");
    }
}
 
 
 */