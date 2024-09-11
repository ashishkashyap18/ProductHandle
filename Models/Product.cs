﻿using System;
using System.Collections.Generic;

namespace ProductHandle.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public int StockQuantity { get; set; }

    public string? SupplierName { get; set; }

    public decimal? Weight { get; set; }
}
