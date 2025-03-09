using System;

public class Program
{
    static int Main()
    {
        App.Great();
        App.ShowHomeMenu();
        UserOption Option = App.GetUserOption();
        App.ProcessUserOption(Option);
        App.GetDate();

        //int hour;
        //Console.Write("Enter an hour (1-23): ");
        //hour = int.Parse(Console.ReadLine() ?? "0");
        //if (hour >= 0 || hour < 24)
        //{
        //    hour = 0;
        //}


        //do
        //{
        //    Console.Write("Enter an hour (1-23): ");
        //    hour = int.Parse(Console.ReadLine() ?? "0");
        //} while (hour < 1 || hour > 23);


        //for (int i = 0; i < 5; i++)
        //{
        //    Console.Write("Enter an hour (1-23): ");
        //    hour = int.Parse(Console.ReadLine() ?? "0");
        //    if (i == 4)
        //        Console.WriteLine("Too many invalid attempts.");
        //}


        //for (int i = 0; i < 5; i++)
        //{
        //    Console.Write("Enter an hour (1-23): ");
        //    hour = int.Parse(Console.ReadLine() ?? "0");

        //    if (hour >= 1 && hour <= 23)
        //        break;

        //    if (i == 4)
        //        Console.WriteLine("Too many invalid attempts.");
        //}

        //Console.WriteLine("Enter percent");
        //int percent = int.Parse(Console.ReadLine() ?? "");
        //SEQConnection sEQConnection = new SEQConnection();
        //try
        //{
        //    if (percent < 0 || percent > 100)
        //    {
        //        throw new ArgumentOutOfRangeException($"{percent}  Percent must be between 0 and 100.");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    sEQConnection.write(LogCategory.Error, $"In percent: {ex.Message}");
        //    Console.WriteLine(ex.ToString());
        //}


        //FileHandler fileHandler = new FileHandler();
        //Console.WriteLine("Enter Your Name");
        //string userName = Console.ReadLine();
        //fileHandler.WriteToFile("userlog.txt", userName);
        //string fileContent = fileHandler.ReadFromFile("userlog.txt");
        //Console.WriteLine("Display\n");
        //Console.WriteLine(fileContent);
        return 0;
    }
}
