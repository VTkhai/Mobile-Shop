using MobileShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileShopOnline.Controllers.Factory_Method
{
    public class ProductFactory : IProductFactory
    {
        private MobileShopOnlineEntities db = DatabaseManager.GetInstance().GetDatabase();
        public ProductFactory(MobileShopOnlineEntities dbContext)
        {
            db = dbContext;
        }

        public Product CreateProduct(int productId)
        {
            return db.Products.FirstOrDefault(p => p.ProductID == productId);
        }
    }
}