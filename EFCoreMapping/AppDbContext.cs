using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCoreMapping
{
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
            modelBuilder.Entity<AccountCoOwner>().HasKey(a => new { a.AccountId, a.CoOwnerId });
            modelBuilder.Entity<CDAccount>().HasBaseType<Account>();
        }
    }
}
