using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirstApproach.Constants
{
    internal class Constant
    {
        public static int NO_OF_OPTIONS = 6;
        public static string NAME_VALIDATION_PATTERN = "^[a-zA-Z .]{2,}$";
        public static string PROJECT_NAME_VALIDATION_PATTERN = "^[a-zA-Z0-9 ]{5,50}$";
        public static string EMAIL_VALIDATION_PATTERN = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$\r\n";
        public static string PHONENUMBER_VALIDATION_PATTERN = "^([\\-\\s]?)?[6-9]\\d{9}$\r\n";
    }
}
