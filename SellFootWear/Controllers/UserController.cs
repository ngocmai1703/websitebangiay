using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SellFootWear.Models;
namespace SellFootWear.Controllers
{
    public class UserController : Controller
    {
        db_SellFootwearEntities db = new db_SellFootwearEntities();
        // GET: User
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
            Customer isUser = db.Customers.FirstOrDefault(m => m.UserName == UserName && m.Password == Password);
            if (isUser != null)
            {
                Session["UserName"] = isUser;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notice = "Tên Đăng nhập hoặc mật khẩu không đúng! ";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "User");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection collection, Customer kh)
        {
            var sCustomName = collection["CustomName"];
            var sUserName = collection["UserName"];
            var sPassword = collection["Password"];
            var sPasswordAgin = collection["PasswordAgin"];
            var sAddress = collection["Address"];
            var sEmail = collection["Email"];
            var sPhoneNumber = collection["PhoneNumber"];
            var dDaybirth = String.Format("{0:MM/dd/yyyy}", collection["Daybirth"]);
            if (String.IsNullOrEmpty(sCustomName))
            {
                ViewData["err1"] = "Họ tên không được rỗng";
            }
            else if (String.IsNullOrEmpty(sUserName))
            {
                ViewData["err2"] = "Tên đăng nhập không được rỗng";
            }
            else if (String.IsNullOrEmpty(sPassword))
            {
                ViewData["err3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(sPasswordAgin))
            {
                ViewData["err4"] = "Phải nhập lại mật khẩu";
            }
            else if (sPassword != sPasswordAgin)
            {
                ViewData["err4"] = "Mật khẩu nhập lại không khớp";
            }
            else if (String.IsNullOrEmpty(sEmail))
            {
                ViewData["err5"] = "Email không được rỗng";
            }

            else if (String.IsNullOrEmpty(sPhoneNumber))
            {
                ViewData["err6"] = "Số điện thoại không được rỗng";
            }
            else if (db.Customers.SingleOrDefault(n => n.UserName == sUserName) != null)
            {
                ViewBag.Notice = "Tên đăng nhập đã tồn tại";
            }
            else if (db.Customers.SingleOrDefault(n => n.Email == sEmail) != null)
            {
                ViewBag.Notice = "Email đã được sử dụng";
            }
            else
            {
                kh.CustomName = sCustomName;
                kh.UserName = sUserName;
                kh.Password = sPassword;
                kh.Email = sEmail;
                kh.Address = sAddress;
                kh.PhoneNumber = sPhoneNumber;
                kh.Daybirth = DateTime.Parse(dDaybirth);
                object p = db.Customers.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return this.Register();
        }
    }
}
