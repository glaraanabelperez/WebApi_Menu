using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi_Menu_Practica.Data;
using WebApi_Menu_Practica.Models;
using static WebApi_Menu_Practica.Data.Product;

namespace WebApi_Menu_Practica.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        public Product product = new Product();

        /// <summary>
        /// Listado de todos los productos 
        /// </summary>
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


        /// <summary>
        /// Listado de todos los productos paginados y filtrados
        /// </summary>
        /// <param name="queryData">Filtros de la consulta</param>
        /// <returns>Listado de los productos</returns>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public DataTableModel List([FromBody] QueryDataModel<Data.Product.Filter, Data.Product.OrderFields> queryData)
        {
          var objList = Data.Product.List(queryData.OrderField, queryData.OrderAsc, queryData.Filter, queryData.From, queryData.Length, out int RecordCount);
          return (new DataTableModel()
          {
              RecordsCount = RecordCount,
              Data = objList
          });
        }

    }
}
