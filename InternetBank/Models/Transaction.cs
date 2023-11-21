using InternetBank.Data;
using InternetBank.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InternetBank.Models
{
    class Transaction
    {
        private static int idCounter = 1;
        public Transaction(int UserAccountID, int recievingAccount, double transferAmount, string transactionMessage)
        {

            int fromAccount = UserAccountID;
            int toAccount = recievingAccount;
            double amount = transferAmount;
            int transactionID = ++idCounter;
            string message = transactionMessage;
            bool successfull = false;

            void TransferMoney()
            {
                bool moneySent = false;
                bool moneyRecieved = false;
                using (BankContext context = new BankContext())
                {
                    var allAccounts = from a in context.Accounts
                                      select a;

                    foreach (Account ac in allAccounts)
                    {
                        if (ac.Id == UserAccountID)
                        {
                            ac.Balance -= amount;
                            moneySent = true;
                        }
                        else if (ac.Id == recievingAccount)
                        {
                            ac.Balance += amount;
                            moneyRecieved = true;
                        }
                    }

                    if (moneySent && moneyRecieved)
                    {
                        Console.WriteLine("Överföring utförd.");
                        //Lägg till för att spara transaktion i tabell
                        //context.Transactions.add(this.Transaction); 
                        context.SaveChanges();
                    }

                    else
                    {
                        Console.WriteLine("Överföring avbruten. Försök igen.");
                    }


                }
            }

            void Deposit()
            {

            }
        }
    }
}
