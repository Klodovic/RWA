using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace RWA.Models.Business_domain
{
    public class RepoSql
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        //Registracija User-a
        public static int CreateNewUser(User user)
        {
            return SqlHelper.ExecuteNonQuery(cs, "CreateNewUser", user.UserName, user.Email, user.Password);
        }

        //Login User-a
        public static int LoginUser(string userName, string password)
        {
            return (int)SqlHelper.ExecuteScalar(cs, "UserLogin", userName, password);
        }

        //*********************************** Filtering buyers and GridView *****************************************//

        //Dohvati Države
        public static IEnumerable<Country> GetCountries()
        {
            var tblDrzave = SqlHelper.ExecuteDataset(cs, "GetAllCountries").Tables[0];

            foreach (DataRow row in tblDrzave.Rows)
            {
                yield return new Country()
                {
                    IDDrzava = (int)row["IDDrzava"],
                    Naziv = row["Naziv"].ToString()
                };
            }
        }

        //Dohvati Gradove
        public static IEnumerable<City> GetCities(int drzavaId)
        {
            var tblGradovi = SqlHelper.ExecuteDataset(cs, "GetAllCities", drzavaId).Tables[0];

            foreach (DataRow row in tblGradovi.Rows)
            {
                yield return new City()
                {
                    IDGrad = (int)row["IDGrad"],
                    Naziv = row["Naziv"].ToString()
                };
            }
        }

        //Dohvati kupce
        public static IEnumerable<Buyer> GetSelectedBuyers(int numOfBuyers, int drzavaId, int gradId)
        {
            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetTopBuyersByCityByCountry", numOfBuyers, drzavaId, gradId).Tables[0].Rows)
            {
                yield return GetBuyerFromDataRow(row);
            }
        }

        //*********************************** Upade Buyer, City and Country *****************************************//

        //Dohvati kupca po njegovom ID-u
        public static IEnumerable<Buyer> GetBuyerByID(int IdKupac)
        {
            SqlDataReader r = SqlHelper.ExecuteReader(cs, "GetBuyerByID", IdKupac);
            DataTable tbl = new DataTable();
            tbl.Load(r);
            foreach (DataRow row in tbl.Rows)
            {
                yield return GetBuyerFromDataRow(row);
            }
        }

        //Dohvati državu po ID-u kupca
        public static int GetBuyerCountryByID(int IdKupac)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(cs, "GetBuyersCountryByID", IdKupac));
        }

        //Ažuriraj odabranog kupca
        public static int UpdateSingleBuyer(Buyer b)
        {
            return SqlHelper.ExecuteNonQuery(cs, "UpdateSingleBuyer", b.IdKupac, b.Ime, b.Prezime, b.Email, b.Telefon, b.GradID);
        }

        //*********************************** MVC - Računi *****************************************//

        //Dohvati sve račune za odabranog kupca
        public static IEnumerable<Receipt> GetAllReceiptOfBuyerByID(int IdKupac)
        {
            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetAllReceiptOfBuyerByID", IdKupac).Tables[0].Rows)
            {
                yield return GetReceiptFromDataRow(row);
            }
        }

        //Dohvati komercijaliste za odabranog kupca
        public static IEnumerable<SalesOfficer> GetSalesOfficerByID(int komercijalistID)
        {
            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetSalesOfficerByID", komercijalistID).Tables[0].Rows)
            {
                yield return GetSalesOfficerFromDataRow(row);
            }
        }

        //Dohvati kreditne kartice za odabranog kupca
        public static IEnumerable<CreditCard> GetCreditCardByID(int kreditnaKraticaID)
        {
            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetCreditCardByID", kreditnaKraticaID).Tables[0].Rows)
            {
                yield return GetCreditCardFromDataRow(row);
            }
        }

        //Dohvati kreditne kartice za odabranog kupca
        public static IEnumerable<Buyer> GetReceitBuyerByID(int kupacID)
        {
            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetBuyerByID", kupacID).Tables[0].Rows)
            {
                yield return GetBuyerFromDataRow(row);
            }
        }

        //*********************************** MVC - Detalji odabranog računa *****************************************//

        //Dohvati sve stavke za odabrani racun
        public static IEnumerable<ItemBundle> GetBundleItemsByReceiptID(int IdRacun)
        {
            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetItemsByReceiptID", IdRacun).Tables[0].Rows)
            {
                yield return GetItemBundleFromDataRow(row);
            }
        }

        //Dohvati sve podatke za odabranu stavku
        public static ItemBundle GetItemBundleDetailsByID(int stavkaID)
        {
            DataSet ds = SqlHelper.ExecuteDataset(cs, "GetItemBundleDetailsByID", stavkaID);
            DataRow row = ds.Tables[0].Rows[0];
            return GetItemBundleFromDataRow(row);
        }

        //Dohvati sve kategorije
        public static IEnumerable<Category> GetCategories()
        {
            IList<Category> kategorije = new List<Category>();
            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetCategories").Tables[0].Rows)
            {
                kategorije.Add(GetAllCategories(row));
            }
            return kategorije;
        }

        //Dohvati sve potkategorije
        public static IEnumerable<SubCategory> GetSubCategoriesByID(int catID)
        {
            IList<SubCategory> potkategorije = new List<SubCategory>();
            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetSubCategoriesByID", catID).Tables[0].Rows)
            {
                potkategorije.Add(GetAllSubCategories(row));
            }
            return potkategorije;
        }

        //Ažuriraj odabranu stavku, proizvod, potkategoriju, kategoriju, komercijalistu i kreditnu karticu
        public static int GetUpdateBundleItemsByIDs(ItemBundle i)
        {
            return SqlHelper.ExecuteNonQuery(cs, "GetUpdateBundleItemsByIDs", i.IDStavka, i.Kolicina, i.CijenaPoKomadu, i.PopustUPostocima, i.UkupnaCijena,
                i.IDProizvod, i.NazivProizvoda, i.BrojProizvoda, i.Boja, i.MinimalnaKolicinaNaSkladistu, i.CijenaBezPDV,
                i.IDPotkategorija);
        }

        //*********************************** Proizvodi CRUD operacije *****************************************//

        //Dohvati sve proizvode
        public static IEnumerable<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetAllProducts").Tables[0].Rows)
            {
                products.Add(GetProductFromDataRow(row));
            }
            return products;
        }

        //Dohvati sve potkategorije po id
        public static IEnumerable<SubCategory> GetAllSubCategoriesByID(int id)
        {
            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetAllSubCategoriesByID", id).Tables[0].Rows)
            {
                yield return GetAllSubCategories(row);
            }
        }

        //Obriši odabrani proizvod
        public static int DeleteProductByID(int id)
        {
            return SqlHelper.ExecuteNonQuery(cs, "DeleteProductByID", id);
        }


        //Dohvati detalje odabranog proizvoda
        public static Product GetProductsDetails(int id)
        {
            DataSet ds = SqlHelper.ExecuteDataset(cs, "GetProductsDetails", id);
            DataRow row = ds.Tables[0].Rows[0];
            return GetProductFromDataRow(row);
        }

        //Dohvati sve potkategorije
        public static IEnumerable<SubCategory> GetAllSubCategories2()
        {
            IList<SubCategory> subs = new List<SubCategory>();
            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetAllSubCategories").Tables[0].Rows)
            {
                subs.Add(GetAllSubCategories(row));
            }
            return subs;
        }

        //Ažuriranje proizvoda
        public static int ProductUpdate(Product p)
        {
            return SqlHelper.ExecuteNonQuery(cs, "ProductUpdate", p.IDProizvod, p.Naziv, p.BrojProizvoda, p.Boja, p.MinimalnaKolicinaNaSkladistu, p.CijenaBezPDV, p.PotkategorijaID);
        }

        //Dodavanje novog proizvoda
        public static int InsertNewProduct(Product p)
        {
            return SqlHelper.ExecuteNonQuery(cs, "InsertNewProduct", p.Naziv, p.BrojProizvoda, p.Boja, p.MinimalnaKolicinaNaSkladistu, p.CijenaBezPDV, p.PotkategorijaID);
        }


        //*********************************** RESTfull *****************************************//

        //Dohvati sve proizvode
        public static IEnumerable<Product> GetArticles()
        {
            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetAllApiProducts").Tables[0].Rows)
            {
                yield return GetProductFromDataRow(row);
            }
        }

        //Dohvati jedan proizvod po ID-u
        public static Product GetArticle(int id)
        {
            DataSet ds = SqlHelper.ExecuteDataset(cs, "GetApiProductByID", id);
            DataRow row = ds.Tables[0].Rows[0];
            return GetProductFromDataRow(row);
        }

        //---------////------------////---------------////---------/////------/////

        //Dohvati sve kategorije
        private static Category GetAllCategories(DataRow row)
        {
            return new Category
            {
                IDKategorija = (int)row["IDKategorija"],
                Naziv = row["Naziv"].ToString()
            };
        }

        //Dohvati sve potkategorije po IDKategorija
        private static SubCategory GetAllSubCategories(DataRow row)
        {
            return new SubCategory
            {
                IDPotkategorija = (int)row["IDPotkategorija"],
                KategorijaID = int.Parse(row["KategorijaID"].ToString()),
                Naziv = row["Naziv"].ToString()
            };
        }

        //Kupac
        public static Buyer GetBuyerFromDataRow(DataRow row)
        {
            Buyer b = new Buyer();
            b.IdKupac = (int)row["IDKupac"];
            b.Ime = row["Ime"].ToString();
            b.Prezime = row["Prezime"].ToString();
            b.Email = row["Email"].ToString();
            b.Telefon = row["Telefon"].ToString();
            b.GradID = (int)row["GradID"];
            return b;
        }

        //Racun
        public static Receipt GetReceiptFromDataRow(DataRow row)
        {
            Receipt r = new Receipt();
            r.IDRacun = (int)row["IDRacun"];
            r.DatumIzdavanja = (DateTime)row["DatumIzdavanja"];
            r.BrojRacuna = row["BrojRacuna"].ToString();
            r.KupacID = (row["KupacID"] as int?) ?? 0;
            r.KomercijalistID = (row["KomercijalistID"] as int?) ?? 0;
            r.KreditnaKarticaID = (row["KreditnaKarticaID"] as int?) ?? 0;
            r.Komentar = row["Komentar"].ToString();
            return r;
        }

        //Komercijalist
        public static SalesOfficer GetSalesOfficerFromDataRow(DataRow row)
        {
            SalesOfficer s = new SalesOfficer();
            s.IDKomercijalist = (int)row["IDKomercijalist"];
            s.Ime = row["Ime"].ToString();
            s.Prezime = row["Prezime"].ToString();
            s.StalniZaposlenik = bool.Parse(row["StalniZaposlenik"].ToString());
            return s;
        }

        //Kreditna kartica
        public static CreditCard GetCreditCardFromDataRow(DataRow row)
        {
            CreditCard c = new CreditCard();
            c.IDKreditnaKartica = (int)row["IDKreditnaKartica"];
            c.Tip = row["Tip"].ToString();
            c.Broj = row["Broj"].ToString();
            c.IstekMjesec = int.Parse(row["IstekMjesec"].ToString());
            c.IstekGodina = int.Parse(row["IstekGodina"].ToString());
            return c;
        }

        //Stavka
        public static Item GetItemFromDataRow(DataRow row)
        {
            Item i = new Item();
            i.IDStavka = (int)row["IDStavka"];
            i.RacunID = (int)row["RacunID"];
            i.Kolicina = int.Parse(row["Kolicina"].ToString());
            i.ProizvodID = (int)row["ProizvodID"];
            i.CijenaPoKomadu = double.Parse(row["CijenaPoKomadu"].ToString());
            i.PopustUPostocima = double.Parse(row["PopustUPostocima"].ToString());
            i.UkupnaCijena = double.Parse(row["UkupnaCijena"].ToString());
            return i;
        }

        //Grupirana stavka
        public static ItemBundle GetItemBundleFromDataRow(DataRow row)
        {
            ItemBundle i = new ItemBundle();
            i.IDStavka = (int)row["IDStavka"];
            i.RacunID = (int)row["RacunID"];
            i.Kolicina = int.Parse(row["Kolicina"].ToString());
            i.CijenaPoKomadu = (decimal)(row["CijenaPoKomadu"]);
            i.PopustUPostocima = (decimal)(row["PopustUPostocima"]);
            i.UkupnaCijena = (decimal)(row["UkupnaCijena"]);

            i.IDProizvod = (int)row["IDProizvod"];
            i.NazivProizvoda = row["Naziv"].ToString();
            i.BrojProizvoda = row["BrojProizvoda"].ToString();
            i.Boja = row["Boja"].ToString();
            i.MinimalnaKolicinaNaSkladistu = int.Parse(row["MinimalnaKolicinaNaSkladistu"].ToString());
            i.CijenaBezPDV = (decimal)(row["CijenaBezPDV"]);

            i.IDPotkategorija = (int)row["IDPotkategorija"];
            i.NazivPotkategorije = row["NazivPotkategorije"].ToString();

            i.IDKategorija = (int)row["IDKategorija"];
            i.NazivKategorije = row["NazivKategorije"].ToString();

            i.IDKomercijalist = (int)row["IDKomercijalist"];
            i.Ime = row["Ime"].ToString();
            i.Prezime = row["Prezime"].ToString();

            i.IDKreditnaKartica = (int)row["IDKreditnaKartica"];
            i.Tip = row["Tip"].ToString();
            i.Broj = row["Broj"].ToString();

            return i;
        }

        //Proizvod
        public static Product GetProductFromDataRow(DataRow row)
        {
            Product p = new Product();
            p.IDProizvod = (int)row["IDProizvod"];
            p.Naziv = row["Naziv"].ToString();
            p.BrojProizvoda = row["BrojProizvoda"].ToString();
            p.Boja = row["Boja"].ToString();
            p.MinimalnaKolicinaNaSkladistu = int.Parse(row["MinimalnaKolicinaNaSkladistu"].ToString());
            p.CijenaBezPDV = (decimal)row["CijenaBezPDV"];
            if (row["PotkategorijaID"] is int)
            {
                p.PotkategorijaID = (int)row["PotkategorijaID"];
            }

            return p;
        }

        //Proizvod
        public static Product GetApiProductFromDataRow(DataRow row)
        {
            Product p = new Product();
            p.IDProizvod = (int)row["IDProizvod"];
            p.Naziv = row["Naziv"].ToString();
            p.BrojProizvoda = row["BrojProizvoda"].ToString();
            p.Boja = row["Boja"].ToString();
            p.MinimalnaKolicinaNaSkladistu = int.Parse(row["MinimalnaKolicinaNaSkladistu"].ToString());
            p.CijenaBezPDV = (decimal)row["CijenaBezPDV"];
            p.PotkategorijaID = (int)row["PotkategorijaID"];

            return p;
        }

        //Potkategorija
        public static SubCategory GetSubCategoryFromDataRow(DataRow row)
        {
            SubCategory s = new SubCategory();
            s.IDPotkategorija = (int)row["IDPotkategorija"];
            s.KategorijaID = int.Parse(row["KategorijaID"].ToString());
            s.Naziv = row["Naziv"].ToString();
            return s;
        }


        //Kategorija
        public static Category GetCategoryFromDataRow(DataRow row)
        {
            Category c = new Category();
            c.IDKategorija = (int)row["IDKategorija"];
            c.Naziv = row["Naziv"].ToString();
            return c;
        }

        //Država
        public static Country GetCountryFromDataRow(DataRow row)
        {
            Country c = new Country();
            c.IDDrzava = (int)row["IDDrzava"];
            c.Naziv = row["Naziv"].ToString();
            return c;
        }

        //Grad
        public static City GetCityFromDataRow(DataRow row)
        {
            City c = new City();
            c.IDGrad = (int)row["IDGrad"];
            c.Naziv = row["Naziv"].ToString();
            c.DrzavaId = (int)row["DrzavaID"];
            return c;
        }

    }
}