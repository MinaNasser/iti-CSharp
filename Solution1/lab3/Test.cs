using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Test
    {
        public static void getGrater()
        {
            Console.Write("Enter the first number: ");
            int num1 = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Enter the second number: ");
            int num2 = int.Parse(Console.ReadLine() ?? "0");
            int greaterNumber = Utils.Greater(num1, num2);
            Console.WriteLine($"The greater number is: {greaterNumber}");
        }
        public static void PrintSwap()
        {
            Console.Write("Enter the first number: ");
            int num1 = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Enter the second number: ");
            int num2 = int.Parse(Console.ReadLine() ?? "0");
            Console.Write($"2 Number Before Swap  First: {num1} Second: {num2} \n");
            Utils.Swap(ref num1, ref num2);
            Console.Write($"2 Number After Swap  First: {num1} Second: {num2} \n");
        }
        public static void FactBasic()
        {
            Console.Write("Enter the  number: ");
            int num1 = int.Parse(Console.ReadLine() ?? "0");
            int Factor = Utils.Fact(num1);
            Console.Write($"Factoral: {num1} =  {Factor} \n");
        }
        public static void FactRecurson()
        {
            Console.Write("Enter the  number: ");
            int num1 = int.Parse(Console.ReadLine() ?? "0");
            int Factor = Utils.FactRecurson(num1);
            Console.Write($"Factoral: {num1} =  {Factor} \n");
        }

        public static void PrintRotate()
        {
            Console.Write("Enter the first number: ");
            int num1 = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Enter the second number: ");
            int num2 = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Enter the Third number: ");
            int num3 = int.Parse(Console.ReadLine() ?? "0");
            Console.Write($"3 Number Before Swap  First: {num1} Second: {num2} Third: {num3} \n");
            Utils.Rotate(ref num1, ref num2,ref num3);
            Console.Write($"3 Number After Swap  First: {num1} Second: {num2} Third: {num3} \n");
        }
    }
}
