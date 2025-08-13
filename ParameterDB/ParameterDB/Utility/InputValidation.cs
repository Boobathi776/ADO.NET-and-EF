using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parameter.Utility
{
    public class InputValidation
    {

       
        public static int GetStudentID()
        {
            Console.Write("Enter a Student ID : ");
            string inputID = Console.ReadLine();
            int studentID;
            while (!(int.TryParse(inputID,out studentID) && studentID > 0 && isAvailable(studentID)))
            {
                Console.Write("Enter a valid Studen ID : ");
                inputID = Console.ReadLine();
            }
            return studentID;
        }

        public static bool isAvailable(int inputID)
        {
            CRUD crud = new CRUD();
            var students = crud.GetAllStudents();
            return students.Any(student => student.Id == inputID);
        }

        public static string GetFirstName()
        {
            Console.Write("Enter your First Name : ");
            string firstName = Console.ReadLine();
            const string namePattern = "^[a-zA-Z .]{2,}$";
            while(!Regex.IsMatch(firstName, namePattern))
            {
                Console.Write("Enter a valid First Name : ");
                firstName = Console.ReadLine();
            }
            return firstName;

        }

        public static string GetLastName()
        {
            Console.Write("Enter your Last Name : ");
            string lastName = Console.ReadLine();
            const string namePattern = "^[a-zA-Z .]{2,}$";
            while (!Regex.IsMatch(lastName, namePattern))
            {
                Console.Write("Enter a valid Last Name : ");
                lastName = Console.ReadLine();
            }
            return lastName;
        }

        public static int GetAge()
        {
            Console.Write("Enter your Age : ");
            string inputAge = Console.ReadLine();
            int age;
            while(!(int.TryParse(inputAge,out age) && age > 0 && age < 120))
            {
                Console.Write("Enter a valid Age : ");
                inputAge = Console.ReadLine();
            }
            return age;
        }


        public static string GetGender()
        {
            Console.Write("Enter your Gender 'M' for Male and 'F' for Female  : ");
            string inputGender = Console.ReadLine();    
            while(!(inputGender.ToLower() == "m" || inputGender.ToLower() =="f"))
            {
                Console.Write("Enter a valid gender m or f : ");
                inputGender = Console.ReadLine();
            }
            return inputGender.ToLower();
        }

        public static int GetValueForSwtich(int range)
        {
            Console.Write($"Choose the opeartion (1-{range}) : ");
            string inputValue = Console.ReadLine();
            int caseNumber;
            while(!(int.TryParse(inputValue,out caseNumber) && range>=caseNumber && caseNumber > 0 ))
            {
                Console.Write("Choose the valid opearation : ");
                inputValue = Console.ReadLine();
            }
            return caseNumber;

        }
    }

}
