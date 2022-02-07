using RWA.Models.Business_domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWA.Models.ViewModel
{
    public class ProductDetails
    {
        public IEnumerable<Product> AllProducts { get; set; }
        public Product Product { get; set; }
        public int IdProduct { get; set; }
    }
}