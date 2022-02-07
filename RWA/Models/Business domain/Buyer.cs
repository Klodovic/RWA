using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWA.Models.Business_domain
{
    public class Buyer
    {
        public int IdKupac { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public int GradID { get; set; }
        public override string ToString() => $"{Ime} {Prezime}";
    }
}