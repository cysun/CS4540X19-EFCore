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
                // Load an object by primary key using Find()
                var jane = db.Employees.Find(2);
                Console.WriteLine(jane);

                // Query objects using LINQ
                var query1 = from e in db.Employees
                             where e.LastName == "Smith"
                             select e;
                var tom = query1.FirstOrDefault();
                Console.WriteLine(tom);

                // Lazy load referenced objects
                Console.WriteLine(tom.Supervisor.Supervisor);

                // Update an object
                tom.FirstName = "Tim";
                db.SaveChanges();

                // Delete an object
                db.Employees.Remove(tom);
                db.SaveChanges();

                // Add a new object
                tom = new Employee
                {
                    FirstName = "Tom",
                    LastName = "Smith",
                    DateHired = new DateTime(2016, 6, 19),
                    Supervisor = jane
                };
                db.Employees.Add(tom);
                db.SaveChanges();
            }
        }
    }
}
