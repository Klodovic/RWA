using RWA.Models.Business_domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA
{
    public partial class buyers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            HttpCookie cookie = Request.Cookies["UserDetails"];
            if (cookie == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                lblUserName.Text = cookie["UserName"];
                lblTime.Text = cookie["Time"];
            }

            if (!IsPostBack)
            {
                CountryLoad();
                ddlGrad.Items.Insert(0, new ListItem("--- Odaberi Grad ---", ""));
                lblError.Visible = false;
            }
        }
        private void CountryLoad()
        {
            try
            {
                ddlDrzava.DataSource = RepoSql.GetCountries();
                ddlDrzava.DataTextField = "Naziv";
                ddlDrzava.DataValueField = "IDDrzava";
                ddlDrzava.DataBind();
                ddlDrzava.Items.Insert(0, new ListItem("--- Odaberi Državu ---", ""));
            }
            catch (Exception e)
            {
                lblError.Text = e.Message;
            }
        }

        private void CityLoad()
        {
            try
            {
                int drzavaID = int.Parse(ddlDrzava.SelectedValue);

                ddlGrad.DataSource = RepoSql.GetCities(drzavaID);
                ddlGrad.DataTextField = "Naziv";
                ddlGrad.DataValueField = "IDGrad";
                ddlGrad.DataBind();
                ddlGrad.Items.Insert(0, new ListItem("--- Odaberi Grad ---", ""));
            }
            catch (Exception e)
            {
                lblError.Text = e.Message;
            }
        }

        protected void ddlDrzava_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CityLoad();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void ddlGrad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int gradID = int.Parse(ddlGrad.SelectedValue);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void btnNumberOfBuyers_Click(object sender, EventArgs e)
        {
            GridView1.Visible = true;
        }


        protected void BtnEdit_Command(object sender, CommandEventArgs e)
        {
            string url = SessionIDKupac(e);
            Response.Redirect($"update.aspx?IDKupac={url}");
        }


        protected void btnDetails_Command(object sender, CommandEventArgs e)
        {
            string url = SessionIDKupac(e);
            Response.Redirect($"/Receipts/AllReceipts?IDKupac={url}");
        }
        private string SessionIDKupac(CommandEventArgs e)
        {
            Session["IDKupac"] = String.Format("{0}", e.CommandArgument);
            string url = String.Format("{0}", e.CommandArgument);
            return url;
        }

    }
}