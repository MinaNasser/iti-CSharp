using Bank.Controllers;

namespace Bank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BankHandler bankHandler = new BankHandler();
            bankHandler.ShowMenu();
        }
    }
}