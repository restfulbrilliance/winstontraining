using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.DataAnnotations;
using WinstonTraining.Core.Models.Commerce.Extensions;
using WinstonTraining.Core.Models.Commerce.Interfaces;

namespace WinstonTraining.Core.Models.Commerce
{
    [CatalogContentType(DisplayName = "SKU", GUID = "3bd24de5-f8ef-4677-a356-16032b1bd1d9", Description = "")]
    public class Sku : SiteVariationContent, IHaveTopLevelCategory, IHaveSubcategory
    {
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
    }
}