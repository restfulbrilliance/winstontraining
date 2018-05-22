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

namespace WinstonTraining.Core.Models.Commerce
{
    [CatalogContentType(DisplayName = "Product", GUID = "9cefe4e8-5801-4867-96d2-e88e52c638a0", Description = "")]
    public class Product : SiteProductContent
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

                var topLevelCategoryContentRef = this.Subcategory.ParentLink;

                if (ContentReference.IsNullOrEmpty(topLevelCategoryContentRef))
                    return null;

                TopLevelCategory topLevelCategory = null;

                if (_contentLoader.Service
                    .TryGet<TopLevelCategory>(topLevelCategoryContentRef, out topLevelCategory))
                {
                    _topLevelCategory = topLevelCategory;
                    return _topLevelCategory;
                }

                return null;
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

                var subcategoryContetRef = this.ParentLink;

                if (ContentReference.IsNullOrEmpty(subcategoryContetRef))
                    return null;

                Subcategory subcategory = null;

                if(_contentLoader.Service
                    .TryGet<Subcategory>(subcategoryContetRef, out subcategory))
                {
                    _subcategory = subcategory;
                    return _subcategory;
                }

                return null;
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