using InternetBank.Data;
using InternetBank.Models;
using InternetBank.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBank
{
    internal static class UserFunctions
    {
        public static void DoUserTasks(string userName)
        {
            using (BankContext context = new BankContext())
            {
                var user = context.Users
                            .Where(x => x.Name == userName)
                            .FirstOrDefault();

                while (user != null)
                {
                mainmenu1:
                    Console.WriteLine("\t\t\n\t\tWelcome to the Bank.");
                    Console.WriteLine("\t\t\n\t\tChoose your options from the menu below:");
                    Console.WriteLine("\t\t\n\t\t-----------------------------------");
                    Console.WriteLine("\t\t1. View Accounts and Balances");
                    Console.WriteLine("\t\t2. Deposit Amount");
                    Console.WriteLine("\t\t3. Withdraw Amount");
                    Console.WriteLine("\t\t4. Transfer Funds");
                    Console.WriteLine("\t\t5. Open New Account");
                    Console.WriteLine("\t\t6. Sign out");
                    Console.WriteLine("\t\t-----------------------------------");

                    Console.Write("\t\tChoose your option from 1 to 6: ");
                    string? command = Console.ReadLine();
                    

                    switch (command.ToLower())
                    {
                        case "1":
                            Console.Clear();
                            
                            Console.WriteLine("\t\t1. View Accounts and Balances: ");
                            ShowAccountsBalance(context, user);
                            Console.Write("\t\tPress ENTER to go to main menu...");
                            while(Console.ReadKey().Key == ConsoleKey.Enter)
                            {
                                goto mainmenu1;
                            }
                            
                            break;

                        case "2":
                            Console.Clear();
                            Console.WriteLine("\t\t2. Deposit Amount: ");
                            DepositMoney(context, user);

                            Console.Write("\t\tPress ENTER to go to main menu...");
                            while (Console.ReadKey().Key == ConsoleKey.Enter)
                            {
                                goto mainmenu1;
                            }
                            break;
                            

                        case "3":
                            Console.Clear();
                            WithdrawMoney(context, user);
                            break;
                        case "4":
                            Console.Clear();
                            TransferFunds(context, user);
                            break;
                        case "5":
                            Console.Clear();
                            CreateAccount(context, user);
                            break;
                        case "6":
                            Console.Clear();
                            startingApplication.startProgram();
                            //goto mainmenu1;
                            return;
                        default:
                            Console.WriteLine($"Unknown command: {command}");
                            break;
                    }
                }
            }
        }
        
        //Method for showing users accountbalance
        private static void ShowAccountsBalance(BankContext context, User user)
        {
            var accounts = context.Accounts
                            .Where(a => a.UserId == user.Id);

            foreach (var account in accounts)
            {
                Console.WriteLine("\t\tHere are some details about your account(s).");
                Console.WriteLine($"\t\tAccountnumber : {account.Id} | AccountName :  {account.Name} | Balance : {account.Balance}");
            }
        }

        private static void CreateAccount(BankContext context, User user)
        {
            Console.WriteLine("\t\t5. Open New Account");
            Console.Write("\t\tEnter Account name: ");
            string accountName = Console.ReadLine();

            Account newAccount = new Account()
            {
                UserId = user.Id,
                Name = accountName,
                Balance = 0,
            };

            bool success = DbHelper.AddAccount(context, newAccount);

            if (success)
            {
                Console.WriteLine($"\t\tThank you. Your new account {accountName} has been created.");
            }
            else
            {
                Console.WriteLine($"\t\tSorry, we are unable to open new account '{accountName}' this time.");
            }

        }

        //Method for gathering info from user to create a transaction between accounts.
        private static void TransferFunds(BankContext context, User user)
        {
            var accounts = context.Accounts
                            .Where(x => x.User.Id == user.Id);

            ShowAccountsBalance(context, user);

            Console.WriteLine("Enter account number that you wish to transfer money from: ");
            string input = Console.ReadLine();
            bool validAccountID = false;
            int fromAccID = -1;
            Account fromAccount = new Account();

            //checks if userinput can be parsed to int and if it's connected to an excisting account.
            while (!Int32.TryParse(input, out fromAccID) || !validAccountID)
            {
                if(fromAccID == -1)
                {
                    Console.WriteLine("Enter a valid account number.");
                }

                else
                {
                    foreach (Account ac in accounts)
                    {
                        if (ac.Id == fromAccID)
                        {
                            fromAccount = ac;
                            validAccountID = true;
                        }
                    }

                    if (!validAccountID)
                    {
                        Console.WriteLine("You are not the owner of this account.");
                    }
                }

                if (fromAccID == -1 || !validAccountID)
                {
                    input = Console.ReadLine();
                }
            }

            //ask usert to input amount to be transfered and check input
            Console.WriteLine("Amount to transfer: ");
            input = Console.ReadLine();
            double amount;

            while (!double.TryParse(input, out amount) || fromAccount.Balance < amount)
            {
                Console.WriteLine("Something went wrong. Enter a valid amount.");
                if (fromAccount.Balance < amount)
                {
                    Console.WriteLine("Insufficient funds. you're too broke for this transaction");
                }
                input = Console.ReadLine();
            }

            //Ask user to input recieving account id and check that it's valid
            int toAccID = -1;
            Account toAccount = new Account();
            Console.WriteLine("Enter account number of the recieving account:");
            input = Console.ReadLine();
            validAccountID = false;
            while (!Int32.TryParse(input, out toAccID) || !validAccountID)
            {
                if (toAccID == -1)
                {
                    Console.WriteLine("Enter a valid account number:");
                }

                var allAccounts = from a in context.Accounts
                                    select a;

                foreach (Account ac in allAccounts)
                {
                    if (ac.Id == toAccID)
                    {
                        toAccount = ac;
                        validAccountID = true;
                    }
                }

                if (!validAccountID)
                {
                    Console.WriteLine("Account does not exist");
                }

                if (fromAccID == -1 || !validAccountID)
                {
                    input = Console.ReadLine();
                }

            }

            //Asks user to input a message for the transaction and checks lenght of message
            Console.WriteLine("Enter a message (20 characters at most)");
            string message = Console.ReadLine();

            while (message.Count() > 20)
            {
                Console.WriteLine("Message too long, maximum of 20 characters allowed");
                message = Console.ReadLine();
            }

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;
            context.SaveChanges();
            //Transaction transferBetweenAccounts = new Transaction(fromAccID, toAccID, amount, message);
        }

        //Method for withdrawing money /Stina Hedman; 
        public static void WithdrawMoney(BankContext context, User user)
        {

            var accounts = context.Accounts
                .Where(x => x.User.Id == user.Id);

            ShowAccountsBalance(context, user);

            Console.WriteLine("Enter the account number that you wish to withdraw from: ");
            string input = Console.ReadLine();
            bool validAccountID = false;
            int fromAccID;
            Account fromAccount = new Account();

            //checks if userinput can be parsed to int and if it's connected to an excisting account.
            while (!Int32.TryParse(input, out fromAccID) || !validAccountID)
            {
                if (fromAccID == -1)
                {
                    Console.WriteLine("Enter a valid account number:");
                }

                else
                {
                    foreach (Account ac in accounts)
                    {
                        if (ac.Id == fromAccID)
                        {
                            fromAccount = ac;
                            validAccountID = true;
                        }
                    }

                    if (!validAccountID)
                    {
                        Console.WriteLine("You are not the owner of this account.");
                    }
                }

                if (fromAccID == -1 || !validAccountID)
                {
                    input = Console.ReadLine();
                }
            }


            // Ask for amount to be withdrawn and check input
            Console.WriteLine("Enter amount to withdraw: ");
            input = Console.ReadLine();
            double amount;
            while (!double.TryParse(input, out amount) || amount > fromAccount.Balance)
            {
                if (amount > fromAccount.Balance)
                {
                    Console.WriteLine("Insufficient funds.");
                }
                Console.WriteLine("Please enter amount again.");
                input = Console.ReadLine();
            }

            fromAccount.Balance -= amount;
            context.SaveChanges();

        }

        //Method for depositing money to account /Stina Hedman; updated by Ashfaqul
        public static void DepositMoney(BankContext context, User user)
        {
            char userInput;

            var accounts = context.Accounts
                .Where(x => x.User.Id == user.Id);

            ShowAccountsBalance(context, user);

            Console.Write("\t\tEnter the account number of the account you want to deposit to: ");
            string input = Console.ReadLine();
            bool validAccountID = false;
            int fromAccID;
            Account fromAccount = new Account();

            //checks if userinput can be parsed to int and if it's connected to an excisting account.
            while (!Int32.TryParse(input, out fromAccID) || !validAccountID)
            {
                if (fromAccID == -1)
                {
                    Console.Write("\t\tPlease, enter a valid account number and try again.");
                }

                //check if the user is the owner of the account
                else
                {
                    foreach (Account ac in accounts)
                    {
                        if (ac.Id == fromAccID)
                        {
                            fromAccount = ac;
                            validAccountID = true;
                        }
                    }

                    if (!validAccountID)
                    {
                        Console.Write("\t\tSorry. Unfortunately, you are not the owner of this account. ");
                        Console.Write("\t\tPlease, try again...");
                    }
                }

                if (fromAccID == -1 || !validAccountID)
                {
                    input = Console.ReadLine();
                }
            }

            
            //Ask for amount to be transfered and check input
            Console.Write("\t\tEnter amount to deposit: ");
            input = Console.ReadLine();
            double amount;
            while (!double.TryParse(input, out amount))
            {
                Console.WriteLine("\t\tSorry, Invalid amount. Please, try again...");
                Console.WriteLine("\t\tPlease, Enter a valid amount: ");
                input = Console.ReadLine();
            }
            fromAccount.Balance += amount;
            Console.WriteLine($"\t\t{amount} is deposited in your account.");
            //Console.WriteLine($"\t\tDo you want to deposit another account? (y/n)");

            do
            {
                ShowAccountsBalance(context, user);
                Console.Write("\t\tDo you want to deposit again? (y/n) --> ");
                userInput = Console.ReadKey().KeyChar;

                switch (Char.ToLower(userInput))
                {
                    case 'y':
                        Console.WriteLine("\t\t\nProcessing another deposit...");
                        while (!Int32.TryParse(input, out fromAccID) || !validAccountID)
                        {
                            if (fromAccID == -1)
                            {
                                Console.Write("\t\t\nEnter a valid account number: ");
                            }

                            //check if the user is the owner of the account
                            else
                            {
                                foreach (Account ac in accounts)
                                {
                                    if (ac.Id == fromAccID)
                                    {
                                        fromAccount = ac;
                                        validAccountID = true;
                                    }
                                }

                                if (!validAccountID)
                                {
                                    Console.WriteLine("\t\tYou are not the owner of this account. ");
                                }
                            }

                            if (fromAccID == -1 || !validAccountID)
                            {
                                input = Console.ReadLine();
                            }
                        }

                        Console.WriteLine("\t\tEnter amount to deposit: ");
                        input = Console.ReadLine();
                        //double amount;
                        while (!double.TryParse(input, out amount))
                        {
                            Console.WriteLine("\t\tSomething went wrong. Enter a valid amount.");
                            input = Console.ReadLine();
                        }
                        fromAccount.Balance += amount;
                        Console.WriteLine($"\t\t{amount} is deposited in your account.");

                        break;
                    case 'n':
                        //Console.WriteLine("\nExiting program. Thank you!");
                        
                        break;
                    default:
                        Console.WriteLine("\nInvalid input. Please enter 'y' or 'n'.");
                        break;
                }

            } while (Char.ToLower(userInput) != 'n');

            context.SaveChanges();
        }

    }
}
