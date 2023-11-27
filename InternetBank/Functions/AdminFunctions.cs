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
            using (BankContext context = new BankContext())
            {

                char userInput;

                Console.WriteLine("\t\t\n\t\tHello !! You are an administrator and you have the right to create users: ");
                Console.WriteLine("\t\tHowever, we have following current users in system: ");
                
                List<User> users = DbHelper.GetAllUsers(context);

                
                foreach (User user in users)
                {
                    Console.WriteLine($"\t\t\n\t\t{user.Name}");
                }

                Console.WriteLine($"\t\t\n\t\tTotal number of users = {users.Count()}");
                adminMenu:
                Console.WriteLine("\t\t\n\t\tSelect the menu below:");
                Console.WriteLine("\t\t\n\t\t-----------------------------------");
                Console.WriteLine("\t\t\n\t\t1. Create user:");
                Console.WriteLine("\t\t2. Exit");
                Console.WriteLine("\t\t\n\t\t-----------------------------------");


                while (true)
                {
                    Console.Write("\t\t\n\t\tEnter your choice between 1 and 2: ");

                    string command = Console.ReadLine();

                    switch (command.ToLower())
                    {

                        case "1":
                            CreateUser(context);
                            break;
                        case "2":
                            Console.WriteLine("\t\t\n\t\tThis will end you session and will EXIT you from the the system");
                            Console.Write("\t\t\n\t\tDo you want to exit now? (y/n): ");
                            userInput = Console.ReadKey().KeyChar;

                            switch (Char.ToLower(userInput))
                            {
                                case 'y':
                                    Console.WriteLine("\t\t\n\t\tExiting program. Thank you!");
                                    Environment.Exit(0); // 0 indicates a successful exit
                                    break;
                                case 'n':
                                    goto adminMenu;
                                    //break;
                                default:
                                    Console.WriteLine("t\t\n\t\tInvalid input. Please enter 'y' or 'n'.");
                                    break;

                            }

                            return;
                        default:
                            Console.WriteLine($"\t\t\n\t\tUnknown command: {command}");

                            break;
                    }
                }
            }
        }

        private static void CreateUser(BankContext context)
        {

            char userInput;
            Console.WriteLine("\t\t\n\t\t1. Create user");

            createUser:
            Console.Write("\t\t\n\t\tEnter Username: ");

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

                Console.WriteLine($"\t\t\n\t\tCreated user {username} with pin {pin}");

               

                Account newAccount = new Account()
                {
                   UserId = newUser.Id,
                   Name = "Allkonto",
                   Balance = 0,

                };

                bool success2 = DbHelper.AddAccount(context, newAccount);

                if (success2)
                {

                   Console.WriteLine($"\t\t\n\t\tAlso created first Account Allkonto for the user.");
                }
                else
                {
                   Console.WriteLine($"\t\t\n\t\tFailed to create Allkonto");
                }            
                 
               


                Console.Write("\t\t\n\t\tDo you want to create more users? (y/n): ");
                userInput = Console.ReadKey().KeyChar;

                switch (Char.ToLower(userInput))
                {
                    case 'y':
                        goto createUser;
                        break;
                    case 'n':
                        DoAdminTasks();
                        break;
                    default:
                        Console.WriteLine("t\t\n\t\tInvalid input. Please enter 'y' or 'n'.");
                        break;


                }
            }
            else
            {

                Console.WriteLine($"\t\t\n\t\tFailed to create user with username {username}");
                return;

            }
        }

    }
}
