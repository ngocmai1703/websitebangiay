using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SellFootWear.Models;
using PagedList;
using PagedList.Mvc;
namespace SellFootWear.Controllers
{
    public class ProductController : Controller
    {

        db_SellFootwearEntities db = new db_SellFootwearEntities();
        //ProductModel objProductModel = new ProductModel();
        // GET: Product
        //private List<Product> ProductNew (int Count)
        //{
        //    return db.Products.OrderByDescending(a => a.UpdateDay).Take(Count).ToList();
        //}
        public ActionResult Shop(int ? page, String SeachString)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            if (!String.IsNullOrEmpty(SeachString))
            {
                var products = db.Products.OrderByDescending(n => n.UpdateDay).Where(n => n.NameProduct.Contains(SeachString)).ToList();
                return View(products.ToPagedList(pageNum, pageSize));
            }
          //var product = db.Products.OrderByDescending(n=>n.UpdateDay).Where(n => n.TypeID == 1).ToList();
          var product = db.Products.OrderByDescending(n=>n.UpdateDay).ToList();
          return View(product.ToPagedList(pageNum, pageSize));

        }
        public ActionResult Shop2(int ? page, String SeachString)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            if (!String.IsNullOrEmpty(SeachString))
            {
                var products = db.Products.OrderByDescending(n => n.UpdateDay).Where(n => n.NameProduct.Contains(SeachString)).ToList();
                return View(products.ToPagedList(pageNum, pageSize));
            }
            var product = db.Products.OrderByDescending(n => n.UpdateDay).Where(n => n.TypeID == 2).ToList();
            return View(product.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Detail(int Id)
        {
            var ojbProduct = db.Products.Where(n => n.pID == Id).FirstOrDefault();
            return View(ojbProduct);
        }
        public ActionResult ProductCategory(int id)
        {
            var brandProduct = db.Products.Where(n => n.BrandID == id).ToList();
            return View(brandProduct);
        }
        public ActionResult Subject_ProductCategory()
        {
            return PartialView(db.Brands.ToList());
        }
        public ActionResult Subject_ProductCategoryShoes()
        {
            return PartialView(db.Brands.ToList());
        }
        public ActionResult ProductCategoryShoes(int id)
        {
            var brandProduct = db.Products.Where(n => n.BrandID == id).ToList();
            return View(brandProduct);
        }
    }
}
