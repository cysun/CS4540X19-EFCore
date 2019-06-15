using System;
using System.Linq;

namespace EFCoreIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                var employees = db.Employees.ToList();
                foreach (var employee in employees)
                    Console.WriteLine(employee);

                var query = from e in db.Employees where e.Id == 3 select e;
                var tom = query.Single();
                Console.WriteLine(tom);
                Console.WriteLine(tom.Supervisor);
                Console.WriteLine(tom.Supervisor.Supervisor);

                var jack = new Employee
                {
                    FirstName = "Jack",
                    LastName = "Jones",
                    Supervisor = tom
                };
                db.Employees.Add(jack);
                db.SaveChanges();
            }
        }
    }
}
