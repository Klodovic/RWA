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
    public partial class registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [Obsolete]
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string EncryptedUserPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtRegistredPassword.Text, "SHA512");

            int returnedValue = RepoSql.CreateNewUser(new User()
            {
                UserName = txtRegistredUserName.Text,
                Email = txtRegistredEmail.Text,
                Password = EncryptedUserPassword
            });

            if (returnedValue == -1)
            {
                lblRegistrationInfo.Text = "Korisničko ime već postoji. Unesite drugo ili se prijavite!";
            }
            else
            {
                Response.Redirect("~/default.aspx");
            }
        }
    }
}