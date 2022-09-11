using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Menu_Practica.Models
{
    public class CategoryModel
    {
        /// <summary>
        /// Identificador producto
        /// </summary>
        public int UserId { get; set; }


        /// <summary>
        /// Identificador Categoria
        /// </summary>
        public int? CategoryId { get; set; }


        /// <summary>
        /// Descripcion
        /// </summary>
        public string Description { get; set; }

    }
}