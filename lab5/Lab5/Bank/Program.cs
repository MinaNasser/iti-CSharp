using lab3;

namespace Bank
{

    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("\n---------------------");
            Console.WriteLine(" BankAccount Handel \n");
            BankAccount b1 = new BankAccount(2000);
            BankAccount b2 = new BankAccount(1000);
            b2.TransferFrom(b1, 1000);
            Console.WriteLine("\n---------------------");
            Console.WriteLine(" Reversed Function \n");
            Utils.tryReverse();
            Console.WriteLine("\n---------------------");
            Console.WriteLine(" File Handel \n");
            Utils.FileSearch();




        }
    }
}
