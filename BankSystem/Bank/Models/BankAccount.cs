using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace Bank.Models
{
    /*public class SEQConnection : IDisposable
    {
        private static readonly Logger _logger;

        static SEQConnection()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Seq("http://localhost:5341/")
                .CreateLogger();
        }

        public void Write(LogCategory cat, string message)
        {
            switch (cat)
            {
                case LogCategory.Error:
                    _logger.Error(message);
                    break;
                case LogCategory.Warning:
                    _logger.Warning(message);
                    break;
                case LogCategory.Information:
                    _logger.Information(message);
                    break;
            }
        }

        public void Dispose()
        {
            _logger.Dispose();
        }
    }
    */
    public class BankAccount : IDisposable
    {
        private static int count;
        private bool disposed = false;
        private static readonly SEQConnection SEQConnection = new SEQConnection();
        private FileHandler _fileHandler = new FileHandler();
        private Queue<BankTransaction> transactions = new Queue<BankTransaction>();

        public decimal Balance { private set; get; }
        public int Number { private set; get; }
        public AccountType Type { get; private set; }

        static BankAccount()
        {
            count = 0;
        }

        public BankAccount(AccountType type = AccountType.Checking, decimal initialBalance = 0)
        {
            if (initialBalance < 0)
            {
                Console.WriteLine("Initial balance cannot be negative. Setting to zero.");
                SEQConnection.Write(LogCategory.Error, "Initial balance cannot be negative. Setting to zero.");
                initialBalance = 0;
            }

            Number = ++count;
            Type = type;
            Balance = initialBalance;
            CreateAccountFile();
        }

        private string GetAccountFileName()
        {
            return $"Account_{Number}.txt";
        }

        private void CreateAccountFile()
        {
            string fileName = GetAccountFileName();
            string content = $"Account Number: {Number}\nAccount Type: {Type}\nInitial Balance: {Balance:C}\n";
            _fileHandler.WriteToFile(fileName, content);
        }

        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be positive.");
                SEQConnection.Write(LogCategory.Error, "Deposit amount must be positive.");
                return false;
            }
            Balance += amount;
            var transaction = new BankTransaction(amount);
            transactions.Enqueue(transaction);
            SaveTransaction(transaction);
            Console.WriteLine($"Deposited {amount:C} into Account {Number}. New Balance: {Balance:C}");
            _fileHandler.WriteToFile(GetAccountFileName(), $"Deposited: {amount:C}, New Balance: {Balance:C}");
            return true;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal amount must be positive.");
                SEQConnection.Write(LogCategory.Error, "Withdrawal amount must be positive.");
                return false;
            }
            if (amount > Balance)
            {
                Console.WriteLine("Insufficient funds for withdrawal.");
                SEQConnection.Write(LogCategory.Error, "Insufficient funds for withdrawal.");
                return false;
            }
            Balance -= amount;
            var transaction = new BankTransaction(-amount);
            transactions.Enqueue(transaction);
            SaveTransaction(transaction);
            Console.WriteLine($"Withdraw {amount:C} from Account {Number}. New Balance: {Balance:C}");
            _fileHandler.WriteToFile(GetAccountFileName(), $"Withdrawn: {amount:C}, New Balance: {Balance:C}");
            return true;
        }

        public bool TransferFrom(BankAccount fromAccount, decimal amount)
        {
            if (fromAccount == null)
            {
                Console.WriteLine("Source account cannot be null.");
                SEQConnection.Write(LogCategory.Error, "Source account cannot be null.");
                return false;
            }
            if (amount <= 0)
            {
                Console.WriteLine("Transfer amount must be positive.");
                SEQConnection.Write(LogCategory.Error, "Transfer amount must be positive.");
                return false;
            }
            if (amount > fromAccount.Balance)
            {
                Console.WriteLine("Insufficient funds in the source account.");
                SEQConnection.Write(LogCategory.Error, "Insufficient funds in the source account.");
                return false;
            }
            if (fromAccount.Withdraw(amount))
            {
                Deposit(amount);
                DisplayBalance();
                string transferRecord = $"Transferred {amount:C} from Account {fromAccount.Number} to Account {Number}";
                _fileHandler.WriteToFile(fromAccount.GetAccountFileName(), transferRecord);
                _fileHandler.WriteToFile(GetAccountFileName(), transferRecord);
                return true;
            }
            return false;
        }

        private void DisplayBalance()
        {
            Console.WriteLine($"Your Balance is {Balance:C} Now");
        }

        public void DisplayAccountInfo()
        {
            Console.WriteLine($"Your Account Number is {Number} and Balance {Balance:C}");
        }

        public void DisplayTransactions()
        {
            if (transactions.Count == 0)
            {
                Console.WriteLine($"\nAccount {Number} does not contain any previous transactions.");
            }
            else
            {
                Console.WriteLine($"\nTransaction History for Account {Number}:");
                Console.WriteLine("--------------------------------------");

                foreach (BankTransaction transaction in transactions)
                {
                    Console.WriteLine(transaction.GetTransaction());
                }
            }
        }

        private void SaveTransaction(BankTransaction transaction)
        {
            _fileHandler.WriteToFile("Transactions.txt", transaction.GetTransaction());
        }

        public void Dispose()
        {
            if (!disposed)
            {
                transactions.Clear();
                disposed = true;
                GC.SuppressFinalize(this);
            }
        }


        
        ~BankAccount()
        {
            Dispose();
        }
    }
}
