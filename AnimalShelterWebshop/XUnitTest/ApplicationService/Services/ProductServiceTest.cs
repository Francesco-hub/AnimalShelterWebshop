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
    public class ProductServiceTest
    {
        [Fact]
        public void ProductService_IsOfTypeProductService()
        {
            var productValidatorMock = new Mock<IProductValidator>();
            var productRepositoryMock = new Mock<IProductRepository>();
            new ProductService(productValidatorMock.Object, productRepositoryMock.Object).Should().BeAssignableTo<IProductService>();
        }

        [Fact]
        public void NewProductService_WthNullValidator_ShouldThrowException()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            Action action = () => new ProductService(null as IProductValidator, productRepositoryMock.Object);
            action.Should().Throw<NullReferenceException>().WithMessage("Validator cannot be null");
        }

       [Fact]
        public void Update_ShouldCallProductValidatorWithTheProductInParam_Once()
        {
            var productValidatorMock = new Mock<IProductValidator>();
            var productRepositoryMock = new Mock<IProductRepository>();
            IProductService productService = new ProductService(productValidatorMock.Object, productRepositoryMock.Object);
            var productToUpdate = new Product();
            Product product = productService.Update(productToUpdate);
            productValidatorMock.Verify(pv => pv.DefaultValidation(productToUpdate), Times.Once);
        }

        [Fact]
        public void testCreate()
        {
            var validatorMock = new Mock<IProductValidator>();
            var repositoryMock = new Mock<IProductRepository>();
            IProductService service = new ProductService(validatorMock.Object, repositoryMock.Object);
            var prod = new Product()
            {
                Id = 1,
                Name = "fakeProd",
                TypeName = "fakeTypeName",
                Price = 123,
                ImageUrl = "",
                OrderProducts = new List<OrderProduct>(),
                Orders = new List<Order>()
            };
            repositoryMock.Setup(r => r.Create(prod)).Returns(prod);
            var createdProduct = service.CreateProduct(prod);
            createdProduct.Should().Be(prod);
        }
        
        [Fact]
        public void Update_ShouldReturn_UpdatedProductWithCorrectId()
        {
            var validatorMock = new Mock<IProductValidator>();
            var repositoryMock = new Mock<IProductRepository>();
            var prod = new Product() {
                Id = 1,
                Name = "fakeProd",
                TypeName = "fakeTypeName",
                Price = 123,
                ImageUrl = "",
                OrderProducts = new List<OrderProduct>(),
                Orders = new List<Order>()
            };

            var productCreated = new Product() {
                Id = 2,
                Name = "fakeProd",
                TypeName = "fakeTypeName2",
                Price = 123,
                ImageUrl = "",
                OrderProducts = new List<OrderProduct>(),
                Orders = new List<Order>()
            };
            repositoryMock.Setup(r => r.Update(prod)).Returns(prod);
            IProductService service = new ProductService(validatorMock.Object, repositoryMock.Object);
            var updatedProduct = service.UpdateProduct(prod);
            updatedProduct.Should().Be(prod);
        }

        [Fact]
        public void testDelete()
        {
            var validatorMock = new Mock<IProductValidator>();
            var repositoryMock = new Mock<IProductRepository>();
            IProductService service = new ProductService(validatorMock.Object, repositoryMock.Object);
            var prod = new Product()
            {
                Id = 1,
                Name = "fakeProd",
                TypeName = "fakeTypeName",
                Price = 123,
                ImageUrl = "",
                OrderProducts = new List<OrderProduct>(),
                Orders = new List<Order>()
            };
            repositoryMock.Setup(r => r.Delete(prod.Id));
            service.CreateProduct(prod);
            service.DeleteProduct(prod.Id);
            Assert.True(service.GetAllProducts().Count == 0);
        }

        [Fact]
        public void testGetAllByType()
        {
            var validatorMock = new Mock<IProductValidator>();
            var repositoryMock = new Mock<IProductRepository>();
            IProductService service = new ProductService(validatorMock.Object, repositoryMock.Object);
            List<Product> prodList = new List<Product>();
            List<Product> prodList2 = new List<Product>();
            var prod = new Product()
            {
                Id = 1,
                Name = "fakeProd",
                TypeName = "Mugs",
                Price = 123,
                ImageUrl = "",
                OrderProducts = new List<OrderProduct>(),
                Orders = new List<Order>()
            };
            prodList.Add(prod);
            prodList2.Add(prod);
            var prod2 = new Product()
            {
                Id = 2,
                Name = "fakeProd2",
                TypeName = "Tshirts",
                Price = 123,
                ImageUrl = "",
                OrderProducts = new List<OrderProduct>(),
                Orders = new List<Order>()
            };
            prodList2.Add(prod2);            
            repositoryMock.Setup(r => r.ReadProductsByType("Mugs")).Returns(prodList);
            var realList = service.GetAllByType("Mugs");
            realList.Should().BeEquivalentTo(prodList);
        }




    }
}
