using RWA.Models.Business_domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA
{
    public partial class update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string idKupac = Session["IdKupac"].ToString();
                    lblInfo.Text = idKupac;

                    CountryLoad();
                    int idDrzava = RepoSql.GetBuyerCountryByID(Convert.ToInt32(idKupac));
                    ddlDrzava.SelectedValue = idDrzava.ToString();
                    CityLoad();
                    foreach (Buyer b in RepoSql.GetBuyerByID(Convert.ToInt32(idKupac)))
                    {
                        txtBuyerName.Text = b.Ime;
                        txtBuyerSurName.Text = b.Prezime;
                        txtBuyerEmail.Text = b.Email;
                        txtBuyerPhone.Text = b.Telefon;
                        ddlGrad.SelectedValue = b.GradID.ToString();
                    }

                    lblError.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/buyers.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string idKupac = Session["IdKupac"].ToString();

            int returnedValue = RepoSql.UpdateSingleBuyer(new Buyer()
            {
                IdKupac = int.Parse(idKupac),
                Ime = txtBuyerName.Text,
                Prezime = txtBuyerSurName.Text,
                Email = txtBuyerEmail.Text,
                Telefon = txtBuyerPhone.Text,
                GradID = int.Parse(ddlGrad.SelectedValue)
            });

            if (returnedValue == -1)
            {
                lblUpdateInfo.Text = "Ažuriranje nije uspjelo!!!";
            }
            else
            {
                Response.Redirect("~/buyers.aspx");
            }

        }
    }
}