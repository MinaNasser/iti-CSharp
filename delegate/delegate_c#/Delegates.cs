using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    delegate string Del(string name);
    public class Delegates
    {
        public static string EnToFR(string input)
        {
            return "From Frinch";
        }
        public static string EnToSp(string input)
        {
            return "From Spanish";
        }
        public static string EnToGer(string input)
        {
            return "From Germinay";
        }

    }
}
