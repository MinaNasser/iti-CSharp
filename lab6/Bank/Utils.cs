using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
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
                Console.WriteLine("Factorial is not defined for negative numbers.");
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
            return n * FactRecurson(n - 1);
        }


        public static void Rotate(ref int a, ref int b, ref int c)
        {
            Swap(ref a, ref c);
        }

        public static void Reverse(string S)
        {
            if (S.Length == 0)
            {
                Console.WriteLine("String is Empty");
            }
            else if (S.Length == 1)
            {
                {
                    Console.WriteLine($"String Reverced: {S} ");
                }
            }
            else
            {
                string reversedString = "";
                for (int i = S.Length - 1; i >= 0; i--)
                {
                    reversedString += S[i]; 
                }
                Console.WriteLine("String Reversed: " + reversedString);

            }



        }


        public static void tryReverse()
        {
            Console.WriteLine("We Will Try for 4 time ");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"Enter Message this is {i+1}");
                string s = Console.ReadLine();
                Reverse(s);
            }
        }

        public static void FileSearch()
        {
            Console.Write("Enter the source file name: ");
            string sourceFileName = Console.ReadLine();

            Console.Write("Enter the destination file name: ");
            string destinationFileName = Console.ReadLine();

            FileHandler fileHandler = new FileHandler();
            fileHandler.CopyFileToUpper(sourceFileName, destinationFileName);
        }

    }
}
