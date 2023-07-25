using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SellFootWear.Models;
namespace SellFootWear.Controllers
{
    public class HomeController : Controller
    {
        db_SellFootwearEntities db = new db_SellFootwearEntities();
        public ActionResult Index()
        {
            var Product = db.Products.ToList();
            return View(Product);
        }


    }
}
