using HonestBobs.Data;

namespace HonestBobs.Business
{
    public interface IProductRepositoryLocator
    {
        IBookRepository BookRepository { get; }

        IMovieRepository MovieRepository { get; }
    }
}
