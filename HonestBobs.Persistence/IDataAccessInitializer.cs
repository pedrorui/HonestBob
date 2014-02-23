namespace HonestBobs.Persistence
{
	/// <summary>
	/// Definies the required properties to initialize a DataAccess object.
	/// </summary>
	public interface IDataAccessInitializer
	{
		/// <summary>
		/// Gets the connection string.
		/// </summary>
		/// <value>
		/// The connection string.
		/// </value>
		string ConnectionString { get; }
	}
}