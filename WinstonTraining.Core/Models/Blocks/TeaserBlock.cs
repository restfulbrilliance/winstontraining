using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using WinstonTraining.Core.Models.Blocks.Interfaces;

namespace WinstonTraining.Core.Models.Blocks
{
    [ContentType(DisplayName = "Teaser Block", GUID = "7b312992-2b30-4fd2-8307-72d193e0b62a", Description = "")]
    public class TeaserBlock : SiteBlockData, INoWrapper
    {
        [Display(
            Name = "Heading",
            Description = "Heading for the block",
            GroupName = SystemTabNames.Content,
            Order = 1000)]
        public virtual string Heading { get; set; }

        [Display(
            Name = "Main body",
            Description = "The main body will be shown in the main area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 1100)]
        public virtual XhtmlString MainBody { get; set; }
    }
}