
namespace P1
{
    public class Program
    {
        public static void Main(string[] args)
        {

            #region Ctor
            //Point p1 = new Point();
            //Console.WriteLine(p1.ToString());
            //p1.X = 1000;
            //Console.WriteLine(p1.ToString());
            //p1 = new Point(10,10);
            //Console.WriteLine(p1.ToString());
            //Console.WriteLine("###############  p2");
            //Point p2 = new Point(10,10);
            //Console.WriteLine(p2.ToString());
            //Console.WriteLine("################### Copy constarctor");
            //Point p3 = new Point(p2);
            //Console.WriteLine(p3.ToString());
            //Console.WriteLine("################### Clone ");
            //Point point = (Point)p1.Clone();
            //Console.WriteLine(point.ToString());

            #endregion

            #region static oprator 

            Point p1 = new Point(10, 20);
            Point P2 = new Point(30, 40);

            Point P3 = default;
            Point P4 = default;

            P3 = p1 + P2;
            Console.WriteLine(P3.ToString());
            Console.WriteLine("############ before +=");
            Console.WriteLine(p1.ToString());
            Console.WriteLine(P2.ToString());
            p1 += P2;
            Console.WriteLine("############ After +=");
            Console.WriteLine(p1.ToString());
            Console.WriteLine(P2.ToString());

            Console.WriteLine("\n\n\n   PreFix and PosFix");


            P3 = ++p1;
            P4 = P2++;
            Console.WriteLine(p1.ToString());
            Console.WriteLine(P2.ToString());
            Console.WriteLine(P3.ToString());
            Console.WriteLine(P4.ToString());

            Console.WriteLine("@@@@@@@@@@@@@");
            Point3D point3 = new Point3D() { XPos = 120, YPos = 220, ZPos = 30 };
            P3 = (Point)point3;
            Console.WriteLine(P3.ToString());

            Console.WriteLine(" $$$$$$$$$$$$$$$$$$$$$%%%%%%%%%%%%%%");
            p1 = new Point(10, 20);
            if (p1)
            {
                Console.WriteLine(p1.ToString());
            }
            else
            {
                Console.WriteLine("Hello ");
            }
            #endregion
        }


    }
}
