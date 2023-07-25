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
using PagedList.Mvc;
using System.IO;

namespace SellFootWear.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private db_SellFootwearEntities db = new db_SellFootwearEntities();

        // GET: Admin/Product
        public ActionResult Index(int? page, String SeachString)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            if (!String.IsNullOrEmpty(SeachString))
            {
                var Productt = db.Products.Where(n => n.NameProduct.Contains(SeachString)).ToList();
                return View(Productt.ToPagedList(pageNum, pageSize));
            }
            var Product = db.Products.OrderByDescending(n => n.UpdateDay).ToList();
            return View(Product.ToPagedList(pageNum, pageSize));
        }
        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.BrandID = new SelectList(db.Brands.ToList().OrderBy(n => n.BrandID), "BrandID", "brand1");
            ViewBag.TypeID = new SelectList(db.Types.ToList().OrderBy(n => n.TypeID), "TypeID", "TypeName");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product product, FormCollection f, HttpPostedFileBase fFileUpload, HttpPostedFileBase fFileUpload1, HttpPostedFileBase fFileUpload2)
        {
            ViewBag.BrandID = new SelectList(db.Brands.ToList().OrderBy(n => n.BrandID), "BrandID", "brand1");
            ViewBag.TypeID = new SelectList(db.Types.ToList().OrderBy(n => n.TypeID), "TypeID", "TypeName");
            if (fFileUpload == null)
            {
                ViewBag.noti = "Hãy chọn ảnh .";
                ViewBag.NameProduct = f["sNameProduct"];
                ViewBag.describeshort = f["describeshort"];
                ViewBag.describe = f["describe"];
                ViewBag.price = decimal.Parse(f["price"]);
                ViewBag.pricediscount = decimal.Parse(f["pricediscount"]);
                ViewBag.BrandID = new SelectList(db.Brands.ToList().OrderBy(n => n.BrandID), "BrandID", "brand1", int.Parse(f["BrandID"]));
                ViewBag.TypeID = new SelectList(db.Types.ToList().OrderBy(n => n.TypeID), "TypeID", "TypeName", int.Parse(f["TypeID"]));
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img/product"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    var sFileName1 = Path.GetFileName(fFileUpload1.FileName);
                    var path1 = Path.Combine(Server.MapPath("~/Content/img/product"), sFileName1);
                    if (!System.IO.File.Exists(path1))
                    {
                        fFileUpload.SaveAs(path1);
                    }
                    var sFileName2 = Path.GetFileName(fFileUpload2.FileName);
                    var path2 = Path.Combine(Server.MapPath("~/Content/img/product"), sFileName2);
                    if (!System.IO.File.Exists(path2))
                    {
                        fFileUpload.SaveAs(path2);
                    }
                    product.NameProduct = f["sNameProduct"];
                    product.describeshort = f["describeshort"];
                    product.describe = f["describe"];
                    product.image = sFileName;
                    product.image2 = sFileName1;
                    product.image3 = sFileName2;
                    product.UpdateDay = Convert.ToDateTime(f["UpdateDay"]);
                    product.price = decimal.Parse(f["price"]);
                    //product.Discount = Convert.(f["Discount"]);
                    product.pricediscount = decimal.Parse(f["pricediscount"]);
                    product.BrandID = int.Parse(f["BrandID"]);
                    product.TypeID = int.Parse(f["TypeID"]);
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

   
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(db.Brands.ToList().OrderBy(n => n.brand1), "BrandID", "brand1", product.BrandID);
            ViewBag.TypeID = new SelectList(db.Types.ToList().OrderBy(n => n.TypeName), "TypeID", "TypeName", product.TypeID);
            return View(product);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product product, FormCollection f, HttpPostedFileBase fFileUpload, HttpPostedFileBase fFileUpload1, HttpPostedFileBase fFileUpload2)
        {
            //int tmp = int.Parse(f["ipID"]);
            //Product product = db.Products.SingleOrDefault(n => n.pID == tmp );
            ViewBag.BrandID = new SelectList(db.Brands.ToList().OrderBy(n => n.brand1), "BrandID", "brand1", product.BrandID);
            ViewBag.TypeID = new SelectList(db.Types.ToList().OrderBy(n => n.TypeName), "TypeID", "TypeName", product.TypeID);
            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img/product"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    product.image = sFileName;
                    var sFileName1 = Path.GetFileName(fFileUpload1.FileName);
                    var path1 = Path.Combine(Server.MapPath("~/Content/img/product"), sFileName1);
                    if (!System.IO.File.Exists(path1))
                    {
                        fFileUpload.SaveAs(path1);
                    }
                    product.image2 = sFileName1;
                    var sFileName2 = Path.GetFileName(fFileUpload2.FileName);
                    var path2 = Path.Combine(Server.MapPath("~/Content/img/product"), sFileName2);
                    if (!System.IO.File.Exists(path2))
                    {
                        fFileUpload.SaveAs(path2);
                    }
                    product.image3 = sFileName2;
                }
                product.NameProduct = f["sNameProduct"];
                product.describeshort = f["describeshort"].Replace("<p>", "").Replace("</p>", "");
                product.describe = f["describe"].Replace("<p>", "").Replace("</p>", "");
                product.UpdateDay = Convert.ToDateTime(f["UpdateDay"]);
                product.price = decimal.Parse(f["price"]);
                //product.Discount = Convert.(f["Discount"]);
                product.pricediscount = decimal.Parse(f["pricediscount"]);
                product.BrandID = int.Parse(f["BrandID"]);
                product.TypeID = int.Parse(f["TypeID"]);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
       

        public ActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
    }
}
