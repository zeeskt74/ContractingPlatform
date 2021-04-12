using ICP.SQLite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICP.Tests
{
    public class ICPTestDbContext : ICPDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            optionbuilder.UseSqlite(@"Data Source=db\\icptestdb.db");
        }
    }
}
