using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreAdventureWorkPractice.DTO
{
    internal class EmployeeDto
    {
        public string EmployeeName { get; set; }
        public DateOnly HiredDate { get; set; }
        public string Gender { get; set; }
    }
}
