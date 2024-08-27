using MobileShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Input;

namespace MobileShopOnline.Controllers.DesignPattern.Command
{
    public class FetchConfirmOrderCommand : ICommand
    {
        private MobileShopOnlineEntities _db = DatabaseManager.GetInstance().GetDatabase();
        private readonly int _orderId;

        public FetchConfirmOrderCommand(MobileShopOnlineEntities db, int orderId)
        {
            _db = db;
            _orderId = orderId;
        }

        public void Execute(Controller controller)
        {
            var prodListOrder = _db.OrderDetails.Where(o => o.IdOrder == _orderId).ToList();
            foreach (var item in prodListOrder)
            {
                var product = _db.Products.FirstOrDefault(p => p.ProductID == item.ProductID);
                product.amount -= item.Quantity;
            }
            var order = _db.Orders.FirstOrDefault(o => o.IdOrder == _orderId);
            order.StatusOrder = 2;
            _db.SaveChanges();
        }
    }
}