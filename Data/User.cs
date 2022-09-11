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

        /// <summary>
        /// Devuelve todos los usuarios sin paginar
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        internal UserModel[] List()
        {
            string queryString = "SELECT *  FROM dbo.Users;";
            List<UserModel> list = new List<UserModel>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var objDR = command.ExecuteReader())
                {
                    if (objDR.HasRows)
                    {
                        while (objDR.Read())
                        {                         
                            while (objDR.Read())
                                list.Add(new UserModel()
                                {
                                    Business_Name = objDR.GetString(1),
                                    Direction = objDR.GetString(2),
                                    Ig = objDR.GetString(3),
                                    Logo = objDR.GetString(4),
                                    OrdersWhatsapp = objDR.GetBoolean(5),
                                    Password = objDR.GetString(6),
                                    Phone = objDR.GetInt32(7),
                                    Slogan = objDR.GetString(8),
                                    user_email = objDR.GetString(9),
                                    User_id = objDR.GetInt32(10)
                                });                                                     
                        }
                    }
                    objDR.Close();                 
                }
                connection.Close();
            }
            return list.ToArray();
        }

        /// <summary>
        /// Devuelve los datos de un usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Datos del usuario</returns>
        internal  UserModel Get(int userId)
        {
            var items = new UserModel();
            using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("User_Get", connection);              
                    connection.Open();
                    using (var objDR = command.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (objDR.HasRows)
                        {
                            while (objDR.Read())
                            {
                                items = new UserModel();
                                while (objDR.Read())
                                {
                                    items.Business_Name = objDR.GetString(1);
                                    items.Direction = objDR.GetString(2);
                                    items.Ig = objDR.GetString(3);
                                    items.Logo = objDR.GetString(4);
                                    items.OrdersWhatsapp = objDR.GetBoolean(5);
                                    items.Password = objDR.GetString(6);
                                    items.Phone = objDR.GetInt32(7);
                                    items.Slogan = objDR.GetString(8);
                                    items.user_email = objDR.GetString(9);
                                    items.User_id = objDR.GetInt32(10);
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
        /// Graba el usuario
        /// </summary>
        /// <param name="data">Datos del usuario</param>
        /// <param name="userId">Identificador del usuario administrador que graba</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        internal  int Save(int?userId, UserModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;

                if (userId.HasValue && userId.Value != 0)
                {
                    objCmd = new SqlCommand("User_Update", connection);
                    objCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
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
                objCmd.Parameters.Add("@User_email", SqlDbType.NVarChar, 250).Value = data.user_email;

 
                connection.Open();
                var result=objCmd.ExecuteNonQuery();
                connection.Close();

                return result;
 
            }

 
            
        }

        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <returns>Retorna 0</returns>
        internal static int Delete(int userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;
                objCmd = new SqlCommand("User_Delete", connection);
                objCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                connection.Open();
                var result = objCmd.ExecuteNonQuery();
                connection.Close();

                return result;

            }

        }

    }
}