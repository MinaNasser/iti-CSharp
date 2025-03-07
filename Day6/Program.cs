namespace Day6
{
    public class Program
    {
        public static void Main(string[] args)
        {

            #region Refrance by Ref and Equals
            //Point p1 = new Point(10, 20);
            //Point p2 = new Point(30, 40);
            //Point p3 = new Point(50, 60);
            //Point p4 = p1;


            //if (p1.Equals(p3))
            //{
            //    Console.WriteLine("P1 = P3 ");
            //}
            //else
            //{
            //    Console.WriteLine("P1 != P3 ");
            //}
            //if (p1.Equals(p4))
            //{
            //    Console.WriteLine("P1 = P4 ");
            //}
            //else
            //{
            //    Console.WriteLine("P1 != P4 ");
            //}
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
            #endregion


            #region object.ReferenceEquals(x, x)
            //int x = 6;
            //if (object.ReferenceEquals(x, x))//un Expicting Result
            //{
            //    Console.WriteLine("EQ");
            //}
            //else
            //{
            //    Console.WriteLine("NEQ");
            //} 
            #endregion

            Parent parent = new Parent(3, 4);

            Console.WriteLine(parent.Sum());

            Derived d = new Derived(3, 4,5);
            Console.WriteLine(d.Sum());

            Console.WriteLine("#############");
            Parent p1 = new Parent(3, 4);
            Derived d1 = new Derived(3, 4,5);
            Console.WriteLine("Base");
            p1.Show();
            Console.WriteLine("Child ");

            d1.Show();


            Console.WriteLine("Base -> Child ");
            Parent p2 = new Derived(3, 4,5);


            p2.Show();
  
        }
    }
}