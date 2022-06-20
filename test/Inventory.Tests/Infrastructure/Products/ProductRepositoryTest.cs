using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Tests.Infrastructure.Products
{
    /// <summary>
    /// Test class to ensure the correct operations in ProductRepository
    /// </summary>
    public class ProductRepositoryTest
    {
        private readonly InventoryContext dbContext;
        private readonly DbContextOptions<InventoryContext> dbContextOptions;
        private readonly ProductRepository productRepository;

        public ProductRepositoryTest()
        {
            //In Memory Database Provider for Testing
            // Build DbContextOptions
            dbContextOptions = new DbContextOptionsBuilder<InventoryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) //use Guid so every test use a different database
                .Options;

            dbContext = new InventoryContext(dbContextOptions);
            productRepository = new ProductRepository(dbContext);
        }


        [Fact]
        public async Task AddProduct_Ok()
        {
            //Variables
            var productToSave = GetProduct();

            // Execute
            var newProduct = await productRepository.AddAsync(productToSave, productToSave.UserCreated);

            //Assert
            Assert.NotNull(newProduct);
            Assert.Equal(productToSave.Name, newProduct.Name);
            Assert.Equal(productToSave.Reference, newProduct.Reference);
            Assert.True(await productRepository.ExistByName(newProduct.Name));
        }


        private Product GetProduct()
        {
            return new Product("Product Test", "Ref-test", "Test product", 3, 700, 0, 1, null, 2, DateTime.Today.AddDays(-1), DateTime.Today.AddYears(1), "user.test");
        }
    }
}
