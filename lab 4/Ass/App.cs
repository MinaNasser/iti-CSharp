


using Serilog;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Security.Cryptography;

public struct App
{

    public static void Great()
    {
        Console.WriteLine("Enter Your Name");
        string name = Console.ReadLine();
        Console.WriteLine($"Hello , {name}");
    }

    public static void ShowHomeMenu()
    {
        Console.ForegroundColor
            = ConsoleColor.Green;
        Console.WriteLine("Choose: ");
        Console.WriteLine();
        Console.WriteLine("1- Add");
        Console.WriteLine("2- Sub");
        Console.WriteLine("3- Mult");
        Console.WriteLine("4- Div");
        Console.WriteLine("5- Exit");
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

        }
        catch (FormatException ex)
        {

            SEQConnection sEQConnection = new SEQConnection();
            sEQConnection.write(LogCategory.Error, $"From GetUserOption Fun {ex.Message}");
            Console.WriteLine("Invalid Option");
        }
        catch (Exception ex)
        {
            SEQConnection sEQConnection = new SEQConnection();
            sEQConnection.write(LogCategory.Error, $"From GetUserOption Fun {ex.Message}");
            Console.WriteLine("Something Went Wrong");
        }
        return Option;
    }

    public static int[] GetNumber()
    {
        int[] result = new int[2];
        try
        {
            Console.Write("Enter the first integer: ");
            int num1 = int.Parse(Console.ReadLine());
            Console.Write("Enter the second integer: ");
            int num2 = int.Parse(Console.ReadLine());
            result[0] = num1;
            result[1] = num2;

        }
        catch (FormatException ex)
        {
            SEQConnection sEQConnection = new SEQConnection();
            sEQConnection.write(LogCategory.Error, ex.Message);

            Console.WriteLine("Error: Please enter valid integers.");
        }
        catch (Exception ex)
        {
            SEQConnection sEQConnection = new SEQConnection();
            sEQConnection.write(LogCategory.Error, ex.Message);
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
        return result;
    }


    public static void Add(ref int a, ref int b)
    {
        Console.WriteLine($"Sum = {a + b}");
    }
    public static void Sub(ref int a, ref int b)
    {
        Console.WriteLine($"Sub = {a - b}");
    }
    public static void Mul(ref int a, ref int b)
    {
        Console.WriteLine($"Sub = {a * b}");
    }
    public static void Div(ref int a, ref int b)
    {
        try
        {
            int result = a / b;

            Console.WriteLine($"Result: {result}");
        }
        catch (DivideByZeroException ex)

        {
            SEQConnection sEQConnection = new SEQConnection();
            sEQConnection.write(LogCategory.Error, $"In Div Func: {ex.Message}");
            Console.WriteLine("Error: Division by zero is not allowed.");
        }
        catch (Exception ex)
        {
            SEQConnection sEQConnection = new SEQConnection();
            sEQConnection.write(LogCategory.Error, $"In Div Func: {ex.Message}");
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }

    }

    public static void ProcessUserOption(UserOption option)
    {
        int[] number = new int[2];

        switch (option)
        {
            case UserOption.Add:
                number = GetNumber();
                Add(ref number[0], ref number[1]);
                break;
            case UserOption.Sub:
                number = GetNumber();
                Sub(ref number[0], ref number[1]);

                break;
            case UserOption.Div:
                number = GetNumber();
                Div(ref number[0], ref number[1]);

                break;
            case UserOption.Mul:
                number = GetNumber();
                Mul(ref number[0], ref number[1]);

                break;
            case UserOption.exit:
                return;
            default:
                {

                }
                break;

        }
    }

    public static void GetDate()
    {
        SEQConnection sEQConnection = new SEQConnection();
        try
        {

            Console.Write("Enter a day number (1-365 or 1-366): ");
            int dayNumber = int.Parse(Console.ReadLine() ?? "1");
            Console.Write("Enter the year: ");
            int year = int.Parse(Console.ReadLine() ?? "2024");

            if (!IsDayNumberValid(dayNumber, year))
            {
                throw new ArgumentException("Day out of range");
            }

            int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (IsLeapYear(year))
            {
                daysInMonth[1] = 29;
            }
            int monthIndex = 0;
            foreach (int days in daysInMonth)
            {
                if (dayNumber <= days)
                {
                    Console.WriteLine($"The date is: {GetMonthName(monthIndex)} {dayNumber}");
                    break;
                }
                dayNumber -= days;
                monthIndex++;
            }
        }
        catch (ArgumentException ex)
        {

            sEQConnection.write(LogCategory.Error, $"In GetDate Func: {ex.Message}");
            Console.WriteLine(ex.Message);
        }
        catch (FormatException ex)
        {

            sEQConnection.write(LogCategory.Error, $"In GetDate Func: {ex.Message}");
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
        catch (Exception ex)
        {
            
            sEQConnection.write(LogCategory.Error, $"In GetDate Func: {ex.Message}");
        }


    }
    private static bool IsDayNumberValid(int dayNumber, int year)
    {
        if (IsLeapYear(year))
        {
            return dayNumber >= 1 && dayNumber <= 366;
        }
        return dayNumber >= 1 && dayNumber <= 365;
    }
    private static bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }
    private static string GetMonthName(int monthIndex)
    {
        string[] monthNames = { "January", "February", "March", "April", "May", "June",
                                "July", "August", "September", "October", "November", "December" };
        return monthNames[monthIndex];
    }





}