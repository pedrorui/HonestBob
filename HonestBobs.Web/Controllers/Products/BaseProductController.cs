using System.Web.Mvc;
using HonestBobs.Data;

namespace HonestBobs.Web.Controllers.Products
{
	public abstract class BaseProductController : Controller
	{
		protected readonly IRepositoryLocator RepositoryLocator;

		protected BaseProductController(IRepositoryLocator repositoryLocator)
		{
			this.RepositoryLocator = repositoryLocator;
		}
	}
}