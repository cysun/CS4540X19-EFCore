using System;

namespace EFCoreIntro
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateHired { get; set; }
        public virtual Employee Supervisor { get; set; }

        public override string ToString()
        {
            return $"[{Id}, {FirstName} {LastName}, {DateHired:d}]";
        }
    }

    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Employee Leader { get; set; }

        public override string ToString()
        {
            return $"[{Id}, {Name}]";
        }
    }
}
