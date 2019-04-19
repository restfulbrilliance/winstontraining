using EPiServer.Web.Mvc;
using System.Web.Mvc;
using WinstonTraining.Core.Models;

namespace WinstonTraining.Web.Controllers
{
    public class DeveloperPageController : PageController<DeveloperPage>
    {
        public ActionResult Index(DeveloperPage currentPage)
        {
            return View(currentPage);
        }
    }
}