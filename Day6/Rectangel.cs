using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    public class Rectangel : GeoShape
    {
        public Rectangel(int w, int h) : base(w, h)
        {
        }
        public override double Area 
        { get{return Dim1 * Dim2;}}
    }
}
