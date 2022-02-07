using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWA.Models.Business_domain
{
    public class Receipt
    {
        public int IDRacun { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public string BrojRacuna { get; set; }
        public int KupacID { get; set; }
        public int KomercijalistID { get; set; }
        public int KreditnaKarticaID { get; set; }
        public string Komentar { get; set; }
    }
}