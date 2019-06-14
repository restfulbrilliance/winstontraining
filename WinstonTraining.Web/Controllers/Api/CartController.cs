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
        private static Injected<IInventoryProcessor> _inventoryProcessor;

        class CartApiModel
        {
            public string CustomerId { get; set; }
            public List<LineItemApiModel> Items { get; set; }
            public int TotalItems { get; set; }
            public decimal ShippingTotal { get; set; }
            public decimal ShippingSubTotal { get; set; }
            public decimal ShippingDiscountTotal{ get; set; }
            public decimal TaxTotal { get; set; }
            public decimal SubTotal { get; set; }
            public decimal Total { get; set; }
            public string Currency { get; set; }

            public CartApiModel(ICart epiCart)
            {
                CustomerId = epiCart.CustomerId.ToString();
                Items = epiCart.GetAllLineItems().Select(epiLi => new LineItemApiModel(epiLi)).ToList();
                TotalItems = Items.Count;
                ShippingTotal = epiCart.GetShippingTotal().Amount;
                ShippingSubTotal = epiCart.GetShippingSubTotal().Amount;
                ShippingDiscountTotal = epiCart.GetShippingDiscountTotal().Amount;
                TaxTotal = epiCart.GetTaxTotal().Amount;
                SubTotal = epiCart.GetSubTotal().Amount;
                Total = epiCart.GetTotal().Amount;
                Currency = epiCart.Currency.CurrencyCode;
            }
        }

        class LineItemApiModel
        {
            public LineItemApiModel(ILineItem epiLineItem)
            {

                Code = epiLineItem.Code;
                DisplayName = epiLineItem.DisplayName;
                PlacedPrice = epiLineItem.PlacedPrice;
                Quantity = epiLineItem.Quantity;
            }

            public string Code { get; set; }
            public string DisplayName { get; set; }
            public decimal PlacedPrice { get; set; }
            public decimal Quantity { get; set; }
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetCart()
        {
            var customerId = CustomerContext.Current.CurrentContactId;

            if (customerId == null)
                return NotFound();

            var cart = _orderRepository.Service.LoadOrCreateCart<ICart>(customerId, DEFAULT_CART_NAME);

            return Ok(new CartApiModel(cart));
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

            //return Ok(new { Result = true });
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
            if (existingLineItem == null)
            {
                var newLineItem = cart.CreateLineItem(skuCode);
                newLineItem.DisplayName = skuToAdd.DisplayName;
                newLineItem.Quantity = quantityToUpdate;
                cart.AddLineItem(newLineItem);
            }

            else
            {
                var shipment = cart.GetFirstShipment();

                if (quantityToUpdate <= 0)
                    shipment.LineItems.Remove(existingLineItem);

                else
                    cart.UpdateLineItemQuantity(shipment, existingLineItem, quantityToUpdate);
            }

            var validationIssues = new Dictionary<ILineItem, List<ValidationIssue>>();

            cart.ValidateOrRemoveLineItems((item, issue) =>
                validationIssues.AddValidationIssues(item, issue), _lineItemValidator.Service);
        
            cart.UpdatePlacedPriceOrRemoveLineItems(CustomerContext.Current.GetContactById(cart.CustomerId), (item, issue) => validationIssues.AddValidationIssues(item, issue),
                _placedPriceProcessor.Service);

            _orderRepository.Service.Save(cart);

            return Ok(new CartApiModel(cart));
        }
    }
}
