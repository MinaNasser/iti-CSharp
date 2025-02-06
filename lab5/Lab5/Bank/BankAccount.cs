using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class BankAccount
    {
        public decimal Balance;
        private SEQConnection _seq =new SEQConnection();

        public BankAccount(decimal initialBalance)
        {
            if (initialBalance < 0)
            {
                Console.WriteLine("Initial balance cannot be negative.");
                _seq.write(LogCategory.Error, "Initial balance cannot be negative.");
            }
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
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                _seq.write(LogCategory.Error, "Withdrawal amount must be positive.");
                Console.WriteLine("Withdrawal amount must be positive.");
            }
            if (amount > Balance)
            {
                _seq.write(LogCategory.Error, "Insufficient funds for withdrawal.");
                return;

            }
            Balance -= amount;
        }
        public void TransferFrom(BankAccount fromAccount, decimal amount)
        {
            if (fromAccount == null)
            {
                _seq.write(LogCategory.Error, "Source account cannot be null.");
                Console.WriteLine("ource account cannot be null.");
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
            fromAccount.Withdraw(amount);
            this.Deposit(amount);

            this.displayBalanced();
        }
        public  void displayBalanced()
        {
            Console.WriteLine($"Your Balance is {this.Balance:C} Now");
        }
    }


}
