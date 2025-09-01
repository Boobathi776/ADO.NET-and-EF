using EF_Core_Assessment_1.Models;
using EF_Core_Assessment_1.Services;
using System.Reflection.Metadata;

namespace EF_Core_Assessment_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NorthWindService service = new NorthWindService();

            int option = ShowAndGetOption();
            do
            {
                switch (option)
                {
                    case 1:
                        {
                            service.AddNewCustomer();
                            option = ShowAndGetOption();
                        }
                        break;
                    case 2:
                        {
                            service.DisplayCustomersAndTheirOrdersCount();
                            option = ShowAndGetOption();
                        }
                        break;
                    case 3:
                        {
                            service.TopNnumberOfExpensiveProducts(5);
                            option = ShowAndGetOption();
                        }
                        break;
                    case 4:
                        {
                            service.DisplayEmployeeAndTheirOrdersHandleCount();
                            option = ShowAndGetOption();
                        }
                        break;
                    case 5:
                        {
                            service.DisplayCustomersNotPlacedAnyOrder();
                            option = ShowAndGetOption();
                        }
                        break;
                    case 6:
                        {
                            service.ExecuteSpCustOrderHist();
                            option = ShowAndGetOption();
                        }
                        break;
                    case 7:
                        {
                            service.DisplayProductInGivenCategory();
                            option = ShowAndGetOption();
                        }
                        break;
                    case 8:
                        {
                            service.ChangeEmployeeAddress();
                            option = ShowAndGetOption();
                        }
                        break;
                    case 9:
                        Console.WriteLine("Exiting......");
                        break;
                    default:
                        option = ShowAndGetOption();
                        break;
                }
            } while (option != Constants.Constant.NoOfSwitchOptions);

        }

        static int ShowAndGetOption()
        {
            Console.WriteLine("\n\n1.Add a new record to customers table \n" +
                               "2.Display a list of all customers and their total number of orders \n" +
                               "3.Display the top 5 expensive products.\n" +
                               "4.Display each employee’s full name and number of orders they handled.\n" +
                               "5.Display all customers who didn't place any orders \n" +
                               "6.Execute 'CustOrderHist' stored procedure and display the result \n" +
                               "7.Display all products with category contains the text entered by the user. \n" +
                               "8.For the given EmployeeId update the address \n" +
                               "9.Exit\n\n");
            return InputValidation.InputValidation.GetOption();
        }
    }
}
