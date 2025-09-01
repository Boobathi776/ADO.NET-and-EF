using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreAdventureWorkPractice.DTO
{
    //[NotMapped]
    public class EmployeeManagersDto
    {
        public int RecursionLevel { get;set; }
        public int BusinessEntityID {get;set; }
        public string FirstName {get;set; }
        public string LastName {get;set; } 
        public string OrganizationNode {get;set; }
        public string ManagerFirstName {get;set; }
        public string ManagerLastName {get;set; }

    }
}
