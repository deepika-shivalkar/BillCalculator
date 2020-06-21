namespace BillCalculator.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using BillCalculator.Promotion;

    public class Program
    {
        public static void Main(string[] args)
        {
            Item a = new Item("A", 50.00);
            Item b = new Item("B", 30.00);
            Item c = new Item("C", 20.00);
            Item d = new Item("D", 15.00);
            List<IPromotion> promoList = GetAvailablePromotions(a, b, c, d);
            DisplayUnitPriceWithoutOffer(a, b, c, d);
            DisplayActivePromotions();
            string scenario = string.Empty;

            // Scenario A
            ExecuteScenarioA(a, b, c, promoList);

            // Scenario B
            ExecuteScenarioB(a, b, c, promoList);

            // Scenario C
            ExecuteScenarioC(a, b, c, d, promoList);
        }

        private static void ExecuteScenarioC(Item a, Item b, Item c, Item d, List<IPromotion> promoList)
        {
            string scenario = "C";
            ShoppingCart sc = new ShoppingCart();
            sc.AddItem(a, 3);
            sc.AddItem(b, 5);
            sc.AddItem(c, 1);
            sc.AddItem(d, 1);
            Dictionary<Item, int> cartDetails = sc.GetCartDetails();
            DisplayScenarioInputs(cartDetails, scenario);
            double cartToral = sc.GetCartTotalWithPromotion(promoList);
            DisplayCartTotal(cartToral);
        }

        private static void ExecuteScenarioB(Item a, Item b, Item c, List<IPromotion> promoList)
        {
            string scenario = "B";
            ShoppingCart sc = new ShoppingCart();
            sc.AddItem(a, 5);
            sc.AddItem(b, 5);
            sc.AddItem(c, 1);
            Dictionary<Item, int> cartDetails = sc.GetCartDetails();
            DisplayScenarioInputs(cartDetails, scenario);
            double cartTotal = sc.GetCartTotalWithPromotion(promoList);
            DisplayCartTotal(cartTotal);
        }

        private static void ExecuteScenarioA(Item a, Item b, Item c, List<IPromotion> promoList)
        {
            string scenario = "A";
            ShoppingCart sc = new ShoppingCart();
            sc.AddItem(a);
            sc.AddItem(b);
            sc.AddItem(c);
            Dictionary<Item, int> cartDetails = sc.GetCartDetails();
            DisplayScenarioInputs(cartDetails, scenario);
            double cartTotal = sc.GetCartTotalWithPromotion(promoList);
            DisplayCartTotal(cartTotal);
        }

        private static void DisplayCartTotal(double cartTotal)
        {
            Console.WriteLine("Total   " + cartTotal);
        }

        private static void DisplayScenarioInputs(Dictionary<Item, int> cartDetails, string scenario)
        {
            Console.WriteLine("\nScenario " + scenario);
            foreach (Item item in cartDetails.Keys)
            {
                Console.WriteLine(item.GetName()+" "+cartDetails[item].ToString());
            }

            Console.WriteLine("=============");
        }

        private static void DisplayScenarioInputs(string scenario)
        {
            Console.WriteLine("Scenario " + scenario + "\n");
        }

        private static void DisplayActivePromotions()
        {
            Console.WriteLine("Active Promotions\n");
            Console.WriteLine("3 Of A's for 130");
            Console.WriteLine("2 Of B's for 45");
            Console.WriteLine("C and D for 30");
        }

        private static void DisplayUnitPriceWithoutOffer(Item a, Item b, Item c, Item d)
        {
            Console.WriteLine("Unit price for SKU IDs\n");
            Console.WriteLine(a.GetName() + "    " + a.GetPrice());
            Console.WriteLine(b.GetName() + "    " + b.GetPrice());
            Console.WriteLine(c.GetName() + "    " + c.GetPrice());
            Console.WriteLine(d.GetName() + "    " + d.GetPrice());
            Console.WriteLine("\n");
        }

        private static List<IPromotion> GetAvailablePromotions(Item a, Item b, Item c, Item d)
        {
            IPromotion threeAFor130 = new StandAloneAbsolutePromotion(a, 3, 130.00);
            IPromotion twoBFor45 = new StandAloneAbsolutePromotion(b, 2, 45.00);
            List<Item> itemList = new List<Item>();
            itemList.Add(c);
            itemList.Add(d);
            IPromotion cAndDFor30 = new CumulativeAbsolutePromotion(itemList, 30.00);
            List<IPromotion> promoList = new List<IPromotion>();
            promoList.Add(threeAFor130);
            promoList.Add(twoBFor45);
            promoList.Add(cAndDFor30);
            return promoList;
        }
    }
}
