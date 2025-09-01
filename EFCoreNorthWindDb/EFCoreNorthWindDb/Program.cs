using EFCoreNorthWindDb.Constants;
using EFCoreNorthWindDb.InputValidation;
using EFCoreNorthWindDb.Models;
using EFCoreNorthWindDb.Service;

namespace EFCoreNorthWindDb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int option = ShowOptionAndGetOption();
            NorthWindService service = new NorthWindService();
            do
            {
                switch (option)
                {
                    case 1:
                        service.AddProduct();
                        option = ShowOptionAndGetOption();
                        break;
                    case 2:
                        service.AddCategoryAndProducts();
                        option = ShowOptionAndGetOption();
                        break;
                    case 3:
                        service.AddNewEmployeeAndManager();
                        option = ShowOptionAndGetOption();
                        break;
                    case 4:
                        service.AddNewEmployeeToExistingManager();
                        option = ShowOptionAndGetOption();
                        break;
                    case 5:
                        service.UpdateExistingEmployeetoManager();
                        option = ShowOptionAndGetOption();
                        break;
                    case 6:
                        service.CustomersPlacedOrderInParticularYear();
                        option = ShowOptionAndGetOption();
                        break;
                    case 7:
                        service.CustomersMostRecentOrder();
                        option = ShowOptionAndGetOption();
                        break;
                    case 8:
                        service.CustomersWithAboveTotalOrderValue(50000);
                        option = ShowOptionAndGetOption();
                        break;
                    case 9:
                        service.DisplayCategoriesWithAverageUnitPrice();
                        option = ShowOptionAndGetOption();
                        break;
                    case 10:
                        service.ProductsNeverBeenOrdered();
                        option = ShowOptionAndGetOption();
                        break;
                    case 11:
                        service.DisplayTopMostOrderedProducts(3);
                        option = ShowOptionAndGetOption();
                        break;
                    case 12:
                        service.DisplayProductsWithSupplierAndCategoryName();
                        option = ShowOptionAndGetOption();
                        break;
                    case 13:
                        service.DisplayProductsWhereUnitPriceGreaterThanCategoryAverage();
                        option = ShowOptionAndGetOption();
                        break;
                    case 14:
                        service.DisplayEmployeesAndTheirTotalSales();
                        option = ShowOptionAndGetOption();
                        break;
                    case 15:
                        service.EmployeeWhoHandledMostOrders(1997);
                        option = ShowOptionAndGetOption();
                        break;
                    case 16:
                        service.EmployeesWithSameTerritory();
                        option = ShowOptionAndGetOption();
                        break;
                    case 17:
                        service.DisplayEmployeeWithDictinctCustomersCount();
                        option = ShowOptionAndGetOption();
                        break;
                    case 18:
                        service.DisplayEmployeeAndTheirFirstOrder();
                        option = ShowOptionAndGetOption();
                        break;
                    case 19:
                        service.DisplayShipperDeliverTime();
                        option = ShowOptionAndGetOption();
                        break;
                    case 20:
                        service.DisplayOrdersThatDeliverdAfterGivenDays(30);
                        option = ShowOptionAndGetOption();
                        break;
                    case 21:
                        service.DisplayTopShipper();
                        option = ShowOptionAndGetOption();
                        break;
                    case 22:
                        service.TopEmployeeBasedOnSalesInEachYear();
                        option = ShowOptionAndGetOption();
                        break;
                    case 23:
                        service.DisplayProductsOrderedByEveryCustomers();
                        option = ShowOptionAndGetOption();
                        break;
                    case 24:
                        service.DisplaySuppliersWhoSupplyMoreThanNoOfProduct(5);
                        option = ShowOptionAndGetOption();
                        break;
                    case 25:
                        service.CustomersWithSingleHighestOrderValue();
                        option = ShowOptionAndGetOption();
                        break;
                    case 26:
                        service.CustomersWhoOrderedAllProductsInGivenCategory(1);
                        option = ShowOptionAndGetOption();
                        break;
                    case 27:
                        service.DisplayMostProfitableProduct();
                        option = ShowOptionAndGetOption();
                        break;
                    case 28:
                        Console.WriteLine("Exiting.........");
                        break;
                    default:
                        Console.WriteLine("Invalid input....");
                        option = ShowOptionAndGetOption();
                        break;
                }
            } while (option != Constant.NoOfOptions);
        }

        static int ShowOptionAndGetOption()
        {
            Console.WriteLine("\n\n1.\tPlace new order. \n" +
                              "2.\tCreate new category with multiple products.\n" +
                              "3.\tAdd a new employee with manager (both should be new)\n" +
                              "4.\tAdd a new employee to existing manager\n" +
                              "5.\tUpdate existing emplyee to new manager\n" +
                              "6.\tFind customers who placed orders in 1997 but not in 1998.\n" +
                              "7.\tCustomers and Their most recent order date.\n" +
                              "8.\tShow all customers whose total order value exceeds 50000.\n" +
                              "9.\tList each category with the average unit price of products.\n" +
                              "10.\tShow products that have never been ordered.\n" +
                              "11.\tFind the top 3 most ordered products (by total quantity sold).\n" +
                              "12.\tList products along with their supplier name and category name.\n" +
                              "13.\tDisplay all products where UnitPrice > Category Average Price.\n" +
                              "14.\tList employees with the total sales amount they handled.\n" +
                              "15.\tShow the employee who handled the most orders in 1997.\n" +
                              "16.\tFind employees who share the same territory.\n" +
                              "17.\tDisplay each employee with the number of distinct customers they served.\n" +
                              "18.\tList employees along with the first order they ever handled.\n" +
                              "19.\tFor each shipper, calculate the average delivery time (ShippedDate – OrderDate).\n" +
                              "20.\tList orders that took more than 30 days to deliver.\n" +
                              "21.\tFind the top shipper based on the number of orders shipped.\n" +
                              "22.\tShow the top employee per year based on total sales.\n" +
                              "23.\tFind all products that were ordered by every customer.\n" +
                              "24.\tFind suppliers who supply more than 5 products.\n" +
                              "25.\tList the customer(s) with the single highest order value.\n" +
                              "26.\tList customers who have ordered all products in a given category (e.g., Beverages).\n" +
                              "27.\tShow the most profitable product (highest total sales revenue).\n" +
                              "28.\tExit\n");
            return InputValidation.InputValidation.GetSwitchOption();
        }
    }
}
