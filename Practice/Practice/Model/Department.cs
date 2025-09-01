using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Model
{
    public class Department
    {
        public int DepartmentId { get; set; }   // PK by convention
        public string Name { get; set; }

        // Navigation property (1 dept → many employees)
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
