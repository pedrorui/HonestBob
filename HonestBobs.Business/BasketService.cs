using System.Collections.Generic;
using System.Linq;
using HonestBobs.Domain;

namespace HonestBobs.Business
{
	/// <summary>
	/// The service to interact with the basket, persist the basket is an option.
	/// </summary>
	public class BasketService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BasketService"/> class.
		/// </summary>
		public BasketService()
		{
			if (this.Basket == null)
			{
				this.Basket = new Basket();
			}
		}

		/// <summary>
		/// Gets the basket.
		/// </summary>
		/// <value>
		/// The basket.
		/// </value>
		public Basket Basket { get; private set; }

		/// <summary>
		/// Adds the product to the basket.
		/// </summary>
		/// <param name="product">The product.</param>
		/// <param name="quantity">The quantity.</param>
		/// <returns></returns>
		public BasketService AddToBasket(Product product, int quantity)
		{
			return this.AddToBasket(new BasketItem
			{
				Product = product,
				Quantity = quantity
			});
		}

		/// <summary>
		/// Adds the basket item to the basket.
		/// </summary>
		/// <param name="basketItem">The basket item.</param>
		/// <returns></returns>
		public BasketService AddToBasket(BasketItem basketItem)
		{
			var existingbasketItem = this.Basket.Items.FirstOrDefault(item => item.Product.Id == basketItem.Product.Id);
			if (existingbasketItem == null)
			{
				this.Basket.Items.Add(basketItem);
			}
			else
			{
				existingbasketItem.Quantity += basketItem.Quantity;
			}
			return this;
		}

		/// <summary>
		/// Clears all basket items.
		/// </summary>
		/// <returns></returns>
		public BasketService Empty()
		{
			this.Basket.Items.Clear();
			return this;
		}
	}
}