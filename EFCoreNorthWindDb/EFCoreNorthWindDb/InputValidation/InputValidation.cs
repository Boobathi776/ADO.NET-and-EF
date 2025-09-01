using EFCoreNorthWindDb.Constants;
using EFCoreNorthWindDb.Models;
using EFCoreNorthWindDb.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EFCoreNorthWindDb.InputValidation
{
    internal class InputValidation
    {
        public static int GetSwitchOption()
        {
            try
            {
                Console.Write($"Enter your option (1-{Constant.NoOfOptions}) : ");
                string inputValue = Console.ReadLine();
                int option;
                while (!(int.TryParse(inputValue, out option) && option > 0 && option <= Constant.NoOfOptions))
                {
                    Console.Write($"Enter a valid option (1-{Constant.NoOfOptions}) : ");
                    inputValue = Console.ReadLine();
                }
                return option;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : unable to get the user input from the user for the switch option....");
                throw;
            }
        }

        /*====================================
                    PRODUCT
        ======================================*/
        public static int GetProductID()
        {
            try
            {
                List<Product> products = NorthWindRepo.GetProducts();
                Console.WriteLine($"{"Product ID",-10}{"Product Name",-40}{"Product Quantity Per unit",-30}{"UnitPrice",-20}");
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.ProductId,-10}{product.ProductName,-40}{product.QuantityPerUnit,-30}{product.UnitPrice,-20}");
                }

                Console.Write("\n\nEnter a Product ID : ");
                string inputString = Console.ReadLine();
                int productID;

                while (!(int.TryParse(inputString, out productID) && products.Any(p => p.ProductId == productID)))
                {
                    Console.Write("Enter a valid Product ID : ");
                    inputString = Console.ReadLine();
                }
                return productID;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to get the product ID from the user.....");
                throw;
            }
        }

        public static string GetProductName()
        {
            try
            {
                Console.Write("Enter the Product Name : ");
                string productName = Console.ReadLine();
                while (!(Regex.IsMatch(productName, Constant.NameValidationPattern)))
                {
                    Console.Write("Enter a valid Product Name : ");
                    productName = Console.ReadLine();
                }
                return productName;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to get the Product name from the user...");
                throw;
            }
        }

        public static decimal GetUnitPriceOfTheProduct(string productName)
        {
            try
            {
                Console.Write($"Enter a unit price for {productName} :  ");
                string inputUnitPrice = Console.ReadLine();
                decimal unitPrice;
                while (!(decimal.TryParse(inputUnitPrice, out unitPrice) && unitPrice > 0))
                {
                    Console.Write($"Enter a valid price for {productName} : ");
                    inputUnitPrice = Console.ReadLine();
                }
                return unitPrice;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to get the product unit price from the user.....");
                throw;
            }
        }

        public static int GetProductQuantity(int productID)
        {
            try
            {
                var products = NorthWindRepo.GetProducts();
                var product = products.FirstOrDefault(p => p.ProductId == productID);
                string productName = product.ProductName;
                var stock = product.UnitsInStock;
                Console.Write($"How much you want \"{product.ProductName}\" Available Stock = \"{product.UnitsInStock}\" : ");
                string inputQuantity = Console.ReadLine();
                int quantity;
                while (!(int.TryParse(inputQuantity, out quantity) && quantity > 0 && quantity <= stock))
                {
                    Console.Write("Enter a valid quantity : ");
                    inputQuantity = Console.ReadLine();
                }
                return quantity;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : Unable to get the Quantity from the user....");
                throw;
            }
        }

        public static short GetUnitsInStock(string productName)
        {
            try
            {
                Console.Write($"Enter stock for {productName} :  ");
                string inputStock = Console.ReadLine();
                short stock;
                while (!(short.TryParse(inputStock, out stock) && stock > 0))
                {
                    Console.Write($"Enter a valid stock for {productName} : ");
                    inputStock = Console.ReadLine();
                }
                return stock;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Unable get the stock for a product....");
                throw;
            }
        }

        /*========================================
                        CATEGORY
        =========================================*/
        public static string GetCategoryName()
        {
            try
            {
                Console.Write("Enter the Category Name : ");
                string categoryName = Console.ReadLine();
                while (!(Regex.IsMatch(categoryName, Constant.NameValidationPattern)))
                {
                    Console.Write("Enter a valid Category Name : ");
                    categoryName = Console.ReadLine();
                }
                return categoryName;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to get the category name from the user...");
                throw;
            }
        }

        /*======================================
                    CUSTOMER
        ========================================*/
        public static string GetCustomerID()
        {
            try
            {
                Console.Write("Enter a Customer ID (3-5 characters) : ");
                string customerID = Console.ReadLine();
                List<Customer> customers = NorthWindRepo.GetCustomers();
                while (!(customerID.Length > 3 && Regex.IsMatch(customerID, Constant.CustomerIDValidationPattern) && customers.Any(c => c.CustomerId == customerID)))
                {
                    Console.Write("Enter a valid customer ID : ");
                    customerID = Console.ReadLine();
                }
                return customerID;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : unable to get the Customer ID from the user......");
                throw;
            }
        }

        /*==============================================
                        EMPLOYEE
        ================================================*/
        public static int GetEmployeeID()
        {
            try
            {
                Console.Write("Enter a Employee ID : ");
                string inputString = Console.ReadLine();
                int employeeID;
                List<Employee> employees = NorthWindRepo.GetEmployees();
                while(!(int.TryParse(inputString, out employeeID) && employees.Any(e=>e.EmployeeId == employeeID) ))
                {
                    Console.Write("Enter a valid Employee ID : ");
                    inputString  = Console.ReadLine();  
                }
                return employeeID;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : unable to get the Employee ID from the user......");
                throw;
            }
        }

        public static string GetEmployeeName(string prefix)
        {
            try
            {
                Console.Write($"Enter the Employee {prefix} Name : ");
                string employeeName = Console.ReadLine();
                while (!(Regex.IsMatch(employeeName, Constant.NameValidationPattern)))
                {
                    Console.Write($"Enter a valid Employee {prefix} Name : ");
                    employeeName = Console.ReadLine();
                }
                return employeeName;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : unable to get the Employee {prefix} name from the user...");
                throw;
            }

        }

        /*============================================
                        MANAGER
        ==============================================*/
        public static int GetManagerID()
        {
            try
            {
                Console.Write("Enter a Manager ID : ");
                string inputString = Console.ReadLine();
                int managerID;
                List<Employee> managers = NorthWindRepo.GetManagers();
                while (!(int.TryParse(inputString, out managerID) && managers.Any(e => e.EmployeeId == managerID)))
                {
                    Console.Write("Enter a valid Manager ID : ");
                    inputString = Console.ReadLine();
                }
                return managerID;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : unable to get the Manager ID from the user......");
                throw;
            }
        }

        public static string GetManagerName(string prefix)
        {
            try
            {
                Console.Write($"Enter the Manager {prefix} Name : ");
                string managerName = Console.ReadLine();
                while (!(Regex.IsMatch(managerName, Constant.NameValidationPattern)))
                {
                    Console.Write($"Enter a valid Manager {prefix} Name : ");
                    managerName = Console.ReadLine();
                }
                return managerName;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : unable to get the Manager {prefix} name from the user...");
                throw;
            }
        }

        public static int GetYear()
        {
            try
            {
                Console.Write("Enter a year : ");
                string inputYear = Console.ReadLine();
                int year;
                while(!(inputYear.Length == 4 && int.TryParse(inputYear,out year) && year > 1000 && year <=DateTime.Now.Year))
                {
                    Console.Write("Enter a valid year : ");
                    inputYear = Console.ReadLine();
                }
                return year;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error : unable to get the year from the user....");
                throw;
            }
        }
        public static string GetYesOrNo()
        {
            try
            {
                Console.Write("Enter your choice (y-n) : ");
                string inputChoice = Console.ReadLine().ToLower();
                while(!(Regex.IsMatch(inputChoice,Constant.YesOrNoPattern) && inputChoice.Length==1))
                {
                    Console.Write("Enter a valid choice : ");
                    inputChoice = Console.ReadLine().ToLower();
                }
                return inputChoice;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error : unable to get yes or no from the user....");
                throw;
            }
        }

    }
}
