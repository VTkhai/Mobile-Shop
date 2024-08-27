using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileShopOnline.Controllers.Factory_Method;
using MobileShopOnline.Controllers.Iterator;
using MobileShopOnline.Models;

namespace MobileShopOnline.Controllers
{
    public class FavoriteProductController : Controller
    {
        MobileShopOnlineEntities db = DatabaseManager.GetInstance().GetDatabase();
        private IProductFactory productFactory;

        public FavoriteProductController()
        {
            productFactory = new ProductFactory(db);
        }
        // GET: FavoriteProduct

        public ActionResult FavoriteList(int id)
        {
            var favoriteProducts = db.FavoriteProducts.Where(n => n.UserID == id).ToList();

            List<Product> products = new List<Product>();
            foreach (var favoriteProduct in favoriteProducts)
            {
                int productId = favoriteProduct.ProductID ?? 0;
                products.Add(productFactory.CreateProduct(productId));
            }

            ViewBag.ProductInfor = products;

            // Sử dụng Iterator để lặp qua danh sách sản phẩm yêu thích
            IFavoriteProductIterator iterator = new FavoriteProductIterator(favoriteProducts);
            List<FavoriteProduct> favoriteProductList = new List<FavoriteProduct>();
            while (iterator.HasNext())
            {
                favoriteProductList.Add(iterator.Next());
            }

            return View(favoriteProductList);
        }


        
        [HttpPost]
        public ActionResult InsertListFavorite(FavoriteProduct favoriteProd)
        {
            if (ModelState.IsValid)
            {
                var productAvail = db.FavoriteProducts.FirstOrDefault(p => p.ProductID == favoriteProd.ProductID && p.UserID == favoriteProd.UserID);
                if (productAvail != null)
                    return RedirectToAction("Index/" + favoriteProd.ProductID,"Details");
                else
                {

                    db.FavoriteProducts.Add(favoriteProd);
                    db.SaveChanges();

                    return RedirectToAction("FavoriteList/" + favoriteProd.UserID, "FavoriteProduct");
                }
            }
            return View("Index/" + favoriteProd.ProductID, "Details");
        }

        public ActionResult DeleteProduct(FavoriteProduct favoriteProd)
        {
            if (ModelState.IsValid)
            {
                var prod = db.FavoriteProducts.FirstOrDefault(p => p.ProductID == favoriteProd.ProductID && p.UserID == favoriteProd.UserID);
                db.FavoriteProducts.Remove(prod);
                db.SaveChanges();
            }
            return RedirectToAction("FavoriteList/" + favoriteProd.UserID, "FavoriteProduct");
        }
    }
}