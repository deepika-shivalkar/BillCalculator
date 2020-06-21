using System;
using Xunit;

namespace BillCalculator.Tests
{
    public class BillCalculatorTests
    {
        [Fact]
        public void ShouldAddSingleItemsToCart()
        {
            ShoppingCart c = new ShoppingCart();
            Item A = new Item("A", 50.00);
            c.AddItem(A);
        }
    }
}
