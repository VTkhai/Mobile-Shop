using MobileShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileShopOnline.Controllers
{
    public class DatabaseManager
    {
        private static readonly object lockObject = new object();
        private static DatabaseManager instance;
        private readonly MobileShopOnlineEntities _db;

        // Constructor private để ngăn việc tạo mới từ bên ngoài
        private DatabaseManager()
        {
            _db = new MobileShopOnlineEntities(); // Khởi tạo đối tượng cơ sở dữ liệu
        }

        // Phương thức để truy cập vào instance của DatabaseManager
        public static DatabaseManager GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new DatabaseManager();
                    }
                }
            }
            return instance;
        }

        // Phương thức để truy cập đến đối tượng cơ sở dữ liệu
        public MobileShopOnlineEntities GetDatabase()
        {
            return _db;
        }

        // Các phương thức khác để thực hiện các thao tác khác với cơ sở dữ liệu có thể được thêm vào đây
    }
}
