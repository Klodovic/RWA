using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWA.Models.Business_domain
{
    public class Item    {
        public int IDStavka { get; set; }
        public int RacunID { get; set; }
        public int Kolicina { get; set; }
        public int ProizvodID { get; set; }
        public double CijenaPoKomadu { get; set; }
        public double PopustUPostocima { get; set; }
        public double UkupnaCijena { get; set; }
    }
}