namespace BillCalculator
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ShoppingCart
    {
        private Dictionary<Item, int> itemDetails = new Dictionary<Item, int>();

        public void AddItem(Item item)
        {
            this.AddItem(item, 1);
        }

        public Dictionary<Item, int> GetCartDetails()
        {
            return this.itemDetails;
        }

        public void AddItem(Item item, int orderQuantity)
        {
            if (this.itemDetails.ContainsKey(item))
            {
                int itemQuantity = this.itemDetails[item];
                this.itemDetails[item] = itemQuantity + orderQuantity;
            }
            else
            {
                this.itemDetails.Add(item, orderQuantity);
            }
        }

        public double GetCartTotal()
        {
            return this.GetCartTotal(this.itemDetails);
        }

        public double GetCartTotal(Dictionary<Item, int> itemDetails)
        {
            double totalBilll = 0;
            foreach (Item item in itemDetails.Keys)
            {
                totalBilll = totalBilll + (item.GetPrice() * itemDetails[item]);
            }

            return totalBilll;
        }

        public int GetItemsCount()
        {
            int count = 0;
            foreach (Item item in this.itemDetails.Keys)
            {
                count = count + this.itemDetails[item];
            }

            return count;
        }

        public double GetCartTotalWithOffer(IPromotion promotion)
        {
            Dictionary<Item, int> itemDetailsWithoutOffer = new Dictionary<Item, int>();
            double promoPrice = this.PriceOfItemsWithPromotion(promotion, ref itemDetailsWithoutOffer);

            double nonPromoPrice = this.PriceOfItemsWithoutPromotion(ref itemDetailsWithoutOffer);

            return promoPrice + nonPromoPrice;
        }

        private double PriceOfItemsWithoutPromotion(ref Dictionary<Item, int> itemDetailsWithoutOffer)
        {
            return this.GetCartTotal(itemDetailsWithoutOffer);
        }

        private double PriceOfItemsWithPromotion(IPromotion promotion, ref Dictionary<Item, int> itemDetailsWithoutOffer)
        {
            foreach (Item item in this.itemDetails.Keys)
            {
                itemDetailsWithoutOffer.Add(item, this.itemDetails[item]);
            }

            double discountedTotal = promotion.Execute(ref itemDetailsWithoutOffer);
            return discountedTotal;
        }
    }
}
