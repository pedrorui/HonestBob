using System.Collections.Generic;
using System.Web.Mvc;
using HonestBobs.Data;
using HonestBobs.Domain;

namespace HonestBobs.Web.Controllers.Products
{
	public class MoviesController : BaseProductController
	{
		public MoviesController(IRepositoryLocator repositoryLocator)
			: base(repositoryLocator)
		{
		}

		public ActionResult Index()
		{
			IEnumerable<Movie> movies = this.RepositoryLocator.MovieRepository.FetchAll();
			return this.View(movies);
		}
	}
}