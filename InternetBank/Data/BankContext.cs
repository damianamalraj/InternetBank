using InternetBank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBank.Data
{
    internal class BankContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Fill in connection string here!!


            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\.;Initial Catalog=Bank;Integrated Security=True;Pooling=False");
            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\TestDatabase;Initial Catalog=Bankdb;Integrated Security=True;Pooling=False");
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=internetbank;User Id=sa;Password=cH@sdotnet23;TrustServerCertificate=true");


        }
    }
}
