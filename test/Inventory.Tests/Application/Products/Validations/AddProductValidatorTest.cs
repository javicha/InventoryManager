using Inventory.Application.Common.Enums;
using Inventory.Application.Contracts.Persistence;
using Inventory.Application.Features.Products.Commands.AddProduct;
using Inventory.Domain.Entities;
using Moq;
using Moq.AutoMock;
using System.Collections;

namespace Inventory.Tests.Application.Products.Validations
{
    /// <summary>
    /// Test class to ensure the correct operation of AddProductCommandValidator 
    /// </summary>
    public class AddProductValidatorTest
    {
        private readonly AutoMocker mocker;
        private readonly AddProductCommandValidator validator;
        private readonly Mock<IProductRepository> productRepository;

        public AddProductValidatorTest()
        {
            mocker = new AutoMocker();
            validator = mocker.CreateInstance<AddProductCommandValidator>();
            productRepository = mocker.GetMock<IProductRepository>();
        }

        [Fact]
        public async Task AddProductCommandValidator_OK()
        {
            // Variables 
            var command = new AddProductCommand("Test", "Ref-test", "Test product", ProductTypeEnum.Primer, 400,
                ProductManufacturerEnum.Agilent, 5, null, ProductSupplierEnum.Supplier1, DateTime.Today, DateTime.Today.AddYears(1));

            // Stub
            productRepository.Setup(q => q.ExistByName(command.Name)).ReturnsAsync(false);

            // Execute
            var result = await this.validator.ValidateAsync(command);

            // Assert
            Assert.True(result.IsValid);

            // Verify
            productRepository.Verify(q => q.ExistByName(command.Name), Times.Once());
        }

        [Fact]
        public async Task AddProductCommandValidator_ProductAlreadyExist()
        {
            // Variables
            var command = new AddProductCommand("Test", "Ref-test", "Test product", ProductTypeEnum.Primer, 400,
                ProductManufacturerEnum.Agilent, 5, null, ProductSupplierEnum.Supplier1, DateTime.Today, DateTime.Today.AddYears(1));

            // Stub
            productRepository.Setup(q => q.ExistByName(command.Name)).ReturnsAsync(true);

            // Execute
            var result = await this.validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Equal("A product with that name already exists in inventory", result.Errors.First().ErrorMessage);

            // Verify
            productRepository.Verify(q => q.ExistByName(command.Name), Times.Once());
        }


        [Theory]
        [ClassData(typeof(AddProductCommandValidator_FieldConstraintFailed_UseCaseTestSupport))]
        public async Task AddProductCommandValidator_FieldConstraintFailed(AddProductCommand command, string expected)
        {
            // Stub
            productRepository.Setup(q => q.ExistByName(command.Name)).ReturnsAsync(false);

            // Execute
            var result = await this.validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            var validationError = result.Errors.First();
            Assert.Contains(expected, validationError.ErrorMessage);

            // Verify
            productRepository.Verify(q => q.AddAsync(It.IsAny<Product>(), It.IsAny<string>()), Times.Never());
        }
    }


    public class AddProductCommandValidator_FieldConstraintFailed_UseCaseTestSupport : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            return new List<object[]>()
            {
                new object[] { new AddProductCommand("", "Ref-test", "Test product", ProductTypeEnum.Primer, 400, ProductManufacturerEnum.Agilent, 5, null, ProductSupplierEnum.Supplier1, DateTime.Today, DateTime.Today.AddYears(1)), "{Name}" }, //Name required
                new object[] { new AddProductCommand("Test", "", "Test product", ProductTypeEnum.Primer, 400, ProductManufacturerEnum.Agilent, 5, null, ProductSupplierEnum.Supplier1, DateTime.Today, DateTime.Today.AddYears(1)), "{Reference}" }, //Reference required
                new object[] { new AddProductCommand("Test", "Ref-test", "Test product", null, 400, ProductManufacturerEnum.Agilent, 5, null, ProductSupplierEnum.Supplier1, DateTime.Today, DateTime.Today.AddYears(1)), "{Type}" }, //Type required
                new object[] { new AddProductCommand("Test", "Ref-test", "Test product", ProductTypeEnum.Primer, 400, ProductManufacturerEnum.Agilent, 0, null, ProductSupplierEnum.Supplier1, DateTime.Today, DateTime.Today.AddYears(1)), "{NumUnits}" }  //Num units > 0
            }.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
