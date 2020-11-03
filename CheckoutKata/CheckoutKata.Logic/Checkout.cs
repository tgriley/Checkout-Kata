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
        
        private readonly IList<decimal> _modifiers = new List<decimal>();

        public decimal TotalPrice
        {
            get
            {
                var totalPrice = Items.Sum(x => x.UnitPrice);
                var modifierTotal = _modifiers.Sum();
                return totalPrice + modifierTotal;
            }
        }

        public void Scan(string sku)
        {
            Items.Add(ItemsDictionary[sku]);
        }
        
        public void ProcessSpecialOffers()
        {
            _modifiers.Clear();

            var groupedCheckoutItems = Items
                .OrderBy(x => x.SKU)
                .GroupBy(x => x.SKU);

            foreach (var group in groupedCheckoutItems)
            {
                if (SpecialOffers.Any(x => x.SKU == group.Key))
                {
                    var specialOffer = SpecialOffers.First(x => x.SKU == group.Key);

                    var offerApplications = Math.DivRem(group.Count(), specialOffer.Quantity, out var remainder);
                    var itemBaseValue = group.First().UnitPrice;
                    var modifierPerApplication = specialOffer.OfferPrice - itemBaseValue * specialOffer.Quantity;
                    var totalOfferSaving = modifierPerApplication * offerApplications;

                    _modifiers.Add(totalOfferSaving);
                }
            }
        }
    }
}