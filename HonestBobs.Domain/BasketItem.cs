namespace HonestBobs.Domain
{
	/// <summary>
	/// Represents a basket item that contains a product, a quantity and a total price for the item.
	/// </summary>
	public class BasketItem
	{
		/// <summary>
		/// Gets or sets the product.
		/// </summary>
		/// <value>
		/// The product.
		/// </value>
		public Product Product { get; set; }

		/// <summary>
		/// Gets or sets the product quantity.
		/// </summary>
		/// <value>
		/// The quantity.
		/// </value>
		public int Quantity { get; set; }

		/// <summary>
		/// Gets the total of this item.
		/// </summary>
		/// <value>
		/// The total.
		/// </value>
		public decimal Total
		{
			get { return this.Product.UnitPrice*this.Quantity; }
		}
	}
}