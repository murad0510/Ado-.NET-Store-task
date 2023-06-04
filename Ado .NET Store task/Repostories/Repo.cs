using Ado.NET_Store_task.Model;
using Ado.NET_Store_task.ViewModel;
using Ado.NET_Store_task.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
                SqlCommand command = new SqlCommand("DELETE FROM Product WHERE Id=@id", conn);
                command.Transaction = sqlTransaction;

                SqlParameter parameterName = new SqlParameter();
                parameterName.ParameterName = "@id";
                parameterName.SqlDbType = SqlDbType.Int;
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

        public Category SeacrhCategory(int catego)
        {
            Category category = new Category();
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                var query = "SELECT * FROM Categories WHERE Id=@category";

                SqlDataReader reader = null;

                using (var command = new SqlCommand(query, conn))
                {

                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@category";
                    parameter.SqlDbType = SqlDbType.Int;
                    parameter.Value = catego;

                    command.Parameters.Add(parameter);

                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        category.Id = (int)reader[0];
                        category.Name = reader[1].ToString();
                    }
                }
            }

            return category;
        }

        public Category SeacrhCategoryName(string name)
        {
            Category category = new Category();
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                var query = "SELECT * FROM Categories WHERE Name=@name";

                SqlDataReader reader = null;

                using (var command = new SqlCommand(query, conn))
                {

                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@name";
                    parameter.SqlDbType = SqlDbType.NVarChar;
                    parameter.Value = name;

                    command.Parameters.Add(parameter);

                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        category.Id = (int)reader[0];
                        category.Name = reader[1].ToString();
                    }
                }
            }

            return category;
        }

        public void AddPanelUserControl()
        {
            List<Product> products = new List<Product>();
            GetAllProducts(products);
            App.MyPanel.Children.Clear();
            FoodsUserControl cs;
            FoodsUserControlViewModel foodUsercontrolViewModel;
            int left = 70;
            int up = 10;
            int right = 0;
            int down = 70;
            for (int i = 0; i < products.Count; i++)
            {
                cs = new FoodsUserControl();
                foodUsercontrolViewModel = new FoodsUserControlViewModel();
                foodUsercontrolViewModel.Foodname = products[i].Name;
                foodUsercontrolViewModel.FoodPrice = products[i].Prices;
                foodUsercontrolViewModel.Image = products[i].Image;
                foodUsercontrolViewModel.Category = products[i].CategoryId;

                cs.Margin = new Thickness(left, up, right, down);
                cs.DataContext = foodUsercontrolViewModel;
                App.MyPanel.Children.Add(cs);
            }
        }

        //public void AddProduct(string name,decimal price,string category)
        //{
        //    using (var conn = new SqlConnection())
        //    {
        //        conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
        //        conn.Open();

        //        SqlTransaction sqlTransaction = null;

        //        sqlTransaction = conn.BeginTransaction();

        //        //SqlCommand command = new SqlCommand("INSERT INTO Product(Name) VALUES(@name)", conn);
        //        SqlCommand command = new SqlCommand("INSERT INTO Product(Name,Price,CategoriesId) VALUES(@name,@price,@category)", conn);
        //        command.Transaction = sqlTransaction;

        //        SqlParameter parameterName = new SqlParameter();
        //        parameterName.ParameterName = "@id";
        //        parameterName.SqlDbType = SqlDbType.Int;
        //        parameterName.Value = id;

        //        command.Parameters.Add(parameterName);

        //        try
        //        {
        //            command.ExecuteNonQuery();
        //            sqlTransaction.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"{ex.Message}");
        //            sqlTransaction.Rollback();
        //        }
        //    }
        //}


        public void UpdateProduct(string oldname, string newname, decimal price)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                var query = "UPDATE Product SET Name=@newname , Prices=@price WHERE Name=@oldname";

                SqlTransaction sqlTransaction = null;

                sqlTransaction = conn.BeginTransaction();

                using (var command = new SqlCommand(query, conn))
                {
                    command.Transaction = sqlTransaction;
                    SqlParameter parameterName = new SqlParameter();
                    parameterName.ParameterName = "@oldname";
                    parameterName.Value = oldname;
                    parameterName.SqlDbType = SqlDbType.NVarChar;

                    SqlParameter parameterNewName = new SqlParameter();
                    parameterNewName.ParameterName = "@newname";
                    parameterNewName.Value = newname;
                    parameterNewName.SqlDbType = SqlDbType.NVarChar;

                    SqlParameter parameterPrice = new SqlParameter();
                    parameterPrice.ParameterName = "@price";
                    parameterPrice.Value = price;
                    parameterPrice.SqlDbType = SqlDbType.Money;

                    command.Parameters.Add(parameterName);
                    command.Parameters.Add(parameterPrice);
                    command.Parameters.Add(parameterNewName);

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
}
