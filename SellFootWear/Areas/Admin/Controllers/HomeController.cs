using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SellFootWear.Models;
namespace SellFootWear.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        db_SellFootwearEntities db = new db_SellFootwearEntities();
        
        // GET: Admin/Home
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
        public ActionResult Login(FormCollection forms)
        {
            var UserName = forms["UserName"];
            var Password = forms["Password"];
            var admin = db.Admins.SingleOrDefault(n => n.UserAdmin == UserName && n.Password == Password);
            if (admin != null)
            {
                Session["UserNameAdmin"] = admin;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notice = "Tên Đăng nhập hoặc mật khẩu không đúng! ";
            }
            return View();
        }
    }
}
