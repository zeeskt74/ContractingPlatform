using ICP.SQLite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICP.SQLite
{
    public class ICPDbContext : DbContext
    {
        private static bool _created = false;
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Contract> Contracts { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedContractors(modelBuilder);
            SeedContracts(modelBuilder);

        }

        
        private static void SeedContractors(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contractor>()
                            .HasData(new Contractor()
                            {
                                Id = 1,
                                Name = "Test 1",
                                Address = "ABC",
                                Phone = "123456789",
                                Type = "Carrier",
                                HealthStatus = "Yellow"
                            });

            modelBuilder.Entity<Contractor>()
                .HasData(new Contractor()
                {
                    Id = 2,
                    Name = "Test 2",
                    Address = "XYZ",
                    Phone = "123456789",
                    Type = "Carrier",
                    HealthStatus = "Green"
                });

            modelBuilder.Entity<Contractor>()
                .HasData(new Contractor()
                {
                    Id = 3,
                    Name = "Test 3",
                    Address = "LYU",
                    Phone = "123456789",
                    Type = "Carrier",
                    HealthStatus = "Red"
                });
        }

        private static void SeedContracts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>()
                            .HasData(new Contract()
                            {
                                Id = 1,
                                MainContractorId = 1,
                                RelationContractorId = 2
                            });

            modelBuilder.Entity<Contract>()
                .HasData(new Contract()
                {
                    Id = 2,
                    MainContractorId = 2,
                    RelationContractorId = 3
                });
        }
    }
}
