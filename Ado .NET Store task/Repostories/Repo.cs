using Ado.NET_Store_task.Model;
using Ado.NET_Store_task.ViewModel;
using Ado.NET_Store_task.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Ado.NET_Store_task.Repostories
{
    public class Repo
    {
        public async Task GetAllProducts(ObservableCollection<Products> products)
        {

            //using (var conn = new SqlConnection())
            //{
            //    conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
            //    conn.Open();

            //    var query = "SELECT * FROM Product";

            //    SqlDataReader reader = null;

            //    using (var command = new SqlCommand(query, conn))
            //    {
            //        reader = command.ExecuteReader();

            //        while (reader.Read())
            //        {
            //            Product product = new Product();
            //            product.Id = (int)reader[0];
            //            product.Name = reader[1].ToString();
            //            product.Prices = (decimal)reader[2];
            //            product.CategoryId = (int)reader[3];
            //            product.Image = reader[4].ToString();
            //            products.Add(product);
            //        }
            //    }
            //}



            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                var query = "SELECT * FROM Product";

                SqlDataReader reader = null;

                using (var command = new SqlCommand(query, conn))
                {
                    reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        Products product = new Products();
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

        public async Task AllProducts(Categories SelectedItem, Categories category)
        {
            ObservableCollection<Products> products = new ObservableCollection<Products>();
            await GetAllProducts(products);

            FoodsUserControl cs;
            FoodsUserControlViewModel foodUsercontrolViewModel;
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].CategoryId == SelectedItem.Id || SelectedItem.Name == category.Name)
                {
                    //SelectionChangeCategoriesComboBox(products[i].Name, products[i].Prices, products[i].Image, products[i].CategoryId);
                    cs = new FoodsUserControl();
                    foodUsercontrolViewModel = new FoodsUserControlViewModel();
                    foodUsercontrolViewModel.Foodname = products[i].Name;
                    foodUsercontrolViewModel.FoodPrice = products[i].Prices;
                    foodUsercontrolViewModel.Image = products[i].Image;
                    foodUsercontrolViewModel.Category = products[i].CategoryId;

                    cs.DataContext = foodUsercontrolViewModel;
                    App.MyPanel.Children.Add(cs);
                }
            }
        }


        //public async Task GetAllCategories(ObservableCollection<Category> categories)
        //{
        //    //using (var conn = new SqlConnection())
        //    //{
        //    //    conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
        //    //    conn.Open();

        //    //    var query = "SELECT * FROM Categories";

        //    //    SqlDataReader reader = null;

        //    //    using (var command = new SqlCommand(query, conn))
        //    //    {
        //    //        reader = command.ExecuteReader();

        //    //        while (reader.Read())
        //    //        {
        //    //            Category category = new Category();
        //    //            category.Id = (int)reader[0];
        //    //            category.Name = reader[1].ToString();
        //    //            categories.Add(category);
        //    //        }
        //    //    }
        //    //}

        //    using (var conn = new SqlConnection())
        //    {
        //        conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
        //        conn.Open();

        //        var query = "SELECT * FROM Categories";

        //        SqlDataReader reader = null;

        //        using (var command = new SqlCommand(query, conn))
        //        {
        //            reader = await command.ExecuteReaderAsync();
        //            while (await reader.ReadAsync())
        //            {
        //                Category category = new Category();
        //                category.Id = (int)reader[0];
        //                category.Name = reader[1].ToString();
        //                categories.Add(category);
        //            }
        //        }
        //    }
        //}


        public async Task GetAllCategories(ObservableCollection<Categories> categories)
        {
            //using (var conn = new SqlConnection())
            //{
            //    conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
            //    conn.Open();

            //    var query = "SELECT * FROM Categories";

            //    SqlDataReader reader = null;

            //    using (var command = new SqlCommand(query, conn))
            //    {
            //        reader = command.ExecuteReader();

            //        while (reader.Read())
            //        {
            //            Category category = new Category();
            //            category.Id = (int)reader[0];
            //            category.Name = reader[1].ToString();
            //            categories.Add(category);
            //        }
            //    }
            //}

            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                var query = "SELECT * FROM Categories";

                SqlDataReader reader = null;

                using (var command = new SqlCommand(query, conn))
                {
                    reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        Categories category = new Categories();
                        category.Id = (int)reader[0];
                        category.Name = reader[1].ToString();
                        categories.Add(category);
                    }
                }
            }
        }

        public async Task DeleteProduct(int id)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                SqlTransaction sqlTransaction = null;

                var query = "DELETE FROM Product WHERE Id=@id";

                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;

                sqlTransaction = conn.BeginTransaction();

                //SqlCommand command = new SqlCommand("INSERT INTO Product(Name) VALUES(@name)", conn);

                command.Transaction = sqlTransaction;

                SqlParameter parameterName = new SqlParameter();
                parameterName.ParameterName = "@id";
                parameterName.SqlDbType = SqlDbType.Int;
                parameterName.Value = id;

                command.Parameters.Add(parameterName);

                try
                {
                    await command.ExecuteNonQueryAsync();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                    sqlTransaction.Rollback();
                }
            }
        }

        //public Categories SeacrhCategor(int catego)
        //{
        //    Categories category = new Categories();
        //    using (var conn = new SqlConnection())
        //    {
        //        conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
        //        conn.Open();

        //        var query = "SELECT * FROM Categories WHERE Id=@category";

        //        SqlCommand command = conn.CreateCommand();
        //        command.CommandText = query;

        //        SqlParameter parameter = new SqlParameter();
        //        parameter.ParameterName = "@category";
        //        parameter.SqlDbType = SqlDbType.Int;
        //        parameter.Value = catego;

        //        command.Parameters.Add(parameter);
        //        using (var reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                category.Id = (int)reader[0];
        //                category.Name = reader[1].ToString();
        //            }
        //        }
        //    }

        //    return category;
        //}

        //public void SelectionChangeCategoriesComboBo(string name, decimal price, string image, int categoryId)
        //{
        //    FoodsUserControl cs;
        //    FoodsUserControlViewModel foodUsercontrolViewModel;
        //    int left = 70;
        //    int up = 10;
        //    int right = 0;
        //    int down = 70;
        //    cs = new FoodsUserControl();
        //    foodUsercontrolViewModel = new FoodsUserControlViewModel();
        //    foodUsercontrolViewModel.Foodname = name;
        //    foodUsercontrolViewModel.FoodPrice = price;
        //    foodUsercontrolViewModel.Image = image;
        //    foodUsercontrolViewModel.Category = categoryId;
        //    cs.Margin = new Thickness(left, up, right, down);
        //    cs.DataContext = foodUsercontrolViewModel;
        //    App.MyPanel.Children.Add(cs);
        //}

        //public Categories SeacrhCategoryNam(string name)
        //{
        //    Categories category = new Categories();
        //    using (var conn = new SqlConnection())
        //    {
        //        conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
        //        conn.Open();

        //        var query = "SELECT * FROM Categories WHERE Name=@name";

        //        SqlDataReader reader = null;

        //        using (var command = new SqlCommand(query, conn))
        //        {

        //            SqlParameter parameter = new SqlParameter();
        //            parameter.ParameterName = "@name";
        //            parameter.SqlDbType = SqlDbType.NVarChar;
        //            parameter.Value = name;

        //            command.Parameters.Add(parameter);

        //            reader = command.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                category.Id = (int)reader[0];
        //                category.Name = reader[1].ToString();
        //            }
        //        }
        //    }

        //    return category;
        //}

        public async Task AddCategoriesCombobox(ObservableCollection<Categories> CategoriesComboBoxItemSource)
        {
            ObservableCollection<Categories> categories = new ObservableCollection<Categories>();
            await GetAllCategories(categories);

            //CategoriesComboBoxItemSource = categories;

            for (int i = 0; i < categories.Count; i++)
            {
                CategoriesComboBoxItemSource.Add(categories[i]);
            }
        }

        public async Task AddPanelUserControl()
        {
            ObservableCollection<Products> products = new ObservableCollection<Products>();
            await GetAllProducts(products);
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

        public async Task AddProduct(string name, decimal price, int categoryId)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                SqlTransaction sqlTransaction = null;

                sqlTransaction = conn.BeginTransaction();

                SqlCommand command = new SqlCommand("INSERT INTO Product(Name,Prices,CategoriesId) VALUES(@name,@price,@categoryId)", conn);
                command.Transaction = sqlTransaction;

                SqlParameter parameterCategoryId = new SqlParameter();
                parameterCategoryId.ParameterName = "@categoryId";
                parameterCategoryId.SqlDbType = SqlDbType.Int;
                parameterCategoryId.Value = categoryId;

                SqlParameter parameterName = new SqlParameter();
                parameterName.ParameterName = "@name";
                parameterName.SqlDbType = SqlDbType.NVarChar;
                parameterName.Value = name;

                SqlParameter parameterPrice = new SqlParameter();
                parameterPrice.ParameterName = "@price";
                parameterPrice.SqlDbType = SqlDbType.Money;
                parameterPrice.Value = price;

                command.Parameters.Add(parameterName);
                command.Parameters.Add(parameterCategoryId);
                command.Parameters.Add(parameterPrice);

                try
                {
                    await command.ExecuteNonQueryAsync();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                    sqlTransaction.Rollback();
                }
            }
        }


        public async Task UpdateProduct(string oldname, string newname, decimal price)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                var query = "UPDATE Product SET Name=@newname , Prices=@price WHERE Name=@oldname";


                //SqlCommand command = conn.CreateCommand();
                //command.CommandText = query;

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
                        await command.ExecuteNonQueryAsync();
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
