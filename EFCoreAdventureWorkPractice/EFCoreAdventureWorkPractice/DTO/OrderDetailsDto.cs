using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreAdventureWorkPractice.DTO
{
    internal class OrderDetailsDto
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalDue { get; set; }
        public DateTime OrderDate {  get; set; }

    }
}
