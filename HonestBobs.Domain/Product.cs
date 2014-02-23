namespace HonestBobs.Domain
{
	/// <summary>
	/// The base class for a product.
	/// </summary>
	public abstract class Product : Entity<int>
	{
		/// <summary>
		/// Gets or sets the product name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the product description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the product price.
		/// </summary>
		/// <value>
		/// The price.
		/// </value>
		public decimal UnitPrice { get; set; }

		/// <summary>
		/// Gets the name of the product type.
		/// </summary>
		/// <value>
		/// The name of the type.
		/// </value>
		public string TypeName
		{
			get { return this.GetType().Name; }
		}
	}
}