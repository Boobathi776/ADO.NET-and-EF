using EF_CodeFirstApproach.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirstApproach.Service
{
    internal class DepartmentService
    {
        DepartmentRepository departmentRepository;
        public DepartmentService() 
        {
            departmentRepository = new DepartmentRepository();  
        }


        public void ShowAllDepartments()
        {
            var departments = departmentRepository.GetDepartments();
            Console.WriteLine($"{"ID",-10}{"Department name",-30}");
            foreach (var department in departments)
            {
                Console.WriteLine($"{department.DepartmentID,-10}{department.DepartmentName,-30}");
            }
        }

        public bool IsAvailableDepartment(int id)
        {
            var departments = departmentRepository.GetDepartments();
            return departments.Any(d => d.DepartmentID == id);
        }
    }
}
