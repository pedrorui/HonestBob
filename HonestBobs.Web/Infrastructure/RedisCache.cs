using ServiceStack.Caching;
using ServiceStack.Redis;
using System;
using System.Linq.Expressions;

namespace HonestBobs.Web.Infrastructure
{
    /// <summary>
    /// Implements cache using a redis client.
    /// </summary>
    public class RedisCache : ICache, IDisposable
    {
        private readonly ICacheClient cacheClient;
        private bool disposed = false;
       
        public RedisCache(string hostName)
        {
            this.cacheClient = new RedisClient(hostName);
            this.AbsoluteExpiration = new TimeSpan(0, 10, 0);
        }

        /// <summary>
        /// Gets or sets the cache absolute expiration interval.
        /// </summary>
        public TimeSpan AbsoluteExpiration { get; set; }

        public T Execute<T>(Func<T> function, string key)
        {
            T item = this.cacheClient.Get<T>(key);
            if (item != null)
            {
                return item;
            }

            T result = function();
            this.Add(result, key);

            return result;
        }

        public T Execute<T>(Expression<Func<T>> expression)
        {
            return this.Execute(expression.Compile(), expression.ToString());
        }

        public void Remove(string key)
        {
            this.cacheClient.Remove(key);
        }

        public void Reset()
        {
            this.cacheClient.FlushAll();
        }

        private void Add<T>(T item, string key)
        {
            var expiresAt = DateTime.Now.Add(this.AbsoluteExpiration);
            this.cacheClient.Set(key, item, expiresAt);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RedisCache()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.cacheClient.Dispose();                
            }

            this.disposed = true;
        }
    }
}