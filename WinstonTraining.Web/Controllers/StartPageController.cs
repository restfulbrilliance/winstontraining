using EPiServer.Web.Mvc;
using System.Web.Mvc;
using WinstonTraining.Core.Models;

namespace WinstonTraining.Web.Controllers
{
    public class StartPageController : PageController<StartPage>
    {
        public ActionResult Index(StartPage currentPage)
        {
            return View(currentPage);
        }
    }
}