using MobileShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileShopOnline.Controllers.Observer
{
    public class CategoryListView : ICategoryObserver
    {
        public void Update(List<Category> categories)
        {
            Console.WriteLine("Category list updated:");
            foreach (var category in categories)
            {
                Console.WriteLine($"ID: {category.CategoryID}, Name: {category.CategoryName}");
            }
        }
    }
}