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
    }
}
