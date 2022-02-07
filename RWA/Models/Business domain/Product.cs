using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RWA.Models.Business_domain
{
    public class Product
    {
        public int IDProizvod { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [Display(Name = "Broj proizvoda")]
        public string BrojProizvoda { get; set; }


        public string Boja { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [Display(Name = "Minimalna količina na skladištu")]
        public int MinimalnaKolicinaNaSkladistu { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [Display(Name = "Cijena bez PDV-a")]
        public decimal CijenaBezPDV { get; set; }

        [Display(Name = "Potkategorija")]
        public int? PotkategorijaID { get; set; }
    }
}