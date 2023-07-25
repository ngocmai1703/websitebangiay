using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SellFootWear.Models;
using PagedList;

namespace SellFootWear.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private db_SellFootwearEntities db = new db_SellFootwearEntities();
       
        // GET: Admin/Order
        public ActionResult Index(int? page, int? id)
        {
            int pageSize = 10; 
            int pageNum = (page ?? 1);
            var orders = db.Orders.Include(o => o.Customer).ToList();
            return View(orders.ToPagedList(pageNum, pageSize));
        }
       

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
          
            return View(order);
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
            ViewBag.CustomID = new SelectList(db.Customers, "CustomID", "CustomName");
            return View();
        }

        // POST: Admin/Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,CustomID,Orderday,value,Paid,Deliveryday")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomID = new SelectList(db.Customers, "CustomID", "CustomName", order.CustomID);
            return View(order);
        }

        // GET: Admin/Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomID = new SelectList(db.Customers, "CustomID", "CustomName", order.CustomID);
            return View(order);
        }

        // POST: Admin/Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,CustomID,Orderday,value,Paid,Deliveryday")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomID = new SelectList(db.Customers, "CustomID", "CustomName", order.CustomID);
            return View(order);
        }

        public ActionResult Delete(int? id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Order_Print(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
    }
}
