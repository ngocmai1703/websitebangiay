using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellFootWear.Models
{
    public class ProductModel
    {
       
        //public List<Brand> ListBrand { get; set; }
        //public List<Type> ListType { get; set; }
        public List<OrderDetail> LstOrderDetail { get; set; }
        public List<Customer> LstCustomer { get; set; }
        public List<Order> LstOrder { get; set; }

    }
}