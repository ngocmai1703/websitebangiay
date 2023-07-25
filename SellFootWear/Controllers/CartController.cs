using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SellFootWear.Models;
using System.Data;
using System.Data.Entity;
using System.Net;
namespace SellFootWear.Controllers
{
    public class CartController : Controller
    {
        db_SellFootwearEntities db = new db_SellFootwearEntities();
        // GET: Cart
        public ActionResult CartIndex()
        {
            List<Cart> lstCart = TakeCart();
            if (lstCart.Count == 0)
            {
                //return RedirectToAction("Index", "FrontEnd")
            }
            ViewBag.Total = Total();
            ViewBag.TotalMoney = TotalMoney();
            return View(lstCart);
        }
        public List<Cart> TakeCart()
        {
            List<Cart> lstCart = Session["Carts"] as List<Cart>;
            if (lstCart == null)
            {
                lstCart = new List<Cart>();
                Session["Carts"] = lstCart;
            }
            return lstCart;
        }

        public ActionResult AddToCart(int ms, string url)
        {
            List<Cart> lstCart = TakeCart();
            Cart sp = lstCart.Find(n => n.ipID == ms);
            if (sp == null)
            {
                sp = new Cart(ms);
                lstCart.Add(sp);
            }
            else
            {
                sp.iQuantity++;
            }
            return Redirect(url);
        }
        private int Total()
        {
            int iTotal = 0;
            List<Cart> lstCart = Session["Carts"] as List<Cart>;
            if (lstCart != null)
            {
                iTotal = lstCart.Sum(n => n.iQuantity);
            }
            return iTotal;
        }
        private double TotalMoney()
        {
            double dTongTien = 0;
            List<Cart> lstCart = Session["Carts"] as List<Cart>;
            if (lstCart != null)
            {
                dTongTien = lstCart.Sum(n => n.dTotal);
            }
            return dTongTien;
        }
        public ActionResult CartPartial()
        {
            ViewBag.Total = Total();
            ViewBag.TotalMoney = TotalMoney();
            return PartialView();
        }
        public ActionResult Removefromcart(int ipID)
        {
            List<Cart> lstCart = TakeCart();
            Cart sp = lstCart.SingleOrDefault(n => n.ipID == ipID);
            if (sp != null)
            {
                lstCart.RemoveAll(n => n.ipID == ipID);
                if (lstCart.Count == 0)
                {
                    return RedirectToAction("Shop", "Product");
                }
            }
            return RedirectToAction("CartIndex");
        }
        public ActionResult DeleteCart()

        {
            List<Cart> lstCart = TakeCart();
            lstCart.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult UpdateCart(int ipID, FormCollection f)
        {
            List<Cart> lstCart = TakeCart();
            Cart sp = lstCart.SingleOrDefault(n => n.ipID == ipID);
            if (sp != null)
            {
                sp.iQuantity = int.Parse(f["qty"].ToString());
            }
            return RedirectToAction("CartIndex");
        }
        [HttpGet]
        public ActionResult Checkout()
        {
            if (Session["UserName"] == null || Session["UserName"].ToString() == "")
            {
                return RedirectToAction("Login", "User");
            }
            List<Cart> lstCart = TakeCart();
            ViewBag.Total = Total();
            ViewBag.TotalMoney = TotalMoney();
            return View(lstCart);
        } 
        [HttpPost]
        public ActionResult Checkout(FormCollection f)
        {
            Order ddh = new Order();
            Customer kh = (Customer)Session["UserName"];
            List<Cart> lstCart = TakeCart();
            ddh.CustomID = kh.CustomID;
            ddh.Orderday = DateTime.Now;
            var Deliveryday = String.Format("{0:MM/dd/yyyy}", f["Deliveryday"]);
            ddh.Deliveryday = Convert.ToDateTime(DateTime.Now);
            ddh.value = Convert.ToInt32(lstCart.Sum(n => n.dTotal));
            ddh.Paid = false;
            db.Orders.Add(ddh);
            db.SaveChanges();
            foreach (var item in lstCart)
            {
                OrderDetail ctdh = new OrderDetail();
                ctdh.OrderID = ddh.OrderID;
                ctdh.pID = item.ipID;
                ctdh.amount = item.iQuantity;
                ctdh.price = (decimal)item.dPrice;
                ctdh.intomoney = (double)item.dPrice*item.iQuantity;
                db.OrderDetails.Add(ctdh);
            }
            db.SaveChanges();
            Session["Carts"] = null;
            return RedirectToAction("OrderConfirmation", "Cart");
        }
        public ActionResult OrderConfirmation()
        {
            return View();
        }
    }
}
