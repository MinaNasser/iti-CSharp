using IsFormattable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFormattable
{


    class Coordinate : IPrintable
    {
        private double x, y;

        public Coordinate(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void Print()
        {
            Console.WriteLine($"Coordinate: ({x}, {y})");
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

    }

}