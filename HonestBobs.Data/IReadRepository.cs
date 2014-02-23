using System.Collections.Generic;
using HonestBobs.Domain;

namespace HonestBobs.Data
{
	/// <summary>
	///     The ReadRepository interface.
	/// </summary>
	/// <typeparam name="TKey">The type of the entity key</typeparam>
	/// <typeparam name="TEntity">The type of the entity</typeparam>
	public interface IReadRepository<in TKey, out TEntity> where TEntity : Entity<TKey>
	{
		/// <summary>
		///     Fetch all items from the repository.
		/// </summary>
		/// <returns>All the entities.</returns>
		IEnumerable<TEntity> FetchAll();

		/// <summary>
		///     Fetch a specific item from the repository.
		/// </summary>
		/// <param name="id">The primary key used to uniquely identify objects in the repository.</param>
		/// <returns>
		///     The <see cref="TEntity" />.
		/// </returns>
		TEntity FetchById(TKey id);
	}
}