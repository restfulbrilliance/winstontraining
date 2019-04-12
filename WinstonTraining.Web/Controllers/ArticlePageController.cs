using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using System.Web.Mvc;
using WinstonTraining.Core.Models;
using WinstonTraining.Core.Services.Interfaces;

namespace WinstonTraining.Web.Controllers
{
    public class ArticlePageController : PageController<ArticlePage>
    {
        //private static Injected<ITestService> _testService;
        public ITestService TestService { get; set; }

        public ArticlePageController(ITestService testService)
        {
            TestService = testService;
        }

        public ActionResult Index(ArticlePage currentPage)
        {
            //rs: using Service Locator
            //var testService = ServiceLocator.Current.GetInstance<ITestService>();
            //var serviceReturnVal = testService.TestMethod();

            //rs: using Injected
            //var serviceReturnVal2 = _testService.Service.TestMethod();

            //rs: using constructor based injected
            var serviceReturnVal3 = TestService.TestMethod();

            return View(currentPage);
        }
    }
}