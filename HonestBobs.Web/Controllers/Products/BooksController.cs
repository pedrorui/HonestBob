using System.Collections.Generic;
using System.Web.Mvc;
using HonestBobs.Data;
using HonestBobs.Domain;

namespace HonestBobs.Web.Controllers.Products
{
	public class BooksController : BaseProductController
	{
		public BooksController(IRepositoryLocator repositoryLocator)
			: base(repositoryLocator)
		{
		}

		//
		// GET: /Books/
		public ActionResult Index()
		{
			IEnumerable<Book> books = this.RepositoryLocator.BookRepository.FetchAll();
			return this.View(books);
		}
	}
}