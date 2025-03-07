using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    public class ShapesEngin
    {
        public static double SumOfAreas(GeoShape[] shapes)
        {
            double sum = 0;
            foreach (GeoShape shape in shapes)
            {
                sum += shape.Area;
            }
            return sum;
        }
    }
}
