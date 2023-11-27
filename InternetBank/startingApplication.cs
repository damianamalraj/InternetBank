using System;
using InternetBank.Data;

namespace InternetBank
{
    public class startingApplication
    {
        private static int failedLoginAttempts = 0;
        private const int maxFailedAttempts = 3; // Maximum number of attempts
        private const int timeoutDuration = 180; // Timeout duration in seconds

        public static void startProgram()
        {
            Console.Clear();
            Console.WriteLine("\t\tHi, Welcome to the Bank!");
            Console.WriteLine("\t\tPlease enter your login credentials to log in to the system.");

            while (true)
            {
                Console.Write("\t\t\n\t\tEnter Username: ");
                string userName = Console.ReadLine();

                Console.Write("\t\t\n\t\tYour pin code: ");
                string pin = Console.ReadLine();

                if (userName == "admin")
                {
                    if (pin != "1234")
                    {
                        Console.WriteLine("\t\tSorry, Wrong password!");
                        failedLoginAttempts++;
                    }
                    else
                    {
                        failedLoginAttempts = 0;
                        AdminFunctions.DoAdminTasks();
                        break;
                    }
                }
                else
                {
                    using (BankContext context = new BankContext())
                    {
                        var user = context.Users.FirstOrDefault(x => x.Name == userName && x.Pin == pin);
                        if (user != null)
                        {
                            failedLoginAttempts = 0;
                            UserFunctions.DoUserTasks(userName, context);
                            break;
                        }
                        else
                        {
                            failedLoginAttempts++;
                        }
                    }
                }

                // Lockout logic for both admin and user
                if (failedLoginAttempts >= maxFailedAttempts)
                {
                    for (int remainingSeconds = timeoutDuration; remainingSeconds > 0; remainingSeconds--)
                    {
                        Console.Clear();
                        Console.WriteLine($"Too many incorrect attempts. Please wait for {remainingSeconds} seconds.");
                        System.Threading.Thread.Sleep(1000); // Wait for 1 second
                    }
                    Console.Clear();
                    Console.WriteLine("You may now attempt your login again. If further issues persist, consider contacting admin at admin@tigerconsole.se");
                    failedLoginAttempts = 0; // Reset the counter after timeout
                }
                else
                {
                    Console.WriteLine($"Incorrect credentials. {maxFailedAttempts - failedLoginAttempts} attempts remaining.");
                }
            }
        }
    }
}