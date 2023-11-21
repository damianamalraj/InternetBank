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
                    Console.WriteLine("1. Se dina konton och saldo\n2.Överföring mellan konton\n3.Ta ut pengar\n4.Sätt in pengar\n5.Öppna nytt konto\n6.Logga ut");
                    string? command = Console.ReadLine();

                    switch (command.ToLower())
                    {
                        case "1":
                            ShowAccountsBalance(context, user);
                            break;
                        case "2":
                            TransferFunds(context, user);
                            break;
                        case "3":
                            WithdrawMoney(context, user);
                            break;
                        case "4":
                            DepositMoney(context, user);
                            break; ;
                        case "5":
                            CreateAccount(context, user);
                            break;
                        case "6":
                            return;
                        default:
                            Console.WriteLine($"Unknown command: {command}");
                            break;
                    }
                }
            }
        }

        private static void ShowAccountsBalance(BankContext context, User user)
        {
            var accounts = context.Accounts
                            .Where(a => a.UserId == user.Id);

            foreach (var account in accounts)
            {
                Console.WriteLine($"{account.Id} {account.Name} -> [{account.Balance}]");
            }
        }

        private static void CreateAccount(BankContext context, User user)
        {
            Console.WriteLine("Create Account");
            Console.Write("Enter Account name: ");
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
                Console.WriteLine($"Created Account {accountName}");
            }
            else
            {
                Console.WriteLine($"Failed to create Account with accountName {accountName}");
            }

        }

        //Method for gathering info from user to create a transaction between accounts. /Stina Hedman
        private static void TransferFunds(BankContext context, User user)
        {

            //foreach (Account a in context.Accounts)
            //{
            //    if(a.)
            //    userAccs.Add(a);
            //}
            //var accounts = from a in context.Accounts
            //                select a;

            var accounts = context.Accounts
                            .Where(x => x.User.Id == user.Id);

            foreach (Account a in accounts)
            {
                
                Console.WriteLine($"{a.Id}");
            }

            ShowAccountsBalance(context, user);


            Console.WriteLine("Ange konto att föra över pengar från");
            string input = Console.ReadLine();
            bool validAccountID = false;
            int fromAccID;
            Account fromAccount = new Account();

            //checks if userinput can be parsed to int and if it's connected to an excisting account.
            while (!Int32.TryParse(input, out fromAccID) || !validAccountID)
            {

                Console.WriteLine("Ange ett giltigt konto id.");

                foreach (Account ac in accounts)
                {
                    if (ac.Id == fromAccID)
                    {
                        fromAccount = ac;
                        validAccountID = true;
                    }
                }

                if (validAccountID == false)
                {
                    Console.WriteLine("detta id tillhör inte ett av dina konton");
                }

                input = Console.ReadLine();
            }

            //ask for amount to be transfered and check input
            Console.WriteLine("Ange summa som ska överföras: ");
            input = Console.ReadLine();
            double amount;
            while (!double.TryParse(input, out amount) || fromAccount.Balance < amount)
            {
                Console.WriteLine("Något gick fel. Ange ett giltigt antal.");
                if (fromAccount.Balance < amount)
                {
                    Console.WriteLine("kontot har inte täckning för detta belopp.");
                }
                input = Console.ReadLine();
            }

            //ask for recieving account id and check that it's valid
            int toAccID;
            Console.WriteLine("Ange kontonummer för det konto du vill föra över pengar till:");
            input = Console.ReadLine();
            validAccountID = false;
            while (!Int32.TryParse(input, out toAccID) || !validAccountID)
            {

                var allAccounts = from a in context.Accounts
                                    select a;

                foreach (Account ac in allAccounts)
                {
                    if (ac.Id == toAccID)
                    {
                        validAccountID = true;
                    }
                }
                
            }

            //Asks user to input a message for the transaction and checks lenght of message
            Console.WriteLine("Ange meddelande (max 20 tecken)");
            string message = Console.ReadLine();

            while (message.Count() > 20)
            {
                Console.WriteLine("Ange ett kortare meddelande, max 20 tecken");
                message = Console.ReadLine();
            }
            Transaction transferBetweenAccounts = new Transaction(fromAccID, toAccID, amount, message);
        }

        public static void WithdrawMoney(BankContext context, User user)
        { //let user choose account to withdraw from
            List<Account> userAccs = new List<Account>();
            
            
                var accounts = from a in context.Accounts
                               select a;

                foreach (Account ac in accounts)
                {
                    if (ac.User.Id == user.Id)
                    {
                        userAccs.Add(ac);
                    }
                }
            

            Console.WriteLine("Ange konto du vill sätta ta ut pengar från:");
            for (int i = 0; i > userAccs.Count - 1; i++)
            {

            }

            //check if avaliable funds on account
        }

        public static void DepositMoney(BankContext context, User user)
        {

            var accounts = context.Accounts
                .Where(x => x.User.Id == user.Id);

            ShowAccountsBalance(context, user);

            Console.WriteLine("Ange konto id att föra över pengar till");
            string input = Console.ReadLine();
            bool validAccountID = false;
            int fromAccID;
            Account fromAccount = new Account();

            //checks if userinput can be parsed to int and if it's connected to an excisting account.
            while (!Int32.TryParse(input, out fromAccID) || !validAccountID)
            {

                Console.WriteLine("Ange ett giltigt konto id.");

                foreach (Account ac in accounts)
                {
                    if (ac.Id == fromAccID)
                    {
                        fromAccount = ac;
                        validAccountID = true;
                    }
                }

                if (validAccountID == false)
                {
                    Console.WriteLine("detta id tillhör inte ett av dina konton");
                }

                input = Console.ReadLine();
            }

            //ask for amount to be transfered and check input
            Console.WriteLine("Ange summa för isättning: ");
            input = Console.ReadLine();
            double amount;
            while (!double.TryParse(input, out amount))
            {
                Console.WriteLine("Något gick fel. Ange ett giltigt antal.");
                input = Console.ReadLine();
            }

            Transaction

        }

    }
}
