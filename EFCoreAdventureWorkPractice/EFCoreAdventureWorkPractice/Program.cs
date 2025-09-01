using System.Linq;
using System.Collections;
using EFCoreAdventureWorkPractice.Models;
using EFCoreAdventureWorkPractice.BusinessLogic;
using EFCoreAdventureWorkPractice.Validation;
using EFCoreAdventureWorkPractice.Constant;
using System.Reflection.Metadata;
using EFCoreAdventureWorkPractice.DTO;
namespace EFCoreAdventureWorkPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int option = ShowOptionAndGetOption();
            var service = new AdventureService();
            do
            {
                switch (option)
                {
                    case 1:
                        {
                            var products = service.GetListOfProducts().Take(5).ToList();
                            ShowProducts(products);
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 2:
                        {
                            int year = 2010; // hardcoded value
                            var employees = service.GetEmployeesHiredAfter2010(year);
                            ShowEmployees(employees);
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 3:
                        {
                            var products = service.RetrieveTopMostExpensiveProducts();
                            ShowProducts(products);
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 4:
                        {
                            string city = "london"; //hardcoded
                            var customers = service.FindCustomersByCity(city);
                            Console.WriteLine(customers.Count());
                            ShowCustomers(customers);
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 5:
                        {
                            int minimumCount = 0;
                            var products = service.GetOutOfStockProducts(minimumCount);
                            ShowProducts(products);
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 6:
                        {
                            int noOfRecentOrders = 5;
                            var orders = service.GetNoOfRecentOrders(noOfRecentOrders);
                            ShowOrderDetails(orders);
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 7:
                        {
                            int customerID = 29672;
                            service.GetAllOrdersForCustomer(customerID);
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 8:
                        {
                            //List all employees along with their managers’ names.
                            //service.ListAllEmployeesWithManager();
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 9:
                        {
                            service.ShowDepartmentNameForEmployee();
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 10:
                        {
                            service.TotalSalesForEachYear();
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 11:
                        {
                            service.AverageListPriceOfSubCategoryProducts();
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 12:
                        {
                            service.BestSellingProductByQuantitySold();
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 13:
                        {
                            int noOfPeoples = 5;
                            service.RetrieveTopSalesPeople(noOfPeoples);
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 14:
                        {
                            service.CustomersNeverPlacedOrder();
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 15:
                        {
                            service.TerritorySalesAndCustomers();
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 16:
                        {
                            service.ExecuteSp();
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 17:
                        {
                            service.RunRawSqlQuery();
                            option = ShowOptionAndGetOption();
                        }
                        break;
                    case 18:
                        Console.WriteLine("Exiting....");
                        break;
                }
            }
            while (option != Constants.NoOfCaseOptions);


        }

        static int ShowOptionAndGetOption()
        {
            Console.WriteLine("\n\n1.Get and show some products" +
                                "\n2.Get all employees hired after 2010." +
                                "\n3.Retrieve the top 10 most expensive products." +
                                "\n4.Find all customers from London and order them by last name." +
                                "\n5.List all products that are out of stock (SafetyStockLevel = 0)." +
                                "\n6.Get the 5 most recent orders placed." +
                                "\n7.Get all orders for a given customer (include order date, product names, and total due)." +
                                "\n8.List all employees along with their managers’ names." +
                                "\n9.Show the department name for each employee." +
                                "\n10.Find the total sales for each year." +
                                "\n11.Get the average list price of products in each subcategory." +
                                "\n12.Find the best-selling product by total quantity sold." +
                                "\n13.Retrieve the top 5 salespeople with the highest sales in 2013" +
                                "\n14.Find customers who have never placed an order." +
                                "\n15.For each territory, show the total sales amount and number of customers." +
                                "\n16.Execute the stored procedure uspGetEmployeeManagers using EF Core" +
                                "\n17.Run a raw SQL query to get products with ListPrice > 1000." +
                                "\n18.Exit\n");
            return InputValidaiton.GetSwitchOption();
        }

        static void ShowProducts(List<ProductDto> products)
        {
            Console.WriteLine($"{"Product Name ",-25}{"Product number",-20}{"Price",-10}");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name,-25}{product.ProductNumber,-20}{product.ListPrice,-10}");
            }
        }

        static void ShowEmployees(List<EmployeeDto> employees)
        {
            Console.WriteLine($"{"Employee name",-30}{"Hired Date",-15}{"Gender",-15}\n");
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.EmployeeName,-30}{employee.HiredDate,-15}{employee.Gender,-15}");
            }
        }

        static void ShowCustomers(List<CustomerDto> customers)
        {
            Console.WriteLine($"{"Customer First name",-30}{"Customer Last name",-25}{"City",-25}\n");
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.FirstName,-30}{customer.LastName,-25}{customer.City,-25}");
            }
        }

        static void ShowOrderDetails(List<OrderDetailsDto> orders)
        {
            Console.WriteLine($"{"Order ID",-30}{"Customer name",-25}{"Order Date",-25}{"Total Due",-20}\n");
            foreach (var order in orders)
            {
                Console.WriteLine($"{order.OrderID,-30}{order.CustomerName,-25}{order.OrderDate,-25}{order.TotalDue,-20}\n");
            }
        }
    }
}
