﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBank.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pin { get; set; }
        // public bool isAdmin { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
