using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D8
{
    public class FancyWidget : Widget
    {
        protected override void First()
        {
            Console.WriteLine("FancyWidget's overridden First method");
        }
        public new  void Second()
        {
            Console.WriteLine("FancyWidget's hidden Second method");
        }
        public void InVokeFirst()
        {
            First();
        }
    }
}
