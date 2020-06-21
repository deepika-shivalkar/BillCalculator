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
            int quantity = itemDetailsWithoutOffer[item];
            int executionCount = quantity / eligibleQuantity;
            if (!IsOfferEligible(executionCount))
            {
                return 0;
            }
            else
            {
                RemoveItemsEligibleForPromotion(ref itemDetailsWithoutOffer, executionCount);
                double discountedPrice = executionCount * promotionPrice;
                return discountedPrice;
            }
        }

        private void RemoveItemsEligibleForPromotion(ref Dictionary<Item, int> itemDetailsWithoutOffer, int executionCount)
        {
            int itemCountToRemove = executionCount * eligibleQuantity;
            itemDetailsWithoutOffer[item] = itemDetailsWithoutOffer[item] - itemCountToRemove;
            if (itemDetailsWithoutOffer[item] == 0)
            {
                itemDetailsWithoutOffer.Remove(item);
            }
        }

        private bool IsOfferEligible(int executionCount)
        {
            return executionCount > 0;
        }
    }
}
