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
    internal static class AdminFunctions
    {
        public static void DoAdminTasks()
        {
            int loginAttempts = 0;
            const int maxLoginAttempts = 3;
            const int timeoutSeconds = 20;

            do
            {
                Console.Write("Enter admin password: ");
                string password = Console.ReadLine();

                if (password == "1234")
                {
                    Console.WriteLine("Admin login successful!");
                    using (BankContext context = new BankContext())
                    {
                        // Admin tasks code...
                        Console.WriteLine("Current users in system: ");
                        List<User> users = DbHelper.GetAllUsers(context);

                        foreach (User user in users)
                        {
                            Console.WriteLine($"{user.Name}");
                        }

                        Console.WriteLine($"Total number of users = {users.Count()}");
                        Console.WriteLine("Choose your option from the below: ");
                        Console.WriteLine("(Please press 'c' to Create and 'x' to Exit Internet Bank.)");
                        Console.WriteLine("\n\t\t1. Create new user");
                        Console.WriteLine("\t\t2. Exit");

                        while (true)
                        {
                            Console.Write("Enter command: ");
                            string command = Console.ReadLine();

                            switch (command.ToLower())
                            {
                                case "c":
                                    CreateUser(context);
                                    break;
                                case "x":
                                    Console.WriteLine("Exiting...");
                                    break;
                                default:
                                    Console.WriteLine($"Unknown command: {command}");
                                    break;
                            }
                        }
                        //return;
                    }
                }

                else
                {
                    loginAttempts++;
                    Console.WriteLine($"Incorrect password. Attempts left: {maxLoginAttempts - loginAttempts}");

                    if (loginAttempts >= maxLoginAttempts)
                    {
                        Console.WriteLine($"Too many incorrect attempts. Please try again after {timeoutSeconds} seconds.");
                        System.Threading.Thread.Sleep(timeoutSeconds * 1000);
                        loginAttempts = 0; // Reset login attempts after timeout
                    }
                }
            } while (loginAttempts < maxLoginAttempts);

        }

        private static void CreateUser(BankContext context)
        {
            Console.WriteLine("Create user");
            Console.Write("Enter user name: ");
            string username = Console.ReadLine();

            Random random = new Random();
            string pin = random.Next(1000, 10000).ToString();

            User newUser = new User()
            {
                Name = username,
                Pin = pin,
            };
            bool success = DbHelper.AddUser(context, newUser);

            if (success)
            {
                Console.WriteLine($"Created user {username} with pin {pin}");

                Account newAccount = new Account()
                {
                    UserId = newUser.Id,
                    Name = "Allkonto",
                    Balance = 0,
                };

                bool success2 = DbHelper.AddAccount(context, newAccount);

                if (success2)
                {
                    Console.WriteLine($"Created Account Allkonto");
                }
                else
                {
                    Console.WriteLine($"Failed to create Allkonto");
                }
            }
            else
            {
                Console.WriteLine($"Failed to create user with username {username}");
            }
        }

    }
}
