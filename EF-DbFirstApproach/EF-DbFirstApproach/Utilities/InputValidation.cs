using EF_DbFirstApproach.CRUD;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EF_DbFirstApproach.Utilities
{
    public class InputValidation
    {


        public static int GetStudentID()
        {
            Console.Write("Enter a Student ID : ");
            string inputID = Console.ReadLine();
            int studentID;
            while (!(int.TryParse(inputID, out studentID) && studentID > 0 && isAvailable(studentID)))
            {
                Console.Write("Enter a valid Studen ID : ");
                inputID = Console.ReadLine();
            }
            return studentID;
        }

        public static bool isAvailable(int inputID)
        {
            Crud crud = new Crud();
            var students = crud.GetAllStudentDetails();
            return students.Any(student => student.StudentID == inputID);
        }


        public static int GetDepartmentID()
        {
            Console.Write("Enter the Department ID : ");
            string inputID = Console.ReadLine();
            int departmentID;
            while(!(int.TryParse(inputID ,out departmentID) && departmentID > 0 && isAvailableDepartment(departmentID)))
            {
                Console.Write("Enter a valid department ID : ");
            }
            return departmentID;
        }
       
        public static bool isAvailableDepartment(int id)
        {
            Crud crud = new Crud();
            var departments = crud.GetAllDepartments();
            return departments.Any(department => department.DepartmentID == id);
        }
        public static string GetName()
        {
            Console.Write("Enter your Name : ");
            string firstName = Console.ReadLine();
            const string namePattern = "^[a-zA-Z .]{2,}$";
            while (!Regex.IsMatch(firstName, namePattern))
            {
                Console.Write("Enter a valid  Name : ");
                firstName = Console.ReadLine();
            }
            return firstName;

        }

       
        public static int GetValueForSwtich(int range)
        {
            Console.Write($"Choose the opeartion (1-{range}) : ");
            string inputValue = Console.ReadLine();
            int caseNumber;
            while (!(int.TryParse(inputValue, out caseNumber) && range >= caseNumber && caseNumber > 0))
            {
                Console.Write("Choose the valid opearation : ");
                inputValue = Console.ReadLine();
            }
            return caseNumber;

        }
    }

}
