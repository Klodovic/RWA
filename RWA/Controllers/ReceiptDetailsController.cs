using RWA.Models.Business_domain;
using System;
using System.Web.Mvc;
using System.Net;

namespace RWA.Controllers
{
    public class ReceiptDetailsController : Controller
    {
        // GET: ReceiptDetails
        public ActionResult Details(int? racunID)
        {
            try
            {
                var details = RepoSql.GetBundleItemsByReceiptID(racunID.Value);
                return View(details);
            }
            catch (Exception e)
            {
                return Redirect("/Errors/ServerError" +  $"{e.Message}");
            }
        }

        [HttpGet]
        public ActionResult Update(int stavkaID, int catID)
        {
            try
            {
                ViewBag.c = RepoSql.GetCategories();
                ViewBag.s = RepoSql.GetSubCategoriesByID(catID);
                return View(RepoSql.GetItemBundleDetailsByID(stavkaID));
            }
            catch (Exception e)
            {
                return Redirect("/Errors/ServerError" + $"{e.Message}");
            }
        }

        [HttpPost]
        public ActionResult Update(ItemBundle i, int racunID)
        {
            try
            {
                RepoSql.GetUpdateBundleItemsByIDs(i);
                return Redirect("/ReceiptDetails/Details/?racunID=" + $"{racunID}");
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult DinamicSubCategories(int id)
        {
            try
            {
                return Json(RepoSql.GetSubCategoriesByID(id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

    }
}