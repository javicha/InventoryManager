﻿using Inventory.Domain.Entities;

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
        Task<Product> GetByName(string name);

        /// <summary>
        /// Check if a product exists from its name
        /// </summary>
        /// <param name="name">Exact name of the product</param>
        /// <returns>True if the product exists. False otherwise</returns>
        Task<bool> ExistByName(string name);
    }
}