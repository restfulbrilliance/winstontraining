using EPiServer.Web.Mvc;
using WinstonTraining.Core.Models.Blocks;
using WinstonTraining.Web.Constants;

namespace WinstonTraining.Web.Infrastructure.Rendering
{
    public class ViewTemplateModelRegistrator : IViewTemplateModelRegistrator
    {
        public void Register(TemplateModelCollection viewTemplateModelRegistrator)
        {
            viewTemplateModelRegistrator.Add(typeof(TeaserBlock),
                new EPiServer.DataAbstraction.TemplateModel()
                {
                    Name = "FullTeaserBlock",
                    Description = "FullTeaserBlock",
                    Path = $"~/Views/Shared/AlternateBlockRenderers/{DisplayOptionsTags.Full}{nameof(TeaserBlock)}.cshtml",
                    Tags = new string[] { DisplayOptionsTags.Full }
                });

            viewTemplateModelRegistrator.Add(typeof(TeaserBlock),
                new EPiServer.DataAbstraction.TemplateModel()
                {
                    Name = "HalfTeaserBlock",
                    Description = "HalfTeaserBlock",
                    Path = $"~/Views/Shared/AlternateBlockRenderers/{DisplayOptionsTags.Half}{nameof(TeaserBlock)}.cshtml",
                    Tags = new string[] { DisplayOptionsTags.Half }
                });
        }
    }
}