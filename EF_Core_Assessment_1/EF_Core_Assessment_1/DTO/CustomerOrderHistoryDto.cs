using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Assessment_1.DTO
{
    public class CustomerOrderHistoryDto
    {
        public string ProductName { get; set; }
        public int Total { get; set; }
    }
}
