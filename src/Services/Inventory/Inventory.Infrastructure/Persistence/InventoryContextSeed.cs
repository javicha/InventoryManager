using Inventory.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Inventory.Infrastructure.Persistence
{
    /// <summary>
    /// Class to insert some test products into the inventory at application startup
    /// </summary>
    public class InventoryContextSeed
    {
        public static async Task SeedAsync(InventoryContext inventoryContext, ILogger<InventoryContextSeed> logger)
        {
            if (!inventoryContext.Products.Any())
            {
                inventoryContext.Products.AddRange(GetTestProducts());
                await inventoryContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(InventoryContext).Name);
            }
        }

        private static IEnumerable<Product> GetTestProducts()
        {
            return new List<Product>
            {
                new Product("Kit de primers1", "Ref-9874S", "Kit de primers para secuenciación por técnica Sanger", 1, 400, 1, 5, null, 2, DateTime.Today, DateTime.Today.AddYears(1), "javier.val"),
                new Product("Sondas MLPA", "S-665T", "Sondas para para captura de amplicón por técnica MLPA", 2, null, 2, 2, 1, 0, DateTime.Today, DateTime.Today.AddYears(2), "javier.val"),
                new Product("Primer TTN", "Pr-TTN-01", "Primers FW y RV para fragmento TTN", 0, 50, 0, 1, null, 2, DateTime.Today.AddDays(-1), DateTime.Today.AddYears(1), "javier.val"),
                new Product("Kit de barcodes", "B-55TR", "Kit de barcodes para captura de SNPs", 3, 700, 0, 1, null, 2, DateTime.Today.AddDays(-1), DateTime.Today.AddYears(2), "javier.val")        
            };
        }
    }
}
