using EPiServer;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;

namespace WinstonTraining.Core.Models
{
    [ContentType(DisplayName = "Start Page", GUID = "7e644167-4b7b-419d-bc6f-db432430ccc8", Description = "")]
    [AvailableContentTypes(
        Include = new Type[] { typeof(ArticlePage), typeof(CartPage) }, 
        Exclude = new Type[] { typeof(StartPage) } 
    )]
    public class StartPage : ArticlePage
    {
        //rs: note that IContentLoader only supports read from Epi
        private static Injected<IContentLoader> _contentLoader;

        [Ignore]
        public IEnumerable<ArticlePage> ChildArticles
        {
            get
            {
                return _contentLoader.Service.GetChildren<ArticlePage>(this.ContentLink);
            }
        }
    }
}