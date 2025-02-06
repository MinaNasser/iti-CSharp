using System.Security.Cryptography;
using System.Security.Principal;

namespace Ass
{
    public class Program
    {
        enum AccountType
        {
            Checkin,
            Saving
        }
        struct BankAccount
        {
            public AccountType Type;
            public long AccountNum;
            public decimal Balance;
        }
        public static void Main(string[] args)
        {
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
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        static bool IsDayNumberValid(int dayNumber, int year)
        {
            if (IsLeapYear(year))
            {
                return dayNumber >= 1 && dayNumber <= 366;
            }
            return dayNumber >= 1 && dayNumber <= 365;
        }
        static bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }
        static string GetMonthName(int monthIndex)
        {
            string[] monthNames = { "January", "February", "March", "April", "May", "June",
                                "July", "August", "September", "October", "November", "December" };
            return monthNames[monthIndex];
        }
    }
}



//Console.WriteLine("Enter Your name ");
//string? name = Console.ReadLine();
//Console.WriteLine($"Hello {name}");
//AccountType checkin = AccountType.Checkin;
//AccountType saved = AccountType.Saving;
//Console.WriteLine($"Account 1 : {checkin}");
//Console.WriteLine($"Account 2 : {saved}");
//BankAccount bankAccount = new BankAccount();
//bankAccount.Type = checkin;
//bankAccount.AccountNum = 1022584;
//bankAccount.Balance = 5000;
//Console.WriteLine("\n---------------\n");
//Console.WriteLine($"Account Type : {bankAccount.Type}");
//Console.WriteLine($"Account Balance : {bankAccount.Balance}");
//Console.WriteLine($"Account Number  : {bankAccount.AccountNum}");

//BankAccount account;

//Console.Write("Enter Account Number: ");
//account.AccountNum = long.Parse(Console.ReadLine());

//Console.Write("Enter Account Balance: ");
//account.Balance = decimal.Parse(Console.ReadLine());

//Console.Write("Enter Account Type (0 for Checking, 1 for Savings): ");
//account.Type = (AccountType)int.Parse(Console.ReadLine());

//Console.WriteLine("\nAccount Summary:");
//Console.WriteLine("Account Number: " + account.AccountNum);
//Console.WriteLine("Balance: " + account.Balance);
//Console.WriteLine("Account Type: " + account.Type);
