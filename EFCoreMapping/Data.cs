using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCoreMapping
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public Address Address { get; set; }

        [InverseProperty("Owner")]
        public List<Account> Accounts { get; set; }

        public List<Phone> Phones { get; set; }
    }

    [Owned]
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [Column(TypeName = "char(5)")]
        public string Zip { get; set; }
    }

    public class Phone
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string Number { get; set; }
    }

    public class Account
    {
        public int AccountId { get; set; }

        public decimal Balance { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public int OwnerId { get; set; }
        public Customer Owner { get; set; }

        public List<AccountCoOwners> CoOwners { get; set; }

        public bool Deleted { get; set; } = false;
    }

    public class AccountCoOwners
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int CoOwnerId { get; set; }
        public Customer CoOwner { get; set; }
    }

    public class CDAccount : Account
    {
        public int Term { get; set; }
    }

    class AppDbContext : DbContext
    {
        private static readonly string ConnectionString;

        static AppDbContext()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            ConnectionString = config.GetConnectionString("Default");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>().HasKey(p => new { p.CustomerId, p.Number });
            modelBuilder.Entity<Account>().Property(a => a.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Account>().Property(a => a.Deleted).HasDefaultValue(false);
            modelBuilder.Entity<Account>().HasQueryFilter(a => !a.Deleted);
            modelBuilder.Entity<AccountCoOwners>().HasKey(a => new { a.AccountId, a.CoOwnerId });
            modelBuilder.Entity<CDAccount>().HasBaseType<Account>();
        }
    }
}
