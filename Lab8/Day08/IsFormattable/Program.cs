using IsFormattable;
using lab3;
using System;

namespace Bank
{

    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Testing different types with IsItFormattable method:");

            Console.WriteLine($"int (123): {Utils.IsItFormattable(123)}");         // True
            Console.WriteLine($"double (123.45): {Utils.IsItFormattable(123.45)}"); // True
            Console.WriteLine($"ulong (123456789UL): {Utils.IsItFormattable(123456789UL)}"); // True

            Console.WriteLine($"object (new object()): {Utils.IsItFormattable(new object())}"); // False
            Console.WriteLine($"DateTime (DateTime.Now): {Utils.IsItFormattable(DateTime.Now)}"); // True
            Console.WriteLine($"Custom class instance: {Utils.IsItFormattable(new MyClass())}"); // False
            Console.WriteLine($"Custom class implementing IFormattable: {Utils.IsItFormattable(new MyFormattableClass())}"); // True
            Console.WriteLine("\n\n#################\n\n"); 
            
            Console.WriteLine("Testing different types with IPrintable method:");

            int first = 10;
            string Second = "Mina Nasser";
            Coordinate c = new Coordinate(21.0, 68.0);
            Utils.Display(first);
            Utils.Display(Second);
            Utils.Display(c);
            Utils.Display(new MyClass());



        }
    }
    public class MyClass
    {
        public MyClass()
        {

            Console.WriteLine("From My Class Constractor");
        }
        public override string ToString()
        {
            return "From My Class As test";
        }
    }

    class MyFormattableClass : IFormattable
    {


        public string ToString()
        {
            return "MyFormattableClass Instance";
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            return "Formatted MyFormattableClass";
        }
    }






}
