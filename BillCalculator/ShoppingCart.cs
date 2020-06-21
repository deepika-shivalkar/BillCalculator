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
            double totalBilll = 0;
            foreach (Item item in this.itemDetails.Keys)
            {
                totalBilll = totalBilll + (item.GetPrice() * this.itemDetails[item]);
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
            foreach (Item item in this.itemDetails.Keys)
            {
                itemDetailsWithoutOffer.Add(item, this.itemDetails[item]);
            }

            double discountedTotal = promotion.Execute(ref itemDetailsWithoutOffer);

            foreach (Item item in itemDetailsWithoutOffer.Keys)
            {
                discountedTotal = discountedTotal + (item.GetPrice() * itemDetailsWithoutOffer[item]);
            }

            return discountedTotal;
        }
    }
}
