using MobileShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Routing;
using MobileShopOnline.Controllers.Validation;

namespace MobileShopOnline.Controllers
{
    // Proxy để kiểm tra xem người dùng đã đăng nhập chưa
    // Proxy Pattern
    public class AuthProxy : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = SessionManager.GetInstance().GetSession();
            var account = session["Account"];

            // Kiểm tra xem phiên đăng nhập của người dùng có tồn tại hay không
            if (account == null)
            {
                // Nếu không có phiên đăng nhập, chuyển hướng người dùng đến trang đăng nhập
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Users" },
                        { "action", "Login" }
                    }
                );
            }

            base.OnActionExecuting(filterContext);
        }
    }

    public class UsersController : Controller
    {
        private readonly DatabaseManager _dbManager;
        private readonly MobileShopOnlineEntities db;

        private HttpSessionStateBase session;
        public UsersController()    
        {
            session = SessionManager.GetInstance().GetSession();
            _dbManager = DatabaseManager.GetInstance();
            db = _dbManager.GetDatabase();
        }
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginValidation custValidation)
        {

            //if (ModelState.IsValid)
            //{
            //    Customer cust = new Customer
            //    {
            //        UserEmail = custValidation.UserEmail,
            //        UserPassword = custValidation.UserPassword
            //    };
            //    // Ánh xạ dữ liệu từ CustomerValidation sang Customer
            //    var adminAccount = db.AdminAccounts.FirstOrDefault(k => k.Email == cust.UserEmail && k.Password == cust.UserPassword);

            //    if (adminAccount != null)
            //    {
            //        Session["Account"] = adminAccount;
            //        return RedirectToAction("Index", "Admin/AdminHome");
            //    }
            //    var account = db.Customers.FirstOrDefault(k => k.UserEmail == cust.UserEmail && k.UserPassword == cust.UserPassword);
            //    if (account != null)
            //    {
            //        Session["Account"] = account;
            //        return RedirectToAction("Index", "Home");
            //    }
            //    else
            //        ViewBag.ThongBao = "*Tên đăng nhập hoặc mật khẩu không đúng";
            //}
            //return View(custValidation);
            var authenticationFacade = new AuthenticationFacade();
            var authenticatedAccount = authenticationFacade.AuthenticateUser(custValidation);

            if (authenticatedAccount != null)
            {
                session["Account"] = authenticatedAccount;

                if (authenticatedAccount is AdminAccount)
                {
                    return RedirectToAction("Index", "Admin/AdminHome");
                }
                else if (authenticatedAccount is Customer)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(custValidation);

        }

        public ActionResult Logout()
        {
            session["Account"] = null;
            session["MyCart"] = null;
            return RedirectToAction("Index", "Home");
        }

        [AuthProxy]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detail([Bind(Include = "UserID,UserName,UserEmail,PhoneNumber,UserPassword,AvatarImage")] Customer customer, HttpPostedFileBase ImageUser)
        {
            if (ModelState.IsValid)
            {
                if (ImageUser != null)
                {
                    //Lấy tên file của hình được up lên

                    var fileName = Path.GetFileName(ImageUser.FileName);

                    //Tạo đường dẫn tới file

                    var path = Path.Combine(Server.MapPath("~/image"), fileName);
                    //Lưu tên

                    customer.AvatarImage = fileName;
                    //Save vào Images Folder
                    ImageUser.SaveAs(path);

                }
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Detail","Users");
            }
            return View(customer);
        }
        // Sử dụng proxy để kiểm tra đăng nhập trước khi truy cập
        [AuthProxy]
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [AuthProxy]
        public ActionResult SignUp(CustomerValidation custValidation)
        {
            // Kiểm tra xác nhận mật khẩu và các điều kiện khác

            if (ModelState.IsValid)
            {
                // Ánh xạ dữ liệu từ CustomerValidation sang Customer
                Customer customer = new Customer
                {
                    UserName = custValidation.UserName,
                    UserEmail = custValidation.UserEmail,
                    PhoneNumber = custValidation.PhoneNumber,
                    UserPassword = custValidation.UserPassword,
                    // Các trường dữ liệu khác của Customer có thể được ánh xạ tương tự
                };

                if (customer.UserPassword != custValidation.RePassword)
                {
                    ViewBag.Notification = "Mật khẩu xác nhận không chính xác";
                    return View();
                }

                string email = customer.UserEmail;
                var cus = db.Customers.FirstOrDefault(k => k.UserEmail == email);

                if (cus != null)
                {
                    ViewBag.NotificationEmail = "Đã có người đăng ký bằng email này";
                    return View();
                }

                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Login", "Users");
            }
            return View(custValidation);
        }




    }
}