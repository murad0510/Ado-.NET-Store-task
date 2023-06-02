using Ado.NET_Store_task.Commands;
using Ado.NET_Store_task.Model;
using Ado.NET_Store_task.Views.UserControls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Ado.NET_Store_task.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {

        //public RelayCommand CategoriesComboBox { get; set; }

        private ObservableCollection<Category> categoriesComboBox=new ObservableCollection<Category>();

        public ObservableCollection<Category> CategoriesComboBoxItemSource
        {
            get { return categoriesComboBox; }
            set { categoriesComboBox = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            List<Product> products = new List<Product>();
            List<Category> categories = new List<Category>();

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
                cs.Margin = new Thickness(left, up, right, down);
                cs.DataContext = foodUsercontrolViewModel;
                App.MyPanel.Children.Add(cs);
            }

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

            for (int i = 0; i < categories.Count; i++)
            {
                CategoriesComboBoxItemSource.Add(categories[i]);
            }

        }
    }
}
