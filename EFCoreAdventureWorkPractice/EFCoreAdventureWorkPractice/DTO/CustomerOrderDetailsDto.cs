using EFCoreAdventureWorkPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreAdventureWorkPractice.DTO
{
    internal class CustomerOrderDetailsDto
    {
        public int OrderID {  get; set; }
        public decimal TotalDue { get; set; }
        public ICollection<Product> products { get; set; }

    }
}
