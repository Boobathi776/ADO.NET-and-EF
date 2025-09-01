using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirstApproach.Model
{
    public class EmployeeProject
    {
        public int EmployeeID { get; set; }
        public int ProjectID { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Project Project  { get; set; }
    }
}
