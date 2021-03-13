using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.Storefront.Tests.CustomerSpecs
{
    [TestClass]
    public class WhenChangingTheNameOfCustomer : WithValidCustomer
    {
        private dynamic _newName;

        public override void When()
        {
            _newName = new 
            {
                FirstName = "Mickey",
                SecondName = "Mouse"
            };

            sut.FirstName = _newName.FirstName;
            sut.SecondName = _newName.SecondName;
        }

        [TestMethod]
        public void ThenThePropertiesShouldMatchTheNewName()
        {
            Assert.AreEqual(_newName.FirstName, sut.FirstName);
            Assert.AreEqual(_newName.SecondName, sut.SecondName);
        }
    }
}