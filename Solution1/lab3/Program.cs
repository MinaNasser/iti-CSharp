



using System.Diagnostics;

namespace lab3
{
    public class Program
    {
        public static void PrintLine()
        {
            FunOne();
            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine(string.Empty);
        }

        private static void FunOne()
        {
            FunTwo(50);
        }

        private static void FunTwo(int v)
        {
            StackTrace stackTrace = new StackTrace();

            StackFrame[] frames = stackTrace.GetFrames();

            foreach (StackFrame frame in frames)
            {
                Console.WriteLine(frame.GetMethod().Name);
            }
            //Console.WriteLine(v);
        }

        public static void Main(string[] args)
        {
            //Console.WriteLine("\n******************\n");
            //Console.WriteLine("\n Grater  | Method \n");
            //Test.getGrater();
            //Console.WriteLine("\n******************\n");
            //Console.WriteLine("\nSwap  | Method \n");
            //Test.PrintSwap();
            //Console.WriteLine("\n******************\n");
            //Console.WriteLine("\n Fact Basic  | Method \n");
            //Test.FactBasic();
            //Console.WriteLine("\n******************\n");
            //Console.WriteLine("\n Fact Recurson  | Method \n");
            //Test.FactRecurson();
            //Console.WriteLine("\n******************\n");
            //Console.WriteLine("\n Rotate   | Method \n");
            //Test.PrintRotate();

            #region MyRegion
            Console.WriteLine("Hello Enter Number : ");
            PrintLine();
            int x = int.Parse(Console.ReadLine());
            PrintLine();
            int y = x++;
            Console.WriteLine($"X = {x}");
            Console.WriteLine($"Y = {y}"); 
            #endregion

           







        }
    }
}