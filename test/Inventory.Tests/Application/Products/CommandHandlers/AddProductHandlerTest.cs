using AutoMapper;
using Inventory.Application.Common.Enums;
using Inventory.Application.Contracts.Persistence;
using Inventory.Application.Features.Products.Commands.AddProduct;
using Inventory.Application.Mappings;
using Inventory.Domain.Entities;
using Moq;
using Moq.AutoMock;

namespace Inventory.Tests.Application.Products.CommandHandlers
{
    /// <summary>
    /// Test class to ensure the correct operation of AddProductCommandHandler 
    /// </summary>
    public class AddProductHandlerTest
    {
        private readonly AutoMocker mocker;
        private readonly AddProductCommandHandler handler;
        private readonly Mock<IProductRepository> productRepository;

        public AddProductHandlerTest()
        {
            mocker = new AutoMocker();
            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
            mocker.Use(mapperConfig.CreateMapper());
            handler = mocker.CreateInstance<AddProductCommandHandler>();
            productRepository = mocker.GetMock<IProductRepository>();
        }

        [Fact]
        public async Task AddProductHandler_OK()
        {
            //Variables
            Product newProduct = GetProduct();
            var command = new AddProductCommand("Test", "Ref-test", "Test product", ProductTypeEnum.Primer, 400, 
                ProductManufacturerEnum.Agilent, 5, null, ProductSupplierEnum.Supplier1, DateTime.Today, DateTime.Today.AddYears(1));

            //Stub
            this.productRepository.Setup(r => r.AddAsync(It.IsAny<Product>(), It.IsAny<string>())).ReturnsAsync(newProduct);

            //Execute
            var result = await this.handler.Handle(command, new CancellationToken());

            //Assert
            Assert.NotNull(result);

            Assert.Equal(command.Name, newProduct.Name);
            Assert.Equal(command.Reference, newProduct.Reference);
            Assert.Equal(command.ExpirationDate, newProduct.ExpirationDate);

            //Verify
            productRepository.Verify(r => r.AddAsync(It.IsAny<Product>(), It.IsAny<string>()), Times.Once(), "Repository not called");
        }

        private Product GetProduct()
        {
            return new Product("Test", "Ref-test", "Test product", 3, 700, 0, 1, null, 2, DateTime.Today.AddDays(-1), DateTime.Today.AddYears(1), "user.test");
        }
    }
}
