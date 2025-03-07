using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    public class Parent
    {
        protected int x;
        protected int y;

        public Parent(int _x ,int _y)
        {
            x= _x;
            y= _y;
        }

        public int  Sum()
        {
            return x+y;
        }
        public virtual void Show()
        {
            Console.WriteLine("I'M Base ");
        }

    }


    class Derived : Parent
    {
        protected int z;
        public Derived(int _x , int _y , int _z):base(_x,_y)
        {
            z = _z;
        }
        public new int Sum()
        {
            return z+ base.Sum();

        }
        public override void Show()
        {
            Console.WriteLine("I'M Derived");
        }
    }
}
