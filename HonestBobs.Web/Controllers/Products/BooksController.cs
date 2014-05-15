using System.Collections.Generic;
using System.Web.Mvc;
using HonestBobs.Core;
using HonestBobs.Data;
using HonestBobs.Domain;

namespace HonestBobs.Web.Controllers.Products
{
	public class BooksController : Controller
	{
		private readonly IBookRepository bookRepository;

		public BooksController(IBookRepository bookRepository)
		{
			Guard.ArgumentNotNull(bookRepository, "bookRepository");
			this.bookRepository = bookRepository;
		}

		//
		// GET: /Books/
		public ActionResult Index()
		{
			IEnumerable<Book> books = this.bookRepository.FetchAll();
			return this.View(books);
		}
	}
}