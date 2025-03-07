namespace P1
{
    public class Point3D
    {
        public int XPos{get;set;}
        public int YPos{get;set;}
        public int ZPos{get;set;}
        public override string ToString()
        {
            //return base.ToString();
            return $"{XPos},{YPos},{ZPos}";
        }
    }
}