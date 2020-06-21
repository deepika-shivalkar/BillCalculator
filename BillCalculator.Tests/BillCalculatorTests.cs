using System;
using System.Collections.Generic;
using Xunit;

namespace BillCalculator.Tests
{
    public class BillCalculatorTests
    {
        [Fact]
        public void ShouldAddSingleItemsToCart()
        {
            ShoppingCart c = new ShoppingCart();
            Item A = new Item("A", 50.00);
            c.AddItem(A);
            Dictionary<Item, int> cartDetails = c.GetCartDetails();
            Assert.NotNull(cartDetails);
            Assert.Equal(1, cartDetails[A]);
        }

        [Fact]
        public void ShouldAddMultipleItemsToCart()
        {
            ShoppingCart c = new ShoppingCart();
            c.AddItem(new Item("A", 50.00), 3);
            Dictionary<Item, int> cartDetails = c.GetCartDetails();
            Assert.NotNull(cartDetails);
            Assert.Equal(3, cartDetails[new Item("A", 50.00)]);
        }

    }
}
