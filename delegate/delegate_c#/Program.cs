using System;

namespace Delegate
{

    public class Program
    {
        public static int Main()
        {
            Delegates delegates = new Delegates();
            Del del = new Del(Delegates.EnToGer);
            Console.WriteLine(del.Invoke("HI"));
            Console.WriteLine(del("Hi"));
            return 0;
        }
    }
}