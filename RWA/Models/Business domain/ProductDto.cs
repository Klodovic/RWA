using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RWA.Models.Business_domain
{
    public class ProductDto
    {
        public int IDProizvod { get; set; }
        public string Naziv { get; set; }
        [Display(Name = "Broj proizvoda")]
        public string BrojProizvoda { get; set; }

        public string Boja { get; set; }

        [Display(Name = "Minimalna količina na skladištu")]
        public int MinimalnaKolicinaNaSkladistu { get; set; }

    }
}