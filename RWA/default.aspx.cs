using RWA.Models;
using RWA.Models.Business_domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [Obsolete]
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string EncryptedUserPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA512");

            int returnedValue = RepoSql.LoginUser(txtUserName.Text, EncryptedUserPassword);

            if (returnedValue == 1)
            {
                HttpCookie cookie = new HttpCookie("UserDetails");
                cookie["UserName"] = txtUserName.Text;
                cookie["Time"] = DateTime.Now.ToString("HH:mm:ss");
                cookie.Expires = DateTime.Now.AddHours(4);
                Response.Cookies.Add(cookie);
                Response.Redirect("~/buyers.aspx");
            }
            else
            {
                lblLoginInfo.Text = "Neispravan unos korisničkog imena i/ili lozinke";
                txtUserName.Text = "";
                txtPassword.Text = "";
            }
        }
    }
}