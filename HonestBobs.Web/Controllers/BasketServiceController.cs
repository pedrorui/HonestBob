using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.SessionState;
using HonestBobs.Business;
using HonestBobs.Data;
using HonestBobs.Web.Infrastructure;
using HonestBobs.Web.Models;

namespace HonestBobs.Web.Controllers
{
	public class BasketServiceController : ApiController, IRequiresSessionState
	{
		private readonly IRepositoryLocator repositoryLocator;

		private readonly ISessionManager sessionManager;

		public BasketServiceController(IRepositoryLocator repositoryLocator, ISessionManager sessionManager)
		{
			this.repositoryLocator = repositoryLocator;
			this.sessionManager = sessionManager;
		}

		public string Get()
		{
			return "Alive";
		}

		public HttpResponseMessage Post(AddToBasketRequest request)
		{
			var basketService = this.sessionManager.GetItem<BasketService>();
			var product = new ProductLocatorService(this.repositoryLocator).FindProduct(request.ProductId, request.ProductTypeName);

			if (product == null)
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}

			basketService.AddToBasket(product, request.Quantity);
			this.sessionManager.PersistItem(basketService);

			return new HttpResponseMessage(HttpStatusCode.OK);
		}
	}
}