using System;
using System.Collections.Generic;
using HonestBobs.Data;
using HonestBobs.Domain;

namespace HonestBobs.Business
{
	/// <summary>
	/// Locates a product on the catalogue and loads it.
	/// </summary>
	public class ProductLocatorService
	{
		private readonly IDictionary<string, Func<int, Product>> productJumpTable = new Dictionary<string, Func<int, Product>>();

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductLocatorService"/> class.
		/// </summary>
		/// <param name="repositoryLocator">The repository locator.</param>
		public ProductLocatorService(IRepositoryLocator repositoryLocator)
		{
			this.productJumpTable.Add(typeof(Book).Name, repositoryLocator.BookRepository.FetchById);
			this.productJumpTable.Add(typeof(Movie).Name, repositoryLocator.MovieRepository.FetchById);
		}

		/// <summary>
		/// Finds the product using the product id and type.
		/// </summary>
		/// <param name="productId">The product identifier.</param>
		/// <param name="productTypeName">Name of the product type.</param>
		/// <returns>The product object.</returns>
		public Product FindProduct(int productId, string productTypeName)
		{
			return this.productJumpTable[productTypeName](productId);
		}

		/// <summary>
		/// Finds the product by type and id.
		/// </summary>
		/// <typeparam name="T">The product type.</typeparam>
		/// <param name="productId">The product identifier.</param>
		/// <returns>The product object.</returns>
		public T FindProduct<T>(int productId) where T : Product
		{
			return (T)this.FindProduct(productId, typeof(T).Name);
		}
	}
}