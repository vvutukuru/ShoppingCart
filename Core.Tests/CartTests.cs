using Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Test
{
    [TestClass]
    public class CartTests
    {

        [TestMethod]
        public void CartShouldBeEmptyWhenInitialized(){
            var cart = new Cart();
            Assert.AreEqual(0, cart.CartItems.Count());
        }

        [TestMethod]
        public void AddingItemWithZeroQuantityShouldThrowArgumentException(){
            var cart = new Cart();
            var productCode = "_DoveSoap_";
            var quantity = 0;
            var unitPrice = 39.99m;
            Assert.ThrowsException<ArgumentException>(() => cart.AddItem(productCode, quantity, unitPrice), "Quantity cannot be 0 or less");
        }
        [TestMethod]
        public void AddingItemWithNullProductCodeShouldThrowArgumentException()
        {
            var cart = new Cart();            
            Assert.ThrowsException<ArgumentException>(() => cart.AddItem(null, 2, 39.99m), "Invalid Product Code");
        }
        [TestMethod]
        public void AddingItemWithEmptyProductCodeShouldThrowArgumentException()
        {
            var cart = new Cart();           
            Assert.ThrowsException<ArgumentException>(() => cart.AddItem("", 2, 39.99m), "Invalid Product Code");
        }
        [TestMethod]
        public void AddingItemWithLessthanZeroQuantityShouldThrowArgumentException()
        {
            var cart = new Cart();
            var productCode = "_DoveSoap_";
            var quantity = -10;
            var unitPrice = 39.99m;
            Assert.ThrowsException<ArgumentException>(() => cart.AddItem(productCode, quantity, unitPrice));
        }
        [TestMethod]
        public void AddingSingleItemToEmptyCartShouldAddSingleItemWithSpecifiedQuantity()
        {
            var cart = new Cart();
            var productCode = "_DoveSoap_";
            var quantity = 2;
            var unitPrice = 39.99m;
            cart.AddItem(productCode, quantity, unitPrice);           
            Assert.IsTrue(cart.CartItems.SequenceEqual(new List<CartItem> { new CartItem { ProductCode = productCode, Quantity = quantity, UnitPrice = unitPrice } }));

        }

        [TestMethod]
        public void AddingSameItemMultipleTimesShouldShouldUpdateTheExistingItemQuantity()
        {
            var cart = new Cart();
            var productCode = "_DoveSoap_";           
            var unitPrice = 39.99m;
            cart.AddItem(productCode, 5, unitPrice);
            cart.AddItem(productCode, 3, unitPrice);
            Assert.IsTrue(cart.CartItems.SequenceEqual(new List<CartItem> { new CartItem { ProductCode = productCode, Quantity = 8, UnitPrice = unitPrice } }));
           
        }

        [TestMethod]
        public void AddingDifferentItemsShouldShouldShouldAddOneLineItemPerProducts()
        {
            var cart = new Cart();
            var soap = "_DoveSoap_";
            var deo = "_AxeDeo_";
            var soapPrice = 39.99m;
            var deoPrice = 99.99m;
            cart.AddItem(soap, 5, soapPrice);
            cart.AddItem(deo, 3, deoPrice);
            Assert.IsTrue(cart.CartItems.SequenceEqual(new List<CartItem> { new CartItem { ProductCode = soap, Quantity = 5, UnitPrice = soapPrice } , new CartItem { ProductCode = deo, Quantity = 3, UnitPrice = deoPrice } }));

        }

        [TestMethod]
        public void EmptyCartTotalSalesTaxShoudBeZero()
        {
            var cart = new Cart();          
            Assert.AreEqual(0m, cart.TotalSalesTax());
        }
        [TestMethod]
        public void EmptyCartTotalPriceShoudBeZero()
        {
            var cart = new Cart();
            Assert.AreEqual(0m, cart.TotalPriceAfterTax());
        }

        [TestMethod]
        public void ItemsTotalShouldCalculateCartTotalBeforeTax()
        {
            var cart = new Cart();
            var soap = "_DoveSoap_";
            var deo = "_AxeDeo_";
            var soapPrice = 39.99m;
            var deoPrice = 99.99m;
            cart.AddItem(soap, 2, soapPrice);
            cart.AddItem(deo, 2, deoPrice);
            Assert.AreEqual(279.96m, cart.TotalPriceBeforeTax());
        }

        [TestMethod]
        public void TotalSalesTaxShouldCalculateTotalTaxOfTheCart()
        {
            var cart = new Cart();
            var soap = "_DoveSoap_";
            var deo = "_AxeDeo_";
            var soapPrice = 39.99m;
            var deoPrice = 99.99m;
            cart.AddItem(soap, 2, soapPrice);
            cart.AddItem(deo, 2, deoPrice);
            Assert.AreEqual( 35.00m, cart.TotalSalesTax()); 
        }

        [TestMethod]
        public void TotalShouldCalculateCartTotal()
        {
            var cart = new Cart();
            var soap = "_DoveSoap_";
            var deo = "_AxeDeo_";
            var soapPrice = 39.99m;
            var deoPrice = 99.99m;
            cart.AddItem(soap, 2, soapPrice);
            cart.AddItem(deo, 2, deoPrice);
            Assert.AreEqual(314.96m, cart.TotalPriceAfterTax());
        }
    }
}
