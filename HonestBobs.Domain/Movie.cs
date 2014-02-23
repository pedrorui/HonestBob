namespace HonestBobs.Domain
{
	public class Movie : Product
	{
		public string Format { get; set; }

		public int PackageUnits { get; set; }

		public string Studio { get; set; }
	}
}