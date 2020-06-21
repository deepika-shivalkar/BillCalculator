using System;
using System.Collections.Generic;
using System.Text;

namespace BillCalculator
{
    public interface IPromotion
    {
        double Execute(ref Dictionary<Item, int> itemDetailsWithoutOffer);
    }
}
