using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileShopOnline.Controllers;
using MobileShopOnline.Controllers.DesignPattern.Command;
using MobileShopOnline.Models;

namespace MobileShopOnline.Areas.Admin.Controllers
{
    public class AdminOrderController : Controller
    {
        private readonly MobileShopOnlineEntities db = DatabaseManager.GetInstance().GetDatabase();
        // GET: Admin/AdminOrder
        public ActionResult Index()
        {
            ICommand command = new FetchOrderListCommand(db);
            command.Execute(this);
            return View();
        }
        public ActionResult Details(int id)
        {
            ICommand command = new FetchOrderDetailsCommand(db, id);
            command.Execute(this);
            return View();
        }

        public ActionResult Confirm(int id)
        {
            ICommand command = new FetchConfirmOrderCommand(db, id);
            command.Execute(this);
            return RedirectToAction("Index");
        }
    }
}