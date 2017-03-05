using System.Collections.Generic;
using System.Linq;

namespace HonestBobs.Domain
{
	/// <summary>
	/// Represents a basket for a user.
	/// </summary>
	public class Basket
	{
        public Basket()
        {
            this.Items = new LinkedList<BasketItem>();
        }

		/// <summary>
		/// Gets or sets the basket items.
		/// </summary>
		/// <value>
		/// The items.
		/// </value>
		public ICollection<BasketItem> Items { get; private set; }

		/// <summary>
		/// Gets the total of the basket.
		/// </summary>
		/// <value>
		/// The total.
		/// </value>
		public decimal Total
		{
			get { return this.Items.Any() ? this.Items.Sum(basketItem => basketItem.Total) : 0; }
		}
	}
}