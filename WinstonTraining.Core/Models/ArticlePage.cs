using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace WinstonTraining.Core.Models
{
    [ContentType(DisplayName = "Article Page", GUID = "5b1030e9-1072-4554-976b-3200f7dab43e", Description = "Basic Article Page")]
    public class ArticlePage : SitePageData
    {
        [Display(
            Name = "Title",
            Description = "Article title, falls back to Name if not defined.",
            GroupName = SystemTabNames.Content,
            Order = 2000)]
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
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 2100)]
        public virtual XhtmlString MainBody { get; set; }

        public string Test()
        {
            var articlePage = new ArticlePage();
            return articlePage.Title;
        }
    }
}