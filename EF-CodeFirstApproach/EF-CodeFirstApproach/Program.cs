using EF_CodeFirstApproach.Model;
using EF_CodeFirstApproach.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EF_CodeFirstApproach.Constants;

namespace EF_CodeFirstApproach
{
    public class Program
    {
        static void Main(string[] args)
        {
            EmployeeServices employeeServices = new EmployeeServices();
            //employeeServices.CreateNewEmployee();

            int option = ShowOption();
            do
            {
                switch (option)
                {
                    case 1:
                        employeeServices.CreateNewEmployeeWithProject();
                        option = ShowOption();
                        break;
                    case 2:
                        {
                            Console.WriteLine("You are working with project services...");
                            ProjectService projectService = new ProjectService();
                            projectService.CreateNewProject();
                            option = ShowOption();
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Going to show a all the projects..........");
                            ProjectService projectService = new ProjectService();
                            projectService.ShowAllProjects();
                            option = ShowOption();
                            break;
                        }
                    case 4:
                        Console.WriteLine("under construction..........");
                        option = ShowOption();
                        break;
                    case 5:
                        Console.WriteLine("under construction..........");
                        option = ShowOption();
                        break;
                    case 6:
                        Console.WriteLine("Exiting........");
                        break;
                    default:
                        Console.WriteLine("Invalid option has been Entered...");
                        break;
                }

            } while (option != Constant.NO_OF_OPTIONS);

            //using (EmployeeContext employeeContext = new EmployeeContext())
            //{
            //    employeeContext.Departments.ToList().ForEach(employee =>
            //    {
            //        Console.WriteLine(employee.DepartmentName);
            //    });
            //}
        }

        public static int ShowOption()
        {
            Console.WriteLine("\n\n1.Create a new Employee with project\n" +
                              "2.Create a new project\n" +
                              "3.View all projects" +
                              "4.Insert All related data at a Time\n" +
                              "5.Update All related data at a Time\n" +
                              "6.Bulk delete\n" +
                              "6.Exit\n");

            return InputValidation.GetMenuOption();
        }

    }
}
