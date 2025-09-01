using System;
using System.Collections.Generic;

namespace EF_Core_Assessment_1.Models;

public partial class CurrentProductList
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;
}
