using MobileShopOnline.Controllers.Strategy;
using MobileShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.DesignPattern.Strategy
{
    public class Over12MilStrategy : IPriceRangeStrategy
    {
        public IQueryable<Product> FilterProducts(MobileShopOnlineEntities db, int categoryId)
        {
            var products = db.Products.OrderByDescending(p => p.ProductID).Where(p => p.Price > 12000000);
            if (categoryId != 0)
                products = products.Where(item => item.CategoryID == categoryId);
            return products;
        }
    }
}