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
                if (this.promoDetails.ContainsKey(item))
                {
                    int itemQuantity = this.promoDetails[item];
                    this.promoDetails[item] = itemQuantity + 1;
                }
                else
                {
                    this.promoDetails.Add(item, 1);
                }
            }
        }

        public double Execute(ref Dictionary<Item, int> itemDetailsWithoutOffer)
        {
            int finalExecutionCount = this.GetExecutionCountForPromo(ref itemDetailsWithoutOffer);

            if (!IsOfferEligible(finalExecutionCount))
            {
                return 0;
            }

            this.RemoveItemsEligibleForPromotion(itemDetailsWithoutOffer, finalExecutionCount);

            double discountedPrice = finalExecutionCount * this.promotionPrice;

            return discountedPrice;
        }

        private static bool IsOfferEligible(int finalExecutionCount)
        {
            return finalExecutionCount > 0;
        }

        private void RemoveItemsEligibleForPromotion(Dictionary<Item, int> itemDetailsWithoutOffer, int finalExecutionCount)
        {
            foreach (Item promoItem in this.promoDetails.Keys)
            {
                itemDetailsWithoutOffer[promoItem] = itemDetailsWithoutOffer[promoItem] - (finalExecutionCount * this.promoDetails[promoItem]);
                if (itemDetailsWithoutOffer[promoItem] == 0)
                {
                    itemDetailsWithoutOffer.Remove(promoItem);
                }
            }

        }

        private int GetExecutionCountForPromo(ref Dictionary<Item, int> itemDetailsWithoutOffer)
        {
            int finalExecutionCount = int.MaxValue;
            foreach (Item promoItem in this.promoDetails.Keys)
            {
                if (!itemDetailsWithoutOffer.ContainsKey(promoItem) || itemDetailsWithoutOffer[promoItem] < this.promoDetails[promoItem])
                {
                    return 0;
                }

                int itemCountFromCart = itemDetailsWithoutOffer[promoItem];
                int itemCountEligibleForOffer = this.promoDetails[promoItem];
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
