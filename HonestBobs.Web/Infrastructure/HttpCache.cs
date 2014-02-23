using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace HonestBobs.Web.Infrastructure
{
	/// <summary>
	/// Implements cache using the native HttPRuntime cache.
	/// </summary>
	public class HttpCache : ICache
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HttpCache"/> class.
		/// </summary>
		public HttpCache()
		{
			this.AbsoluteExpirationMinutes = 10;
		}

		/// <summary>
		/// Gets or sets the cache absolute expiration interval in minutes.
		/// </summary>
		/// <value>
		/// The absolute expiration interval.
		/// </value>
		public int AbsoluteExpirationMinutes { get; set; }

		/// <summary>
		/// Executes the specified function, caching the result.
		/// </summary>
		/// <typeparam name="T">The type of the method return.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="key">The key.</param>
		/// <returns>
		/// The function execution result.
		/// </returns>
		public T Execute<T>(Func<T> function, string key)
		{
			object item = HttpRuntime.Cache[key];
			if (item != null)
			{
				return (T) item;
			}

			T result = function();
			this.Add(result, key);

			return result;
		}

		/// <summary>
		/// Removes the object refered by the specified key from the cache.
		/// </summary>
		/// <param name="key">The key.</param>
		public void Remove(string key)
		{
			HttpRuntime.Cache.Remove(key);
		}

		/// <summary>
		/// Resets the cache.
		/// </summary>
		public void Reset()
		{
			IEnumerable<string> cacheKeysToRemove = HttpRuntime.Cache.Cast<DictionaryEntry>().Select(cacheItem => cacheItem.Key.ToString());

			foreach (var cacheKey in cacheKeysToRemove)
			{
				this.Remove(cacheKey);
			}
		}

		/// <summary>
		/// Adds the specified item to the cache.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item">The item.</param>
		/// <param name="key">The key.</param>
		private void Add<T>(T item, string key)
		{
			HttpRuntime.Cache.Insert(key, item, null, DateTime.Now.AddMinutes(this.AbsoluteExpirationMinutes), Cache.NoSlidingExpiration);
		}
	}
}