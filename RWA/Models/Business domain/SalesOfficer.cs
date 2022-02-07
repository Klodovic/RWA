using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWA.Models.Business_domain
{
    public class SalesOfficer
    {
        public int IDKomercijalist { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public bool StalniZaposlenik { get; set; }
    }
}