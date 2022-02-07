using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RWA.Models.Business_domain
{
    public class ItemBundle
    {
        public int IDStavka { get; set; }

        public int RacunID { get; set; }
        [Display(Name = "Količina")]
        public int Kolicina { get; set; }

        [Display(Name = "Cijena po komadu")]
        [DataType(DataType.Currency)]
        public decimal CijenaPoKomadu { get; set; }

        [Display(Name = "Popust u postocima")]
        [DataType(DataType.Currency)]
        public decimal PopustUPostocima { get; set; }

        [Display(Name = "Ukupna cijena")]
        [DataType(DataType.Currency)]
        public decimal UkupnaCijena { get; set; }

        
        public int IDProizvod { get; set; }

        [Required(ErrorMessage = "Naziv proizvoda je obavezan!")]
        [Display(Name = "Naziv proizvoda")]
        public string NazivProizvoda { get; set; }

        [Required(ErrorMessage = "Broj proizvoda je obavezan!")]
        [Display(Name = "Broj proizvoda")]
        public string BrojProizvoda { get; set; }

        [Required(ErrorMessage = "Boja proizvoda je obavezna!")]
        public string Boja { get; set; }

        [Required(ErrorMessage = "Minimalna količina na skladištu je obavezna!")]
        [Display(Name = "Minimalna količina na skladištu")]
        public int MinimalnaKolicinaNaSkladistu { get; set; }

        [Required(ErrorMessage = "Cijena bez PDV-a je obavezna!")]
        [Display(Name = "Cijena bez PDV-a")]
        [DataType(DataType.Currency)]
        public decimal CijenaBezPDV { get; set; }

        public int? IDPotkategorija { get; set; }
        [Required(ErrorMessage = "Potkategorija je obavezna!")]

        [Display(Name = "Potkategorija")]
        public string NazivPotkategorije { get; set; }

        public int IDKategorija { get; set; }
        [Required(ErrorMessage = "Kategorija je obavezna!")]

        [Display(Name = "Kategorija")]
        public string NazivKategorije { get; set; }

        public int IDKomercijalist { get; set; }
        [Display(Name = "Ime komercijaliste")]
        public string Ime { get; set; }

        [Display(Name = "Prezime komercijaliste")]
        public string Prezime { get; set; }

        public int IDKreditnaKartica { get; set; }
        [Display(Name = "Tip kreditne kartice")]
        public string Tip { get; set; }

        [Display(Name = "Broj kreditne kartice")]
        public string Broj { get; set; }



    }
}