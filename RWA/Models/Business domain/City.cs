using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWA.Models.Business_domain
{
    public class City
    {
        public int IDGrad { get; set; }
        public string Naziv { get; set; }
        public int DrzavaId { get; set; }

        public override string ToString() => $"{Naziv}";
    }
}