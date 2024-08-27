using MobileShopOnline.Controllers.Validation;
using MobileShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShopOnline.Controllers
{
    public class AuthenticationFacade : Controller
    {
        MobileShopOnlineEntities db = DatabaseManager.GetInstance().GetDatabase();
        public object AuthenticateUser(LoginValidation custValidation)
        {
            if (ModelStateIsValid(custValidation))
            {
                var customer = MapToCustomer(custValidation);

                var adminAccount = AuthenticateAdmin(customer);
                if (adminAccount != null)
                {
                    return adminAccount;
                }

                var userAccount = AuthenticateUser(customer);
                if (userAccount != null)
                {
                    return userAccount;
                }
                else
                {
                    ViewBag.ThongBao = "*Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }

            return null;
        }

        private bool ModelStateIsValid(LoginValidation custValidation)
        {
            return ModelState.IsValid;
        }

        private Customer MapToCustomer(LoginValidation custValidation)
        {
            return new Customer
            {
                UserEmail = custValidation.UserEmail,
                UserPassword = custValidation.UserPassword
            };
        }

        private AdminAccount AuthenticateAdmin(Customer customer)
        {
            return db.AdminAccounts.FirstOrDefault(k => k.Email == customer.UserEmail && k.Password == customer.UserPassword);
        }

        private Customer AuthenticateUser(Customer customer)
        {
            return db.Customers.FirstOrDefault(k => k.UserEmail == customer.UserEmail && k.UserPassword == customer.UserPassword);
        }
    }
}