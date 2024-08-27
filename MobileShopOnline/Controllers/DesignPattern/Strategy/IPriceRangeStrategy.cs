using MobileShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileShopOnline.Controllers.Strategy
{
    public interface IPriceRangeStrategy
    {
        IQueryable<Product> FilterProducts(MobileShopOnlineEntities db, int categoryId);
    }
}