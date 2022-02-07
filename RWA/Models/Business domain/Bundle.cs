using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace RWA.Models.Business_domain
{
    public class Bundle
    {
        //public Itemssss Item { get; set; }
        public Product Product { get; set; }
        public SubCategory SubCategory { get; set; }
        public Receipt Receipt { get; set; }
    }
}