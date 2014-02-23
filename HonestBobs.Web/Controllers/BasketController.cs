using System.Web.Mvc;
using HonestBobs.Business;
using HonestBobs.Web.Infrastructure;

namespace HonestBobs.Web.Controllers
{
    public class BasketController : Controller
    {
	    private readonly ISessionManager sessionManager;

	    public BasketController(ISessionManager sessionManager)
	    {
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