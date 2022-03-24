using Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Core.Tests
{
    [TestClass]
    public class Class1
    {
        [TestMethod]
        public void AddSingleItemToEmptyCartShouldAddSingleItem()
        {
            var cart = new Cart();
            var productCode = "_DoveSoap_";
            var quantity = 1;
            var unitPrice = 39.99;
            cart.AddItem(productCode, quantity, unitPrice);
            Assert.AreEqual(cart.CartItems.Count(), 1);
        }
    }
}
