using EPiServer;
using EPiServer.Commerce.Order;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WinstonTraining.Core.Models.Commerce;
using WinstonTraining.Core.Utilities.CartExtensions;

namespace WinstonTraining.Web.Controllers.Api
{
    [RoutePrefix("api/cart")]
    public class CartController : ApiController
    {
        private const string DEFAULT_CART_NAME = "Default";
        private const string WISHLIST_NAME = "Wishlist";

        private static Injected<IOrderRepository> _orderRepository { get; set; }
        private static Injected<IContentLoader> _contentLoader { get; set; }
        private static Injected<ReferenceConverter> _referenceConverter { get; set; }
        private static Injected<ILineItemValidator> _lineItemValidator { get; set; }
        private static Injected<IPlacedPriceProcessor> _placedPriceProcessor { get; set; }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetCart()
        {
            var customerId = CustomerContext.Current.CurrentContactId;

            if (customerId == null)
                return NotFound();

            var cart = _orderRepository.Service.LoadOrCreateCart<ICart>(customerId, DEFAULT_CART_NAME);
            return Ok(cart);
        }

        [HttpGet]
        [Route("add/{skuCode}/{quantityToAdd}")]
        public IHttpActionResult AddToCart(string skuCode, int quantityToAdd = 1)
        {
            var customerId = CustomerContext.Current.CurrentContactId;

            if (customerId == null)
                return NotFound();

            var contentRefToSku = _referenceConverter.Service.GetContentLink(skuCode);

            if (ContentReference.IsNullOrEmpty(contentRefToSku))
                return NotFound();

            if (!_contentLoader.Service.TryGet<Sku>(contentRefToSku, out Sku skuToAdd))
                return NotFound();

            var cart = _orderRepository.Service.LoadOrCreateCart<ICart>(customerId, DEFAULT_CART_NAME);

            var existingLineItem = cart.GetAllLineItems().FirstOrDefault(li => li.Code.Equals(skuCode, StringComparison.InvariantCultureIgnoreCase));

            // We don't already have the SKU in the cart
            if(existingLineItem == null)
            {
                var newLineItem = cart.CreateLineItem(skuCode);
                newLineItem.DisplayName = skuToAdd.DisplayName;
                newLineItem.Quantity = quantityToAdd;
                cart.AddLineItem(newLineItem);
            }

            else
            {
                var shipment = cart.GetFirstShipment();
                cart.UpdateLineItemQuantity(shipment, existingLineItem, existingLineItem.Quantity + quantityToAdd);
            }

            var validationIssues = new Dictionary<ILineItem, List<ValidationIssue>>();

            cart.ValidateOrRemoveLineItems((item, issue) =>
                validationIssues.AddValidationIssues(item, issue), _lineItemValidator.Service);
        
            cart.UpdatePlacedPriceOrRemoveLineItems(CustomerContext.Current.GetContactById(cart.CustomerId), (item, issue) => validationIssues.AddValidationIssues(item, issue),
                _placedPriceProcessor.Service);

            _orderRepository.Service.Save(cart);
            return Ok(cart);
        }

        [HttpDelete]
        [Route("clear")]
        public IHttpActionResult ClearCart()
        {
            var customerId = CustomerContext.Current.CurrentContactId;

            if (customerId == null)
                return NotFound();

            var cart = _orderRepository.Service.LoadOrCreateCart<ICart>(customerId, DEFAULT_CART_NAME);

            var lineItemsInFirstShipment = cart.GetFirstShipment().LineItems;

            if(lineItemsInFirstShipment != null && lineItemsInFirstShipment.Count > 0)
            {
                cart.GetFirstShipment().LineItems.Clear();
                _orderRepository.Service.Save(cart);
            }

            return Ok();
        }
    }
}
