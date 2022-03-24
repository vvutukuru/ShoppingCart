using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Entities
{
    public class Cart
    {
        const decimal SALES_TAX_RATE = 12.5m;
        public int CartId { get; set; }
        public int UserId { get; set; }
        private readonly List<CartItem> _items = new List<CartItem>();
        public IReadOnlyCollection<CartItem> CartItems => _items.AsReadOnly();

        public void AddItem(string productCode, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity cannot be 0 or less");
            }
            if (string.IsNullOrEmpty(productCode))
            {
                throw new ArgumentException("Invalid Product Code");
            }
            if (!CartItems.Any(t => t.ProductCode.Equals(productCode, StringComparison.CurrentCultureIgnoreCase)))
            {
                _items.Add(new CartItem
                {
                    ProductCode = productCode,
                    Quantity = quantity,
                    UnitPrice = unitPrice
                });
                return;
            }
            var existingItem = CartItems.FirstOrDefault(t => t.ProductCode.Equals(productCode, StringComparison.CurrentCultureIgnoreCase));

            existingItem.Quantity += quantity;

        }

        public decimal TotalSalesTax()
        {
            return Math.Round(ItemsTotalPrice() * (SALES_TAX_RATE) / 100, 2);
        }

        public decimal TotalPriceAfterTax()
        {
            return Math.Round(ItemsTotalPrice() * (SALES_TAX_RATE + 100) / 100, 2);
        }
        public decimal TotalPriceBeforeTax()
        {
            return ItemsTotalPrice();
        }
        private decimal ItemsTotalPrice()
        {
            return Math.Round(CartItems.Sum(t => t.UnitPrice * t.Quantity), 2);
        }

    }
}
