using System;
using System.Collections.Generic;
using System.Linq;
using EF_DbFirstApproach.Model;

namespace EF_DbFirstApproach
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StudentDataBaseEntities1 DBEntities = new StudentDataBaseEntities1())
            {
                List<Department> listDepartments = DBEntities.Departments.ToList();
                Console.WriteLine();
                foreach (Department dept in listDepartments)
                {
                    Console.WriteLine("======================================================================================================");
                    Console.WriteLine("Department = {0}, Department Name = {1}", dept.DepartmentID, dept.DepartmentName);
                    Console.WriteLine();
                    foreach (Student stud in dept.Students)
                    {
                        Console.WriteLine("Student ID = {0},Name = {1},Department ID = {2}\n", stud.StudentID, stud.StudentName, stud.DepartmentID);
                    }
                    Console.WriteLine("======================================================================================================");

                }
                Console.ReadKey();
            }
        }
    }

}

