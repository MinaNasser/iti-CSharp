using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class BankAccount
    {
        private static int count = 0;
        private int number;
        private decimal balance;
        public decimal Balance { private set; get; }
        public int Number { private set; get; }
        private SEQConnection _seq =new SEQConnection();

        public BankAccount(decimal initialBalance)
        {
            if (initialBalance < 0)
            {
                Console.WriteLine("Initial balance cannot be negative.");
                _seq.write(LogCategory.Error, "Initial balance cannot be negative.");
            }
            count++;
            Number = count;
            Balance = initialBalance;
        }
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                _seq.write(LogCategory.Error, "Deposit amount must be positive.");
                Console.WriteLine("Deposit amount must be positive.");
            }
            Balance += amount;
        }
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                _seq.write(LogCategory.Error, "Withdrawal amount must be positive.");
                Console.WriteLine("Withdrawal amount must be positive.");
                return false;

            }
            if (amount > Balance)
            {
                _seq.write(LogCategory.Error, "Insufficient funds for withdrawal.");
                return false;

            }
            Balance -= amount;
            return true;
        }
        public void TransferFrom(BankAccount fromAccount, decimal amount)
        {
            if (fromAccount == null)
            {
                _seq.write(LogCategory.Error, "Source account cannot be null.");
                Console.WriteLine("Source account cannot be null.");
            }
            if (amount <= 0)
            {
                _seq.write(LogCategory.Error, "Transfer amount must be positive.");
                Console.WriteLine("Transfer amount must be positive.");
            }
            if (amount > fromAccount.Balance)
            {
                _seq.write(LogCategory.Error, "Insufficient funds in the source account.");
                Console.WriteLine("Insufficient funds in the source account.");
                return ;
            }
            if(fromAccount.Withdraw(amount))
            {
                this.Deposit(amount);
                this.displayBalanced();
            }
        }
        private  void displayBalanced()
        {
            Console.WriteLine($"Your Balance is {this.Balance:C} Now");
        }
        public void DisplayAccountInfo()
        {
            Console.WriteLine($"Your Account Number is {this.Number} and Balance  {this.Balance:C}");
        }
    }


}
