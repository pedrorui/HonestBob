using ServiceStack.Caching;
using ServiceStack.Redis;
using System;
using System.Linq.Expressions;

namespace HonestBobs.Web.Infrastructure
{
    public class RedisCache : ICache, IDisposable
    {
        private readonly ICacheClient cacheClient;

        public RedisCache(string hostName)
        {
            this.cacheClient = new RedisClient(hostName);
        }

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
            this.cacheClient.Add(key, item);
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
            if (disposing)
            {
                this.cacheClient.Dispose();
            }
        }
    }
}