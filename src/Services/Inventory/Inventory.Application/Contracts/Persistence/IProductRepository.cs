using Inventory.Domain.Entities;

namespace Inventory.Application.Contracts.Persistence
{
    /// <summary>
    /// Specific contracts for the Product entity
    /// </summary>
    public interface IProductRepository : IAsyncRepository<Product>
    {
        /// <summary>
        /// Get a product from the inventory based on its exact name
        /// </summary>
        /// <param name="name">Exact name of the product</param>
        /// <returns>The product, if exists. Null otherwise</returns>
        Task<Product?> GetByName(string name);

        /// <summary>
        /// Check if a product exists from its name
        /// </summary>
        /// <param name="name">Exact name of the product</param>
        /// <returns>True if the product exists. False otherwise</returns>
        Task<bool> ExistByName(string name);

        /// <summary>
        /// Get the paginated list of all the products in the inventory
        /// </summary>
        /// <param name="startIndex">Number of page to retrieve</param>
        /// <param name="count">Size of page</param>
        /// <param name="text">Text to filter</param>
        /// <returns></returns>
        Task<Tuple<List<Product>, int>> GetAllProductsPagAsync(int startIndex, int count, string? text);

        /// <summary>
        /// Method that searches for products that have expired in a given date range.
        /// </summary>
        /// <param name="dateFrom">Search start date</param>
        /// <param name="dateTo">Search end date</param>
        /// <returns></returns>
        Task<List<Product>> GetProductsExpiredByDateAsync(DateTime dateFrom, DateTime dateTo);
    }
}
