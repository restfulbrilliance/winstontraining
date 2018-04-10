﻿using EPiServer.Web.Mvc;
using System.Web.Mvc;
using WinstonTraining.Core.Models;

namespace WinstonTraining.Web.Controllers
{
    public class ArticlePageController : PageController<ArticlePage>
    {
        public ActionResult Index(ArticlePage currentPage)
        {
            return View(currentPage);
        }
    }
}