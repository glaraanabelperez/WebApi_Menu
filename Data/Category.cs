﻿using System;
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

        internal static ProductModel List()
        {
            string queryString = "SELECT *  FROM dbo.Users;";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new ProductModel();
                    }
                }
                connection.Close();
            }
            return null;
        }



        #region consulta

        /// <summary>
        /// Campos de la consulta
        /// </summary>
        public enum OrderFields
        {
            /// <summary>
            /// Precio
            /// </summary>
            Price = 0,
            /// <summary>
            /// Destacado
            /// </summary>
            Featured = 1

        }

        /// <summary>
        /// Clase para definir los valores de los filtros
        /// </summary>
        //[Serializable]
        public class Filter   
        {
      

            /// <summary>
            /// Filtro para el campo "Estado"
            /// </summary>
            public bool? State { get; set; }

            /// <summary>
            /// Búsqueda por comercio
            /// </summary>
            public int? UserId { get; set; }

            /// <summary>
            /// Búsqueda por texto libre
            /// </summary>
            public string FreeText { get ; set; }

            /// <summary>
            /// Búsqueda por texto libre
            /// </summary>
            public int? CategoryId { get; set; }

            /// <summary>
            /// Destacados
            /// </summary>
            public bool? Featured { get; set; }

   
        }

        #endregion

        #region Sql

        private const string SELECT_ALL =
        ";SELECT A.Id_Product, A.CreatedOn,A.[Title], A.[Description], A.Featured as Featured, A.Id_Categorie_FK as Id_Category, A.Id_User_FK as Id_User , " +
            " A.Name_Image, A.Price as Price ,A.Promotion" +
            ", U.Business_Name as Buisness, o.[Description] as CategoryName " +
            " FROM Products AS A "+
            " INNER JOIN dbo.[Users] U ON U.Id_User = A.Id_User_FK " +
            " INNER JOIN dbo.Categories O ON O.Id_Category = A.Id_Categorie_FK " +
            " {2} " +
            " ORDER BY {0} {1} ";

        private const string OFFSET = " OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY ";                 //{1} Desde - {2} Hasta

        private const string SELECTCOUNT = "SELECT COUNT(1) AS rows FROM [Products] AS A";
        #endregion
        
        /// <summary>
        /// Obtiene la lista de registros paginada, ordenada y filtrada
        /// </summary>
        /// <param name="orderField">Campo de orden</param>
        /// <param name="orderAscendant">Orden ascendente</param>
        /// <param name="from">Registro desde el cual traer los datos (en base cero)</param>
        /// <param name="length">Cantidad de registros a obtener</param>
        /// <param name="filter">Filtros a utilizar</param>
        /// <param name="recordCount">Cantidad de registros encontrados con los filtros establecidos</param>
        /// <returns>Registros obtenidos con los filtros y orden seleccionados</returns>
        public static DataTable List(OrderFields? orderField, bool? orderAscendant, Filter filter, int? from, int? length, out int recordCount)
        {

            if (from.HasValue != length.HasValue)
                throw new ArgumentOutOfRangeException(nameof(from));

            if (length.HasValue && length > 100)
                throw new ArgumentOutOfRangeException(nameof(length));

            string strOrderField;
            if (orderField.HasValue)
                strOrderField = orderField.ToString();
            else
                strOrderField = OrderFields.Featured.ToString();


            using (var connection = new SqlConnection(connectionString))
            {
                using(SqlCommand objSqlCmd = new SqlCommand("", connection))
                {
                    //Agregando parametros
                    string strFilter = string.Empty;
                    if (filter != null)
                    {
                        if (filter.CategoryId.HasValue)
                        {
                            strFilter += " AND [A].Id_Categorie_FK=@Id_Category";
                            objSqlCmd.Parameters.Add("@Id_Categorie", SqlDbType.Int).Value = filter.CategoryId.Value;

                        }
                        if (filter.UserId.HasValue)
                        {
                            strFilter += " AND [A].Id_User_FK=@Id_User";
                            objSqlCmd.Parameters.Add("@Id_User", SqlDbType.Int).Value = filter.UserId.Value;
                        }
                        if (filter.State.HasValue)
                        {
                            strFilter += " AND [A].State = @State";
                            objSqlCmd.Parameters.Add("@State", SqlDbType.Decimal).Value = filter.State.Value;
                        }
                        if (filter.Featured.HasValue)
                        {
                            strFilter += " AND [A].Featured = @Featured";
                            objSqlCmd.Parameters.Add("@Featured", SqlDbType.Bit).Value = filter.Featured.Value;
                        }
                        if (!string.IsNullOrWhiteSpace(filter.FreeText))
                        {
                            strFilter += " AND CONTAINS(R.*, @FreeText )";
                            objSqlCmd.Parameters.Add("@FreeText", SqlDbType.NVarChar, 1000).Value = filter.FreeText;
                        }


                        if (!string.IsNullOrWhiteSpace(strFilter))
                            strFilter = " WHERE " + strFilter.Substring(5);
                    }

                    string strWithParams = string.Format(SELECT_ALL, strOrderField, !orderAscendant.HasValue || orderAscendant.Value ? "ASC" : "DESC", strFilter);
                    objSqlCmd.CommandType = CommandType.Text;

                    connection.Open();

                    if (from.HasValue)
                    {
                        strWithParams += string.Format(OFFSET, from, length);

                        objSqlCmd.CommandText = SELECTCOUNT + strFilter + strWithParams;
#if DEBUG
                        System.Diagnostics.Trace.WriteLine(objSqlCmd.CommandText);
#endif

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = objSqlCmd;

                        DataSet dataset = new DataSet();
                        adapter.Fill(dataset);
                        connection.Close();
                        recordCount = (int)dataset.Tables[0].Rows[0][0];
                        return dataset.Tables[1];
                    }
                    else
                    {
                        objSqlCmd.CommandText = strWithParams;
                        recordCount = -1;
#if DEBUG
                        System.Diagnostics.Trace.WriteLine(strWithParams);
#endif

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = objSqlCmd;

                        DataSet dataset = new DataSet();
                        adapter.Fill(dataset);
                        connection.Close();

                        return dataset.Tables[0];
                    }


                }
            }
             
        }

        /// <summary>
        /// Graba el producto
        /// </summary>
        /// <param name="data">Datos del prodcuto</param>
        /// <param name="userId">Identificador del usuario que graba</param>
        /// <returns><c>true</c> Si se guardaron los datos, en caso contrario quiere decir que el nombre está repetido</returns>
        internal static int Save(int userId, ProductModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;

                if (data.ProductId.HasValue && data.ProductId.Value != 0)
                {
                    objCmd = new SqlCommand("Product_Update", connection);
                    objCmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = data.ProductId.Value;
                }
                else
                    objCmd = new SqlCommand("Product_Add", connection);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("@UserId", SqlDbType.Date).Value = data.UserId;
                objCmd.Parameters.Add("@CategoryId", SqlDbType.Date).Value = data.CategoryId;
                objCmd.Parameters.Add("@Title", SqlDbType.Money).Value = data.Title;
                objCmd.Parameters.Add("@@SubTitle", SqlDbType.Money).Value = data.Subtitle;
                objCmd.Parameters.Add("@Description", SqlDbType.Char, 5).Value = data.Description;
                objCmd.Parameters.Add("@Featured", SqlDbType.Decimal).Value = data.Featured;
                objCmd.Parameters.Add("@NameImage", SqlDbType.Bit).Value = data.NameImage;
                objCmd.Parameters.Add("@Price", SqlDbType.Int).Value = data.Price;
                objCmd.Parameters.Add("@Promotion", SqlDbType.Int).Value = data.Promotion;
                objCmd.Parameters.Add("@State", SqlDbType.Bit).Value = data.State;
                

                //if (result == Const.SQL_NO_PERMISSION)
                //    throw new SecurityException();
                connection.Open();
                var result=objCmd.ExecuteNonQuery();
                connection.Close();

                return result;
                //if (result < 0)
                //{
                //   return (SaveResult)result;
                //}

                //    return SaveResult.Ok;
            }

 
            
        }

        internal static int Delete(int userId, int productId)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;
                objCmd = new SqlCommand("Product_Delete", connection);
                objCmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;

                connection.Open();
                var result = objCmd.ExecuteNonQuery();
                connection.Close();

                return result;

            }



        }

    }
}