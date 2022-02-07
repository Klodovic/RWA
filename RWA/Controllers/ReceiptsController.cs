using RWA.Models.Business_domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RWA.Controllers
{
    public class ReceiptsController : Controller
    {

        // GET: Receipts
        public ActionResult AllReceipts(int? IdKupac)
        {
            try
            {
                var model = RepoSql.GetAllReceiptOfBuyerByID((int)IdKupac);
                return View(model);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult SalesOfficer(int id)
        {
            try
            {
                var kom = RepoSql.GetSalesOfficerByID(id);
                return Content(kom.First().Ime + " " + kom.First().Prezime);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult CreditCard(int id)
        {
            try
            {
                var c = RepoSql.GetCreditCardByID(id);
                return Content(c.FirstOrDefault().Tip);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}