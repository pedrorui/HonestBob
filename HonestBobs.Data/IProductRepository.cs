using System.Collections.Generic;
using HonestBobs.Domain;

namespace HonestBobs.Data
{
	public interface IProductRepository<TProduct> : IReadRepository<int, TProduct> where TProduct : Product
	{
		IList<TProduct> GetProductsByCategory(Category category);
	}
}