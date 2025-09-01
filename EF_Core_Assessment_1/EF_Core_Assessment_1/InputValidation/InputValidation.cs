using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EF_Core_Assessment_1.Constants;

namespace EF_Core_Assessment_1.InputValidation
{
    public class InputValidation
    {

        public static int GetOption()
        {
            try
            {
                Console.Write($"Enter your option (1-{Constants.Constant.NoOfSwitchOptions}) : ");
                string inputValue = Console.ReadLine();
                int option;
                while (!(int.TryParse(inputValue, out option) && option > 0 && option <=Constants.Constant.NoOfSwitchOptions))
                {
                    Console.Write($"Enter a valid option (1-{Constants.Constant.NoOfSwitchOptions}) : ");
                    inputValue = Console.ReadLine();    
                }
                return option;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR : unable to get a option from ther user for a swith case....");
                return -1;
            }

        }
        public static string GetName()
        {
            try
            {
                Console.Write("Enter your name : ");
                string username = Console.ReadLine();
                while (!(username.Length > 2 && Regex.IsMatch(username, Constants.Constant.NameValidationPattern)))
                {
                    Console.Write("Enter a valid name : ");
                    username = Console.ReadLine();
                }
                return username;
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR : Unable to get the username form the user....");
                throw;
            }
            
        }


        public static string GetCustomerID()
        {
            try
            {
                Console.Write("Enter a Customer ID (all character must be upper case) :");
                string customerID = Console.ReadLine();
                while (!(Regex.IsMatch(customerID,"^[A-Z]{3,5}$")))
                {
                    Console.Write("Enter a valid customer ID (all character must be upper case) :");
                    customerID = Console.ReadLine();
                }
                return customerID;
            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to get a customer id ....");
                throw;
            }
        }
        public static string GetString(string value)
        {
            try
            {
                Console.Write($"Enter a {value} : ");
                string inputString = Console.ReadLine();
                while(!(Regex.IsMatch(inputString, Constants.Constant.NameValidationPattern)))
                {
                    Console.Write($"Enter a Valid {value} (only characters) : ");
                    inputString = Console.ReadLine();
                }
                return inputString;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to get the string for category from input validation....");
                throw;
            }
        }

        public static int GetEmployeeID()
        {
            try
            {
                Console.Write("Enter the Employee ID :");
                string inputValue = Console.ReadLine();
                int employeeID;
                while (!(int.TryParse(inputValue, out employeeID) && employeeID > 0))
                {
                    Console.WriteLine("Enter a valid ID :");
                    inputValue = Console.ReadLine();
                }
                return employeeID;
            }
            catch(Exception e)
            {
                Console.WriteLine("unable to get the proper employee id..");
                throw;
            }
           
        }

        public static string GetAddrss(string forWhat)
        {
            try
            {
                Console.Write($"Enter {forWhat} Address : ");
                string employeeAddress = Console.ReadLine();
                while(!(Regex.IsMatch(employeeAddress,Constants.Constant.AddressValidationPattern)))
               {
                    Console.Write($"Enter a valid {forWhat} address : ");
                    employeeAddress = Console.ReadLine();
                }
                return employeeAddress;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error : unable to get the {forWhat} address.. ");
                throw;
            }
        } 
    }
}
