namespace HonestBobs.Web.Infrastructure
{
	public interface ISessionManager
	{
		/// <summary>
		/// Gets the item from the session, if the item cannot be found then create a new object instance.
		/// </summary>
		/// <typeparam name="T">The type of the item, also used as the session key.</typeparam>
		/// <returns>The item.</returns>
		T GetItem<T>() where T : new();

		/// <summary>
		/// Gets the item from the session, if the item cannot be found then create a new object instance.
		/// </summary>
		/// <typeparam name="T">The type of item to return.</typeparam>
		/// <param name="key">The key.</param>
		/// <returns>
		/// The item.
		/// </returns>
		T GetItem<T>(string key) where T : new();

		/// <summary>
		/// Persists the item into the session.
		/// </summary>
		/// <typeparam name="T">The type of the item.</typeparam>
		/// <param name="item">The item.</param>
		/// <returns>
		/// true if persisted, otherwise false.
		/// </returns>
		bool PersistItem<T>(T item);

		/// <summary>
		/// Persists the item into the session.
		/// </summary>
		/// <typeparam name="T">The type of the item.</typeparam>
		/// <param name="item">The item.</param>
		/// <param name="key">The key.</param>
		/// <returns>
		/// true if persisted, otherwise false.
		/// </returns>
		bool PersistItem<T>(T item, string key);
	}
}