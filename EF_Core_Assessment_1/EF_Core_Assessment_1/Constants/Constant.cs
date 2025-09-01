using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Assessment_1.Constants
{
    public class Constant
    {
        public static int NoOfSwitchOptions = 9;
        public static string NameValidationPattern = "^[a-zA-Z .]{2,}$";
        public static string AddressValidationPattern = "^[a-zA-Z0-9-/, .]{3,}$";
    }
}
