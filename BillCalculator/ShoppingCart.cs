namespace BillCalculator.ShoppingCart
{
    using BillCalculator.Promotion;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ShoppingCart
    {
        private Dictionary<Item, int> itemDetails = new Dictionary<Item, int>();

        public void AddItem(Item item)
        {
            AddItem(item, 1);
        }

        public Dictionary<Item, int> GetCartDetails()
        {
            return itemDetails;
        }

        public void AddItem(Item item, int orderQuantity)
        {
            if (orderQuantity <= 0)
            {
                return;
            }

            if (itemDetails.ContainsKey(item))
            {
                int itemQuantity = itemDetails[item];
                itemDetails[item] = itemQuantity + orderQuantity;
            }
            else
            {
                itemDetails.Add(item, orderQuantity);
            }
        }

        public double GetCartTotal()
        {
            return GetCartTotal(itemDetails);
        }

        public double GetCartTotal(Dictionary<Item, int> itemDetails)
        {
            double totalBilll = 0;
            foreach (Item item in itemDetails.Keys)
            {
                totalBilll = totalBilll + item.GetPrice() * itemDetails[item];
            }

            return totalBilll;
        }

        public int GetItemsCount()
        {
            int count = 0;
            foreach (Item item in itemDetails.Keys)
            {
                count = count + itemDetails[item];
            }

            return count;
        }

        public double GetCartTotalWithPromotion(List<IPromotion> promoList)
        {
            Dictionary<Item, int> itemDetailsWithoutPromotion = new Dictionary<Item, int>();
            double promoPrice = PriceOfItemsWithPromotion(promoList, ref itemDetailsWithoutPromotion);

            double nonPromoPrice = PriceOfItemsWithoutPromotion(ref itemDetailsWithoutPromotion);

            return promoPrice + nonPromoPrice;
        }

        private double PriceOfItemsWithoutPromotion(ref Dictionary<Item, int> itemDetailsWithoutOffer)
        {
            return GetCartTotal(itemDetailsWithoutOffer);
        }

        private double PriceOfItemsWithPromotion(List<IPromotion> promoList, ref Dictionary<Item, int> itemDetailsWithoutOffer)
        {
            foreach (Item item in itemDetails.Keys)
            {
                itemDetailsWithoutOffer.Add(item, itemDetails[item]);
            }

            double discountedTotal = 0;

            foreach (IPromotion promotion in promoList)
            {
                discountedTotal = discountedTotal + promotion.Execute(ref itemDetailsWithoutOffer);
            }

            return discountedTotal;
        }
    }
}
