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
            ShoppingCart c = new ShoppingCart();
            c.AddItem(B, 5);
            Dictionary<Item, int> cartDetails = c.GetCartDetails();
            List<IPromotion> promoList = new List<IPromotion>();
            promoList.Add(twoBfor45);
            Assert.NotNull(cartDetails);
            Assert.Equal(120, c.GetCartTotalWithPromotion(promoList));
        }

        [Fact]
        public void ShouldExecuteSingleCumulativeAbsolutePromotionOnShoppingCard()
        {
            Item C = new Item("C", 20.00);
            Item D = new Item("D", 15.00);
            List<Item> itemList = new List<Item>();
            itemList.Add(C);
            itemList.Add(D);
            IPromotion cAndDFor30 = new CumulativeAbsolutePromotion(itemList, 30.00);
            ShoppingCart c = new ShoppingCart();
            c.AddItem(C, 2);
            c.AddItem(D);
            Dictionary<Item, int> cartDetails = c.GetCartDetails();
            List<IPromotion> promoList = new List<IPromotion>();
            promoList.Add(cAndDFor30);
            Assert.NotNull(cartDetails);
            Assert.Equal(50, c.GetCartTotalWithPromotion(promoList));
        }

    }
}
