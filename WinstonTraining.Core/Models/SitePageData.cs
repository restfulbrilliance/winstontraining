﻿using EPiServer.Core;
using System.ComponentModel.DataAnnotations;
using WinstonTraining.Core.Groups;

namespace WinstonTraining.Core.Models
{
    public abstract class SitePageData : PageData
    {
        [Display(Name = "Meta No Index", Order = 1000, GroupName = CustomGroups.META_TAB)]
        public virtual bool MetaNoIndex { get; set; }

        [Display(Name = "Meta No Follow", Order = 1100, GroupName = CustomGroups.META_TAB)]
        public virtual bool MetaNoFollow { get; set; }
    }
}