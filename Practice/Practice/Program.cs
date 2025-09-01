using Practice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EmployeeContext())
            {
                var dept = new Department { Name = "IT" };
                context.Departments.Add(dept);

                var emp = new Employee { Name = "Boobathi raja", Email = "boobathi@gislen.com", Department = dept };
                context.Employees.Add(emp);

                context.SaveChanges();

                Console.WriteLine("Employee added successfully!");
            }
        }
    }
}
    