﻿using Inventory.Domain.Common;
using System.Linq.Expressions;

namespace Inventory.Application.Contracts.Persistence
{
	/// <summary>
	/// Generic repository interface for entities of type EntityBase
	/// </summary>
	public interface IAsyncRepository<T> where T : EntityBase
    {
		/// <summary>
		/// For the given entity in the type, get all its records in the database
		/// </summary>
		/// <returns>List with all entities</returns>
		Task<IReadOnlyList<T>> GetAllAsync();

		/// <summary>
		/// For the given entity in the type, get the records that match a certain condition
		/// </summary>
		/// <param name="predicate">Clause used to filter records</param>
		/// <returns>List with all entities filtered</returns>
		Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// For the given entity in the type, get an entity from its unique identifier
		/// </summary>
		/// <param name="id">Entity identifier</param>
		/// <returns>The entity retrieved</returns>
		Task<T?> GetByIdAsync(int id);

		/// <summary>
		/// Persist an entity in the database
		/// </summary>
		/// <param name="entity">Entity to persist in database</param>
		/// <returns>The entity created</returns>
		Task<T> AddAsync(T entity);

		/// <summary>
		/// Update the data of an entity in the database
		/// </summary>
		/// <param name="entity">Entity to update in database</param>
		Task UpdateAsync(T entity);

		/// <summary>
		/// Delete an entity in the database
		/// </summary>
		/// <param name="entity">Entity to delete in database</param>
		/// <returns></returns>
		Task DeleteAsync(T entity);

		/// <summary>
		/// Sof delete for entity. It does not delete the entity from the database. It only sets a deletion date
		/// </summary>
		/// <param name="entity">Entity to remove</param>
		/// <param name="userName">User performing the deletion</param>
		/// <returns></returns>
		Task SoftDeleteAsync(T entity, string userName);
	}
}
