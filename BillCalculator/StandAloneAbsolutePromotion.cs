namespace BillCalculator.Promotion
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BillCalculator.ShoppingCart;

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
            int quantity = itemDetailsWithoutOffer[this.item];
            int executionCount = quantity / this.eligibleQuantity;
            if (!this.IsOfferEligible(executionCount))
            {
                return 0;
            }
            else
            {
                this.RemoveItemsEligibleForPromotion(ref itemDetailsWithoutOffer, executionCount);
                double discountedPrice = executionCount * this.promotionPrice;
                return discountedPrice;
            }
        }

        private void RemoveItemsEligibleForPromotion(ref Dictionary<Item, int> itemDetailsWithoutOffer, int executionCount)
        {
            int itemCountToRemove = executionCount * this.eligibleQuantity;
            itemDetailsWithoutOffer[this.item] = itemDetailsWithoutOffer[this.item] - itemCountToRemove;
            if (itemDetailsWithoutOffer[this.item] == 0)
            {
                itemDetailsWithoutOffer.Remove(this.item);
            }
        }

        private bool IsOfferEligible(int executionCount)
        {
            return executionCount > 0;
        }
    }
}
