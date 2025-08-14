using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_DbFirstApproach.Model;
using EF_DbFirstApproach.Utilities;

namespace EF_DbFirstApproach.CRUD
{
    public class Crud
    {
        public List<Student> GetAllStudentDetails()
        {
           StudentDataBaseEntities1 studentDatabase = new StudentDataBaseEntities1();
            var students = studentDatabase.Students.ToList();
            return students;
        }

        public List<Department> GetAllDepartments()
        {
            StudentDataBaseEntities1 studentDatabase = new StudentDataBaseEntities1();
            var departments = studentDatabase.Departments.ToList();
            return departments;
        }

        //Update a Student Information 
        public void UpdateStudent(int StudentID)
        {
            using (StudentDataBaseEntities1 studentDatabase = new StudentDataBaseEntities1())
            {
                var student = studentDatabase.Students.Find(StudentID);
                Console.WriteLine($"OLD NAME : {student.StudentName}");
                string newName = InputValidation.GetName();
                student.StudentName = newName;
                Console.WriteLine($"OLD Department Name :{student.Department.DepartmentName}");
                ShowAllDepartments(); //Show all the departments to choose from  
                int departmentID = InputValidation.GetDepartmentID();
                student.DepartmentID = departmentID;
                studentDatabase.SaveChanges();
            }
        }


        public void DeleteStudent(int StudentID)
        {
            using (StudentDataBaseEntities1 studentDatabase = new StudentDataBaseEntities1())
            {
                var student = studentDatabase.Students.Find(StudentID);
                studentDatabase.Students.Remove(student);
                Console.WriteLine(student.StudentName + "is deleted from the Database...");
                studentDatabase.SaveChanges();
            }
        }
        public void ShowALLStudents()
        {
            using (StudentDataBaseEntities1 studentDatabase = new StudentDataBaseEntities1())
            {
                var students = studentDatabase.Students.ToList();
                Console.WriteLine($"\n{"Student ID",-15}{"Student Name",-20}{"Department ID",-20}");
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.StudentID,-15} {student.StudentName,-20} {student.Department.DepartmentName,-20}");
                }
            }
             
        }

        public void ShowAllDepartments()
        {
            using (StudentDataBaseEntities1 studentDatabase = new StudentDataBaseEntities1())
            {

                var departments = studentDatabase.Departments.ToList();
                int i = 1;
                Console.WriteLine("All the available Departments : ");
                foreach (var department in departments)
                {
                    Console.WriteLine(i + ". " + department.DepartmentName);
                    i++;
                }
            }
        }
    }



}
