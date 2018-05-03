using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.DataAnnotations;
using System;

namespace WinstonTraining.Core.Models.Commerce
{
    [CatalogContentType(DisplayName = "Top Level Category", GUID = "d8a5dac0-5514-4f50-9c1d-7c495617f6d3", Description = "")]
    [AvailableContentTypes(
        Include = new Type[] { typeof(Subcategory) },
        Exclude = new Type[] { typeof(ProductContent), typeof(VariationContent) }
    )]
    public class TopLevelCategory : SiteNodeContent { }
}