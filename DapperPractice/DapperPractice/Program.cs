using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Z.Dapper.Plus;
namespace DapperPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DapperGetPractice();
            DapperBulkInsert();
        }

         static void DapperGetPractice()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudentDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "select * from Customers where CustomerID = @customerID";
                //QuerySingle when return a one row
                var value = connection.QuerySingle(sql, new { customerID = 1 });
                Console.WriteLine(value.FirstName + "  " + value.LastName);


                string noOfRowsSql = "select count(*) from Customers;";
                var count = connection.ExecuteScalar(noOfRowsSql);
                Console.WriteLine("No of customers  =  " + count);


                //Query multiple
                string multipleQuery = @"select * from Customers;
                                select * from Products";
                Dapper.SqlMapper.GridReader reader = connection.QueryMultiple(multipleQuery);

                var customers = reader.Read();
                var products = reader.Read();
                foreach(var customer in customers)
                {
                    Console.WriteLine(customer.FirstName + "  " + customer.LastName);   
                }
                foreach(var product in products)
                {
                    Console.WriteLine(product.ProductName + " " + product.Category);
                }


                Console.WriteLine("\n\n Calling stored procedure \n\n");
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", 1);
                var customer2 = connection.QuerySingleOrDefault("GetCustomerById", parameters, commandType: CommandType.StoredProcedure);
                Console.WriteLine(customer2.FirstName + " " + customer2.LastName);
            }
        }


        static void DapperBulkInsert()
        {
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StudentDB"].ConnectionString))
            {
                connection.BulkInsert(new[] { new Customer{CustomerID = 10,FirstName = "asdf",LastName = "asdf",Email = "booasdflkafs",PhoneNumber = "1234567890",Address="asdflj"},
                new Customer{CustomerID = 12,FirstName = "asdf",LastName = "asdf",Email = "booasasddflkafs",PhoneNumber = "123asa4567890",Address="asdflj"},
                new Customer{CustomerID = 13,FirstName = "asdf",LastName = "asdf",Email = "boasoasdflkafs",PhoneNumber = "1234as567890",Address="asdflj"},
                new Customer{CustomerID = 14,FirstName = "asdf",LastName = "asdf",Email = "booasdflkasdafs",PhoneNumber = "123as4567890",Address="asdflj"}});
            }

        }
    }
    public class Customer
    {
        public int CustomerID { get; set; }  // If identity, don't set value
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
