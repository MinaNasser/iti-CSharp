using System.Diagnostics;

namespace D4
{
    public class Program

    {
        public static void FunO()
        {
            FunT(50);
        }

        private static void FunT(int v)
        {
            StackTrace st = new StackTrace();
            StackFrame[] frame = st.GetFrames();
            foreach (StackFrame sf in frame)
            {
                Console.WriteLine(sf.GetMethod().Name);
            }
        }

        public static void PrintLine()
        {
            
            int i  ;
            for (i = 0; i< 5; i++)
            {
                Console.Write('#');
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            FunO(); 
            int i ;
            Console.WriteLine("Enter new Value");
            PrintLine();
            i = int.Parse(Console.ReadLine());
            int y= i++;
            PrintLine();
            Console.WriteLine($"I : {i}");
            PrintLine();
            Console.WriteLine($"Y : {y}");

        }
    }
}