using EFCoreAdventureWorkPractice.DTO;
using EFCoreAdventureWorkPractice.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreAdventureWorkPractice.BusinessLogic
{
    internal class AdventureService
    {

        //Q.1
        public List<ProductDto> GetListOfProducts()
        {
            try
            {
                using (var context = new AdventureWorks2019Context())
                {
                    var products = context.Products.Select(p => new ProductDto { Name = p.Name, ProductNumber = p.ProductNumber, ListPrice = p.ListPrice }).ToList();
                    return products;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured while i try to get the product list....");
                Console.WriteLine(ex.ToString());
                throw;
            }

        }

        //Q.2
        public List<EmployeeDto> GetEmployeesHiredAfter2010(int year)
        {
            try
            {
                using (var context = new AdventureWorks2019Context()) 
                {
                    //int currentYear = DateTime.Now.Year;
                    var employees = context.Employees.Where(e => e.HireDate.Year > 2010).ToList();
                    Console.WriteLine(employees.Count.ToString());

                    List<EmployeeDto> filteredEmployees = employees
                                           .Join(context.People,
                                           people => people.BusinessEntityId,
                                           employee => employee.BusinessEntityId,
                                           (people, employee) =>
                                               new EmployeeDto
                                               {
                                                   EmployeeName = employee.FirstName + " " + employee.MiddleName + " " + employee.LastName,
                                                   HiredDate = people.HireDate,
                                                   Gender = people.Gender
                                               }).ToList();
                    return filteredEmployees;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error :  unable to get the hired employees after 2010...");
                throw;
            }

        }

        //Q.3
        public List<ProductDto> RetrieveTopMostExpensiveProducts()
        {
            try
            {
                using(var context = new AdventureWorks2019Context())
                {
                    var products = context.Products
                        .OrderByDescending(p=>p.ListPrice)
                        .Take(10)
                        .Select(p=>new ProductDto() { Name=p.Name,ProductNumber=p.ProductNumber,ListPrice = p.ListPrice})
                        .ToList();
                    return products;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error : unable to fint the top 10 expensive products.....");
                throw;
            }
        }


        //Q.4
        public List<CustomerDto> FindCustomersByCity(string cityName)
        {
            try
            {
                using(var context = new AdventureWorks2019Context())
                {

                    //var businessEntityAddresses = context.BusinessEntityAddresses
                    //    .Include(e=>e.Address)
                    //    .Where(b=>b.AddressId == b.Address.AddressId && b.Address.City == cityName)
                    //    .ToList();

                    //var persons = context.People.ToList();

                    //List<CustomerDto> customerList = persons
                    //    .Join(businessEntityAddresses,
                    //    p => p.BusinessEntityId,
                    //    be => be.BusinessEntityId,
                    //    (p, be) => new CustomerDto() { FirstName = p.FirstName, LastName = p.LastName, City = be.Address.City })
                    //    .ToList();

                    List<CustomerDto> customerList = context.Customers
                        .Include(c => c.Person)
                        .ThenInclude(p => p.BusinessEntity)
                        .ThenInclude(be => be.BusinessEntityAddresses)
                        .Where(obj => obj.Person.BusinessEntity.BusinessEntityAddresses.Any(add => add.Address.City == cityName))
                        .Select(cus => new CustomerDto()
                        {
                            FirstName =  cus.Person.FirstName,
                            LastName = cus.Person.LastName,
                            City = cityName
                        })
                        .ToList();

                    return customerList;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error :unable to get the users by their country name...");
                throw;
            }
        }

        //Q.5
        public List<ProductDto> GetOutOfStockProducts(int minimumCount)
        {
            try
            {
                using(var context = new AdventureWorks2019Context())
                {
                    var products = context.Products.Where(p => p.SafetyStockLevel <= minimumCount)
                        .Select(p => new ProductDto()
                        { 
                            Name = p.Name,
                            ProductNumber = p.ProductNumber,
                            ListPrice = p.ListPrice,
                        }).ToList();
                    return products;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error  : while try to get the out of stock products ....");
                throw;
            }
        }

        //Q.6
        public List<OrderDetailsDto> GetNoOfRecentOrders(int NoOfRecentOrders)
        {
            try
            {
                using(var context = new AdventureWorks2019Context())
                {
                    var orders = context.SalesOrderHeaders
                        .Include(s => s.Customer)
                        .Include(s => s.SalesOrderDetails)
                        .ThenInclude(so => so.SpecialOfferProduct)
                        .OrderByDescending(soh => soh.OrderDate)
                        .Select(soh => new OrderDetailsDto()
                        {
                            OrderID = soh.SalesOrderId,
                            TotalDue = soh.TotalDue,
                            CustomerName = soh.Customer.Person.FirstName,
                            OrderDate = soh.OrderDate
                        })
                        .Take(NoOfRecentOrders)
                        .ToList();

                    return orders;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error :  unable to get the Recent orders at line no 179 in AdventureSerive.cs file..");
                throw;
            }
        }

        //Q.7 
        public void GetAllOrdersForCustomer(int customerId)
        {
            try
            {
                using (var context = new AdventureWorks2019Context())
                {
                    var customer = context.Customers
                        .Where(c => c.CustomerId == customerId)
                        .Include(c => c.SalesOrderHeaders)
                            .ThenInclude(soh => soh.SalesOrderDetails)
                                .ThenInclude(sod => sod.SpecialOfferProduct)
                                    .ThenInclude(sop => sop.Product)
                        .FirstOrDefault();

                    if (customer == null)
                    {
                        Console.WriteLine("Customer not found.");
                        return;
                    }

                    Console.WriteLine($"Customer ID: {customer.CustomerId}");
                    Console.WriteLine($"Total Orders = {customer.SalesOrderHeaders.Count}");

                    foreach (var order in customer.SalesOrderHeaders)
                    {
                        Console.WriteLine($"Order ID = {order.SalesOrderId}");

                        foreach (var ord in order.SalesOrderDetails)
                        {
                            Console.WriteLine($"Product Name  = {ord.SpecialOfferProduct.Product.Name}\n" +
                                              $"Price = {ord.SpecialOfferProduct.Product.ListPrice}\n" +
                                              $"Qty = {ord.OrderQty}\n" +
                                              $"Total = {ord.LineTotal}\n");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: unable to get customer order details. {e.Message}");
                throw;
            }
        }

        // Q.8 Working................
        public void ShowEmployeesAndTheirManagers()
        {
            try
            {
                using (var context = new AdventureWorks2019Context())
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : Unable to find the Manager name....");
            }
        }

        //Q.9 
        public void ShowDepartmentNameForEmployee()
        {
            try
            {
                using(var context = new AdventureWorks2019Context())
                {

                    //solution 1
                    var result = context.EmployeeDepartmentHistories
                        .Join(context.Employees,
                        edh => edh.BusinessEntityId,
                        e => e.BusinessEntityId,
                        (edh, e) => new { edh, e })
                        .Join(context.Departments,
                        temp => temp.edh.DepartmentId,
                        d => d.DepartmentId,
                        (temp, d) => new { EmployeeName = temp.e.BusinessEntity.FirstName, DepartmentName = d.Name })
                        .ToList();

                    foreach (var item in result)
                    {
                        Console.WriteLine($"{item.EmployeeName,-30}{item.DepartmentName,-30}");
                    }

                    //Solution 2
                  //  var employeeDetails = context.EmployeeDepartmentHistories
                  //      .Include(e => e.Department)
                  //      .AsNoTracking()
                  //      .ToList();

                  //  var employees = context.Employees.ToList();
                  //var results =  employees.Join(employeeDetails,
                  // e => e.BusinessEntityId,
                  // ed => ed.BusinessEntityId,
                  // (e, ed) => new { EmployeeName = e.BusinessEntity.FirstName + " "+e.BusinessEntity.LastName,
                  //     DepartmentName = ed.Department.Name
                  // }
                  // ).ToList();

                  //  Console.WriteLine($"{"EmployeeName",-30}{"DepartmentName",-30}");

                  //  foreach (var item in results)
                  //  {
                  //      Console.WriteLine($"{item.EmployeeName,-30}{item.DepartmentName,-30}");
                  //  }

                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to show a department details for the Employees..");
                throw;
            }
        }

        //10. total sales in each year
        public void TotalSalesForEachYear()
        {
            try
            {
                using (var context = new AdventureWorks2019Context())
                {
                    var totalSales = context.SalesOrderHeaders
                        .GroupBy(soh=>soh.OrderDate.Year)
                        .Select(soh=> new { Year = soh.Key , TotalRevenue = soh.Sum(o=>o.TotalDue) })
                        .ToList();

                    foreach(var item in totalSales)
                    {
                        Console.WriteLine($"{item.Year,-15}{item.TotalRevenue,-20}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : Unable to find total sales for each year....");
            }
        }

     

        //Q.11
        public void AverageListPriceOfSubCategoryProducts()
        {
            try
            {
                using (var context = new AdventureWorks2019Context())
                {
                    var result = context.ProductSubcategories
                        .Distinct()
                        .Include(p => p.Products)
                        .AsNoTracking()
                        .Select(p => new { SubCategory = p.Name, Average = p.Products.Average(p => p.ListPrice) })
                        .ToList();

                    foreach (var item in result)
                    {
                        Console.WriteLine($"{item.SubCategory,-20}{item.Average,-20}");
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRor : unable to fetch the sub category products...");
                throw;
            }
        }

        //Question no 12 
        public void BestSellingProductByQuantitySold()
        {
            try
            {
                using (var context = new AdventureWorks2019Context())
                {
                    var products = context.SalesOrderDetails
                        .GroupBy(sod => sod.ProductId)
                        .Select(sod=> new { ProductID = sod.Key ,Quantity = sod.Sum(p => p.OrderQty) })
                        .Join(context.Products,
                         obj=>obj.ProductID,
                         p=>p.ProductId,
                        (obj,p)=>new {ProductName = p.Name , QuantityHere = obj.Quantity})
                        .OrderByDescending(obj=>obj.QuantityHere)
                        .ToList();

                    foreach(var prod in products)
                    {
                        Console.WriteLine($"{prod.ProductName,-30}{prod.QuantityHere,-30}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : Unable to find the best selling product......");
            }
        }

        // Q.13  Working................
        public void RetrieveTopSalesPeople(int count)
        {
            try
            {
                using (var context = new AdventureWorks2019Context())
                {
                    var salesPerson = context.SalesOrderHeaders
                        .Where(soh=>soh.OrderDate.Year == 2013 && soh.SalesPersonId != null)
                        .Select(soh=> new
                        {
                            SalesPersonId = soh.SalesPersonId,
                            Name = soh.SalesPerson.BusinessEntity.BusinessEntity.FirstName
                        })
                        .GroupBy(soh=>new { soh.SalesPersonId, soh.Name })
                        .Select(soh=>new { SalesPersonID = soh.Key.SalesPersonId,Name = soh.Key.Name , NoOfOrders = soh.Count()})
                        .Take(count)
                        .OrderByDescending(obj=>obj.NoOfOrders)
                        .ToList();

                    foreach(var item in salesPerson)
                    {
                        Console.WriteLine($"{item.SalesPersonID,-10}{item.Name,-30}{item.NoOfOrders,-30}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : Unable to find the Sales people....");
            }
        }

        //Q.14
        public void CustomersNeverPlacedOrder()
        {
            try
            {
                using(var context = new AdventureWorks2019Context())
                {
                    var customers = context.Customers
                        .Where(c => !c.SalesOrderHeaders.Any())
                        .Join(context.People,
                        c => c.CustomerId,
                        p => p.BusinessEntityId,
                        (c, p) => new { c , p })
                        .ToList();

                    foreach(var customer in customers)
                    {
                        Console.WriteLine($"{customer.c.CustomerId,-10}{customer.p.FirstName,-20}{customer.c.SalesOrderHeaders.Count}");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to get the customers that never placed orderss...");
            }
        }

        //Q.15
        public void TerritorySalesAndCustomers()
        {
            try
            {
                using(var context = new AdventureWorks2019Context())
                {
                    var territory = context.SalesTerritories
                        .Include(st => st.Customers)
                        .AsNoTracking()
                        .ToList();
                    Console.WriteLine($"{"Territory Name",-20}{"Total Sales Amount",-20}{"No of Customers",-20}");
                   foreach(var terr in territory)
                    {
                        Console.WriteLine($"{terr.Name,-20}{terr.SalesYtd,-20}{terr.Customers.Count}");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : Unable to get the details from the territory.....");
                throw;
            }
        }

        //Q.16
        public void ExecuteSp()
        {
            try
            {
                using(var context = new AdventureWorks2019Context())
                {
                    //SqlParameter spParam = new SqlParameter("@BusinessEntityID", SqlDbType.Int);
                    //spParam.Direction = ParameterDirection.Input;
                    //spParam.Value = 1;
                    int BusinessEntityID = 10;
                    var employees = context.Set<EmployeeManagersDto>().FromSqlInterpolated($"Exec uspGetEmployeeManagers {BusinessEntityID}").ToList();
                    //Console.WriteLine(employees);
                    //Console.WriteLine(employees.Count());
                    Console.WriteLine($"{"RecursionLevel",-20}{"FirstName",-20}{"LastName",-20}{"ManagerFirstName",-20}{"ManagerLastName",-20}");

                    foreach (var emp in employees)
                    {
                        Console.WriteLine($"{emp.RecursionLevel,-20}{emp.FirstName,-20}{emp.LastName,-20}{emp.ManagerFirstName,-20}{emp.ManagerLastName,-20}");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to execute the Stored procedure....");
                throw;
            }
        }

        //Q.17
        public void RunRawSqlQuery()
        {

            using(var context = new AdventureWorks2019Context())
            {
                string sql = "select ProductId,Name,ListPrice from Production.Product where ListPrice > 1000";

                var data = context.Products.FromSqlRaw(sql).Select(p => new {ProductID = p.ProductId,productName = p.Name,ListPrice = p.ListPrice}).ToList();

                foreach(var item in data)
                {
                    Console.WriteLine($"{item.ProductID,-10}{item.productName,-40}{item.ListPrice,-25}");
                }
            }
        }


    }   

}
