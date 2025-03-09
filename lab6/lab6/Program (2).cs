using lab3;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            SEQConnection _seq = new SEQConnection();
            _seq.write(LogCategory.Error, "The File Not Fund in Main Function");
            Console.WriteLine("The File Not Fund");
        }
        FileHandler _handler = new FileHandler();

        _handler.SummarizeFile(args[0]);
        /*
         FileHandler _handler = new FileHandler();

        _handler.SummarizeFile("D:\\work\\1-material\\14 - C#\\C# Fundamentals\\D06\\lab6\\lab6");
         
         
         */
        //Utils.enterFilePath();
        ////////////////// 
        //Utils.Enter2DArr();


        //int[] evens = { 0, 2, 4, 6, 8 };
        //int crowd = evens[1];
        //int[] copy = new int[evens.Length];
        //copy = (int[])evens.Clone();
        //foreach (int i in copy)
        //{
        //    Console.WriteLine(i);
        //}

        //int[,] arr = Utils.Method();
        //Utils.Parameter(arr);
    }
}