using RWA.Models.Business_domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RWA.Controllers
{
    public class SubCategoryController : Controller
    {
        [Route("SubCategory/AllSubCategories/")]
        [Route("SubCategory/AllSubCategories/{id}")]
        public ActionResult AllSubCategories(int? id)
        {
            if (!id.HasValue)
            {
                return Content("");
            }
            else
            {
                return Content(RepoSql.GetAllSubCategoriesByID(id.Value).First().Naziv);
            }
        }
    }
}