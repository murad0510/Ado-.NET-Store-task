using Ado.NET_Store_task.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public void DeleteProduct(int id)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                SqlTransaction sqlTransaction = null;

                sqlTransaction = conn.BeginTransaction();

                //SqlCommand command = new SqlCommand("INSERT INTO Product(Name) VALUES(@name)", conn);
                SqlCommand command = new SqlCommand("DELETE FROM Product WHERE Id=@id");
                command.Transaction = sqlTransaction;

                SqlParameter parameterName = new SqlParameter();
                parameterName.ParameterName = "@id";
                parameterName.SqlDbType=SqlDbType.Int;
                parameterName.Value = id;

                command.Parameters.Add(parameterName);

                try
                {
                    command.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                    sqlTransaction.Rollback();
                }
            }
        }
    }
}
