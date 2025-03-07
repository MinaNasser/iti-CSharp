using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    public class GeoShape
    {
        public int Dim1 { get; init; }
        public int Dim2 { get; init; }


        public GeoShape(int _d1 , int _d2 )
        {
            
            Dim1 = _d1;
            Dim2 = _d2;
        }



        public virtual double Area { get; } = -1;
    }



    


}
