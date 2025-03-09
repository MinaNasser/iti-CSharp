using System;
using System.Linq;
using System.Text;

namespace Bank.Utils
{
    public class Utils
    {
        public static int Greater(int a, int b) => a > b ? a : b;

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
                return -1;
            }

            int result = 1;
            for (int i = 2; i <= n; i++)
                result *= i;

            return result;
        }

        public static int FactRecursion(int n)
        {
            if (n < 0) return -1;
            if (n == 0 || n == 1) return 1;
            return n * FactRecursion(n - 1);
        }

        public static void Rotate(ref int a, ref int b, ref int c)
        {
            int temp = a;
            a = b;
            b = c;
            c = temp;
        }

        public static string Reverse(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "String is Empty";

            return new string(s.Reverse().ToArray());
        }

        public static void TryReverse()
        {
            Console.WriteLine("We Will Try for 4 times");
            for (int i = 0; i < 4; i++)
            {
                Console.Write($"Enter Message ({i + 1}/4): ");
                string input = Console.ReadLine();
                Console.WriteLine("Reversed: " + Reverse(input));
            }
        }

        public static void FileSearch()
        {
            Console.Write("Enter the source file name: ");
            string sourceFileName = Console.ReadLine();

            if (!System.IO.File.Exists(sourceFileName))
            {
                Console.WriteLine("Error: Source file does not exist.");
                return;
            }

            Console.Write("Enter the destination file name: ");
            string destinationFileName = Console.ReadLine();

            FileHandler fileHandler = new FileHandler();
            fileHandler.CopyFileToUpper(sourceFileName, destinationFileName);

            Console.WriteLine("File copied successfully!");
        }
    }
}
