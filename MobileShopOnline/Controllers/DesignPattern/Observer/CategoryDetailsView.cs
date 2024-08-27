using MobileShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileShopOnline.Controllers.Observer
{
    //Admin Categories Controller
    public class CategoryDetailsView : ICategoryObserver
    {
        public void Update(List<Category> categories)
        {
            Console.WriteLine("Category details updated:");
            foreach (var category in categories)
            {
                Console.WriteLine($"ID: {category.CategoryID}, Name: {category.CategoryName}, Image: {category.CategoryImage}");
            }
        }
    }
}