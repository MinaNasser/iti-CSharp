


using System.Drawing;

namespace P1
{
    public class Point : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        #region ctor
        public Point()
        {
            X = 0;
            Y = 0;

        }
        public Point(int _x,int _y)
        {
            X = _x;
            Y = _y;

        }
        //Copy Constractyor ctor
        public Point(Point p)
        {
            X = p.X;
            Y = p.Y;
        }
        ~Point()
        {
            Console.WriteLine("Distractor Run");
            GC.Collect();


        }
        #endregion

        #region Operator Overloding
        public static Point operator +(Point L, Point R)
        {
            return new Point()
            {
                X = (L?.X ?? 0) + (R?.X ?? 0),
                Y = (L?.Y ?? 0) + (R?.Y ?? 0)
            };
        }
        public static Point operator *(Point L, Point R)
        {
            return new Point()
            {
                X = (L?.X ?? 0) * (R?.X ?? 0),
                Y = (L?.Y ?? 0) * (R?.Y ?? 0)
            };
        }

        public static Point operator ++(Point p)
        {
            return new Point(p.X + 1, p.Y + 1);
        }
        public static bool operator ==(Point p1, Point p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }
        public static bool operator !=(Point p1, Point p2)
        {
            return p1.X != p2.X && p1.Y != p2.Y;
        }
        public static bool operator >(Point p1, Point p2)
        {
            return p1.X > p2.X && p1.Y > p2.Y;
        }
        public static bool operator <(Point p1, Point p2)
        {
            //return p1.X < p2.X && p1.Y < p2.Y;
            throw new NotImplementedException();
        }


        public static explicit operator string(Point p)
        {
            return p.ToString();
        }
        public static implicit operator long (Point p)
        {
            //return Math.Sqrt(Math.Pow(p.X, (double)p.X) + Math.Pow(p.Y, p.Y));
            return Convert.ToInt64(Math.Sqrt((p.X * p.X) + (p.Y * p.Y)));
        }

        public static  explicit operator Point (Point3D d)
        {
             return new Point (d.XPos, d.YPos);

        }





        public static bool operator true (Point p)
        {
            return p.X != 0 || p.Y!= 0;
        }

        public static bool operator false(Point p)
        {
            return p.X == 0 && p.Y == 0;
        }


        #endregion



        public override string ToString()
        {
            return $"""
                    Class Name {base.ToString()}
                    X = {X}
                    Y = {Y}
                """;
        }
        public object Clone()
        {
            return new Point() {

                X = this.X,
                Y = this.X
            };
        }
    }
}
