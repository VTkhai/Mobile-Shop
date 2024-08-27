using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileShopOnline.Controllers.Flyweight
{
    public class ProductFlyweightFactory
    {
        private Dictionary<int, ProductFlyweight> flyweights = new Dictionary<int, ProductFlyweight>();

        public ProductFlyweight GetProductFlyweight(int productId, string productName, decimal initialPrice, decimal price, int categoryId, string image1, string image2, string image3, int? amount, string productIntroduction, string chipset,string ram,string memory,string screenSize,string os,string camera,string pin,string resolution)
        {
            if (!flyweights.ContainsKey(productId))
            {
                flyweights[productId] = new ProductFlyweight(productId,
                    productName,
                    initialPrice,
                    price,
                    categoryId,
                    image1,
                    image2,
                    image3,
                    amount,
                    productIntroduction, chipset, ram, memory, screenSize, os,camera,pin,resolution
                    
                );
            }
            return flyweights[productId];
        }
    }
}