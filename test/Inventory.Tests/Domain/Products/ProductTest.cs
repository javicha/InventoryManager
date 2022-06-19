using Inventory.Domain.Entities;

namespace Inventory.Tests.Domain.Products
{
    public class ProductTest
    {
        [Fact]
        public void Product_Create()
        {
            var name = "Product Test";
            var reference = "Ref-test";
            var description = "Test product";
            var typeId = 1;
            var basePrice = 700;
            var manufacturerId = 0;
            var numUnits = 1;
            var minStock = 1;
            var supplierId = 2;
            var receiptDate = System.DateTime.Today;
            var expirationDate = System.DateTime.Today.AddYears(3);
            var userCreated = "user.test";

            var product = new Product(name, reference, description, typeId, basePrice,
                manufacturerId, numUnits, minStock, supplierId, receiptDate, expirationDate, userCreated);

            Assert.Equal(name, product.Name);
            Assert.Equal(reference, product.Reference);
            Assert.Equal(description, product.Description);
            Assert.Equal(typeId, product.TypeId);
            Assert.Equal(basePrice, product.BasePrice);
            Assert.Equal(manufacturerId, product.ManufacturerId);
            Assert.Equal(numUnits, product.NumUnits);
            Assert.Equal(minStock, product.MinStock);
            Assert.Equal(supplierId, product.SupplierId);
            Assert.Equal(receiptDate, product.ReceiptDate);
            Assert.Equal(expirationDate, product.ExpirationDate);
            Assert.Equal(userCreated, product.UserCreated);
            Assert.Equal(userCreated, product.UserModified);
        }
    }
}
