using System;

namespace Bank.Models
{
    public class BankTransaction
    {
        public DateTime TransactionDate { get; }
        public decimal Amount { get; }

        public BankTransaction(decimal amount)
        {
            TransactionDate = DateTime.Now;
            Amount = amount;
        }

        public void DisplayTransaction()
        {
            Console.WriteLine($"{TransactionDate}: {(Amount > 0 ? "Deposit" : "Withdrawal")} {Amount:C}");
        }

        public string GetTransaction()
        {
            return $"{TransactionDate}: {(Amount > 0 ? "Deposit" : "Withdrawal")} {Amount:C}";
        }
    }
}
