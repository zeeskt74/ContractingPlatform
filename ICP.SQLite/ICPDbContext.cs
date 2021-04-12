using ICP.SQLite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICP.SQLite
{
    class ICPDbContext : DbContext
    {
        public DbSet<Contractor> Contractor { get; set; }
        public ICPDbContext() : base()
        {
        }
    }
}
