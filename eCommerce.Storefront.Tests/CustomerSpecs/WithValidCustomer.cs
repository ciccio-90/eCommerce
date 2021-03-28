using System;
using eCommerce.Storefront.Model.Customers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.Storefront.Tests.CustomerSpecs
{
    [TestClass]
    public abstract class WithValidCustomer
    {
        [TestInitialize]
        public void Context()
        {
            sut = new Customer()
            {
                FirstName = "Francesco",
                SecondName = "Guagnano",
                Email = "francescoguagnano@alice.it",
                UserId = Guid.NewGuid().ToString()
            };

            When();
        }

        public abstract void When();        

        public Customer sut { get; set; }
    }
}