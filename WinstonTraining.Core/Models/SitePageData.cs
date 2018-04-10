using EPiServer.Core;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using WinstonTraining.Core.Tabs;

namespace WinstonTraining.Core.Models
{
    public abstract class SitePageData : PageData
    {
        [Display(Name = "Meta No Index", Order = 1000, GroupName = CustomTabs.META_TAB)]
        public virtual bool MetaNoIndex { get; set; }

        [Display(Name = "Meta No Follow", Order = 1100, GroupName = CustomTabs.META_TAB)]
        public virtual bool MetaNoFollow { get; set; }
    }
}