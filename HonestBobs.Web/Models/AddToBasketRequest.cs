namespace HonestBobs.Web.Models
{
	/// <summary>
	/// The request to add a product to the basket using the basket api.
	/// </summary>
	public class AddToBasketRequest
	{
		/// <summary>
		/// Gets or sets the product identifier.
		/// </summary>
		/// <value>
		/// The product identifier.
		/// </value>
		public int ProductId { get; set; }

		/// <summary>
		/// Gets or sets the product type name.
		/// </summary>
		/// <value>
		/// The name of the product type.
		/// </value>
		public string ProductTypeName { get; set; }

		/// <summary>
		/// Gets or sets the quantity of product to add to the basket.
		/// </summary>
		/// <value>
		/// The quantity.
		/// </value>
		public int Quantity { get; set; }
	}
}