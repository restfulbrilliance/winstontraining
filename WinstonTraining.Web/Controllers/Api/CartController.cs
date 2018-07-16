using EPiServer.Commerce.Order;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WinstonTraining.Web.Controllers.Api
{
    [RoutePrefix("api/cart")]
    public class CartController : ApiController
    {
        private const string DEFAULT_CART_NAME = "Default";
        private const string WISHLIST_NAME = "Wishlist";

        private static Injected<IOrderRepository> _orderRepository { get; set; }

        [HttpGet]
        public IHttpActionResult GetCart()
        {
            var customerId = CustomerContext.Current.CurrentContactId;

            if (customerId == null)
                return NotFound();

            var cart = _orderRepository.Service.LoadOrCreateCart<ICart>(customerId, DEFAULT_CART_NAME);
            return Ok(cart);
        }
    }
}
