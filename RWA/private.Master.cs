using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA
{
    public partial class _private : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            Response.Cookies["UserDetails"].Expires = DateTime.Now.AddDays(-1);
            Response.Redirect("~/default.aspx", true);
        }
    }
}