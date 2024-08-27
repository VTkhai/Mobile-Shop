using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileShopOnline.Controllers.Flyweight
{
    public class ProductFlyweight
    {
        public int ProductID { get; }
        public string ProductName { get; }
        public decimal? InitialPrice { get; }
        public decimal? Price { get; }
        public int CategoryID { get; }
        public string Image1 { get; }
        public string Image2 { get; }
        public string Image3 { get; }
        public int? Amount { get; }
        public string ProductIntroduction { get; }
        public string Chipset { get; }
        public string Ram { get; }
        public string Memory { get; }
        public string ScreenSize { get; }
        public string OS { get; }
        public string Camera { get; }
        public string Pin { get; }
        public string Resolution { get; }

        public ProductFlyweight(int productId, string productName, decimal initialPrice, decimal price, int categoryId,
        string image1, string image2, string image3, int? amount, string productIntroduction, string chipset,
        string ram, string memory, string screenSize, string os, string camera, string pin, string resolution)
        {
            ProductID = productId;
            ProductName = productName;
            InitialPrice = initialPrice;
            Price = price;
            CategoryID = categoryId;
            Image1 = image1;
            Image2 = image2;
            Image3 = image3;
            Amount = amount;
            ProductIntroduction = productIntroduction;
            Chipset = chipset;
            Ram = ram;
            Memory = memory;
            ScreenSize = screenSize;
            OS = os;
            Camera = camera;
            Pin = pin;
            Resolution = resolution;
        }
    }
}