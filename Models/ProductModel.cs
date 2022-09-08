using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Menu_Practica.Models
{
    public class ProductModel
    {
        /// <summary>
        /// Identificador producto
        /// </summary>
        public int? IdProduct { get; set; }


        /// <summary>
        /// Identificador Categoria
        /// </summary>
        public int IdCategory { get; set; }

        /// <summary>
        /// Identificador usuario al cual pertenece
        /// </summary>
        public int IdUser { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// Titulo
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Descripcion
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Imagen
        /// </summary>
        public string NameImage { get; set; }

        /// <summary>
        /// Fecha publicacion
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Precio
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Destacado
        /// </summary>
        public bool Featured { get; set; }

        /// <summary>
        /// Promocion del producto
        /// </summary>
        public string Promotion { get; set; }
    }
}