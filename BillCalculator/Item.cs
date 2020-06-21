using System;
using System.Collections.Generic;
using System.Text;

namespace BillCalculator
{
    public class Item
    {
        private string name;
        private double price;

        public Item(string name, double price)
        {
            this.name = name;
            this.price = price;
        }
    }
}
