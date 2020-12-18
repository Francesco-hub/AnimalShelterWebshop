using FluentAssertions;
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
    public class OrderServiceTest
    {
        [Fact]
        public void testGetAllOrders()
        {
            var validatorMock = new Mock<IOrderValidator>();
            var repositoryMock = new Mock<IOrderRepository>();
            IOrderService service = new OrderService(validatorMock.Object, repositoryMock.Object);
            List<Order> orderList = new List<Order>();
            Order order = new Order()
            {
                Id = 1,
                CustomerId = 1,
                Customer = new Customer(),
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = "",
                OrderProducts = new List<OrderProduct>(),
                Products = new List<Product>(),
                TotalPrice = 123
            };
             orderList.Add(order);
            Order order2 = new Order()
            {
                Id = 2,
                CustomerId = 1,
                Customer = new Customer(),
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = "",
                OrderProducts = new List<OrderProduct>(),
                Products = new List<Product>(),
                TotalPrice = 123
            };
            orderList.Add(order2);            
            repositoryMock.Setup(r => r.ReadOrderByCustomerID(order.CustomerId)).Returns(orderList);
            Assert.True(service.FindOrderByCustomerID(1).Count == 2);
        }

        [Fact]
        public void testCreate()
        {
            var validatorMock = new Mock<IOrderValidator>();
            var repositoryMock = new Mock<IOrderRepository>();
            IOrderService service = new OrderService(validatorMock.Object, repositoryMock.Object);
            List<Order> orderList = new List<Order>();
            Order order = new Order()
            {
                Id = 3,
                CustomerId = 1,
                Customer = new Customer(),
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = "",
                OrderProducts = new List<OrderProduct>(),
                Products = new List<Product>(),
                TotalPrice = 123
            };
            orderList.Add(order);
            repositoryMock.Setup(r => r.Create(order)).Returns(order);
            var createdOrder = service.CreateOrder(order);
            createdOrder.Should().Be(order);
        }
    }
}
