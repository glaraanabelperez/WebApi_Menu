using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApi_Menu_Practica.Models;

namespace WebApi_Menu_Practica.Data
{
    public class Product
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["MenuConnection"].ConnectionString;
        static string queryString = "SELECT *  FROM dbo.Products;";

        internal static ProductModel List()
        {
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
            }
            return null;
        }
    }
}