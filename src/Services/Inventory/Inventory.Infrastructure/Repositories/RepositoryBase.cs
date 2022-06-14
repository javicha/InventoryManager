using Inventory.Application.Contracts.Persistence;
using Inventory.Domain.Common;
using Inventory.Domain.ExtensionMethods;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Inventory.Infrastructure.Repositories
{
    /// <summary>
    /// Class that contains the implementation of the methods common to all repositories
    /// </summary>
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
    {
        protected readonly InventoryContext _inventoryContext; //Reachable to subclases

        public RepositoryBase(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext ?? throw new ArgumentNullException(nameof(inventoryContext));
        }

        /// <summary>
        /// Save an entity to database
        /// </summary>
        /// <param name="entity">Given entity</param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entity)
        {
            _inventoryContext.Set<T>().Add(entity);
            await _inventoryContext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Removes a given entity from database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(T entity)
        {
            _inventoryContext.Set<T>().Remove(entity);
            await _inventoryContext.SaveChangesAsync();
        }

        /// <summary>
        /// Sof delete for entity. It does not delete the entity from the database. It only sets a deletion date
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        /// <param name="userName">User performing the deletion</param>
        /// <returns></returns>
        public async Task SoftDeleteAsync(T entity, string userName)
        {
            entity.SetDeleteAuditParams(userName);
            await UpdateAsync(entity);
        }

        /// <summary>
        /// Get a list of all entities of a given type
        /// </summary>
        /// <returns>List of all entities</returns>
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _inventoryContext.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Get a list of entities matching a given predicate
        /// </summary>
        /// <param name="predicate">List of entities</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _inventoryContext.Set<T>().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Unique identifier</param>
        /// <returns>The entity with the given identifier</returns>
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _inventoryContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Update a given entity in database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            _inventoryContext.Entry(entity).State = EntityState.Modified;
            await _inventoryContext.SaveChangesAsync();
        }
    }
}
