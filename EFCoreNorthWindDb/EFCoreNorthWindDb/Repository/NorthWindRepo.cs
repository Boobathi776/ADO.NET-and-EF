using EFCoreNorthWindDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreNorthWindDb.Repository
{
    internal class NorthWindRepo
    {
        public static List<Employee> GetEmployees()
        {
            try
            {
                List<Employee> employees;
                using (var context = new NorthwndContext())
                {
                    employees = context.Employees.ToList();
                }
                return employees;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : Unable to get the employee details from the Database...");
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public static List<Employee> GetManagers()
        {
            try
            {
                List<Employee> managers;
                using (var context = new NorthwndContext())
                {
                    managers = context.Employees
                        .Where(e=>e.ReportsTo.HasValue)
                        .Select(e => e.ReportsToNavigation).ToList();
                }
                if (managers != null)
                    return managers;
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : Unable to get the Manager details from the Database...");
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public static List<Customer> GetCustomers()
        {
            try
            {
                List<Customer> customers;
                using(var context = new NorthwndContext())
                {
                    customers = context.Customers.ToList();
                }
                return customers;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to get the customer details from the database.....");
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        internal static List<Product> GetProducts()
        {
            try
            {
                List<Product> products;
                using(var context = new NorthwndContext())
                {
                    products = context.Products.ToList();
                }
                return products;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to get the products from the database.....");
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public static Product GetProduct(int productID)
        {
            try
            {
                Product product;
                using (var context = new NorthwndContext())
                {
                    product = context.Products.Find(productID);
                }
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : unable to get the product from the database....");
                throw;
            }
        }

    }
}
