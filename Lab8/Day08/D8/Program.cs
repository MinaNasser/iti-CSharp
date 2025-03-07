namespace D8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello ");
            Widget w = new Widget();
            w.First();
            w.Second();

            FancyWidget f = new FancyWidget();
            f.First(); f.Second();
        }
    }
}