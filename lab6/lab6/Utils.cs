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
                Console.WriteLine($"Enter Message this is {i + 1}");
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

        public static void enterFilePath()
        {
            Console.Write("Enter the source file name: ");
            string sourceFileName = Console.ReadLine();
            FileHandler file = new FileHandler();
            file.SummarizeFile(sourceFileName);

        }
        /*
         * multiply matrices together
         */
        private static void Mul2DArr(int[,] a1, int[,] b)
        {
            int rowsA = a1.GetLength(0);
            int colsA = a1.GetLength(1);
            int rowsB = b.GetLength(0);
            int colsB = b.GetLength(1);
            if (colsA != rowsB)
            {
                Console.WriteLine("Matrix multiplication is not possible. The number of columns of matrix A must be equal to the number of rows of matrix B.");

                return;
            }

            int[,] result = new int[rowsA, colsB];
            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    for (int k = 0; k < colsA; k++)
                    {
                        result[i, j] += a1[i, k] * b[k, j];
                    }
                }
            }
            Console.WriteLine("Result of multiplication:");
            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    Console.Write(result[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        public static void Enter2DArr()
        {
            int[,] arr1 = new int[2, 2];
            int[,] arr2 = new int[2, 2];
            Console.WriteLine("Please Enter items for Array 1:");
            for (int i = 0; i < arr1.GetLength(0); i++)
            {
                for (int j = 0; j < arr1.GetLength(1); j++)
                {
                    Console.Write($"Enter item for arr1[{i + 1}, {j + 1}]: ");
                    arr1[i, j] = int.Parse(Console.ReadLine() ?? "0");
                }
            }
            Console.WriteLine("Please Enter items for Array 2:");
            for (int i = 0; i < arr2.GetLength(0); i++)
            {
                for (int j = 0; j < arr2.GetLength(1); j++)
                {
                    Console.Write($"Enter item for arr2[{i + 1}, {j + 1}]: ");
                    arr2[i, j] = int.Parse(Console.ReadLine() ?? "0");
                }
            }
            Mul2DArr(arr1, arr2);
        }


        public static int[,] Method()
        {
            return new int[3, 5] { { 42, 42, 42, 42, 42 },
                           { 42, 42, 42, 42, 42 },
                           { 42, 42, 42, 42, 42 } };
        }

        public static void Parameter(int[,] arr)
        {
            Console.WriteLine("Length of the first dimension: " + arr.GetLength(0)); 
            Console.WriteLine("Length of the second dimension: " + arr.GetLength(1)); 
        }

    }


}

