namespace D8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello with interface ");
            Widget w = new Widget();
            IWidget widget = w;
            widget.First();
            widget.Second();
        }
    }
}