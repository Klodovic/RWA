using RWA.Models.Business_domain;
using RWA.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RWA.Controllers
{
    public class RestfullController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Article()
        {
            return View();
        }

        public ActionResult Edit()
        {
            ViewBag.s = RepoSql.GetAllSubCategories2();
            return View();
        }

        public ActionResult Update()
        {
            ViewBag.s = RepoSql.GetAllSubCategories2();
            return View();
        }








    }
}