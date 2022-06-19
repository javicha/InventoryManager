using Inventory.Application.Contracts.Persistence;
using Inventory.Application.Features.Products.Commands.RemoveProductByName;
using Inventory.Domain.Entities;
using MediatR;
using Moq;
using Moq.AutoMock;

namespace Inventory.Tests.Application.Products.CommandHandlers
{
    public class RemoveProductByNameHandlerTest
    {
        private readonly AutoMocker mocker;
        private readonly RemoveProductByNameCommandHandler handler;
        private readonly Mock<IProductRepository> productRepository;

        public RemoveProductByNameHandlerTest()
        {
            mocker = new AutoMocker();
            handler = mocker.CreateInstance<RemoveProductByNameCommandHandler>();
            productRepository = mocker.GetMock<IProductRepository>();
        }

        [Fact]
        public async Task RemoveProductByNameHandler_OK()
        {
            //Variables
            var command = new RemoveProductByNameCommand("Product Test");
            var productToDelete = GetProduct();

            //Stub
            productRepository.Setup(r => r.GetByName(It.IsAny<string>())).ReturnsAsync(productToDelete);

            //Execute
            var result = await this.handler.Handle(command, new CancellationToken());

            //Assert
            Assert.Equal(Unit.Value, result);

            //Verify
            productRepository.Verify(r => r.GetByName(It.IsAny<string>()), Times.Once());
            productRepository.Verify(r => r.SoftDeleteAsync(productToDelete, It.IsAny<string>()), Times.Once());
        }

        private Product GetProduct()
        {
            return new Product("Product Test", "Ref-test", "Test product", 3, 700, 0, 1, null, 2, DateTime.Today.AddDays(-1), DateTime.Today.AddYears(1), "user.test");
        }
    }
}
