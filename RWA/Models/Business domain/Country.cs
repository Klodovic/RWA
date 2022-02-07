using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWA.Models.Business_domain
{
    public class Country
    {
        public int IDDrzava { get; set; }
        public string Naziv { get; set; }

        public override string ToString() => $"{Naziv}";

    }
}