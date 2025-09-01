using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }     // PK by convention
        public string Name { get; set; }
        public string Email { get; set; }

        // Foreign key (by convention EF understands "DepartmentId")
        public int DepartmentId { get; set; }

        // Navigation property (Employee → Department)
        public virtual Department Department { get; set; }
    }
}
