using RWA.Models.Business_domain;
using RWA.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RWA.Controllers
{
    public class ProductController : Controller
    {
        // GET: Products
        public ActionResult ALlProducts()
        {
            try
            {
                var model = RepoSql.GetAllProducts();
                return View(model);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }


        //Create new product
        public ActionResult Create(Product p)
        {
            ViewBag.s = RepoSql.GetAllSubCategories2();

            if (ModelState.IsValid)
            {
                try
                {
                    RepoSql.InsertNewProduct(p);
                    return RedirectToAction(actionName: "ALlProducts");
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                } 
            }

            ModelState.Clear();

            return View();
        }

        //Delete selected product
        public ActionResult Delete(int id)
        {
            try
            {
                RepoSql.DeleteProductByID(id);
                return new HttpStatusCodeResult(HttpStatusCode.OK, "Proizvod je uspješno obrisan");
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Proizvod nije obrisan");
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var product = RepoSql.GetProductsDetails(id);
                var model = new ProductDetails
                {
                    AllProducts = RepoSql.GetAllProducts(),
                    Product = product,
                    IdProduct = product.IDProizvod
                };
                return View(model);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult Get(int id)
        {
            try
            {
                var product = RepoSql.GetProductsDetails(id);
                var model = new
                {
                    idproizvod = product.IDProizvod,
                    naziv = product.Naziv,
                    brojproizvoda = product.BrojProizvoda,
                    boja = product.Boja,
                    minimalnakolicinanaskladistu = product.MinimalnaKolicinaNaSkladistu,
                    cijenabezpdv = product.CijenaBezPDV
                };

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.s = RepoSql.GetAllSubCategories2();
                var model = RepoSql.GetProductsDetails(id);
                return View(model);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost]
        public ActionResult Edit(Product p)
        {
            try
            {
                RepoSql.ProductUpdate(p);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}