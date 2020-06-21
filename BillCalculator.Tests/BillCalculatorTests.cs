namespace BillCalculator.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class BillCalculatorTests
    {
        [Fact]
        public void ShouldAddSingleItemsToCart()
        {
            ShoppingCart c = new ShoppingCart();
            Item a = new Item("A", 50.00);
            c.AddItem(a);
            Dictionary<Item, int> cartDetails = c.GetCartDetails();
            Assert.NotNull(cartDetails);
            Assert.Equal(1, cartDetails[a]);
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

        [Fact]
        public void ShouldCalculateShoppingCartTotalWithoutOffer()
        {
            ShoppingCart c = new ShoppingCart();
            c.AddItem(new Item("A", 50.00), 3);
            c.AddItem(new Item("B", 30.00), 5);
            c.AddItem(new Item("C", 20.00), 2);
            c.AddItem(new Item("D", 15.00), 1);
            Dictionary<Item, int> cartDetails = c.GetCartDetails();
            Assert.NotNull(cartDetails);
            Assert.Equal(355, c.GetCartTotal());
        }

        [Fact]
        public void ShouldCountItemsInShoppingCart()
        {
            ShoppingCart c = new ShoppingCart();
            c.AddItem(new Item("A", 50.00), 3);
            c.AddItem(new Item("B", 30.00), 5);
            c.AddItem(new Item("C", 20.00), 2);
            c.AddItem(new Item("D", 15.00), 1);
            Dictionary<Item, int> cartDetails = c.GetCartDetails();
            Assert.NotNull(cartDetails);
            Assert.Equal(11, c.GetItemsCount());
        }

        [Fact]
        public void ShouldExecuteSingleStandAloneAbsolutePromotionOnShoppingCart()
        {
            Item B = new Item("B", 30.00);
            IPromotion twoBfor45 = new StandAloneAbsolutePromotion(B, 2, 45.00);
           
        }

    }
}
