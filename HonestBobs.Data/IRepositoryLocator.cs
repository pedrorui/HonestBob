namespace HonestBobs.Data
{
	/// <summary>
	///     The RepositoryLocator interface.
	/// </summary>
	public interface IRepositoryLocator
	{
		/// <summary>
		/// Gets an instance of the book repository.
		/// </summary>
		/// <value>
		/// The book repository.
		/// </value>
		IBookRepository BookRepository { get; }

		/// <summary>
		/// Gets an instance of the movie repository.
		/// </summary>
		/// <value>
		/// The movie repository.
		/// </value>
		IMovieRepository MovieRepository { get; }

		/// <summary>
		/// Gets an instance of the category repository.
		/// </summary>
		/// <value>
		/// The category repository.
		/// </value>
		ICategoryRepository CategoryRepository { get; }
	}
}