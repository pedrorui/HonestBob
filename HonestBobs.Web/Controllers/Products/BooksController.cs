using System.Collections.Generic;
using System.Web.Mvc;
using HonestBobs.Core;
using HonestBobs.Data;
using HonestBobs.Domain;
using HonestBobs.Web.Infrastructure;

namespace HonestBobs.Web.Controllers.Products
{
	public class BooksController : Controller
	{
        private readonly ICache cache;
        private readonly IBookRepository bookRepository;

		public BooksController(IBookRepository bookRepository, ICache cache)
		{
			Guard.ArgumentNotNull(bookRepository, "bookRepository");
            Guard.ArgumentNotNull(cache, "cache");

            this.bookRepository = bookRepository;
            this.cache = cache;
		}

		//
		// GET: /Books/
		public ActionResult Index()
		{
			IEnumerable<Book> books = this.cache.Execute(() => this.bookRepository.FetchAll());
            return this.View(books);
		}
	}
}