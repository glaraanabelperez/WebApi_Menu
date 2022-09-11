using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WebApi_Menu_Practica.Models;

namespace WebApi_Menu_Practica.Data
{
    public class Category
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["MenuConnection"].ConnectionString;

        /// <summary>
        /// Devuelve todos las categorias d eun usuario
        /// </summary>
        /// <returns>Lista de categorias</returns>
        internal static CategoryModel[] List(int userId)
        {
            var items = new List<CategoryModel>();
            using (var connection = new SqlConnection(connectionString))
            {
                var objCmd = new SqlCommand("Category_ListByUser", connection);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                //connection.Open();
                using (var objDR = objCmd.ExecuteReader())
                {
                    if (objDR.HasRows)
                    {
                        while (objDR.Read())
                        {
                            var c = new CategoryModel();
                            while (objDR.Read())
                            {
                                c.CategoryId = objDR.GetInt32(0);
                                c.Description = objDR.GetString(1);
                                c.UserId = objDR.GetInt32(2);

                                items.Add(c);
                            }
                            objDR.Close();
                        }
                    }
                }
                connection.Close();

                return  items.ToArray(); ;
            }

        
        }

        /// <summary>
        /// Devuelve los datos de una categoria
        /// </summary>
        /// <param name="userId">Identificador del categoria</param>
        /// <returns>Datos de categoria</returns>
        internal static CategoryModel Get(int userId)
        {
            var items = new CategoryModel();
            using (var connection = new SqlConnection(connectionString))
            {
                var objCmd = new SqlCommand("Category_Get", connection);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                connection.Open();
                using (var objDR = objCmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (objDR.HasRows)
                    {
                        while (objDR.Read())
                        {
                            items = new CategoryModel();
                            while (objDR.Read())
                            {
                                items.CategoryId= objDR.GetInt32(0);
                                items.Description = objDR.GetString(1);
                                items.UserId = objDR.GetInt32(2);
                               
                            }
                            objDR.Close();
                        }
                    }
                }
                connection.Close();
            }
            return items;

        }

        /// <summary>
        /// Graba la categoria
        /// </summary>
        /// <param name="data">Datos de la categoria</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        internal static int Save(int? categoryId, CategoryModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;

                if (categoryId.HasValue && categoryId != 0)
                {
                    objCmd = new SqlCommand("Category_Update", connection);
                    objCmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId;
                }
                else
                    objCmd = new SqlCommand("Category_Add", connection);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("@UserId", SqlDbType.Date).Value = data.UserId;
                objCmd.Parameters.Add("@CategoryId", SqlDbType.Date).Value = data.CategoryId;
                objCmd.Parameters.Add("@Description", SqlDbType.Char, 5).Value = data.Description;

                connection.Open();
                var result=objCmd.ExecuteNonQuery();
                connection.Close();

                return result;
    
            }
           
        }

        /// <summary>
        /// Elimina un categoria
        /// </summary>
        /// <returns>Retorna 0</returns>
        internal static int Delete(int categoryId)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;
                objCmd = new SqlCommand("Category_Delete", connection);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId;

                connection.Open();
                var result = objCmd.ExecuteNonQuery();
                connection.Close();

                return result;

            }

        }

    }
}