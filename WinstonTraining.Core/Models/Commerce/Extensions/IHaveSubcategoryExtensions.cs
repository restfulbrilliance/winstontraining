using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using WinstonTraining.Core.Models.Commerce.Interfaces;

namespace WinstonTraining.Core.Models.Commerce.Extensions
{
    public static class IHaveSubcategoryExtensions
    {
        private static Injected<IContentLoader> _contentLoader { get; set; }

        public static Subcategory SubcategoryEx(this IHaveSubcategory instance)
        {
            if (ContentReference.IsNullOrEmpty(instance.ParentLink))
                return null;

            Subcategory subcategory = null;

            if (_contentLoader.Service
                .TryGet<Subcategory>(instance.ParentLink, out subcategory))
            {
                return subcategory;
            }

            return null;
        }
    }
}
