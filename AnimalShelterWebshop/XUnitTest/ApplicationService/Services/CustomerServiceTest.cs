using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebshopApp.Core.ApplicationService;
using WebshopApp.Core.ApplicationService.Services;
using WebshopApp.Core.DomainService;
using WebshopApp.Core.Entity;
using Xunit;

namespace XUnitTest.ApplicationService.Services
{
    public class CustomerServiceTest
    {
        [Fact]
        public void testGetAllCustomers()
        {
            var validatorMock = new Mock<ICustomerValidator>();
            var repositoryMock = new Mock<ICustomerRepository>();
            ICustomerService service = new CustomerService(validatorMock.Object, repositoryMock.Object);
            List<Customer> custList = new List<Customer>();
            Customer cust = new Customer()
            {
                Id = 1,
                FirstName = "FakeName",
                LastName = "FakeLastName",
                PasswordHash = new byte[1],
                PasswordSalt = new byte[1],
                IsAdmin = false,
                Orders = new List<Order>()
            };
            custList.Add(cust);
            Customer cust2 = new Customer()
             {
                 Id = 2,
                 FirstName = "FakeName2",
                 LastName = "FakeLastName2",
                 PasswordHash = new byte[1],
                 PasswordSalt = new byte[1],
                 IsAdmin = false,
                 Orders = new List<Order>()
             };
            custList.Add(cust2);
            repositoryMock.Setup(r => r.ReadAllCustomers()).Returns(custList);
            Assert.True(service.GetAllCustomers().Count == 2);
        }
    }
}
