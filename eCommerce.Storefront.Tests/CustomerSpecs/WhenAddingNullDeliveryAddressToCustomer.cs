using System;
using eCommerce.Storefront.Model.Customers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.Storefront.Tests.CustomerSpecs
{
    [TestClass]
    public class WhenAddingNullDeliveryAddressToCustomer : WithValidCustomer
    {
        private DeliveryAddress _invalidAddress;

        public override void When()
        {
            _invalidAddress = null;
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ThenNullReferenceExceptionWillBeThrown()
        {
            sut.AddAddress(_invalidAddress);
        }        
    }
}