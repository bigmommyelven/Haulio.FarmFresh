﻿using System.Collections.Generic;

namespace Haulio.FarmFresh.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string SupplierName { get; set; }
        public List<Product> Products { get; set; }
    }
}
