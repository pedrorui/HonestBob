namespace HonestBobs.Persistence.Infrastructure
{
	/// <summary>
	/// The base repository.
	/// </summary>
	public abstract class BaseRepository
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BaseRepository" /> class.
		/// </summary>
		/// <param name="dataAccessProvider">The data access.</param>
		protected BaseRepository(IDataAccessProvider dataAccessProvider)
		{
			this.DataAccessProvider = dataAccessProvider;
		}

		/// <summary>
		/// Gets the data access.
		/// </summary>
		/// <value>
		/// The data access.
		/// </value>
		protected IDataAccessProvider DataAccessProvider { get; private set; }
	}
}