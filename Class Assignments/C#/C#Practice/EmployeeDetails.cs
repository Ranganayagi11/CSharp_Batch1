using System;

namespace C_Practice
{
    internal class EmployeeDetails
    {
        protected int id;
        protected string name;
        protected DateTime dob;
        protected double salary;

        public EmployeeDetails(int id, string name, DateTime dob, double salary)
        {
            this.id = id;
            this.name = name;
            this.dob = dob;
            this.salary = salary;
        }

        public virtual void Salary()
        {
            Console.WriteLine($"Employee ID: {id}, Name: {name}, Salary: {salary}");
        }
    }

    internal class Manager : EmployeeDetails
    {
        private double onsiteAllowance;
        private double bonus;

        public Manager(int id, string name, DateTime dob, double salary, double onsiteAllowance, double bonus)
            : base(id, name, dob, salary)
        {
            this.onsiteAllowance = onsiteAllowance;
            this.bonus = bonus;
        }

        public override void Salary()
        {
            double totalSalary = salary + onsiteAllowance + bonus;
            Console.WriteLine($"Manager ID: {id}, Name: {name}, Total Salary: {totalSalary}");
        }
    }


    /*class Employee
    {
        static void Main()
        {
            EmployeeDetails emp = new EmployeeDetails(1, "Ranganayagi", new DateTime(2003, 1, 11), 50000);
            emp.Salary();   
            Manager mgr = new Manager(2, "Rani", new DateTime(1999, 8, 15), 70000, 10000, 5000);
            mgr.Salary();  
        }
    }*/
}
