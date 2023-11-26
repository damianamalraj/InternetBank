using System;
using System.Media;
using InternetBank.Data;

namespace InternetBank
{
	public class startingApplication
	{
		public static void startProgram()
		{
            Console.Clear();
            Console.WriteLine("\t\tHi, Welcome to the Bank!");
            Console.WriteLine("\t\tPlease enter your login credentials to log in to the system.");

            Console.Write("\t\t\n\t\tEnter Username: ");
            string userName = Console.ReadLine();

            Console.Write("\t\t\n\t\tYour pin code: ");
            string pin = Console.ReadLine();

            if (userName == "admin")
            {
                if (pin != "1234")
                {
                    Console.WriteLine("\t\tSorry, Wrong password!");
                    return;
                }

                AdminFunctions.DoAdminTasks();
                return;
            }
            using (BankContext context = new BankContext())
            {

                var user = context.Users
                            .Where(x => x.Name == userName)
                            .Where(x => x.Pin == pin);

                if (user != null)
                {

                    UserFunctions.DoUserTasks(userName);
                }

            }

            // Code here for user login *****

        }

        
    }
}

