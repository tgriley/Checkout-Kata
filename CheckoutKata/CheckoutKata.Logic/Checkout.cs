using System.Collections.Generic;

namespace CheckoutKata.Logic
{
    public class Checkout
    {
        private static readonly IDictionary<string, Item> Products = new Dictionary<string, Item>()
        {
            {"A99", new Item() {SKU = "A99", UnitPrice = 0.5m}},
            {"B15", new Item() {SKU = "B15", UnitPrice = 0.3m}},
            {"C40", new Item() {SKU = "C40", UnitPrice = 0.6m}}
        };
        
        public readonly IList<Item> Items = new List<Item>();
        
        public void Scan(string sku)
        {
            Items.Add(Products[sku]);
        }
    }
}