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
    }
}
