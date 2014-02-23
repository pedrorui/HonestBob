using System.Collections.Generic;
using System.Web.Mvc;
using HonestBobs.Data;
using HonestBobs.Domain;
using HonestBobs.Web.Infrastructure;

namespace HonestBobs.Web.Controllers
{
	public class HomeController : Controller
	{
		private const string CacheKey = "HomeController_Categories";

		private readonly ICache cache;
		private readonly IRepositoryLocator repositoryLocator;

		/// <summary>
		/// Initializes a new instance of the <see cref="HomeController"/> class.
		/// </summary>
		/// <param name="repositoryLocator">The repository locator.</param>
		/// <param name="cache">The cache.</param>
		public HomeController(IRepositoryLocator repositoryLocator, ICache cache)
		{
			this.repositoryLocator = repositoryLocator;
			this.cache = cache;
		}

		/// <summary>
		/// Displays a list of all available categories.
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			this.ViewBag.Title = "Home Page";

			IEnumerable<Category> categories = this.cache.Execute(() => this.repositoryLocator.CategoryRepository.FetchAll(), CacheKey);
			return this.View(categories);
		}
	}
}