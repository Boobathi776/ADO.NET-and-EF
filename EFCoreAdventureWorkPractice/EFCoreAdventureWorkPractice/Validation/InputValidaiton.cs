using EFCoreAdventureWorkPractice.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreAdventureWorkPractice.Validation
{
    internal class InputValidaiton
    {
        public static int GetSwitchOption()
        {
            try
            {
                Console.Write($"Enter your option (1-{Constants.NoOfCaseOptions}) : ");
                string inputValue = Console.ReadLine();
                int option;
                while(!(int.TryParse(inputValue,out option) && option > 0  && option <= Constants.NoOfCaseOptions))
                {
                    Console.Write($"Enter a valid option (1-{Constants.NoOfCaseOptions})");
                    inputValue = Console.ReadLine();
                }
                return option;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error :  unable to get the option for switch case .....");
                throw;
            }
        }
    }
    }
