using EF_DbFirstApproach.Constants;
using EF_DbFirstApproach.CRUD;
using EF_DbFirstApproach.Utilities;
using System;

namespace EF_DbFirstApproach
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.Clear();
            Console.WriteLine("*****************************************************************************************************");
            Console.WriteLine("*************************************          CRUD using EF        *********************************");
            Console.WriteLine("*****************************************************************************************************");
            Crud crud = new Crud();

            int option = ShowOptions();

            do
            {
                switch (option)
                {
                    case 1:
                        Console.WriteLine("\nINSERT A NEW STUDENT\n"); //not completed
                        option = ShowOptions();
                        break;
                    case 2:
                        Console.WriteLine("\nUPDATE A STUDENT\n");
                        crud.ShowALLStudents();
                        crud.UpdateStudent(InputValidation.GetStudentID());
                        option = ShowOptions();
                        break;
                    case 3:
                        Console.WriteLine("\nDELETE A STUDENT\n");
                        crud.ShowALLStudents();
                        crud.DeleteStudent(InputValidation.GetStudentID());
                        option = ShowOptions();
                        break;
                    case 4:
                        Console.WriteLine("\nVIEW ALL STUDENT\n");
                        crud.ShowALLStudents();
                        option = ShowOptions();
                        break;
                    case 5:
                        Console.WriteLine("Exiting.......");
                        break;

                    default:
                        Console.WriteLine("Invalid choice......");
                        option = ShowOptions();
                        break;
                }
            } while (option != Constant.NoOfOptions);

        }

        public static int ShowOptions()
        {
            Console.Write("1.Insert new Student\n2.Update Student\n3.Delete a Student\n4.View all Student\n5.EXIT\n");
            int option = InputValidation.GetValueForSwtich(Constant.NoOfOptions);
            return option;
        }

    }
}

