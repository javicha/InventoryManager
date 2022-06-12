using Inventory.Domain.Entities;

namespace Inventory.Application.Contracts.Persistence
{
    /// <summary>
    /// Specific contracts for the Product entity
    /// </summary>
    public interface IProductRepository : IAsyncRepository<Product>
    {
        /// <summary>
        /// Remove an item from the inventory by name
        /// </summary>
        /// <param name="name">Exact product name</param>
        /// <returns>True if it has been removed successfully. False otherwise.</returns>
        Task<bool> RemoveByNameAsync(string name);
    }
}
