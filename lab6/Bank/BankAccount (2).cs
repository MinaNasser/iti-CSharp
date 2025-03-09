using System;
using System.Collections.Generic;
using System.IO;

namespace Bank
{
    public class BankAccount
    {
        private static int count;
        private int number;
        private decimal balance;
        private bool disposed = false;
        private SEQConnection _seq = new SEQConnection();
        private FileHandler _fileHandler = new FileHandler();

        public decimal Balance { private set; get; }
        public int Number { private set; get; }
        public AccountType Type { get; private set; }
        private Queue<BankTransaction> transactions = new Queue<BankTransaction>();

        static BankAccount()
        {
            count = 0;
        }

        public BankAccount()
        {
            Number = ++count;
            Type = AccountType.Checking;
            Balance = 0;
            CreateAccountFile();
        }

        public BankAccount(AccountType type)
        {
            Number = ++count;
            Type = type;
            Balance = 0;
            CreateAccountFile();
        }

        public BankAccount(decimal initialBalance)
        {
            if (initialBalance < 0)
            {
                Console.WriteLine("Initial balance cannot be negative. Setting to zero.");
                _seq.write(LogCategory.Error, "Initial balance cannot be negative. Setting to zero.");
                initialBalance = 0;
            }
            Number = ++count;
            Type = AccountType.Checking;
            Balance = initialBalance;
            CreateAccountFile();
        }

        public BankAccount(AccountType type, decimal initialBalance)
        {
            if (initialBalance < 0)
            {
                Console.WriteLine("Initial balance cannot be negative. Setting to zero.");
                _seq.write(LogCategory.Error, "Initial balance cannot be negative. Setting to zero.");
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

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                _seq.write(LogCategory.Error, "Deposit amount must be positive.");
                Console.WriteLine("Deposit amount must be positive.");
                return;
            }
            Balance += amount;
            var transaction = new BankTransaction(amount);
            transactions.Enqueue(transaction);
            Console.WriteLine($"Deposited {amount:C} into Account {Number}. New Balance: {Balance:C}");
            _fileHandler.WriteToFile(GetAccountFileName(), $"Deposited: {amount:C}, New Balance: {Balance:C}");
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                _seq.write(LogCategory.Error, "Withdrawal amount must be positive.");
                return false;
            }
            if (amount > Balance)
            {
                _seq.write(LogCategory.Error, "Insufficient funds for withdrawal.");
                return false;
            }
            Balance -= amount;
            var transaction = new BankTransaction(-amount);
            transactions.Enqueue(transaction);
            Console.WriteLine($"Withdraw {amount:C} from Account {Number}. New Balance: {Balance:C}");
            _fileHandler.WriteToFile(GetAccountFileName(), $"Withdrawn: {amount:C}, New Balance: {Balance:C}");

            return true;
        }

        public void TransferFrom(BankAccount fromAccount, decimal amount)
        {
            if (fromAccount == null)
            {
                _seq.write(LogCategory.Error, "Source account cannot be null.");
                Console.WriteLine("Source account cannot be null.");
                return;
            }
            if (amount <= 0)
            {
                _seq.write(LogCategory.Error, "Transfer amount must be positive.");
                Console.WriteLine("Transfer amount must be positive.");
                return;
            }
            if (amount > fromAccount.Balance)
            {
                _seq.write(LogCategory.Error, "Insufficient funds in the source account.");
                Console.WriteLine("Insufficient funds in the source account.");
                return;
            }
            if (fromAccount.Withdraw(amount))
            {
                this.Deposit(amount);
                this.displayBalance();
                string transferRecord = $"Transferred {amount:C} from Account {fromAccount.Number} to Account {this.Number}";
                _fileHandler.WriteToFile(fromAccount.GetAccountFileName(), transferRecord);
                _fileHandler.WriteToFile(this.GetAccountFileName(), transferRecord);
            }
        }

        private void displayBalance()
        {
            Console.WriteLine($"Your Balance is {this.Balance:C} Now");
        }

        public void DisplayAccountInfo()
        {
            Console.WriteLine($"Your Account Number is {this.Number} and Balance {this.Balance:C}");
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
                    Console.WriteLine($"{transaction.TransactionDate}: {transaction.Amount:C}");
                }
            }
        }

        public void Dispose()
        {
            if (!disposed)
            {
                while (transactions.Count > 0)
                {
                    transactions.Dequeue().Dispose();
                }
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
