using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreNorthWindDb.Constants
{
    public class Constant
    {
        public static readonly int NoOfOptions = 28 ;
        public static readonly string CustomerIDValidationPattern = "^[A-Z]{3,5}$";
        public static readonly string YesOrNoPattern = "^[ynYN]{1}$";
        public static readonly string NameValidationPattern = "^[a-zA-Z- .0-9]{3,50}$";
    }
}
