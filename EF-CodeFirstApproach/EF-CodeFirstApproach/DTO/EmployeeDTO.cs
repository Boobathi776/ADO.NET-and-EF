using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirstApproach.DTO
{
    public class EmployeeDetails
    {
        public int? Id { get; set; }
        public string Name { get; set; }    
        public string Email{ get; set; }
        public string Phone { get; set; }   
        public int? DepartmentID { get; set; }
        public int? ProjectID { get; set; }

    }
}
