namespace BillCalculator.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Item
    {
        private string name;
        private double price;

        public Item(string name, double price)
        {
            this.name = name;
            this.price = price;
        }

        public override bool Equals(object obj)
        {
            return obj is Item item &&
                   this.name == item.name &&
                   this.price == item.price;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.name, this.price);
        }

        public string GetName()
        {
            return this.name;
        }

        public double GetPrice()
        {
            return this.price;
        }
    }
}
