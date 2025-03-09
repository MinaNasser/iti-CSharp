using System;
using System.Collections.Generic;
using Bank.Models;

namespace Bank.Controllers
{
    public class BankHandler
    {
        private List<BankAccount> accounts = new List<BankAccount>();
        private FileHandler fileHandler =new FileHandler();

        public void CreateAccount()
        {
            Console.Write("\nEnter initial balance: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal initialBalance) || initialBalance < 0)
            {
                Console.WriteLine("Invalid amount. Setting balance to 0.");
                initialBalance = 0;
                return;
            }

            Console.WriteLine("\nChoose Account Type:");
            Console.WriteLine("1. Savings Account");
            Console.WriteLine("2. Business Account");
            Console.WriteLine("3. Checking Account");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();
            AccountType selectedType = choice switch
            {
                "1" => AccountType.Savings,
                "2" => AccountType.Business,
                "3" => AccountType.Checking,
                _ => AccountType.Checking
            };

            Console.WriteLine(selectedType == AccountType.Checking ? "Invalid choice. Defaulting to Checking Account." : "");

            var account = new BankAccount(selectedType, initialBalance);
            accounts.Add(account);
            Console.WriteLine($"Account {account.Number} ({account.Type}) created with balance {account.Balance:C}");
        
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n===== Bank Menu =====");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Show All Accounts");
                Console.WriteLine("6. Show All Transaction");

                Console.WriteLine("7. Exit");
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
                        HandleTransaction(AccountOperation.Transfer);
                        break;

                    case "5":
                        Console.WriteLine("\n=== Account List ===");
                        foreach (var acc in accounts)
                        {
                            acc.DisplayAccountInfo();
                        }
                        Console.WriteLine("\nPress any key to return to menu...");
                        Console.ReadKey();
                        break;
                    case "6":
                        DisplayHistoryOrTransaction();


                        break;
                    case "7":
                        Console.WriteLine("Exiting... Press any key to close.");
                        Console.ReadKey();
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
            if (!int.TryParse(Console.ReadLine(), out int accNum))
            {
                Console.WriteLine("Invalid account number.");
                return;
            }

            BankAccount account = accounts.Find(a => a.Number == accNum);
            if (account == null)
            {
                Console.WriteLine("Account not found!");
                return;
            }

            if (accountOperation == AccountOperation.Deposit)
            {

                Console.WriteLine("Enter amount to  Deposit: ");

            }
            if (accountOperation == AccountOperation.Withdraw)
            {

                Console.WriteLine("Enter amount to  Withdrw: ");
            }
            if (accountOperation == AccountOperation.Transfer)
            {

                Console.WriteLine("Enter amount to  Transfer: ");
            }
            //Console.WriteLine($"Enter amount to {(accountOperation == AccountOperation.Deposit ? "deposit" : "withdraw")}:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            bool success=false;
            if (accountOperation == AccountOperation.Deposit)
            {
                success = account.Deposit(amount);
            }
            if (accountOperation == AccountOperation.Withdraw)
            {
                success = account.Withdraw(amount);
            }
            if (accountOperation == AccountOperation.Transfer)
            {
                Console.Write("Enter recipient account number: ");
                if (!int.TryParse(Console.ReadLine(), out int recipientAccNum))
                {
                    Console.WriteLine("Invalid recipient account number.");
                    return;
                }

                BankAccount recipientAccount = accounts.Find(a => a.Number == recipientAccNum);
                if (recipientAccount == null)
                {
                    Console.WriteLine("Recipient account not found!");
                    return;
                }

                success = account.TransferFrom(recipientAccount, amount);
            }
            if (!success) return;

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



        private void DisplayHistoryOrTransaction()
        {
            Console.Write("Enter account number: ");
            if (!int.TryParse(Console.ReadLine(), out int accNum))
            {
                Console.WriteLine("Invalid account number.");
                return;
            }

            BankAccount account = accounts.Find(a => a.Number == accNum);
            if (account == null)
            {
                Console.WriteLine("Account not found!");
                return;
            }

            Console.WriteLine("\n===== Account Transactions History =====");

            
            string filePath = $"Account_{accNum}.txt";
            if (File.Exists(filePath))
            {
                Console.WriteLine("\n***********\nWrite Mina\n****************\n");
                string transaction = fileHandler.ReadFromFile(filePath);
                Console.WriteLine(transaction);
            }
            else
            {
                Console.WriteLine("No transaction history found.");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }





    }
}



//if (accountOperation == AccountOperation.Deposit)
//{

//    Console.WriteLine("Enter amount to  Deposit: ");

//}
//if (accountOperation == AccountOperation.Withdraw)
//{

//    Console.WriteLine("Enter amount to  Withdrw: ");
//}
//if (accountOperation == AccountOperation.Transfer)
//{

//    Console.WriteLine("Enter amount to  Transfer: ");
//}