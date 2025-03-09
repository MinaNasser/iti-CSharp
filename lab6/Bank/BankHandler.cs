using System;
using System.Collections.Generic;

namespace Bank
{
    public class BankHandler
    {
        private List<BankAccount> accounts = new List<BankAccount>();

        public void CreateAccount()
        {
            Console.Write("\nEnter initial balance: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal initialBalance))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            Console.WriteLine("\nChoose Account Type:");
            Console.WriteLine("1. Savings Account");
            Console.WriteLine("2. Current Account");
            Console.WriteLine("3. Checking Account");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();
            AccountType selectedType;

            switch (choice)
            {
                case "1":
                    selectedType = AccountType.Savings;
                    break;
                case "2":
                    selectedType = AccountType.Business;
                    break;
                   case "3":
                       selectedType = AccountType.Checking;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Defaulting to Savings Account.");
                    selectedType = AccountType.Savings;
                    break;
            }

            var account = new BankAccount(selectedType,initialBalance);
            accounts.Add(account);
            Console.WriteLine($"Account {account.Number} ({account.Type}) created with balance {account.Balance:C}");
        }


        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n===== Bank Menu =====");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Show All Accounts");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                           CreateAccount();
                      
                    break;

                    case "2":
                        HandleTransaction(AccountOperation.Deposit);
                        break;

                    case "3":
                        HandleTransaction(AccountOperation.Withdraw);

                        break;

                    case "4":
                        Console.WriteLine("\n=== Account List ===");
                        foreach (var acc in accounts)
                        {
                            acc.DisplayAccountInfo();
                        }
                        break;

                    case "5":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }
            }
        }

        private void HandleTransaction(AccountOperation accountOperation)
        {
            Console.Write("Enter account number: ");
            if (int.TryParse(Console.ReadLine(), out int accNum))
            {
                BankAccount account = accounts.Find(a => a.Number == accNum);
                if (account == null)
                {
                    Console.WriteLine("Account not found!");
                    return;
                }

                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    if (accountOperation==AccountOperation.Deposit)
                    {
                        account.Deposit(amount);
                    }
                    else
                    {
                        if (!account.Withdraw(amount))
                        {
                            return; 
                        }
                    }
                    while (true)
                    {
                        Console.WriteLine("\nDo you want to:");
                        Console.WriteLine("1. Show Transactions");
                        Console.WriteLine("2. Return to Main Menu");
                        Console.Write("Choose an option: ");

                        string transactionChoice = Console.ReadLine();
                        if (transactionChoice == "1")
                        {
                            account.DisplayTransactions();
                        }
                        else if (transactionChoice == "2")
                        {
                            return; 
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice, try again.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid amount.");
                }
            }
            else
            {
                Console.WriteLine("Invalid account number.");
            }
        }
    }
}
