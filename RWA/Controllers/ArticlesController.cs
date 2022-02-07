using RWA.App_Start;
using RWA.Models.Business_domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RWA.Controllers
{
    public class ArticlesController : System.Web.Http.ApiController
    {
        //GET      /api/articles     -svi proizvodi
        [HttpGet]
        public IHttpActionResult GetArticles()
        {
            try
            {
                var articles = RepoSql.GetArticles();
                var articlesDto = AutoMapperConfig.Mapper.Map<IEnumerable<ProductDto>>(articles);
                return Ok(articlesDto);
            }
            catch (Exception e)
            {
                return Redirect("/Errors/ServerError" + $"{e.Message}");
            }
        }

        //GET      /api/articles/1    -konkretan proizvod
        [HttpGet]
        public IHttpActionResult GetArticle(int id)
        {
            try
            {
                var article = RepoSql.GetArticle(id);
                var articleDto = AutoMapperConfig.Mapper.Map<ProductDto>(article);
                if (article == null)
                {
                    return NotFound();
                }
                return Ok(articleDto);
            }
            catch (Exception e)
            {
                return Redirect("/Errors/ServerError" + $"{e.Message}");
            }
        }


        //POST     /api/articles      -dodavanje proizvoda
        [HttpPost]
        public IHttpActionResult CreateNewProduct(Product p)
        {
            try
            {
                RepoSql.InsertNewProduct(p);
                var articleDto = AutoMapperConfig.Mapper.Map<ProductDto>(p);
                return Redirect(new Uri("/Restfull/Index", UriKind.Relative));
            }
            catch (Exception e)
            {
                return Redirect("/Errors/ServerError" + $"{e.Message}");
            }
        }


        //PUT      /api/articles/1     -ažuriranje proizvoda
        [HttpPut]
        public IHttpActionResult UpdateProduct(int id, Product p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var articleDB = RepoSql.GetArticle(id);
            if (articleDB == null)
            {
                return NotFound();
            }
            articleDB.IDProizvod = id;
            articleDB.Naziv = p.Naziv;

            RepoSql.ProductUpdate(articleDB);
            return Ok("Proizvod je uspješno ažuriran!");
        }


        //DELETE   /api/articles/1   -brisanje
        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var productDB = RepoSql.GetProductsDetails(id);
            if (productDB == null)
            {
                return NotFound();
            }
            RepoSql.DeleteProductByID(id);
            return Ok("Proizvod je uspješno obrisan!");
        }

    }
}
