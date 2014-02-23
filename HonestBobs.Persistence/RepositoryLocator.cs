using HonestBobs.Data;

namespace HonestBobs.Persistence
{
	/// <summary>
	/// The implementation for the repository locator interface.
	/// </summary>
	public class RepositoryLocator : IRepositoryLocator
	{
		/// <summary>
		/// The data access class.
		/// </summary>
		private readonly IDataAccessProvider dataAccessProvider;

		/// <summary>
		/// Initializes a new instance of the <see cref="RepositoryLocator"/> class.
		/// </summary>
		/// <param name="dataAccessProvider">The data access.</param>
		public RepositoryLocator(IDataAccessProvider dataAccessProvider)
		{
			this.dataAccessProvider = dataAccessProvider;
		}

		/// <summary>
		/// Gets an instance of the book repository.
		/// </summary>
		/// <value>
		/// The book repository.
		/// </value>
		public IBookRepository BookRepository
		{
			get
			{
				return new BookRepository(this.dataAccessProvider);
			}
		}

		/// <summary>
		/// Gets an instance of the movie repository.
		/// </summary>
		/// <value>
		/// The movie repository.
		/// </value>
		public IMovieRepository MovieRepository
		{
			get
			{
				return new MovieRepository(this.dataAccessProvider);
			}
		}

		/// <summary>
		/// Gets an instance of the category repository.
		/// </summary>
		/// <value>
		/// The category repository.
		/// </value>
		public ICategoryRepository CategoryRepository
		{
			get
			{
				return new CategoryRepository(this.dataAccessProvider);
			}
		}
	}
}