using EPiServer.Core;

namespace WinstonTraining.Core.Models.Commerce.Interfaces
{
    public interface IHaveTopLevelCategory : IHaveSubcategory
    {
        TopLevelCategory TopLevelCategory { get; }
    }
}
