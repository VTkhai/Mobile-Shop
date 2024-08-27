﻿using MobileShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileShopOnline.Controllers.Strategy
{
    public class DecreaseWithPriceStrategy : IPriceRangeStrategy
    {
        public IQueryable<Product> FilterProducts(MobileShopOnlineEntities db, int categoryId)
        {
            IQueryable<Product> products = db.Products.OrderByDescending(p => p.Price);
            if (categoryId != 0)
                products = products.Where(item => item.CategoryID == categoryId);
            return products;
        }
    }
}