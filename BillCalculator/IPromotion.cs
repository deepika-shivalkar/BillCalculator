namespace BillCalculator.Promotion
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BillCalculator.ShoppingCart;

    public interface IPromotion
    {
        double Execute(ref Dictionary<Item, int> itemDetailsWithoutOffer);
    }
}
