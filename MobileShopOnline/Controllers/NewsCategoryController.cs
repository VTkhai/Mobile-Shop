using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileShopOnline.Controllers.DesignPattern.Composite;
using MobileShopOnline.Models;

namespace MobileShopOnline.Controllers
{
    public class NewsCategoryController : Controller
    {
        MobileShopOnlineEntities db = DatabaseManager.GetInstance().GetDatabase();
        // GET: NewsCategory
        public ActionResult Index()
        {
            var newsList = db.News.ToList();
            return View(newsList);
        }

        public ActionResult News(int id)
        {
            var news = db.News.FirstOrDefault(n => n.NewsID == id);
            var newsComponent = new NewsLeaf(news);
            var composite = new NewsComposite();
            composite.Add(newsComponent);
            ViewBag.IndexOfDot = news.NewsContent.IndexOf(".");
            return View(composite.GetNews());
        }
    }
}