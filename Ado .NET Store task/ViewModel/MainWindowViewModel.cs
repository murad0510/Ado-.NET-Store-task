using Ado.NET_Store_task.Model;
using Ado.NET_Store_task.Views.UserControls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Ado.NET_Store_task.ViewModel
{
    public class MainWindowViewModel
    {
        static int a = 0;
        static int b = 0;
        public MainWindowViewModel()
        {
            List<Product> products = new List<Product>();

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
                        products.Add(product);
                    }
                }



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
                    cs.Margin = new Thickness(left, up, right, down);
                    cs.DataContext = foodUsercontrolViewModel;
                    App.MyPanel.Children.Add(cs);
                    //MessageBox.Show($"{App.MainWindowGrid.Children.Count}");
                }
            }
        }
    }
}
