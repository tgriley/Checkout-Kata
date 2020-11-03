using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata.Logic
{
    public class Checkout
    {
        private static readonly IDictionary<string, Item> ItemsDictionary = new Dictionary<string, Item>()
        {
            {"A99", new Item() {SKU = "A99", UnitPrice = 0.5m}},
            {"B15", new Item() {SKU = "B15", UnitPrice = 0.3m}},
            {"C40", new Item() {SKU = "C40", UnitPrice = 0.6m}}
        };
        
        private static readonly IList<SpecialOffer> SpecialOffers = new List<SpecialOffer>
        {
            new SpecialOffer() {SKU = "A99", Quantity = 3, OfferPrice = 1.3m},
            new SpecialOffer() {SKU = "B15", Quantity = 2, OfferPrice = 0.45m}
        };

        public readonly IList<Item> Items = new List<Item>();

        public decimal TotalPrice
        {
            get { return Items.Sum(x=> x.UnitPrice); }
        }

        public void Scan(string sku)
        {
            Items.Add(ItemsDictionary[sku]);
        }
    }
}