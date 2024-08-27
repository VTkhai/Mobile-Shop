using MobileShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Input;

namespace MobileShopOnline.Controllers.DesignPattern.Command
{
    public class FetchOrderDetailsCommand : ICommand
    {
        private MobileShopOnlineEntities _db = DatabaseManager.GetInstance().GetDatabase();
        private readonly int _orderId;

        public FetchOrderDetailsCommand(MobileShopOnlineEntities db, int orderId)
        {
            _db = db;
            _orderId = orderId;
        }

        public void Execute(Controller controller)
        {
            var listProdOrder = _db.OrderDetails.Where(order => order.IdOrder == _orderId).ToList();
            decimal finalPrice = 0;
            foreach (var item in listProdOrder)
            {
                finalPrice += (decimal)item.FinalPrice;
            }
            controller.ViewBag.FinalPrice = finalPrice;
            controller.ViewBag.Address = _db.Orders.FirstOrDefault(o => o.IdOrder == _orderId)?.Address;
            controller.ViewBag.Date = _db.Orders.FirstOrDefault(o => o.IdOrder == _orderId)?.DateOrder;
            controller.ViewBag.Id = _db.Orders.FirstOrDefault(o => o.IdOrder == _orderId)?.IdOrder;
            controller.ViewBag.Status = _db.Orders.FirstOrDefault(o => o.IdOrder == _orderId)?.StatusOrder;

            controller.ViewBag.CusInfor = _db.Orders.FirstOrDefault(o => o.IdOrder == _orderId);
            controller.ViewData.Model = listProdOrder;
        }
    }
}