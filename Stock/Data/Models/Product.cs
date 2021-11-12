using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductStock { get; set; }
        public bool ProductStatus { get; set; }
        public decimal ProductPrice { get; set; }
        public string Details { get; set; }
    }
}
