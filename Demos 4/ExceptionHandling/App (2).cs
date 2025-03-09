


using Serilog;
using System.Collections.Concurrent;
using System.Diagnostics;

public struct App
{
    public static void ShowHomeMenu()
    {
        Console.ForegroundColor
            = ConsoleColor.Green;
        Console.WriteLine("Choose: ");
        Console.WriteLine();
        Console.WriteLine("1- Add");
        Console.WriteLine("2- Edit");
        Console.WriteLine("3- Delete");
        Console.WriteLine("4- Details");
        Console.WriteLine("5- List");
        Console.WriteLine();
        Console.ForegroundColor
            = ConsoleColor.White;
    }
    public static UserOption GetUserOption()
    {
        UserOption Option = UserOption.None;
        Console.Write("Choice : ");

        try
        {
            Option = (UserOption)Convert.ToInt32(Console.ReadLine());
            if(Option == UserOption.List) 
            {
                if (InMemoryStorage.Token.Split(new char[','])[1] != "1")
                    Option = UserOption.None;
            }
        }
        
        catch(IndexOutOfRangeException ex)
        {
            Console.WriteLine("You Don't have Permissions");
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Invalid Option");
        }
        catch(Exception ex)
        {
            Console.WriteLine("Something Went Wrong");
        }
        



        return Option;
    }
    public static void ProcessUserOption(UserOption option)
    {
        switch (option)
        {
            case UserOption.Add: break;
            case UserOption.Edit: break;
            case UserOption.Delete: break;
            case UserOption.Details: break;
            case UserOption.List: break;
            default:
                {
                }
                break;

        }
    }
}