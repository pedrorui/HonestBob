using System.Collections.Generic;
using System.Web.Mvc;
using HonestBobs.Core;
using HonestBobs.Data;
using HonestBobs.Domain;
using HonestBobs.Web.Infrastructure;

namespace HonestBobs.Web.Controllers.Products
{
	public class MoviesController : Controller
	{
        private readonly ICache cache;
        private readonly IMovieRepository movieRepository;

		public MoviesController(IMovieRepository movieRepository, ICache cache)
		{
			Guard.ArgumentNotNull(movieRepository, "movieRepository");
            Guard.ArgumentNotNull(cache, "cache");

			this.movieRepository = movieRepository;
            this.cache = cache;
		}

		public ActionResult Index()
		{
			IEnumerable<Movie> movies = this.cache.Execute(() => this.movieRepository.FetchAll());
			return this.View(movies);
		}
	}
}