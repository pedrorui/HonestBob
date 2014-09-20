using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.SessionState;
using HonestBobs.Business;
using HonestBobs.Core;
using HonestBobs.Domain;
using HonestBobs.Web.Infrastructure;
using HonestBobs.Web.Models;

namespace HonestBobs.Web.Controllers
{
	public class BasketServiceController : ApiController, IRequiresSessionState
	{
		private readonly IProductRepositoryLocator productRepositoryLocator;

		private readonly ISessionManager sessionManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="BasketServiceController" /> class.
		/// </summary>
		/// <param name="productRepositoryLocator">The repository locator.</param>
		/// <param name="sessionManager">The session manager.</param>
		public BasketServiceController(IProductRepositoryLocator productRepositoryLocator, ISessionManager sessionManager)
		{
			Guard.ArgumentNotNull(productRepositoryLocator, "productRepositoryLocator");
			Guard.ArgumentNotNull(sessionManager, "sessionManager");

			this.productRepositoryLocator = productRepositoryLocator;
			this.sessionManager = sessionManager;
		}

		public string Get()
		{
			return "Alive";
		}

		public HttpResponseMessage Post(AddToBasketRequest request)
		{
			var basketService = this.sessionManager.GetItem<BasketService>();
			Product product = new ProductLocatorService(this.productRepositoryLocator).FindProduct(request.ProductId, request.ProductTypeName);

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