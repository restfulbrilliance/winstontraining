using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using WinstonTraining.Core.Models.Commerce.Interfaces;

namespace WinstonTraining.Core.Models.Commerce.Extensions
{
    public static class IHaveTopLevelCategoryExtensions
    {
        private static Injected<IContentLoader> _contentLoader { get; set; }

        public static TopLevelCategory TopLevelCategoryEx(this IHaveTopLevelCategory instance)
        {
            var subcategory = instance.Subcategory;

            if (subcategory == null)
                return null;

            var topLevelCategoryContentRef = subcategory.ParentLink;

            TopLevelCategory topLevelCategory = null;

            if (_contentLoader.Service
                .TryGet<TopLevelCategory>(topLevelCategoryContentRef, out topLevelCategory))
            {
                return topLevelCategory;
            }

            return null;
        }
    }
}
