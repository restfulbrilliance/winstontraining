using EPiServer;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.ServiceLocation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WinstonTraining.Core.Models.Commerce.Interfaces;
using WinstonTraining.Core.Models.Commerce.Extensions;

namespace WinstonTraining.Core.Models.Commerce
{
    [CatalogContentType(DisplayName = "Product", GUID = "9cefe4e8-5801-4867-96d2-e88e52c638a0", Description = "")]
    public class Product : SiteProductContent, IHaveTopLevelCategory, IHaveSubcategory
    {
        private static Injected<IContentLoader> _contentLoader { get; set; }
        private static Injected<IRelationRepository> _relationRepository { get; set; }

        [Display(
            Name = "Product description",
            Description = "The main description for the product",
            GroupName = SystemTabNames.Content,
            Order = 2100)]
        public virtual XhtmlString ProductDescription { get; set; }

        private TopLevelCategory _topLevelCategory;

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

        private List<ContentReference> _skuReferences;

        [Ignore]
        public List<ContentReference> SkuReferences
        {
            get
            {
                if (_skuReferences != null)
                    return _skuReferences;

                var variantRelations = 
                    _relationRepository.Service.GetChildren<ProductVariation>(this.ContentLink);

                _skuReferences = variantRelations
                    .Select(relation => relation.Child)
                    .Where(skuReference => !ContentReference.IsNullOrEmpty(skuReference))
                    .ToList();

                return _skuReferences;
            }
        }

        private List<Sku> _skus;

        [Ignore]
        public List<Sku> Skus
        {
            get
            {
                if (_skus != null)
                    return _skus;

                _skus = SkuReferences
                    .Select(skuRef => _contentLoader.Service.Get<SiteVariationContent>(skuRef))
                    .OfType<Sku>()
                    .ToList();

                return _skus;
            }
        }
    }
}