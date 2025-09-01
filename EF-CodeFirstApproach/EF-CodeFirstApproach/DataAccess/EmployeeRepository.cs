using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_CodeFirstApproach.Model;

namespace EF_CodeFirstApproach.DataAccess
{
    internal class EmployeeRepository
    {
        public EmployeeRepository() { }

        public List<Employee> GetEmployees()
        {
            try
            {
                List<Employee> employees = new List<Employee>();
                using(var context = new EmployeeContext())
                {
                    employees = context.Employees.ToList();
                }
                return employees;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error : while try to get the employee data from the Database...");
                Console.WriteLine("Stack Trace : \n"+ex.StackTrace);
                Console.WriteLine("Error message : " + ex.Message);
                throw;
            }
        }
    }
}
