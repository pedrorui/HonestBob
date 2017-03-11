using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Caching;

namespace HonestBobs.Web.Infrastructure
{
    /// <summary>
    /// Implements cache using the native HttpRuntime cache.
    /// </summary>
    public class HttpCache : ICache
    {
        private TimeSpan absoluteExpiration;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpCache" /> class.
        /// </summary>
        public HttpCache(HttpCacheConfiguration configuration)
        {
            this.absoluteExpiration = configuration.TimeToLive;
        }

        /// <summary>
        /// Executes the specified function, caching the result.
        /// </summary>
        /// <typeparam name="T">The type of the method return.</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="key">The key to identify the stored item.</param>
        /// <returns>The function execution result.</returns>
        public T Execute<T>(Func<T> function, string key)
        {
            object item = HttpRuntime.Cache[key];
            if (item != null)
            {
                return (T)item;
            }

            T result = function();
            this.Add(result, key);

            return result;
        }

        /// <summary>
        /// Executes the specified expression, caching the result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression to execute.</param>
        /// <returns>The execution result</returns>
        public T Execute<T>(Expression<Func<T>> expression)
        {
            return this.Execute(expression.Compile(), expression.ToString());
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

            foreach (string cacheKey in cacheKeysToRemove)
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
            HttpRuntime.Cache.Insert(key, item, null, DateTime.Now.AddMinutes(this.absoluteExpiration.Minutes), Cache.NoSlidingExpiration);
        }
    }
}