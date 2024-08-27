using MobileShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileShopOnline.Controllers.DesignPattern.Composite
{
    //News Controller
    public abstract class NewsComponent
    {
        public abstract List<News> GetNews();
    }
}