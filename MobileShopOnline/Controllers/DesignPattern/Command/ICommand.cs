using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShopOnline.Controllers.DesignPattern.Command
{
    //Admin Order Controller
    public interface ICommand
    {
        void Execute(Controller controller);
    }
}