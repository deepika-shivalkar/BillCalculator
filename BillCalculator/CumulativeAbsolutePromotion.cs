namespace BillCalculator.Promotion
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BillCalculator.ShoppingCart;

    public class CumulativeAbsolutePromotion : IPromotion
    {
        private List<Item> itemList;
        private double promotionPrice;
        private Dictionary<Item, int> promoDetails = new Dictionary<Item, int>();

        public CumulativeAbsolutePromotion(List<Item> itemList, double promotionPrice)
        {
            this.itemList = itemList;
            this.promotionPrice = promotionPrice;
            foreach (Item item in itemList)
            {
                if (promoDetails.ContainsKey(item))
                {
                    int itemQuantity = promoDetails[item];
                    promoDetails[item] = itemQuantity + 1;
                }
                else
                {
                    promoDetails.Add(item, 1);
                }
            }
        }

        public double Execute(ref Dictionary<Item, int> itemDetailsWithoutOffer)
        {
            int finalExecutionCount = GetExecutionCountForPromo(ref itemDetailsWithoutOffer);

            if (!IsOfferEligible(finalExecutionCount))
            {
                return 0;
            }

            RemoveItemsEligibleForPromotion(itemDetailsWithoutOffer, finalExecutionCount);

            double discountedPrice = finalExecutionCount * promotionPrice;

            return discountedPrice;
        }

        private static bool IsOfferEligible(int finalExecutionCount)
        {
            return finalExecutionCount > 0;
        }

        private void RemoveItemsEligibleForPromotion(Dictionary<Item, int> itemDetailsWithoutOffer, int finalExecutionCount)
        {
            foreach (Item promoItem in promoDetails.Keys)
            {
                itemDetailsWithoutOffer[promoItem] = itemDetailsWithoutOffer[promoItem] - finalExecutionCount * promoDetails[promoItem];
            }
        }

        private int GetExecutionCountForPromo(ref Dictionary<Item, int> itemDetailsWithoutOffer)
        {
            int finalExecutionCount = int.MaxValue;
            foreach (Item promoItem in promoDetails.Keys)
            {
                if (!itemDetailsWithoutOffer.ContainsKey(promoItem) || itemDetailsWithoutOffer[promoItem] < promoDetails[promoItem])
                {
                    return 0;
                }

                int itemCountFromCart = itemDetailsWithoutOffer[promoItem];
                int itemCountEligibleForOffer = promoDetails[promoItem];
                int executionCount = itemCountFromCart / itemCountEligibleForOffer;
                if (executionCount < finalExecutionCount)
                {
                    finalExecutionCount = executionCount;
                }
            }
            return finalExecutionCount;
        }
    }
}
