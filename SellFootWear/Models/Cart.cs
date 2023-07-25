using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellFootWear.Models
{
    public class Cart
    {
        db_SellFootwearEntities db = new db_SellFootwearEntities();
        //internal int pID;

        public int ipID { get; set; }
        public string sNameProduct { get; set; }
        public string sImage { get; set; }
        public double dPrice { get; set; }
        public int iQuantity { get; set; }
        public double dTotal
        {
            get { return iQuantity * dPrice; }
        }

        //public int MaSach { get; internal set; }
        

        public Cart(int ms)
        {
            ipID = ms;
            Product s = db.Products.Single(n => n.pID == ipID);
            sNameProduct = s.NameProduct;
            sImage = s.image;
            dPrice = double.Parse(s.price.ToString());
            iQuantity = 1;
        }
    }
}
