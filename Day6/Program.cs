namespace Day6
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Point p1 = new Point(10, 20);
            Point p2 = new Point(30, 40);
            Point p3 = new Point(50, 60);
            Point p4 = p1;


            if (p1.Equals(p3))
            {
                Console.WriteLine("P1 = P3 ");
            }
            else
            {
                Console.WriteLine("P1 != P3 ");
            }
            if (p1.Equals(p4))
            {
                Console.WriteLine("P1 = P4 ");
            }
            else
            {
                Console.WriteLine("P1 != P4 ");
            }
            /*
             
                System.Object Memmbers 
                ref
                public virtual bool Equlas(Object O){}
                public static bool Equlas(Object O1 , Object o2){
                    return O1.Equlas(o2)
                }

                value
                public static bool ReferenceEqulas(Object O1 , Object o2)
             
             */

        }
    }
}