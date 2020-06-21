﻿using System;
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

        public override bool Equals(object obj)
        {
            return obj is Item item &&
                   name == item.name &&
                   price == item.price;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, price);
        }
    }
}
