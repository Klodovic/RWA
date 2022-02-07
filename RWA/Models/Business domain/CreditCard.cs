using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWA.Models.Business_domain
{
    public class CreditCard
    {
        public int IDKreditnaKartica { get; set; }
        public string Tip { get; set; }
        public string Broj { get; set; }
        public int IstekMjesec { get; set; }
        public int IstekGodina { get; set; }
    }
}