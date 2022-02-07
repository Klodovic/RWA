using RWA.Models.Business_domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWA.Models.ViewModel
{
    public class ProductEdit
    {
        public Product Product { get; set; }
        public int? IdPotkategorija { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }
    }
}