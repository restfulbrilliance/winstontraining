using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.DataAnnotations;
using System;

namespace WinstonTraining.Core.Models.Commerce
{
    [CatalogContentType(DisplayName = "Subcategory", GUID = "9e7b959d-b383-4490-bd68-63d52b3a91c9", Description = "")]
    [AvailableContentTypes(
        Include = new Type[] { typeof(SiteProductContent), typeof(SiteVariationContent) },
        Exclude = new Type[] { typeof(NodeContent) }
    )]
    public class Subcategory : SiteNodeContent { }
}