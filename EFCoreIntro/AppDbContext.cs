using Microsoft.EntityFrameworkCore;

namespace EFCoreIntro
{
    class AppDbContext : DbContext
    {
        private const string ConnectionString =
            @"Server=ecst-csproj2.calstatela.edu,6301;
              Database=cs4540stu31;
              User ID=cs4540stu31;
              Password=Abcd1234";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(ConnectionString);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
