using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using System.Web.Mvc;
using WinstonTraining.Core.Models;
using System.Linq;
using System;

namespace WinstonTraining.Web.Controllers
{
    public class StartPageController : PageController<StartPage>
    {
        //private static Injected<IContentRepository> _contentRepository;
        //private static Injected<IContentLoader> _contentLoader;

        public ActionResult Index(StartPage currentPage)
        {
            /*
            ContentReference testReference = null;

            var referenceA = currentPage.ContentLink;
            var mainPointer = referenceA.ID;
            var versionPointer = referenceA.WorkID;
            ContentReference referenceB = null;

            //content ref => string complex content ref
            var strComplexRef = referenceA.ToString();

            //string complex content ref => content ref
            var contentReferenceFromString = new ContentReference(strComplexRef);

            if(referenceA.CompareTo(referenceB) == 0)
            {
                //content references are 100% equal, same content, same version
            }

            if(referenceA.CompareToIgnoreWorkID(referenceB))
            {
                //content references are equal, same content, could be different versions
            }

            //return immediate child pages that are of type article page
            var childArticlePages = _contentLoader.Service.GetChildren<ArticlePage>(currentPage.ContentLink);

            //return *all* child pages
            var childPages = _contentLoader.Service.GetChildren<PageData>(currentPage.ContentLink);

            //returns *all* descendant references, including grand-children, etc.
            var descendentsContentReferences = _contentLoader.Service.GetDescendents(currentPage.ContentLink);

            var descendentPages = _contentLoader.Service.GetDescendents(currentPage.ContentLink)
                .Where(cr => !ContentReference.IsNullOrEmpty(cr))
                .Select(cr => _contentLoader.Service.Get<PageData>(cr))
                .Where(pg => pg != null);

            var descendentArticlePages = _contentLoader.Service.GetDescendents(currentPage.ContentLink)
                .Where(cr => !ContentReference.IsNullOrEmpty(cr))
                .Select(cr => _contentLoader.Service.Get<PageData>(cr))
                .Where(pg => pg != null)
                .OfType<ArticlePage>();
            */

            return View(currentPage);
        }

        /*
        private static ArticlePage CreateNewPage()
        {
            //note: in this case we're building an article page instance underneath the start page
            var newArticlePage = _contentRepository.Service.GetDefault<ArticlePage>(ContentReference.StartPage);

            // set properties
            newArticlePage.Name = "My New Page";
            newArticlePage.MainBody = new XhtmlString("<p>Hello World!</p>");
            // ... set additional properties ...

            // tries to save the page, with the permissions that the current code is running underneath
            _contentRepository.Service.Save(newArticlePage, EPiServer.DataAccess.SaveAction.Publish);

            // alternative to above, saves the page, ignoring any permission requirements
            _contentRepository.Service.Save(newArticlePage, EPiServer.DataAccess.SaveAction.Publish,
                EPiServer.Security.AccessLevel.NoAccess);

            //note: in this case, we're building an article page instance underneath Start -> My Parent Page
            var childrenPages = _contentLoader.Service.GetChildren<PageData>(ContentReference.StartPage);

            PageData parentPage = null;

            //Approach #1, use linq
            parentPage = childrenPages.FirstOrDefault(pg => pg.Name == "My Parent Page");

            //Approach #2, use traditional for loops
            foreach(var child in childrenPages)
            {
                if(child.Name == "My Parent Page")
                {
                    parentPage = child;
                    break;
                }
            }

            if (parentPage != null)
            {
                //note: in this case we're building an article page instance underneath Start Page -> My Parent Page
                var anotherNewArticlePage = _contentRepository.Service.GetDefault<ArticlePage>(parentPage.ContentLink);

                // set properties
                anotherNewArticlePage.Name = "Another New Page";
                anotherNewArticlePage.MainBody = new XhtmlString("<p>Hello World!</p>");
                // ... set additional properties ...

                // tries to save the page, with the permissions that the current code is running underneath
                _contentRepository.Service.Save(anotherNewArticlePage, EPiServer.DataAccess.SaveAction.Publish);

                // alternative to above, saves the page, ignoring any permission requirements
                _contentRepository.Service.Save(anotherNewArticlePage, EPiServer.DataAccess.SaveAction.Publish,
                    EPiServer.Security.AccessLevel.NoAccess);
            }
        }

        private static ArticlePage UpdateArticlePage(ContentReference referenceToOriginalPage)
        {
            //get a referenece to the page that you wish to update
            //var originalPage = _contentRepository.Service.Get<ArticlePage>(referenceToOriginalPage);

            ArticlePage originalPage = null;
            if(_contentLoader.Service.TryGet<ArticlePage>(referenceToOriginalPage, out originalPage))
            {
                //the TryGet<> succedded!

                //continue to work with the object
                var name = originalPage.Name;
            }
        }

        private static ContentReference MoveArticlePage(ContentReference pageToMoveReference, ContentReference targetLocationRefenence)
        {
            try
            {
                var movedPageReference = _contentRepository.Service.Move(pageToMoveReference, targetLocationRefenence,
                    EPiServer.Security.AccessLevel.NoAccess,
                    EPiServer.Security.AccessLevel.NoAccess);

                //rs: should be true in most case *UNLESS* custom logic is executed as part of the move event
                if (movedPageReference.Equals(pageToMoveReference))
                {

                }

                return movedPageReference;
            }

            catch (Exception moveEx)
            {
                //log the exception or take some other action
            }

            return ContentReference.EmptyReference;
        }

        private static ContentReference CopyArticlePage(ContentReference pageToCopyReference, ContentReference targetLocationRefenence)
        {
            try
            {
                var copiedPageReference = _contentRepository.Service.Copy(pageToCopyReference, targetLocationRefenence,
                EPiServer.Security.AccessLevel.NoAccess,
                EPiServer.Security.AccessLevel.NoAccess, true);

                return copiedPageReference;
            }

            catch (Exception copyEx)
            {
                //log the exception or take some other action
            }

            return ContentReference.EmptyReference;
        }
        */
    }
}