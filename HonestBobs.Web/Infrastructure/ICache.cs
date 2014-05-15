using System;
using System.Linq.Expressions;

namespace HonestBobs.Web.Infrastructure
{
	/// <summary>
	/// Definitions for a cache.
	/// </summary>
	public interface ICache
	{
		/// <summary>
		/// Executes the specified function, caching the result.
		/// </summary>
		/// <typeparam name="T">The type of the method return.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="key">The key to identify the stored item.</param>
		/// <returns>The function execution result.</returns>
		T Execute<T>(Func<T> function, string key);

		/// <summary>
		/// Executes the specified expression.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		T Execute<T>(Expression<Func<T>> expression);

		/// <summary>
		/// Removes the object refered by the specified key from the cache.
		/// </summary>
		/// <param name="key">The key t oidentify the cache item.</param>
		void Remove(string key);

		/// <summary>
		/// Resets the cache.
		/// </summary>
		void Reset();
	}
}