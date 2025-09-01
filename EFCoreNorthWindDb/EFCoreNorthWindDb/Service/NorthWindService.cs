using EFCoreNorthWindDb.InputValidation;
using EFCoreNorthWindDb.Models;
using EFCoreNorthWindDb.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EFCoreNorthWindDb.Service
{
    internal class NorthWindService
    {
        //1. Place new order - Input from Console CustomerID, EmployeeID, Multiple products (ProductID and Quantity for each)
        public void AddProduct()
        {
            try
            {
                string customerID = InputValidation.InputValidation.GetCustomerID();
                int employeeID = InputValidation.InputValidation.GetEmployeeID();
                List<OrderDetail> orderDetailList = new List<OrderDetail>();
                string choice;
                do
                {
                    int productID = InputValidation.InputValidation.GetProductID();
                    int quantity = InputValidation.InputValidation.GetProductQuantity(productID);
                    Product product = NorthWindRepo.GetProduct(productID);
                    decimal unitPrice = product.UnitPrice ?? 0;
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        ProductId = productID,
                        Quantity = (short)quantity,
                        UnitPrice = unitPrice
                    };
                    orderDetailList.Add(orderDetail);
                    Console.WriteLine("Do you want add more products in your cart ?");
                    choice = InputValidation.InputValidation.GetYesOrNo();
                } while (choice == "y");

                using (var context = new NorthwndContext())
                {
                    var products = context.Products;
                    foreach (var orderitem in orderDetailList)
                    {
                        int productID = orderitem.ProductId;
                        Product product = products.Find(productID);
                        product.UnitsInStock -= orderitem.Quantity;
                    }
                    Order newOrder = new Order()
                    {
                        CustomerId = customerID,
                        EmployeeId = employeeID,
                        OrderDetails = orderDetailList
                    };
                    context.Orders.Add(newOrder);
                    context.SaveChanges();
                    Console.WriteLine("\nYour Order confirmed............\nWelcome!!!!!!!\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to add a new product....");
                Console.WriteLine(e.ToString());
            }
        }

        //2. Create new category with multiple products
        //- Output: CategoryId | CategoryName | ProductId | ProdutName | UnitPrice
        public void AddCategoryAndProducts()
        {
            try
            {
                string categoryName = InputValidation.InputValidation.GetCategoryName();
                List<Product> products = new List<Product>();
                string choice;
                do
                {
                    string productName = InputValidation.InputValidation.GetProductName();
                    decimal unitPrice = InputValidation.InputValidation.GetUnitPriceOfTheProduct(productName);
                    short unitsInStock = InputValidation.InputValidation.GetUnitsInStock(productName);
                    products.Add(new Product() { ProductName = productName, UnitPrice = unitPrice });
                    Console.WriteLine("Do you want add more products in your cart ?");
                    choice = InputValidation.InputValidation.GetYesOrNo();
                } while (choice == "y");

                using (var context = new NorthwndContext())
                {
                    var categories = context.Categories;
                    categories.Add(new Category()
                    {
                        CategoryName = categoryName,
                        Products = products
                    });
                    context.SaveChanges();
                    Console.WriteLine($"The new Category ({categoryName}) Added successfully.....");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : while try to create a new Category of products.....");
                Console.WriteLine(e.ToString());
            }
        }

        //3. Add a new employee with manager (both should be new)
        //Display ManagerId | ManagerName | EmployeeId | EmployeeName
        public void AddNewEmployeeAndManager()
        {
            try
            {
                using (var db = new NorthwndContext())
                {
                    string managerFirstName = InputValidation.InputValidation.GetManagerName("First");
                    string managerLastName = InputValidation.InputValidation.GetManagerName("Second");

                    var manager = new Employee
                    {
                        FirstName = managerFirstName,
                        LastName = managerLastName,
                        InverseReportsToNavigation = new List<Employee>()
                        {
                            new Employee
                            {
                                FirstName = InputValidation.InputValidation.GetEmployeeName("First"),
                                LastName = InputValidation.InputValidation.GetEmployeeName("Second")
                            }
                        }
                    };

                    db.Employees.Add(manager);
                    db.SaveChanges();

                    Console.WriteLine($"\n{"ManagerId",-12}{"ManagerName",-30}{"EmployeeId",-12}{"EmployeeName",-20}");
                    var managers = NorthWindRepo.GetManagers();
                    foreach (var man in managers)
                    {
                        Console.Write($"{man.EmployeeId,-12}{man.FirstName + " " + man.LastName,-30}");
                        foreach (var emp in man.InverseReportsToNavigation)
                        {
                            Console.WriteLine($"{emp.EmployeeId,-12}{emp.LastName + " " + emp.LastName,-30}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to get the new employee and manager and store it in the DB...");
                Console.WriteLine(e.ToString());
            }
        }

        //4. Add a new employee to existing manager
        //Display ManagerId | ManagerName | EmployeeId | EmployeeName
        public void AddNewEmployeeToExistingManager()
        {
            try
            {
                using (var db = new NorthwndContext())
                {
                    int managerId = InputValidation.InputValidation.GetManagerID();

                    var manager = db.Employees.FirstOrDefault(m => m.EmployeeId == managerId);
                    if (manager == null)
                    {
                        Console.WriteLine("Manager not found");
                        return;
                    }

                    string empFirstName = InputValidation.InputValidation.GetEmployeeName("First");
                    string empLastName = InputValidation.InputValidation.GetEmployeeName("Second");

                    var employee = new Employee
                    {
                        FirstName = empFirstName,
                        LastName = empLastName,
                        ReportsTo = manager.EmployeeId
                    };

                    db.Employees.Add(employee);
                    db.SaveChanges();

                    Console.WriteLine($"\n{"ManagerId",-12}{"ManagerName",-20}{"EmployeeId",-12}{"EmployeeName",-20}");
                    Console.WriteLine($"{manager.EmployeeId,-12}{manager.FirstName + " " + manager.LastName,-20}" +
                                      $"{employee.EmployeeId,-12}{employee.FirstName + " " + employee.LastName,-20}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:  unable to add a new employee... Q 4 ");
                Console.WriteLine(e.ToString());
            }
        }

        //5. Update existing emplyee to new manager
        //Get ManagerId and EmployeeId in console
        public void UpdateExistingEmployeetoManager()
        {
            try
            {

                using (var db = new NorthwndContext())
                {
                    int employeeId = InputValidation.InputValidation.GetEmployeeID();

                    var employee = db.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
                    if (employee == null)
                    {
                        Console.WriteLine("Employee not found");
                        return;
                    }
                    int managerId = InputValidation.InputValidation.GetManagerID();

                    var manager = db.Employees.FirstOrDefault(m => m.EmployeeId == managerId);
                    if (manager == null)
                    {
                        Console.WriteLine("Manager not found");
                        return;
                    }

                    employee.ReportsTo = manager.EmployeeId;
                    db.SaveChanges();

                    Console.WriteLine($"\nUpdated Employee:");
                    Console.WriteLine($"{"ManagerId",-12}{"ManagerName",-20}{"EmployeeId",-12}{"EmployeeName",-20}");
                    Console.WriteLine($"{manager.EmployeeId,-12}{manager.FirstName + " " + manager.LastName,-20}" +
                                      $"{employee.EmployeeId,-12}{employee.FirstName + " " + employee.LastName,-20}");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to Add a new manager to an Existing Employee.. Q.5....");
                Console.WriteLine(e.ToString());
            }

        }

        //6. Find customers who placed orders in 1997 but not in 1998. Output: CustomerID | CompanyName
        public void CustomersPlacedOrderInParticularYear()
        {
            try
            {
                int year = InputValidation.InputValidation.GetYear();
                using (var context = new NorthwndContext())
                {
                    var customers = context.Customers
                        .Include(c => c.Orders)
                        .Where(c => !c.Orders.Any(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == 1998))
                        .Where(c => c.Orders.Any(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == year))
                        .ToList();
                    Console.WriteLine(customers.Count);
                    Console.WriteLine($"{"Customer ID",-15}{"Company name",-30}");
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"{customer.CustomerId,-15}{customer.CompanyName,-30}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to get the customers based on the given Year.....");
                Console.WriteLine(e.ToString());
            }
        }

        //7. List customers and their most recent order date.
        //Output: CustomerID | CompanyName | LastOrderDate
        public void CustomersMostRecentOrder()
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var customers = context.Customers
                        .Select(customer =>
                        new
                        {
                            CustomerID = customer.CustomerId,
                            CompanyName = customer.CompanyName,
                            LastOrderDate = customer.Orders
                                            .OrderByDescending(o => o.OrderDate)
                                            .Select(ord => ord.OrderDate)
                                            .FirstOrDefault()
                        })
                        .AsEnumerable()
                        .Select(cus => new
                        {
                            cus.CustomerID,
                            cus.CompanyName,
                            LastOrderDate = cus.LastOrderDate.HasValue ? DateOnly.FromDateTime(cus.LastOrderDate.Value) : (DateOnly?)null
                        })
                        .ToList();

                    Console.WriteLine($"{"Customer ID",-15}{"Company Name",-40}{"Last Order Date",-20}");
                    foreach (var customer in customers)
                    {
                        string lastOrderDate;
                        if (customer.LastOrderDate != null)
                        {
                            lastOrderDate = customer.LastOrderDate.ToString();
                        }
                        else
                        {
                            lastOrderDate = "No Orders";
                        }
                        Console.WriteLine($"{customer.CustomerID,-15}{customer.CompanyName,-40}{lastOrderDate,-20}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to get the customers most recent orders......");
            }
        }

        //8. Show all customers whose total order value exceeds 50000.
        //Output: CustomerID | CompanyName | TotalOrderValue
        public void CustomersWithAboveTotalOrderValue(decimal minimumTotalOrderValue)
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var customers = context.Customers
                        .Select(cus => new
                        {
                            CustomerID = cus.CustomerId,
                            CompanyName = cus.CompanyName,
                            TotalOrderValue = cus.Orders.Sum(or => or.OrderDetails.Sum(od => (od.Quantity * od.UnitPrice) * (Decimal)(1 - od.Discount)))
                        })
                        .Where(cus => cus.TotalOrderValue > minimumTotalOrderValue)
                        .ToList();

                    Console.WriteLine($"{"Customer ID",-15}{"Company Name",-40}{"Total Order Value",-20}");
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"{customer.CustomerID,-15}{customer.CompanyName,-40}{Math.Round(customer.TotalOrderValue, 2),-20}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to get the customers based on the given value...");
            }
        }

        //9. List each category with the average unit price of products.
        //Output: CategoryName | AveragePrice
        public void DisplayCategoriesWithAverageUnitPrice()
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var categories = context.Categories
                        .AsNoTracking()
                        .Select(cate => new
                        {
                            CategoryName = cate.CategoryName,
                            AveragePrice = cate.Products.Average(p => p.UnitPrice)
                        })
                        .ToList();

                    Console.WriteLine($"{"Category name",-30}{"Average Unit price",-15}\n");
                    foreach (var category in categories)
                    {
                        Console.WriteLine($"{category.CategoryName,-30}{Math.Round((Decimal)category.AveragePrice, 3),-15}");
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to get the Categories with Average Unit price.....");
                Console.WriteLine(e.ToString());
            }
        }

        //10. Show products that have never been ordered.
        //Output: ProductID | ProductName
        public void ProductsNeverBeenOrdered()
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var products = context.Products
                        .AsNoTracking()
                        .Where(p => !p.OrderDetails.Any())
                        .ToList();

                    Console.WriteLine($"{"Product ID",-15}{"Product Name",-30}");
                    foreach (var product in products)
                    {
                        Console.WriteLine($"{product.ProductId,-15}{product.ProductName,-30}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to get the products that never been ordered.....");
                Console.WriteLine(e.ToString());
            }
        }

        //11. Find the top 3 most ordered products (by total quantity sold).
        //Output: ProductID | ProductName | TotalQuantitySold
        public void DisplayTopMostOrderedProducts(int noOfTopOrders)
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var products = context.OrderDetails
                        .AsNoTracking()
                        .GroupBy(od => od.Product)
                        .Select(product => new
                        {
                            productID = product.Key.ProductId,
                            productName = product.Key.ProductName,
                            QuantitySold = product.Sum(od => od.Quantity)
                        })
                        .OrderByDescending(p => p.QuantitySold)
                        .Take(noOfTopOrders)
                        .ToList();

                    Console.WriteLine($"{"Product ID",-15}{"Product Name",-30}{"Total Quantity Sold",-20}");
                    foreach (var product in products)
                    {
                        Console.WriteLine($"{product.productID,-15}{product.productName,-30}{product.QuantitySold,-20}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : unable to get the top {noOfTopOrders} from the products....");
                Console.WriteLine(e.ToString());
            }
        }

        //12. List products along with their supplier name and category name.
        //Output: ProductID | ProductName | SupplierName | CategoryName
        public void DisplayProductsWithSupplierAndCategoryName()
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var products = context.Products
                        .AsNoTracking()
                        .Select(prod =>
                        new
                        {
                            ProductID = prod.ProductId,
                            ProductName = prod.ProductName,
                            SupplierName = prod.Supplier.CompanyName,
                            CategoryName = prod.Category.CategoryName
                        })
                        .ToList();
                    Console.WriteLine($"{"ProductID",-15}{"Product Name",-40}{"Supplier Name",-40}{"Category Name",-30}");
                    foreach (var product in products)
                    {
                        Console.WriteLine($"{product.ProductID,-15}{product.ProductName,-40}{product.SupplierName ?? "------",-40}{product.CategoryName,-20}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to get the products with supplier name and category name");
            }
        }

        //13. Display all products where UnitPrice > Category Average Price.
        //Output: ProductID | ProductName | UnitPrice | CategoryAverage
        public void DisplayProductsWhereUnitPriceGreaterThanCategoryAverage()
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var products = context.Products
                        .AsNoTracking()
                        .Select(p => new
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            UnitPrice = p.UnitPrice,
                            CategoryAverage = p.Category.Products.Average(p => p.UnitPrice)
                        })
                        .Where(prd => prd.UnitPrice > prd.CategoryAverage)
                        .ToList();
                    Console.WriteLine("Total Count : " + products.Count());
                    Console.WriteLine($"{"Product ID",-15}{"Product Name",-30}{"Unit price",-20}{"CategoryAverage",-20}");
                    foreach (var product in products)
                    {
                        Console.WriteLine($"{product.ProductId,-15}{product.ProductName,-30}{product.UnitPrice,-20}{product.CategoryAverage,-20}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to get the products from the Db with the price greater than category average price....");
                Console.WriteLine(e.ToString());
            }
        }


        //14. List employees with the total sales amount they handled.
        //Output: EmployeeID | EmployeeName | TotalSales
        public void DisplayEmployeesAndTheirTotalSales()
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var employees = context.Employees
                        .AsNoTracking()
                        .Select(e => new
                        {
                            EmployeeID = e.EmployeeId,
                            EmployeeName = e.FirstName + " " + e.LastName,
                            TotalSales = e.Orders.Sum(ord => (Decimal?)ord.OrderDetails.Sum(od => (od.Quantity * od.UnitPrice) * (Decimal)(1 - od.Discount)) ?? 0)
                        })
                        .ToList();
                    Console.WriteLine("Total Count : " + employees.Count());
                    Console.WriteLine($"{"Employee ID",-15}{"Employee Name",-30}{"Total Sales",-20}");
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"{employee.EmployeeID,-15}{employee.EmployeeName,-30}{employee.TotalSales,-20}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to display employee details along with their total sales...");
                Console.WriteLine(e.ToString());
            }
        }

        //15. Show the employee who handled the most orders in 1997.
        //Output: EmployeeID | EmployeeName | OrdersHandled
        public void EmployeeWhoHandledMostOrders(int year)
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var employee = context.Employees
                        .AsNoTracking()
                        .Select(e => new
                        {
                            EmployeeId = e.EmployeeId,
                            EmployeeName = e.FirstName + " " + e.LastName,
                            OrdersHandled = e.Orders.Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == year).Count()
                        })
                        .OrderByDescending(e => e.OrdersHandled)
                        .First();

                    Console.WriteLine($"{"EmployeeID",-15}{"Employee Name",-30}{"Total Orders Handled",-20}");
                    Console.WriteLine($"{employee.EmployeeId,-15}{employee.EmployeeName,-30}{employee.OrdersHandled,-20}");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : unable to get the employee who handled most of the orders in {year}...");
                Console.WriteLine(e.ToString());
            }
        }

        //16. Find employees who share the same territory.
        //Output: Employee1 | Employee2 | TerritoryName
        public void EmployeesWithSameTerritory()
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var territories = context.Territories
                        .AsNoTracking()
                        .Select(t => new
                        {
                            Employees = t.Employees,
                            TerritoryName = t.TerritoryDescription
                        })
                        .Where(obj => obj.Employees.Count > 1)
                        .ToList();

                    Console.WriteLine($"{"Employee 1",-30}{"Employee 2",-30}{"Territory Name",-20}");
                    foreach (var territory in territories)
                    {
                        foreach (var employee in territory.Employees)
                        {
                            Console.Write($"{employee.EmployeeId,-30}");
                        }
                        Console.Write($"{territory.TerritoryName,-20}");
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : unable to get the employees in the same territory...");
                Console.WriteLine(e.ToString());
            }
        }

        //17. Display each employee with the number of distinct customers they served.
        //Output: EmployeeID | EmployeeName | DistinctCustomers
        public void DisplayEmployeeWithDictinctCustomersCount()
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var employees = context.Employees
                        .AsNoTracking()
                        .Select(e => new
                        {
                            EmployeeID = e.EmployeeId,
                            EmployeeName = e.FirstName + " " + e.LastName,
                            DistinctCustomersCount = e.Orders.Select(o => o.Customer).Distinct().ToList()
                        })
                        .ToList();

                    Console.WriteLine($"{"EmployeeID",-15}{"Employee Name",-30}{"Distinct Customer Count",-20}");
                    foreach (var emp in employees)
                    {
                        Console.WriteLine($"{emp.EmployeeID,-15}{emp.EmployeeName,-30}{emp.DistinctCustomersCount.Count(),-20}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : unable to get the employee with distince no of customers count...");
                Console.WriteLine(e.ToString());
            }
        }

        //18. List employees along with the first order they ever handled.
        //Output: EmployeeID | EmployeeName | FirstOrderDate
        public void DisplayEmployeeAndTheirFirstOrder()
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var employees = context.Employees
                        .AsNoTracking()
                        .Select(e => new
                        {
                            EmployeeID = e.EmployeeId,
                            EmployeeName = e.FirstName + " " + e.LastName,
                            FirstOrderDate = e.Orders.Min(o => o.OrderDate)
                        })
                        .ToList();
                    Console.WriteLine($"{"EmployeeID",-15}{"Employee Name",-30}{"First Order Date",-20}");
                    foreach (var emp in employees)
                    {
                        string firstOrderDate;
                        if (emp.FirstOrderDate != null)
                        {
                            firstOrderDate = DateOnly.FromDateTime(emp.FirstOrderDate.Value).ToString();
                        }
                        else
                        {
                            firstOrderDate = "No orders handled...";
                        }
                        Console.WriteLine($"{emp.EmployeeID,-15}{emp.EmployeeName,-30}{firstOrderDate,-20}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : unable to get the employee and their first order...");
                Console.WriteLine(e.ToString());
            }

        }

        //19. For each shipper, calculate the average delivery time(ShippedDate – OrderDate).
        //Output: ShipperName | AvgDeliveryDays
        public void DisplayShipperDeliverTime()
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var shippers = context.Shippers
                        .AsNoTracking()
                        .Select(s => new
                        {
                            ShipperName = s.CompanyName,
                            AverageDeliveryDay = s.Orders.Select(o => EF.Functions.DateDiffDay(o.OrderDate, o.ShippedDate)).Average()
                        })
                        .ToList();

                    Console.WriteLine($"{"Shipper Name",-40}{"AverageDeliveryDays",-40}");
                    foreach (var shipper in shippers)
                    {
                        double days;
                        if (shipper.AverageDeliveryDay != null)
                        {
                            days = shipper.AverageDeliveryDay.Value;
                        }
                        else
                        {
                            days = 0;
                        }
                        Console.WriteLine($"{shipper.ShipperName,-40}{days,-40}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : unable to get the shippers Delivery time...");
                Console.WriteLine(e.ToString());
            }
        }

        //20. List orders that took more than 30 days to deliver.
        //Output: OrderID | CustomerName | DaysTaken
        public void DisplayOrdersThatDeliverdAfterGivenDays(int days)
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var orders = context.Orders
                        .AsNoTracking()
                        .Select(o => new
                        {
                            OrderID = o.OrderId,
                            CustomerName = o.Customer.CompanyName,
                            DaysTaken = EF.Functions.DateDiffDay(o.OrderDate, o.ShippedDate)
                        })
                        .Where(obj => obj.DaysTaken > 30)
                        .ToList();
                    Console.WriteLine("Toatal Count : " + orders.Count);
                    Console.WriteLine($"{"OrderID",-15}{"Customer Name",-40}{"Days Taken",-20}");
                    foreach (var order in orders)
                    {
                        Console.WriteLine($"{order.OrderID,-15}{order.CustomerName,-40}{order.DaysTaken,-20}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : unable to get the order that take {days} days to deliver...");
                Console.WriteLine(e.ToString());
            }
        }


        //21. Find the top shipper based on the number of orders shipped.
        //Output: ShipperName | OrdersShipped
        public void DisplayTopShipper()
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var shipper = context.Shippers
                        .AsNoTracking()
                        .Select(s => new
                        {
                            ShipperName = s.CompanyName,
                            OrdersShipped = s.Orders.Where(o => o.ShippedDate != null).Count()
                        })
                        .OrderByDescending(obj => obj.OrdersShipped)
                        .FirstOrDefault();

                    Console.WriteLine($"{"Shipper Name",-20}{"Orders Shipped",-40}");
                    Console.WriteLine($"{shipper.ShipperName,-20}{shipper.OrdersShipped,-40}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : unable to get the top shipper based on his shipping...");
                Console.WriteLine(e.ToString());
            }
        }

        //22. Show the top employee per year based on total sales.
        //Output: Year | EmployeeName | TotalSales
        public void TopEmployeeBasedOnSalesInEachYear()
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var employees = context.Orders
                        .Where(o => o.OrderDate.HasValue)
                        .GroupBy(o => new { o.EmployeeId, o.OrderDate.Value.Year })
                        .Select(obj =>
                        new
                        {
                            Year = obj.Key.Year,
                            EmployeeName = obj.Select(e => e.Employee.FirstName + " " + e.Employee.LastName).FirstOrDefault(),
                            TotalSales = obj.Sum(o => o.OrderDetails.Sum(od => (od.Quantity * od.UnitPrice) * (Decimal)(1 - od.Discount)))
                        })
                        .GroupBy(emp => emp.Year)
                        .Select(obj => obj
                            .OrderByDescending(o => o.TotalSales)
                            .FirstOrDefault())
                            .ToList();
                    Console.WriteLine($"{"Year",-20}{"Employee Name",-40}{"TotalSales",-20}");
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"{employee.Year,-20}{employee.EmployeeName,-40}{employee.TotalSales,-20}");
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : unable to get the top employee based on the sales...");
                Console.WriteLine(e.ToString());
            }
        }

        //23. Find all products that were ordered by every customer.
        //Output: ProductID | ProductName
        public void DisplayProductsOrderedByEveryCustomers()
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var customerCount = context.Customers.Count();
                    var products = context.Products
                        .Select(p => new
                        {
                            ProductID = p.ProductId,
                            ProductName = p.ProductName,
                            Counts = p.OrderDetails.Select(oi => oi.Order.CustomerId).Distinct().Count()
                        })
                        .Where(obj => obj.Counts == customerCount)
                        .ToList();

                    Console.WriteLine($"{"ProductID",-15}{"ProductName",-30}");
                    foreach (var product in products)
                    {
                        Console.WriteLine($"{product.ProductID,-15}{product.ProductName,-30}");
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : unable to get the products which is ordered by every customer...");
                Console.WriteLine(e.ToString());
            }
        }

        //24. Find suppliers who supply more than 5 products.
        //Output: SupplierID | SupplierName | ProductCount
        public void DisplaySuppliersWhoSupplyMoreThanNoOfProduct(int minimumProductsCount)
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var Suppliers = context.Suppliers
                        .AsNoTracking()
                        .Select(s => new
                        {
                            SupplierID = s.SupplierId,
                            SupplierName = s.CompanyName,
                            ProductCount = s.Products.Count()
                        })
                        .Where(p => p.ProductCount > minimumProductsCount)
                        .ToList();

                    Console.WriteLine($"{"SupplierID",-15}{"SupplierName",-40}{"ProductCount"}");
                    foreach (var supplier in Suppliers)
                    {
                        Console.WriteLine($"{supplier.SupplierID,-15}{supplier.SupplierName,-40}{supplier.ProductCount}");
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : unable to get the Shippers details for the Question no 24 ...");
                Console.WriteLine(e.ToString());
            }
        }

        //25. List the customer(s) with the single highest order value.
        //Output: CustomerName | OrderID | OrderValue
        public void CustomersWithSingleHighestOrderValue()
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var customers = context.Customers
                        .AsNoTracking()
                        .Select(c => new
                        {
                            CustomerName = c.CompanyName,
                            Order = c.Orders
                                    .Where(o => o.OrderId != null)
                                    .Select(o => new
                                    {
                                        OrderID = o.OrderId,
                                        OrderValue = o.OrderDetails.Sum(od => (od.Quantity * od.UnitPrice) * (Decimal)(1 - od.Discount))
                                    })
                                    .OrderByDescending(o => o.OrderValue)
                                    .FirstOrDefault() ?? null
                        })
                        .ToList();
                    Console.WriteLine("Total Count " + customers.Count());
                    Console.WriteLine($"{"CustomerName",-40}{"OrderID",-15}{"Order Value",-20}");
                    foreach (var customer in customers)
                    {
                        if (customer.Order != null)
                        {
                            Console.WriteLine($"{customer.CustomerName,-40}{customer.Order.OrderID,-15}{customer.Order.OrderValue}");

                        }
                        else
                        {
                            Console.WriteLine($"{customer.CustomerName,-40}{"No orders...",-15}{"No order Value...."}");

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : unable to get the Customers highest order value ...");
                Console.WriteLine(e.ToString());
            }
        }

        //26. List customers who have ordered all products in a given category (e.g., Beverages).
        //Output: CustomerName | CategoryName
        public void CustomersWhoOrderedAllProductsInGivenCategory(int CategoryID)
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var productCount = context.Products.Count(p => p.CategoryId == CategoryID);

                    var customers = context.OrderDetails
                                    .AsNoTracking()
                                    .Where(od => od.Product.CategoryId == CategoryID)
                                    .GroupBy(od => od.Order.CustomerId)
                                    .Select(g => new
                                    {
                                        CustomerName = g.Select(od => od.Order.Customer.CompanyName).FirstOrDefault(),
                                        ProductIdsCount = g.Select(od => od.ProductId).Distinct().Count()
                                    })
                                    .Where(c => c.ProductIdsCount == productCount)
                                    .ToList();
                    Console.WriteLine("*******************************");
                    Console.WriteLine("|        Customer Name        |");
                    Console.WriteLine("*******************************");
                    foreach (var customer in customers)
                    {
                        Console.WriteLine(customer.CustomerName);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : unable to get the customer who ordered all the products in one category ...");
                Console.WriteLine(e.ToString());
            }
        }

        //27.Show the most profitable product (highest total sales revenue).
        //Output: ProductID | ProductName | TotalRevenue
        public void DisplayMostProfitableProduct()
        {
            try
            {
                using (var context = new NorthwndContext())
                {
                    var product = context.Products
                        .AsNoTracking()
                        .Select(p => new
                        {
                            ProductID = p.ProductId,
                            ProductName = p.ProductName,
                            TotalRevenue = p.OrderDetails.Sum(od => (od.Quantity * od.UnitPrice) * (Decimal)(1 - od.Discount))
                        })
                        .OrderByDescending(obj => obj.TotalRevenue)
                        .First();

                    Console.WriteLine($"Product ID : {product.ProductID}\nProduct Name : {product.ProductName}\n" +
                        $"Total Revenue : {product.TotalRevenue}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : unable to get the most profitable product ...");
                Console.WriteLine(e.ToString());
            }
        }
    }
}
