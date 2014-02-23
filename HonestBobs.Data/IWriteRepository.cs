using System.Collections.Generic;
using HonestBobs.Domain;

namespace HonestBobs.Data
{
	/// <summary>
	/// The WriteRepository interface.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	public interface IWriteRepository<in TKey, TEntity> : IReadRepository<TKey, TEntity> where TEntity : Entity<TKey>
	{
		/// <summary>
		/// Add an item to the repository.
		/// </summary>
		/// <param name="item">Item to add.</param>
		/// <returns>
		/// The <see cref="bool" />.
		/// </returns>
		bool Add(TEntity item);

		/// <summary>
		/// Adds or updates an arbitrary number of data items in the repository as appropriate.
		/// </summary>
		/// <param name="items">The items to add or update.</param>
		/// <returns>
		/// The <see cref="bool" />.
		/// </returns>
		bool AddOrUpdate(IEnumerable<TEntity> items);

		/// <summary>
		/// Update an item in the repository.
		/// </summary>
		/// <param name="item">The item to update.</param>
		/// <returns>
		/// The <see cref="bool" />.
		/// </returns>
		bool Update(TEntity item);
	}
}