﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory_App.Models
{
    public class PurchaseCartSummaryMV
    {
        public double SubTotal { get; set; }
        public double ShippingFee { get; set; }

        public double OrderTotal { get; set; }

        public double EstimatedTax { get; set; }

    }
}