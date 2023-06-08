using Ado.NET_Store_task.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ado.NET_Store_task.ViewModel;
using Ado.NET_Store_task.Views.UserControls;
using System.Windows;

namespace Ado.NET_Store_task.Services
{
    public class CategoryServices
    {
        public Categories SeacrhCategory(int catego)
        {
            Categories category = new Categories();
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                var query = "SELECT * FROM Categories WHERE Id=@category";

                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@category";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Value = catego;

                command.Parameters.Add(parameter);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        category.Id = (int)reader[0];
                        category.Name = reader[1].ToString();
                    }
                }
            }

            return category;
        }

        public void SelectionChangeCategoriesComboBox(string name, decimal price, string image, int categoryId)
        {
            FoodsUserControl cs;
            FoodsUserControlViewModel foodUsercontrolViewModel;
            int left = 70;
            int up = 10;
            int right = 0;
            int down = 70;
            cs = new FoodsUserControl();
            foodUsercontrolViewModel = new FoodsUserControlViewModel();
            foodUsercontrolViewModel.Foodname = name;
            foodUsercontrolViewModel.FoodPrice = price;
            foodUsercontrolViewModel.Image = image;
            foodUsercontrolViewModel.Category = categoryId;
            cs.Margin = new Thickness(left, up, right, down);
            cs.DataContext = foodUsercontrolViewModel;
            App.MyPanel.Children.Add(cs);
        }

        public Categories SeacrhCategoryName(string name)
        {
            Categories category = new Categories();
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
    }
}
