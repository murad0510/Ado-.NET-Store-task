using Ado.NET_Store_task.Commands;
using Ado.NET_Store_task.Model;
using Ado.NET_Store_task.Repostories;
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
using System.Windows.Media.Media3D;

namespace Ado.NET_Store_task.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {

        //public RelayCommand CategoriesComboBox { get; set; }

        private ObservableCollection<Category> categoriesComboBox = new ObservableCollection<Category>();

        public ObservableCollection<Category> CategoriesComboBoxItemSource
        {
            get { return categoriesComboBox; }
            set { categoriesComboBox = value; OnPropertyChanged(); }
        }

        private Category category1;

        public Category SelectedItem
        {
            get { return category1; }
            set { category1 = value; OnPropertyChanged(); }
        }

        public void GetAll()
        {

        }


        public RelayCommand SelectionChanged { get; set; }
        Repo repo;

        public MainWindowViewModel()
        {
            List<Product> products = new List<Product>();
            List<Category> categories = new List<Category>();

            repo = new Repo();
            repo.GetAllProducts(products);

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

            for (int i = 0; i < categories.Count; i++)
            {
                CategoriesComboBoxItemSource.Add(categories[i]);
            }

            SelectionChanged = new RelayCommand((obj) =>
            {
                App.MyPanel.Children.Clear();
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].CategoryId == SelectedItem.Id)
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
                }
            });

        }
    }
}
