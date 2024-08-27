using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileShopOnline.Controllers
{
    public class SessionManager
    {
        private static SessionManager instance;
        private static readonly object lockObject = new object();

        private HttpSessionStateBase session;

        // Private constructor để ngăn việc tạo ra các đối tượng SessionManager khác từ bên ngoài lớp
        private SessionManager()
        {
            session = new HttpSessionStateWrapper(HttpContext.Current.Session); // Lấy session hiện tại
        }

        // Tạo một static method để truy cập vào instance của SessionManager
        public static SessionManager GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SessionManager();
                    }
                }
            }
            return instance;
        }

        // Phương thức để truy cập đến Session
        public HttpSessionStateBase GetSession()
        {
            return session;
        }
    }
}