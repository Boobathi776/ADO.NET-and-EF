using EF_Core_Assessment_1.DTO;
using EF_Core_Assessment_1.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EF_Core_Assessment_1.Services
{
    public class NorthWindService
    {

        //Q1.Add a new record to customers table 
        public void AddNewCustomer()
        {
            try
            {
                string companyName = InputValidation.InputValidation.GetString("Company Name");
                string contactName = InputValidation.InputValidation.GetString("Contact Name");
                string contactTitle = InputValidation.InputValidation.GetString("Contact Title");
                string Address = InputValidation.InputValidation.GetAddrss(forWhat: "Customer");
                string City = InputValidation.InputValidation.GetString("City Name");
                //int postalCode;
                string country = InputValidation.InputValidation.GetString("Country Name");
                //string Phone;
                //string Fax;
                using (var context = new NorthWindContext())
                {
                    Customer customer = new Customer()
                    {
                        CustomerId = companyName.Substring(0, 4).ToUpper(),
                        CompanyName = companyName,
                        ContactName = contactName,
                        ContactTitle = contactTitle,
                        Address = Address,
                        City = City,
                        Country = country
                    };

                    var customers = context.Customers;
                    customers.Add(customer);
                    context.SaveChanges();
                    Console.WriteLine("\nThe new Customer added succssfully...");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : unable to add a new Customer....");
                Console.WriteLine(ex.ToString());
            }
        }

        // Q.2 Display a list of all customers and their total number of orders
        // - Output format: CustomerID | CompanyName | TotalOrders
        public void DisplayCustomersAndTheirOrdersCount()
        {
            try
            {
                using (var context = new NorthWindContext())
                {
                    var orders = context.Orders
                        .Include(o => o.Customer)
                        .GroupBy(o => new { o.CustomerId, o.Customer.CompanyName })
                        .Select(order => new
                        {
                            CustomerID = order.Key.CustomerId,
                            CompanyName = order.Key.CompanyName,
                            count = order.Count()
                        })
                        .ToList();
                    Console.WriteLine($"{"CustomerID",-20}{"Company Name",-40}{"OrderCount",-10}");
                    foreach (var order in orders)
                    {
                        Console.WriteLine($"{order.CustomerID,-20}{order.CompanyName,-40}{order.count,-10}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : unable to get the users and their orders count....");
            }
        }

        //Q.3 Display the top 5 expensive products
        //- Output format: ProductID | ProductName | Price
        public void TopNnumberOfExpensiveProducts(int noOfItems)
        {

            try
            {
                using (var context = new NorthWindContext())
                {
                    var products = context.Products
                        .AsNoTracking()
                        .OrderByDescending(p => p.UnitPrice)
                        .Take(noOfItems)
                        .Select(prod => new
                        {
                            productID = prod.ProductId,
                            productName = prod.ProductName,
                            price = prod.UnitPrice
                        })
                        .ToList();

                    Console.WriteLine($"{"ProductID",-15}{"ProductName",-40}{"Price",-15}");

                    foreach (var product in products)
                    {
                        Console.WriteLine($"{product.productID,-15}{product.productName,-40}{product.price,-15}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : unable to get the expensive products ....");
            }
        }

        //4.  Display each employee’s full name and number of orders they handled.
        //.  - Output: EmployeeId | EmployeeName | OrdersHandled

        public void DisplayEmployeeAndTheirOrdersHandleCount()
        {

            try
            {
                using (var context = new NorthWindContext())
                {
                    var employees = context.Orders
                        .AsNoTracking()
                        .Include(o => o.Employee)
                        .GroupBy(o => new
                        {
                            EmployeeID = o.EmployeeId,
                            EmployeeName = o.Employee.FirstName + " " + o.Employee.LastName
                        })
                        .Select(emp => new
                        {
                            EmpID = emp.Key.EmployeeID,
                            Name = emp.Key.EmployeeName,
                            NoOfOrdersHandled = emp.Count()
                        })
                        .OrderBy(obj => obj.EmpID)
                        .ToList();

                    Console.WriteLine($"{"Employee ID ",-20}{"Employee Name",-40}{"OrdersHandled",-20}");
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"{employee.EmpID,-20}{employee.Name,-40}{employee.NoOfOrdersHandled,-20}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : unable to get the Employee details to display their handled orders count....");
            }
        }


        //Q.5 Display all customers who didn't place any orders 
        //- Output format: CustomerID | CustomerName
        public void DisplayCustomersNotPlacedAnyOrder()
        {
            try
            {
                using (var context = new NorthWindContext())
                {
                    var orders = context.Orders.AsNoTracking().ToList();
                    var customers = context.Customers.AsNoTracking().ToList();

                    var RemainingCustomers = customers
                        .GroupJoin(orders,
                        c => c.CustomerId,
                        o => o.CustomerId,
                        (c, o) => new { c, o })
                        .SelectMany(
                        obj => obj.o.DefaultIfEmpty(),
                        (obj, o) => new
                        {
                            CustomerID = obj.c.CustomerId,
                            CustomerName = obj.c.CompanyName,
                            OrderID = o?.OrderId ?? null
                        });

                    Console.WriteLine($"{"Customer ID",-20}{"Customer Name",-40}");
                    foreach (var item in RemainingCustomers)
                    {
                        if (item.OrderID == null)
                        {
                            Console.WriteLine($"{item.CustomerID,-20}{item.CustomerName,-40}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : unable to get the customers that never placed any orders....");
            }
        }

        //6. Execute 'CustOrderHist' stored procedure and display the result 
        //-  get input from console
        public void ExecuteSpCustOrderHist()
        {
            try
            {
                using (var context = new NorthWindContext())
                {

                    string customerID = InputValidation.InputValidation.GetCustomerID();
                    if (context.Customers.Any(c => c.CustomerId == customerID))
                    {
                        SqlParameter param = new SqlParameter("@CustomerID", customerID);
                        param.Direction = System.Data.ParameterDirection.Input;
                        var orders = context.Set<CustomerOrderHistoryDto>().FromSqlRaw("Exec CustOrderHist @CustomerID", param).ToList();

                        Console.WriteLine($"{"ProductName",-30}{"Total Quantity",-20}");
                        foreach (var order in orders)
                        {
                            Console.WriteLine($"{order.ProductName,-30}{order.Total,-20}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no customer id in the given ID.....");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : unable to call the Sp for get the order history....");
            }
        }

        //7. Display all products with category contains the text entered by the user.
        //- Output format: ProductID | ProductName
        public void DisplayProductInGivenCategory()
        {
            try
            {
                using (var context = new NorthWindContext())
                {
                    var categories = context.Categories.ToList();
                    Console.WriteLine("Available Categories...");
                    foreach (var cate in categories)
                    {
                        Console.WriteLine($"{cate.CategoryName}");
                    }

                    string inputString = InputValidation.InputValidation.GetString("Category Name");

                    var category = context.Categories
                        .Where(c => c.CategoryName.ToLower().Contains(inputString.ToLower()))
                        .FirstOrDefault();

                    if (category != null)
                    {
                        Console.WriteLine($"Category Name = {category.CategoryName}");
                        int id = category.CategoryId;
                        var products = context.Products
                            .Where(p => p.CategoryId == id)
                            .ToList();

                        Console.WriteLine($"{"Product ID",-20}{"Product Name",-45}{"CategoryName",-30}");
                        foreach (var product in products)
                        {
                            Console.WriteLine($"{product.ProductId,-20}{product.ProductName,-45}{product.Category.CategoryName,-30}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no product with the entered category name....");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : unable to get the products for a given category....");
            }
        }


        //8.For the given EmployeeId update the address 
        public void ChangeEmployeeAddress()
        {
            try
            {
                int employeeID = InputValidation.InputValidation.GetEmployeeID();
                using (var context = new NorthWindContext())
                {
                    var employee = context.Employees.Find(employeeID);
                    if (employee != null)
                    {
                        Console.WriteLine($"Old Address :\n{employee.Address}");
                        string employeeAddress = InputValidation.InputValidation.GetAddrss(forWhat: "Employee");
                        employee.Address = employeeAddress;
                        context.SaveChanges();
                        Console.WriteLine("\nEmployee address updated Successfully..............\n");
                    }
                    else
                    {
                        Console.WriteLine("There is no Employee with the Given Employee ID...");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : unable to get the employee and change their address....");
            }
        }

    }
}
