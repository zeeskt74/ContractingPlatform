﻿using ICP.SQLite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICP.SQLite
{
    class ICPDbContext : DbContext
    {
        private static bool _created = false;
        public DbSet<Contractor> Contractor { get; set; }
        public ICPDbContext() : base()
        {
        }

        public ICPDbContext(DbContextOptions options) : base(options)
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
        }
    }
}