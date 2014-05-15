using System.Web.Mvc;
using HonestBobs.Business;
using HonestBobs.Core;
using HonestBobs.Web.Infrastructure;

namespace HonestBobs.Web.Controllers
{
	public class BasketController : Controller
	{
		private readonly ISessionManager sessionManager;

		/// <summary>
		///     Initializes a new instance of the <see cref="BasketController" /> class.
		/// </summary>
		/// <param name="sessionManager">The session manager.</param>
		public BasketController(ISessionManager sessionManager)
		{
			Guard.ArgumentNotNull(sessionManager, "sessionManager");
			this.sessionManager = sessionManager;
		}

		//
		// GET: /Basket/
		public ActionResult Index()
		{
			var basketService = this.sessionManager.GetItem<BasketService>();
			return View(basketService.Basket);
		}
	}
}