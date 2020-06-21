using System;
using System.Collections.Generic;
using System.Text;

namespace BillCalculator
{
    public class ShoppingCart
    {
        private Dictionary<Item, int> itemDetails = new Dictionary<Item, int>();
        public void AddItem(Item item)
        {
            if (this.itemDetails.ContainsKey(item))
            {
                int itemQuantity = this.itemDetails[item];
                this.itemDetails[item] = itemQuantity + 1;
            }
            else
            {
                this.itemDetails.Add(item, 1);
            }

        }

        public Dictionary<Item, int> GetCartDetails()
        {
            return this.itemDetails;
        }
    }
}
