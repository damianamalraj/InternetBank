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
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=internetbank;User Id=sa;Password=Admin123;TrustServerCertificate=true");

        }
    }
}
