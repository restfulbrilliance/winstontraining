using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.DataAnnotations;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Pricing;
using WinstonTraining.Core.Models.Commerce.Extensions;
using WinstonTraining.Core.Models.Commerce.Interfaces;
using System.Linq;
using System;

namespace WinstonTraining.Core.Models.Commerce
{
    [CatalogContentType(DisplayName = "SKU", GUID = "3bd24de5-f8ef-4677-a356-16032b1bd1d9", Description = "")]
    public class Sku : SiteVariationContent, IHaveTopLevelCategory, IHaveSubcategory
    {
        private static Injected<IPriceDetailService> _priceDetailService { get; set; }
        //private static Injected<IPriceService> _priceService { get; set; }

        private TopLevelCategory _topLevelCategory;
        private const string DISPLAY_NO_PRICE = "Not purchasable";

        [Ignore]
        public TopLevelCategory TopLevelCategory
        {
            get
            {
                if (_topLevelCategory != null)
                    return _topLevelCategory;

                _topLevelCategory = this.TopLevelCategoryEx();

                return _topLevelCategory;
            }
        }

        private Subcategory _subcategory;

        [Ignore]
        public Subcategory Subcategory
        {
            get
            {
                if (_subcategory != null)
                    return _subcategory;

                _subcategory = this.SubcategoryEx();

                return _subcategory;
            }
        }

        public string GetDisplayPrice(string customerGroupName = null, string customerName = null)
        {
            //TODO: Add support for retrieving customer group / customer specific pricing
            var now = DateTime.Now;
            var listPrices = _priceDetailService.Service.List(this.ContentLink);

            var defaultPrice = listPrices
                .FirstOrDefault(price => price.CustomerPricing.PriceTypeId == CustomerPricing.PriceType.AllCustomers
                    && string.IsNullOrEmpty(price.CustomerPricing.PriceCode)
                    && price.ValidFrom <= now);

            if (defaultPrice == null)
                return DISPLAY_NO_PRICE;

            return $"{defaultPrice.UnitPrice.Currency.CurrencyCode} {defaultPrice.UnitPrice.Amount}";
        }
    }
}