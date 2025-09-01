using EF_CodeFirstApproach.DataAccess;
using System;
using EF_CodeFirstApproach.DTO;
using EF_CodeFirstApproach.Model;

namespace EF_CodeFirstApproach.Service
{
    internal class EmployeeServices
    {
        public void CreateNewEmployeeWithProject()
        {

            string employeeName = InputValidation.GetEmployeeName();
            string email = InputValidation.GetEmployeeEmail();
            string phoneNumber = InputValidation.GetPhoneNumber();
            int departmentID = InputValidation.GetDepartmentID();
            Console.WriteLine(employeeName);
            string projectName;
            string optionForProject = InputValidation.GetYesOrNo("Assign Project");
            if(optionForProject.ToLower()=="y")
            {
               projectName = InputValidation.GetProjectName();
            }
            else
            {
                Console.WriteLine("Invalid option is entered...");
            }
            //EmployeeRepository employeeRepository = new EmployeeRepository();

        }

        public void UpdateEmployeeAndDepartmentAndProject(EmployeeDetails employeeDetails)
        {

        }
    }
}
