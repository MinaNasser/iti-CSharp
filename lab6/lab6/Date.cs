using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public class Date
    {
        public int Year { get; }
        public int Month { get; }
        public int Day { get; }

        public Date(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }
    }

}
