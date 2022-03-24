using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class CartItem
    {       
        public string ProductCode { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CartItem item &&
                   ProductCode == item.ProductCode &&
                   UnitPrice == item.UnitPrice &&
                   Quantity == item.Quantity;
        }
    }
}
