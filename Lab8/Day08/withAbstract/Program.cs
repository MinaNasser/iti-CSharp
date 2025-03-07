namespace D8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello with Abstract ");

            FancyWidget f = new FancyWidget();
            f.InVokeFirst();
            f.Second();

        }
    }
}