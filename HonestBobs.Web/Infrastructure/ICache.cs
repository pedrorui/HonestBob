using System;

namespace HonestBobs.Web.Infrastructure
{
	/// <summary>
	/// Definitions for cache.
	/// </summary>
	public interface ICache
	{
		/// <summary>
		/// Executes the specified function, caching the result.
		/// </summary>
		/// <typeparam name="T">The type of the method return.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="key">The key.</param>
		/// <returns>The function execution result.</returns>
		T Execute<T>(Func<T> function, string key);

		/// <summary>
		/// Removes the object refered by the specified key from the cache.
		/// </summary>
		/// <param name="key">The key.</param>
		void Remove(string key);

		/// <summary>
		/// Resets the cache.
		/// </summary>
		void Reset();
	}
}