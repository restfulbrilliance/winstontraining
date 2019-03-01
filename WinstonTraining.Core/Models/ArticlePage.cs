using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using WinstonTraining.Core.Groups;
using WinstonTraining.Core.Models.Blocks;
using WinstonTraining.Core.Utilities.Html;

namespace WinstonTraining.Core.Models
{
    [ContentType(DisplayName = "Article Page", 
        GUID = "5b1030e9-1072-4554-976b-3200f7dab43e", 
        Description = "Basic Article Page",
        GroupName = CustomGroups.MAIN_CONTENT_TYPE_GROUP,
        Order = 100)]
    [AvailableContentTypes(Availability = Availability.None)]
    public class ArticlePage : SitePageData
    {
        private const int MAIN_BODY_THUMBNAIL_LENGTH = 500;

        [Display(
            Name = "Title",
            Description = "Article title, falls back to Name if not defined.",
            GroupName = SystemTabNames.Content,
            Order = 2000 )]
        public virtual string Title
        {
            get
            {
                var tmpTitle = this.GetPropertyValue(p => p.Title);
                return string.IsNullOrEmpty(tmpTitle) ? Name : tmpTitle;
            }

            set
            {
                this.SetPropertyValue(p => p.Title, value);
            }
        }

        [Display(
            Name = "Main body",
            Description = "The main body will be shown in the main area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 2100)]
        public virtual XhtmlString MainBody { get; set; }

        private string _mainBodyThumbnalNoHtml;

        [Ignore]
        public string MainBodyThumbnailNoHtml
        {
            get
            {
                //rs: we've already calculated this before, so just return it
                if (!string.IsNullOrEmpty(_mainBodyThumbnalNoHtml))
                    return _mainBodyThumbnalNoHtml;

                //rs: we haven't done the work yet, so perform work and store in private backer
                if (MainBody == null || MainBody.IsEmpty)
                    return string.Empty;

                var mainBodyWithoutHtml = HtmlUtility.StripHtml(this.MainBody.ToHtmlString());

                if (mainBodyWithoutHtml.Length <= MAIN_BODY_THUMBNAIL_LENGTH)
                    return mainBodyWithoutHtml;

                //rs: set in the private backer, so we don't need to recalculate in the future
                _mainBodyThumbnalNoHtml = $"{mainBodyWithoutHtml.Substring(0, MAIN_BODY_THUMBNAIL_LENGTH)}...";
                return _mainBodyThumbnalNoHtml;
            }
        }

        [Display(
            Name = "Main content area",
            Description = "The main content area. Can contain blocks, pages, products, etc.",
            GroupName = SystemTabNames.Content,
            Order = 2200)]
        [AllowedTypes(AllowedTypes = new Type[] { typeof(TeaserBlock) })]
        public virtual ContentArea MainContentArea { get; set; }
    }
}