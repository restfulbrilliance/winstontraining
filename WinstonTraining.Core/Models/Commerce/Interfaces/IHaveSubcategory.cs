using EPiServer.Core;

namespace WinstonTraining.Core.Models.Commerce.Interfaces
{
    public interface IHaveSubcategory : IContent
    {
        Subcategory Subcategory { get; }
    }
}
