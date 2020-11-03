using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckoutKata.Logic.Test.Unit
{
    [TestClass]
    public class Checkout
    {
        [TestMethod]
        [DataRow("A99")]
        [DataRow("B15")]
        [DataRow("C40")]
        public void GivenKnownSKU_WhenScanned_ThenAddToItems(string knownSKU)
        {
            //Given
            var checkout = new Logic.Checkout();
            
            //When
            checkout.Scan(knownSKU);
            
            //Then
            Assert.AreEqual(checkout.Items.Count, 1);
            Assert.AreEqual(checkout.Items[0].SKU, knownSKU);
        }
        
        [TestMethod]
        [DataRow(new string[] { "A99", "B15", "C40" })]
        [DataRow(new string[] { "A99", "A99" })]
        public void GivenKnownSKUs_WhenScanned_ThenAddAllToItems(IEnumerable<string> knownSKUs)
        {
            //Given
            var checkout = new Logic.Checkout();
            
            //When
            foreach (var knownSKU in knownSKUs)
            {
                checkout.Scan(knownSKU);
            }
            
            //Then
            Assert.AreEqual(checkout.Items.Count, knownSKUs.Count());
        }
        
        [TestMethod]
        [DataRow(new string[] { "A99", "B15", "C40" })]
        [DataRow(new string[] { "A99", "A99" })]
        public void GivenKnownSKUs_WhenTotalIsRead_ThenSumOfItemsUnitPriceIsReturned(IEnumerable<string> knownSKUs)
        {
            //Given
            var checkout = new Logic.Checkout();
            
            //When
            foreach (var knownSKU in knownSKUs)
            {
                checkout.Scan(knownSKU);
            }
            
            //Then
            Assert.AreEqual(checkout.TotalPrice, checkout.Items.Sum(x=> x.UnitPrice));
        }
        
        [TestMethod]
        public void GivenSpecialOfferSKUsB15A_WhenTotalIsRead_ThenSumOfItemsUnitPriceIsReturned()
        {
            //Given
            var checkout = new Logic.Checkout();

            var knownSKUs = new List<string> {"B15", "B15"};
            
            //When
            foreach (var knownSKU in knownSKUs)
            {
                checkout.Scan(knownSKU);
            }
            
            //Then
            Assert.AreEqual(checkout.TotalPrice, 0.95m);
        }
        
        [TestMethod]
        public void GivenSpecialOfferSKUsB15Broken_WhenTotalIsRead_ThenSumOfItemsUnitPriceIsReturned()
        {
            //Given
            var checkout = new Logic.Checkout();

            var knownSKUs = new List<string> {"B15", "A99", "B15"};
            
            //When
            foreach (var knownSKU in knownSKUs)
            {
                checkout.Scan(knownSKU);
            }
            
            //Then
            Assert.AreEqual(checkout.TotalPrice, 0.95m);
        }

        [TestMethod]
        public void GivenSpecialOfferSKUsA99_WhenTotalIsRead_ThenSumOfItemsUnitPriceIsReturned()
        {
            //Given
            var checkout = new Logic.Checkout();

            var knownSKUs = new List<string> {"A99", "A99", "A99"};
            
            //When
            foreach (var knownSKU in knownSKUs)
            {
                checkout.Scan(knownSKU);
            }
            
            //Then
            Assert.AreEqual(checkout.TotalPrice, 1.3m);
        }
    }
}