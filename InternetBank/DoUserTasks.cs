
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

                            break;
                        case "2":
                            return;
                        case "3":
                            return;
                        case "4":
                            return;
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

    }
}
