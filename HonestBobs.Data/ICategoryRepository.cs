using HonestBobs.Domain;

namespace HonestBobs.Data
{
	public interface ICategoryRepository : IReadRepository<int, Category>
	{
	}
}