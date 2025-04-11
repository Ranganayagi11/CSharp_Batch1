using NUnit.Framework;
using System;
using CarConnect.dao;
using CarConnect.entity;

namespace CarConnectTests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private ICustomerService customerService;

        [SetUp]
        public void Setup()
        {
            customerService = new CustomerService();
        }

        [Test]
        public void TestCustomerAuthenticationWithInvalidCredentials()
        {
            var customer = customerService.GetCustomerByUsername("invalidUser");
            Assert.IsNull(customer);
        }

        [Test]
        public void TestUpdatingCustomerInformation()
        {
            var customer = new Customer
            {
                CustomerID = 4,
                FirstName = "Test",
                LastName = "Updated",
                Email = "updated@email.com",
                PhoneNumber = "9999999999",
                Address = "Updated Address",
                Username = "testuser",
                Password = "password",
                RegistrationDate = DateTime.Now
            };

            customerService.UpdateCustomer(customer);
            var updatedCustomer = customerService.GetCustomerById(4);

            Assert.IsNotNull(updatedCustomer);
            Assert.AreEqual("Updated", updatedCustomer.LastName);
        }
    }
}
