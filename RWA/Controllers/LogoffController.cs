using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RWA.Controllers
{
    public class LogoffController : Controller
    {
        // GET: Logoff
        public ActionResult SignOff()
        {
            Response.Cookies["UserDetails"].Expires = DateTime.Now.AddDays(-1);
            return Redirect("~/default.aspx");
        }
    }
}