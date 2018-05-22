using EPiServer.Web.Mvc;
using System.Web.Mvc;
using WinstonTraining.Core.Models;
using WinstonTraining.Core.Models.Commerce;

namespace WinstonTraining.Web.Controllers
{
    public class ProductController : ContentController<Product>
    {
        public ActionResult Index(Product currentContent, StartPage currentPage)
        {
            return View(currentContent);
        }
    }
}