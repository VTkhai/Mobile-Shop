using MobileShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopOnline.Controllers.Observer
{
    //Admin Categories Controller
    public interface ICategoryObserver
    {
        void Update(List<Category> categories);
    }
}
