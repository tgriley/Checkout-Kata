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
    }
}