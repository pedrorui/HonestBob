namespace HonestBobs.Domain
{
	public class Book : Product
	{
		public string Author { get; set; }

		public string Isbn { get; set; }

		public int Pages { get; set; }
	}
}