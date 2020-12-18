using FluentAssertions;
using System;
using WebshopApp.Core.ApplicationService;
using WebshopApp.Core.ApplicationService.Validators;
using WebshopApp.Core.Entity;
using Xunit;

namespace XUnitTest.Validators
{
    public class ProductValidatorTest
    {
        [Fact]
        public void ProductValidator_ShouldBeOfTypeProductValidator()
        {
            new ProductValidator().Should().BeAssignableTo<IProductValidator>();
        }

        [Fact]
        public void DefaultValidation_WithProductThatsNull_ShuldThrowException()
        {
            IProductValidator productValidator = new ProductValidator();
            Action action = () => productValidator.DefaultValidation(null as Product);
            action.Should().Throw<NullReferenceException>().WithMessage("Product cannot be null");
        }

        [Fact]
        public void DefaultValidation_WithProductHasNoName_ShouldThrowException()
        {
            IProductValidator productValidator = new ProductValidator();
            Action action = () => productValidator.DefaultValidation(new Product() { });
            action.Should().Throw<ArgumentException>().WithMessage("Product needs a name");
        }
    }
}
