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

        private static Injected<IOrderRepository> _orderRepository;
        private static Injected<IContentLoader> _contentLoader;
        private static Injected<ReferenceConverter> _referenceConverter;
        private static Injected<ILineItemValidator> _lineItemValidator;
        private static Injected<IPlacedPriceProcessor> _placedPriceProcessor;

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetCart()
        {
            var customerId = CustomerContext.Current.CurrentContactId;

            if (customerId == null)
                return NotFound();

            var cart = _orderRepository.Service.LoadOrCreateCart<ICart>(customerId, DEFAULT_CART_NAME);

            var lineItems = cart.GetAllLineItems().ToList();
            var total = cart.GetTotal();
            var currency = total.Currency.CurrencyCode;

            var cartResponse = new
            {
                CustomerId = cart.CustomerId,

                //rs: for each line item, create a new anonymous (simplied) object
                Items = lineItems
                            .Where(item => item.Quantity > 0)
                            .Select(item =>
                                new
                                {
                                    Code = item.Code,
                                    DisplayName = item.DisplayName,
                                    PlacedPrice = item.PlacedPrice,
                                    Quantity = item.Quantity

                                }).ToList(),

                TotalItems = lineItems.Count,
                ShippingTotal = cart.GetShippingTotal().Amount,
                TaxTotal = cart.GetTaxTotal().Amount,
                SubTotal = cart.GetSubTotal().Amount,
                Total = total.Amount,
                Currency = currency
            };

            return Ok(cartResponse);
        }

        [HttpDelete]
        [Route("")]
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

        [HttpGet]
        [Route("update/{skuCode}/{quantityToUpdate}")]
        public IHttpActionResult AddOrUpdateToCart(string skuCode, int quantityToUpdate = 1)
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
                newLineItem.Quantity = quantityToUpdate;
                cart.AddLineItem(newLineItem);
            }

            else
            {
                var shipment = cart.GetFirstShipment();
                cart.UpdateLineItemQuantity(shipment, existingLineItem, quantityToUpdate);
            }

            var validationIssues = new Dictionary<ILineItem, List<ValidationIssue>>();

            cart.ValidateOrRemoveLineItems((item, issue) =>
                validationIssues.AddValidationIssues(item, issue), _lineItemValidator.Service);
        
            cart.UpdatePlacedPriceOrRemoveLineItems(CustomerContext.Current.GetContactById(cart.CustomerId), (item, issue) => validationIssues.AddValidationIssues(item, issue),
                _placedPriceProcessor.Service);

            _orderRepository.Service.Save(cart);
            return Ok(cart);
        }

        private void MethodToCallOnLineItem(ILineItem lineItem)
        {
            return;
        }
    }
}
