using Ado.NET_Store_task.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.NET_Store_task.Repostories
{
    public class Repo
    {
        public void GetAllProducts(List<Product> products)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                var query = "SELECT * FROM Product";

                SqlDataReader reader = null;

                using (var command = new SqlCommand(query, conn))
                {
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.Id = (int)reader[0];
                        product.Name = reader[1].ToString();
                        product.Prices = (decimal)reader[2];
                        product.CategoryId = (int)reader[3];
                        product.Image = reader[4].ToString();
                        products.Add(product);
                    }
                }
            }
        }


        public void GetAllCategories(List<Category> categories)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                var query = "SELECT * FROM Categories";

                SqlDataReader reader = null;

                using (var command = new SqlCommand(query, conn))
                {
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.Id = (int)reader[0];
                        category.Name = reader[1].ToString();
                        categories.Add(category);
                    }
                }
            }
        }

        public void InsertProduct()
        {
            using (var conn=new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

            }
        }
    }
}
