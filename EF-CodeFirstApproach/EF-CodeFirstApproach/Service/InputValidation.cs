using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EF_CodeFirstApproach.Constants;

namespace EF_CodeFirstApproach.Service
{
    internal class InputValidation
    {

        /*=============================
                SWITCH CASE VALUE    
         *============================         
         */
        public static int GetMenuOption()
        {
            try
            {
                Console.Write($"Enter a your Option (1-{Constant.NO_OF_OPTIONS}) : ");
                string inputOption = Console.ReadLine();
                int result;
                while (!(inputOption.Length != 0 && int.TryParse(inputOption, out result) && result > 0 && result <= Constant.NO_OF_OPTIONS))
                {
                    Console.Write($"Enter a valid Option (1-{Constant.NO_OF_OPTIONS}) : ");
                    inputOption = Console.ReadLine();
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : while try the get the menu option....");
                Console.WriteLine(ex.ToString());
                throw;
            }

        }


        /*===============================
                    Helper methods
        =================================*/
        public static string GetYesOrNo(string forWhat)
        {
            Console.Write($"Enter y or no for {forWhat}");
            string inputOption = Console.ReadLine();
            while(!(inputOption.Length>0 && inputOption.ToLower()=="y" || inputOption.ToLower()=="n" ))
            {
                Console.Write($"Enter y or n for {forWhat} : ");
                inputOption = Console.ReadLine();
            }
            return inputOption.ToLower();
        }

        //=============================
        //        EMPLOYEE 
        //=============================
        public static string GetEmployeeName()
        {
            try
            {
                Console.Write("Enter the Employee name : ");
                string inputName = Console.ReadLine();
                while (!(Regex.IsMatch(inputName, Constant.NAME_VALIDATION_PATTERN)))
                {
                    Console.Write("Enter a valid Name : ");
                    inputName = Console.ReadLine();
                }
                return inputName.Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while get the employee name from the user.....");
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public static string GetEmployeeEmail()
        {
            try
            {
                Console.Write("Enter a valid Email : ");
                string emailInput = Console.ReadLine();
                while (!(Regex.IsMatch(emailInput, Constant.EMAIL_VALIDATION_PATTERN)))
                {
                    Console.Write("Enter a valid Email : ");
                    emailInput = Console.ReadLine();
                }
                return emailInput.Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : while try to get the email id fromt he user....");
                throw;
            }
        }

        public static string GetPhoneNumber()
        {
            try
            {
                Console.Write("Enter your PhoneNumber(Ex:1234567892) : ");
                string inputNumber = Console.ReadLine();
                while (!(Regex.IsMatch(inputNumber.Trim(), Constant.PROJECT_NAME_VALIDATION_PATTERN)));
                {
                    Console.Write("Enter a valid PhoneNumber : ");
                    inputNumber = Console.ReadLine();
                }
                return inputNumber.Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : while try to get the phone number from the user..");
                Console.WriteLine(ex.ToString());
                throw;
            }
        }


        /*====================================
                     DEPARTMENT
        =======================================*/

        public static int GetDepartmentID()
        {
            DepartmentService departmentService = new DepartmentService();  
            departmentService.ShowAllDepartments();
            Console.Write("\nEnter the One of the Above Department ID : ");
            string inputId = Console.ReadLine();
            int departmentId;
            while(!(int.TryParse(inputId,out departmentId) && departmentId>0 && departmentService.IsAvailableDepartment(departmentId)))
            {
                Console.Write("Enter a valid Department Id : ");
                inputId = Console.ReadLine();
            }
            return departmentId;
        }

       
        /*=====================================
                    PROJECT
        =======================================*/
        public static string GetProjectName()
        {
            try
            {
                Console.Write("Enter a project Name : ");
                string projectName = Console.ReadLine();
                while (!(Regex.IsMatch(projectName, Constant.PROJECT_NAME_VALIDATION_PATTERN)))
                {
                    Console.Write("Enter a valid project name : ");
                    projectName = Console.ReadLine();
                }
                return projectName;
            }
            catch
             (Exception ex)
            {
                Console.WriteLine("Error : while try to get project name in input validation....");
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public static  GetProjectID()
        {
            string inputProjectId = Console.ReadLine();
            int projectID;
            while(!(int.TryParse(inputProjectId,out projectID) && projectID>0 && IsAvailable(projectID))
            {


            }
        }
    }
}
