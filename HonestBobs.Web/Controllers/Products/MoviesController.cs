using System.Collections.Generic;
using System.Web.Mvc;
using HonestBobs.Core;
using HonestBobs.Data;
using HonestBobs.Domain;

namespace HonestBobs.Web.Controllers.Products
{
	public class MoviesController : Controller
	{
		private readonly IMovieRepository movieRepository;

		public MoviesController(IMovieRepository movieRepository)
		{
			Guard.ArgumentNotNull(movieRepository, "movieRepository");
			this.movieRepository = movieRepository;
		}

		public ActionResult Index()
		{
			IEnumerable<Movie> movies = this.movieRepository.FetchAll();
			return this.View(movies);
		}
	}
}