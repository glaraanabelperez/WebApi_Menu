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

        /// <summary>
        /// Listado de todos los productos 
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get(int productId)
        {
            try
            {
                //List<ProductModel> orderDToList;
                ProductModel orderDToList = Data.Product.Get(productId);
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

        /// <summary>
        /// Graba los datos del usuario
        /// </summary>
        /// <param name="data">Datos del usuario</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        [HttpPut]
        public IHttpActionResult Put([FromBody] ProductModel data)
        {
            var userItem = Data.Product.Save(null, data);
            if (userItem != 0)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Update(int productId, [FromBody] ProductModel data)
        {
            var userItem = Data.Product.Save(productId, data);
            if (userItem != 0)
            {
                return NotFound();
            }
            return Ok();
        }

        /// <summary>
        /// Elimina un producto
        /// </summary>
        /// <param name="productoId"> Identificador del producto</param>
        [HttpDelete]
        public IHttpActionResult Delete(int productId) {
            var userItem = Data.Product.Delete(productId);
            if (userItem != 0)
            {
                return NotFound();
             }
            return Ok();
        }
           
  }    
}
