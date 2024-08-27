using MobileShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Input;

namespace MobileShopOnline.Controllers.DesignPattern.Command
{
    public class FetchOrderListCommand : ICommand
    {
        private MobileShopOnlineEntities _db = DatabaseManager.GetInstance().GetDatabase();

        public FetchOrderListCommand(MobileShopOnlineEntities db)
        {
            _db = db;
        }

        public void Execute(Controller controller)
        {
            var orderList = (from order in _db.Orders orderby order.IdOrder descending select order).ToList();
            controller.ViewData["OrderList"] = orderList;
        }
    }
}