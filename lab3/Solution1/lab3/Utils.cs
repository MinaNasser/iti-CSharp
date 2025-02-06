using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Utils
    {
        public static int Greater(int a, int b)
        {
            if (a > b)
            {
                return a;
            }
            else
            {
                return b;
            }
        }

        public static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        public static int Fact(int n)
        {
            if (n < 0)
            {
                Console.WriteLine( "Factorial is not defined for negative numbers.");
                return 0;
            }

            int result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }

            return result;
        }

        public static int FactRecurson(int n)
        {
            if (n == 0 || n == 1)
                return 1;
            return n* FactRecurson(n-1);
        }


        public static void Rotate(ref int a, ref int b,ref int c)
        {
            int temp = c;
            c = b;
            b = a;
            a = temp;
        }
    }

}
