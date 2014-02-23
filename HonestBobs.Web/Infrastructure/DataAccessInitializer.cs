using System.Configuration;
using HonestBobs.Persistence;

namespace HonestBobs.Web.Infrastructure
{
	public class DataAccessInitializer : IDataAccessInitializer
	{
		public string ConnectionString
		{
			get { return ConfigurationManager.ConnectionStrings["HonestBobs"].ConnectionString; }
		}
	}
}