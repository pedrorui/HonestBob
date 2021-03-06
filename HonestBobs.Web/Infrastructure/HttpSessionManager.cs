﻿using System.Web;

namespace HonestBobs.Web.Infrastructure
{
	/// <summary>
	/// Stores and retieves items from the http session.
	/// </summary>
	public class HttpSessionManager : ISessionManager
	{
		private readonly HttpSessionStateBase session;

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpSessionManager" /> class.
		/// </summary>
		public HttpSessionManager()
		{
			var context = new HttpContextWrapper(HttpContext.Current);
			this.session = context.Session;
		}

		/// <summary>
		/// Gets the item from the session, if the item cannot be found then create a new object instance.
		/// </summary>
		/// <typeparam name="T">The type of the item, also used as the session key.</typeparam>
		/// <returns>The item stored on the session.</returns>
		public T GetItem<T>() where T : new()
		{
			return this.GetItem<T>(this.GenerateKey<T>());
		}

		/// <summary>
		/// Gets the item from the session.
		/// </summary>
		/// <typeparam name="T">The type of item to return.</typeparam>
		/// <param name="key">The key.</param>
		/// <returns>The item stored on the session.</returns>
		public T GetItem<T>(string key) where T : new()
		{
			if (this.session == null)
			{
				return new T();
			}

			object item = this.session[key];

			if (item != null)
			{
				return (T)item;
			}

			return new T();
		}

		/// <summary>
		/// Persists the item into the session.
		/// </summary>
		/// <typeparam name="T">The type of the item.</typeparam>
		/// <param name="item">The item.</param>
		/// <returns>true if persisted, otherwise false.</returns>
		public bool PersistItem<T>(T item)
		{
			return this.PersistItem(item, this.GenerateKey<T>());
		}

		/// <summary>
		/// Persists the item into the session.
		/// </summary>
		/// <typeparam name="T">The type of the item.</typeparam>
		/// <param name="item">The item.</param>
		/// <param name="key">The key.</param>
		/// <returns>true if persisted, otherwise false.</returns>
		public bool PersistItem<T>(T item, string key)
		{
			if (this.session == null)
			{
				return false;
			}

			this.session[key] = item;
			return true;
		}

        private string GenerateKey<T>()
        {
            return typeof(T).FullName;
        }
	}
}