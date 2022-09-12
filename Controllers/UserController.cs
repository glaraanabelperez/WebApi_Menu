using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi_Menu_Practica.Data;
using WebApi_Menu_Practica.Models;
using static WebApi_Menu_Practica.Data.User;

namespace WebApi_Menu_Practica.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        public User user = new User();

        /// <summary>
        /// Listado de todos los useros 
        /// </summary>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
 
           var orderDToList = user.List();
           if (orderDToList == null)
           {
               return NotFound();
           }
           return Ok(orderDToList);

        }

        /// <summary>
        /// Devuelve los datos de un turno
        /// </summary>
        /// <param name="cityId">Identificador del turno</param>
        /// <returns>Datos del turno</returns>
        [HttpGet]
        public IHttpActionResult Get(int userId)
        {
            var userItem = user.Get(userId);
            if (userItem == null)
            {
                return NotFound();
            }
            return Ok(userItem);
        }

        /// <summary>
        /// Graba los datos del usuario
        /// </summary>
        /// <param name="data">Datos del usuario</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        [HttpPut]
        public IHttpActionResult Put([FromBody] UserModel data) 
        {
            var userItem = user.Save(null,data);
            if (userItem != 0)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Update(int userId, [FromBody] UserModel data)
        {
            var userItem = user.Save(userId, data);
            if (userItem != 0)
            {
                return NotFound();
            }
            return Ok();
        }

        /// <summary>
        /// Elimina un user
        /// </summary>
        /// <param name="userId"> Identificador del user</param>
        [HttpDelete]
        public IHttpActionResult Delete(int userId)
        {
            var userItem = Data.User.Delete(userId);
            if (userItem != 0)
            {
                return NotFound();
            }
            return Ok();
        }


    }
}
