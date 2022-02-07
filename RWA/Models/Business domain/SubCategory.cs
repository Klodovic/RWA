using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWA.Models.Business_domain
{
    public class SubCategory
    {
        public int IDPotkategorija { get; set; }
        public int KategorijaID { get; set; }
        public string Naziv { get; set; }
    }
}