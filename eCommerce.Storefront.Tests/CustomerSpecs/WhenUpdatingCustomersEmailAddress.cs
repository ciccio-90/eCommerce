using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.Storefront.Tests.CustomerSpecs
{
    [TestClass]
    public class WhenUpdatingCustomersEmailAddress : WithValidCustomer
    {
        private string _email;
        
        public override void When()
        {
            _email = new string("Scott@elbandit.co.uk");

            sut.Email = _email;
        }

        [TestMethod]
        public void ThenTheCustomerEmailPropertyWillBeSet()
        {
            Assert.AreEqual(_email, sut.Email); 
        } 
    }
}