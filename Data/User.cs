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
    public class User
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
      
        /// <summary>
        /// Graba el producto
        /// </summary>
        /// <param name="data">Datos del usuario</param>
        /// <param name="userId">Identificador del usuario administrador que graba</param>
        /// <returns><c>true</c> Si se guardaron los datos, en caso contrario quiere decir que el nombre está repetido</returns>
        internal static int Save(int userId, UserModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;

                if (data.User_id.HasValue && data.User_id.Value != 0)
                {
                    objCmd = new SqlCommand("User_Update", connection);
                    objCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = data.User_id.Value;
                }
                else
                    objCmd = new SqlCommand("User_Add", connection);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("@Direction", SqlDbType.VarChar).Value = data.Direction;
                objCmd.Parameters.Add("@Facebook", SqlDbType.NVarChar, 250).Value = data.Facebook;
                objCmd.Parameters.Add("@Ig", SqlDbType.NVarChar, 250).Value = data.Ig;
                objCmd.Parameters.Add("@Logo", SqlDbType.NVarChar, 25).Value = data.Logo;
                objCmd.Parameters.Add("@OrdersWhatsapp", SqlDbType.TinyInt).Value = data.OrdersWhatsapp;
                objCmd.Parameters.Add("@Password", SqlDbType.NVarChar, 250).Value = data.Password;
                objCmd.Parameters.Add("@Phone", SqlDbType.Int).Value = data.Phone;
                objCmd.Parameters.Add("@Slogan", SqlDbType.NVarChar, 250).Value = data.Slogan;
                objCmd.Parameters.Add("@User_email", SqlDbType.NVarChar, 250).Value = data.User_email;

 
                connection.Open();
                var result=objCmd.ExecuteNonQuery();
                connection.Close();

                return result;
 
            }

 
            
        }

        internal static int Delete(int userId)
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