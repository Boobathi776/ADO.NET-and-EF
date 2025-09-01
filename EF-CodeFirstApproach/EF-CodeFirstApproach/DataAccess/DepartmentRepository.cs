using EF_CodeFirstApproach.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirstApproach.DataAccess
{
    internal class DepartmentRepository
    {
        public List<Department> GetDepartments()
        {
            using (var context = new EmployeeContext())
            {
                var departments = context.Departments.ToList();
                return departments;
            }
        }


    }
}
