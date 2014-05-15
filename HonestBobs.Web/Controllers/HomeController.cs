using System.Collections.Generic;
using System.Web.Mvc;
using HonestBobs.Core;
using HonestBobs.Data;
using HonestBobs.Domain;
using HonestBobs.Web.Infrastructure;

namespace HonestBobs.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ICache cache;
		private readonly ICategoryRepository categoryRepository;

		/// <summary>
		///     Initializes a new instance of the <see cref="HomeController" /> class.
		/// </summary>
		/// <param name="categoryRepository">The category repository.</param>
		/// <param name="cache">The cache.</param>
		public HomeController(ICategoryRepository categoryRepository, ICache cache)
		{
			Guard.ArgumentNotNull(categoryRepository, "categoryRepository");
			Guard.ArgumentNotNull(cache, "cache");

			this.categoryRepository = categoryRepository;
			this.cache = cache;
		}

		/// <summary>
		///     Displays a list of all available categories.
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			this.ViewBag.Title = "Home Page";

			//IEnumerable<Category> categories = this.cache.Execute(() => this.categoryRepository.FetchAll(), CacheKey);
			IEnumerable<Category> categories = this.cache.Execute(() => this.categoryRepository.FetchAll());
			return this.View(categories);
		}
	}
}