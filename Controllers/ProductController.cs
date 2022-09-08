using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi_Menu_Practica.Data;
using WebApi_Menu_Practica.Models;

namespace WebApi_Menu_Practica.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        public Product product = new Product();

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                //List<ProductModel> orderDToList;
                var orderDToList = Product.List();
                return ((IHttpActionResult)orderDToList);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

    
    }
}
