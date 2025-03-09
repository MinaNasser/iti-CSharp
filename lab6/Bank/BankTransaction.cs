
using System;

namespace Bank
{
    public class BankTransaction
    {
        public DateTime TransactionDate { get; }
        public decimal Amount { get; }
        private bool disposed = false;
        FileHandler fileHandler = new FileHandler();
        public BankTransaction(decimal amount)
        {
            TransactionDate = DateTime.Now;
            Amount = amount;
        }

        public void DisplayTransaction()
        {
            Console.WriteLine($"{TransactionDate}: {(Amount > 0 ? "Deposit" : "Withdrawal")} {Amount:C}");
        }
        public string getTransaction()
        {
            return $"{TransactionDate}: {(Amount > 0 ? "Deposit" : "Withdrawal")} {Amount:C}";
        }
        public void Dispose()
        {
            if (!disposed)
            {
                try
                {
                    string fileName = "Transactions.txt";
                    string transactionDetails = $"{TransactionDate}: {(Amount > 0 ? "Deposit" : "Withdrawal")} {Amount:C}";

                    fileHandler.WriteToFile(fileName, transactionDetails);
                    disposed = true;

                    GC.SuppressFinalize(this);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in finalizer: {ex.Message}");
                }

            }
        }

        // Finalizer as a fallback (shouldn't be used if Dispose is called)
        ~BankTransaction()
        {
            Dispose();
        }

    }
}
