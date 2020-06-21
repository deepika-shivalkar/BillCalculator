using System;
using System.Collections.Generic;
using System.Text;

namespace BillCalculator
{
    public class StandAloneAbsolutePromotion : IPromotion
    {
        private Item item;
        private int eligibleQuantity;
        private double promotionPrice;

        public StandAloneAbsolutePromotion(Item item, int eligibleQuantity, double promotionPrice)
        {
            this.item = item;
            this.eligibleQuantity = eligibleQuantity;
            this.promotionPrice = promotionPrice;
        }

        public double Execute(ref Dictionary<Item, int> itemDetailsWithoutOffer)
        {
            int quantity = itemDetailsWithoutOffer[item];
            int executionCount = quantity / this.eligibleQuantity;
            if (executionCount == 0)
            {
                return 0;
            }
            else
            {
                double discountedPrice = executionCount * this.promotionPrice;
                int itemCountToRemove = executionCount * this.eligibleQuantity;
                itemDetailsWithoutOffer[this.item] = itemDetailsWithoutOffer[this.item] - itemCountToRemove;
                if (itemDetailsWithoutOffer[this.item] == 0)
                {
                    itemDetailsWithoutOffer.Remove(this.item);
                }

                return discountedPrice;
            }
        }
    }
}
