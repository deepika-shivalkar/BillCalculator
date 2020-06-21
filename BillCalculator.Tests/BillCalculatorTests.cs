namespace BillCalculator.Tests
{
    using BillCalculator.Promotion;
    using BillCalculator.ShoppingCart;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class BillCalculatorTests
    {
        [Fact]
        public void ShouldAddSingleItemsToCart()
        {
            ShoppingCart sc = new ShoppingCart();
            Item a = new Item("A", 50.00);
            sc.AddItem(a);
            Dictionary<Item, int> cartDetails = sc.GetCartDetails();
            Assert.NotNull(cartDetails);
            Assert.Equal(1, cartDetails[a]);
        }

        [Fact]
        public void ShouldAddMultipleItemsToCart()
        {
            ShoppingCart sc = new ShoppingCart();
            sc.AddItem(new Item("A", 50.00), 3);
            Dictionary<Item, int> cartDetails = sc.GetCartDetails();
            Assert.NotNull(cartDetails);
            Assert.Equal(3, cartDetails[new Item("A", 50.00)]);
        }

        [Fact]
        public void ShouldCalculateShoppingCartTotalWithoutOffer()
        {
            ShoppingCart sc = new ShoppingCart();
            sc.AddItem(new Item("A", 50.00), 3);
            sc.AddItem(new Item("B", 30.00), 5);
            sc.AddItem(new Item("C", 20.00), 2);
            sc.AddItem(new Item("D", 15.00), 1);
            Dictionary<Item, int> cartDetails = sc.GetCartDetails();
            Assert.NotNull(cartDetails);
            Assert.Equal(355, sc.GetCartTotal());
        }

        [Fact]
        public void ShouldCountItemsInShoppingCart()
        {
            ShoppingCart sc = new ShoppingCart();
            sc.AddItem(new Item("A", 50.00), 3);
            sc.AddItem(new Item("B", 30.00), 5);
            sc.AddItem(new Item("C", 20.00), 2);
            sc.AddItem(new Item("D", 15.00), 1);
            Dictionary<Item, int> cartDetails = sc.GetCartDetails();
            Assert.NotNull(cartDetails);
            Assert.Equal(11, sc.GetItemsCount());
        }

        [Fact]
        public void ShouldExecuteSingleStandAloneAbsolutePromotionOnShoppingCart()
        {
            Item b = new Item("B", 30.00);
            IPromotion twoBfor45 = new StandAloneAbsolutePromotion(b, 2, 45.00);
            ShoppingCart sc = new ShoppingCart();
            sc.AddItem(b, 5);
            Dictionary<Item, int> cartDetails = sc.GetCartDetails();
            List<IPromotion> promoList = new List<IPromotion>();
            promoList.Add(twoBfor45);
            Assert.NotNull(cartDetails);
            Assert.Equal(120, sc.GetCartTotalWithPromotion(promoList));
        }

        [Fact]
        public void ShouldExecuteSingleCumulativeAbsolutePromotionOnShoppingCard()
        {
            Item c = new Item("C", 20.00);
            Item d = new Item("D", 15.00);
            List<Item> itemList = new List<Item>();
            itemList.Add(c);
            itemList.Add(d);
            IPromotion cAndDFor30 = new CumulativeAbsolutePromotion(itemList, 30.00);
            ShoppingCart sc = new ShoppingCart();
            sc.AddItem(c, 2);
            sc.AddItem(d);
            Dictionary<Item, int> cartDetails = sc.GetCartDetails();
            List<IPromotion> promoList = new List<IPromotion>();
            promoList.Add(cAndDFor30);
            Assert.NotNull(cartDetails);
            Assert.Equal(50, sc.GetCartTotalWithPromotion(promoList));
        }


        [Fact]
        public void ShouldExecuteScenarioA()
        {
            ShoppingCart sc = new ShoppingCart();
            Item a = new Item("A", 50.00);
            Item b = new Item("B", 30.00);
            Item c = new Item("C", 20.00);
            Item d = new Item("D", 15.00);
            sc.AddItem(a);
            sc.AddItem(b);
            sc.AddItem(c);
            List<IPromotion> promoList = this.GetAvailablePromotions(a, b, c, d);
            Dictionary<Item, int> cartDetails = sc.GetCartDetails();

            Assert.NotNull(cartDetails);
            Assert.Equal(100, sc.GetCartTotalWithPromotion(promoList));
        }

        [Fact]
        public void ShouldExecuteScenarioB()
        {
            ShoppingCart sc = new ShoppingCart();
            Item a = new Item("A", 50.00);
            Item b = new Item("B", 30.00);
            Item c = new Item("C", 20.00);
            Item d = new Item("D", 15.00);
            sc.AddItem(a, 5);
            sc.AddItem(b, 5);
            sc.AddItem(c);
            List<IPromotion> promoList = this.GetAvailablePromotions(a, b, c, d);
            Dictionary<Item, int> cartDetails = sc.GetCartDetails();

            Assert.NotNull(cartDetails);
            Assert.Equal(370, sc.GetCartTotalWithPromotion(promoList));
        }

        [Fact]
        public void ShouldExecuteScenarioC()
        {
            ShoppingCart sc = new ShoppingCart();
            Item a = new Item("A", 50.00);
            Item b = new Item("B", 30.00);
            Item c = new Item("C", 20.00);
            Item d = new Item("D", 15.00);
            sc.AddItem(a, 3);
            sc.AddItem(b, 5);
            sc.AddItem(c);
            sc.AddItem(d);
            List<IPromotion> promoList = this.GetAvailablePromotions(a, b, c, d);
            Dictionary<Item, int> cartDetails = sc.GetCartDetails();

            Assert.NotNull(cartDetails);
            Assert.Equal(280, sc.GetCartTotalWithPromotion(promoList));
        }

        private List<IPromotion> GetAvailablePromotions(Item a, Item b, Item c, Item d)
        {
            IPromotion threeAFor130 = new StandAloneAbsolutePromotion(a, 3, 130.00);
            IPromotion twoBFor45 = new StandAloneAbsolutePromotion(b, 2, 45.00);
            List<Item> itemList = new List<Item>();
            itemList.Add(c);
            itemList.Add(d);
            IPromotion cAndDFor30 = new CumulativeAbsolutePromotion(itemList, 30.00);
            List<IPromotion> promoList = new List<IPromotion>();
            promoList.Add(threeAFor130);
            promoList.Add(twoBFor45);
            promoList.Add(cAndDFor30);
            return promoList;
        }
    }
}
