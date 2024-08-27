using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.DesignPattern.Strategy;
using MobileShopOnline.Controllers.Strategy;
using MobileShopOnline.Models;

namespace MobileShopOnline.Controllers
{
    public class FiltProductController : Controller
    {
        // GET: FiltProduct
        MobileShopOnlineEntities db = DatabaseManager.GetInstance().GetDatabase();

        public ActionResult Index()
        {
            return View();
        }

        private ActionResult FilterProducts(IPriceRangeStrategy strategy, int id)
        {
            var products = strategy.FilterProducts(db, id).ToList();
            ViewBag.CategoryProd = db.Categories.FirstOrDefault(n => n.CategoryID == id);
            ViewBag.IdCategory = id;
            return View(products);
        }

        //dưới 100.000đ
        public ActionResult Under4MilAllProduct(int id)
        {
            var strategy = new Under4MilStrategy();
            return FilterProducts(strategy, id);
        }

        //từ 100.000 đến 250.000
        public ActionResult From4To8MilAllProduct(int id)
        {
            var strategy = new From4To8MilStrategy();
            return FilterProducts(strategy, id);
        }

        //từ 250.000 đến 500.000
        public ActionResult From8To12MilAllProduct(int id)
        {
            var strategy = new From8To12MilStrategy();
            return FilterProducts(strategy, id);
        }

        //trên 500.000
        public ActionResult Over12MilAllProduct(int id)
        {
            var strategy = new Over12MilStrategy();
            return FilterProducts(strategy, id);
        }

        //giá thấp -> cao
        public ActionResult IncreaseWithPrice(int id)
        {
            var strategy = new IncreaseWithPriceStrategy();
            return FilterProducts(strategy, id);
        }

        //giá cao -> thấp
        public ActionResult DescreaseWithPrice(int id)
        {
            var strategy = new DecreaseWithPriceStrategy();
            return FilterProducts(strategy, id);
        }
    }
}