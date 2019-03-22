using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Module2.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int ProductId { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Product name lenght must be in range 3 and 25")]
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }

        [Required]
        [Range(0.01, 99.99, ErrorMessage = "Unit prive must be in range 0.01 and 99.99")]
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public Categories Category { get; set; }
        public Suppliers Supplier { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
