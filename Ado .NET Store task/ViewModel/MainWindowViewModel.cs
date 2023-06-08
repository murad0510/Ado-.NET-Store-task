using Ado.NET_Store_task.Commands;
using Ado.NET_Store_task.Model;
using Ado.NET_Store_task.Repostories;
using Ado.NET_Store_task.Services;
using Ado.NET_Store_task.Views;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Ado.NET_Store_task.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {

        //public RelayCommand CategoriesComboBox { get; set; }
        public RelayCommand AddProduct { get; set; }

        private ObservableCollection<Categories> categoriesComboBox = new ObservableCollection<Categories>();

        public ObservableCollection<Categories> CategoriesComboBoxItemSource
        {
            get { return categoriesComboBox; }
            set { categoriesComboBox = value; OnPropertyChanged(); }
        }

        private Categories category1;

        public Categories SelectedItem
        {
            get { return category1; }
            set { category1 = value; OnPropertyChanged(); }
        }

        private int selectedindex;

        public int SelectedIndex
        {
            get { return selectedindex; }
            set { selectedindex = value; OnPropertyChanged(); }
        }



        public async void GetAll(ObservableCollection<Products> products, ObservableCollection<Categories> categories)
        {
            repo = new Repo();
            //await repo.GetAllProducts(products);
            await repo.AddPanelUserControl();
        }

        public async void GetAllProducts(Categories SelectedItem, Categories category)
        {
            repo = new Repo();

            await repo.AllProducts(SelectedItem, category);
        }

        public async void GetAllCategories(ObservableCollection<Categories> categories)
        {
            repo = new Repo();

            await repo.AddCategoriesCombobox(categories);
        }


        public RelayCommand SelectionChanged { get; set; }
        Repo repo;


        public MainWindowViewModel()
        {
            ObservableCollection<Products> products = new ObservableCollection<Products>();
            ObservableCollection<Categories> categories = new ObservableCollection<Categories>();
            CategoryServices categoryServices= new CategoryServices();

            GetAll(products, categories);

            SelectedIndex = categoryServices.SeacrhCategoryName("Butun mehsullar").Id - 1;

            GetAllCategories(CategoriesComboBoxItemSource);

            //FoodsUserControl cs;
            //FoodsUserControlViewModel foodUsercontrolViewModel;
            //int left = 70;
            //int up = 10;
            //int right = 0;
            //int down = 70;

            //for (int i = 0; i < products.Count; i++)
            //{
            //    cs = new FoodsUserControl();
            //    foodUsercontrolViewModel = new FoodsUserControlViewModel();
            //    foodUsercontrolViewModel.Foodname = products[i].Name;
            //    foodUsercontrolViewModel.FoodPrice = products[i].Prices;
            //    foodUsercontrolViewModel.Image = products[i].Image;
            //    foodUsercontrolViewModel.Category = products[i].CategoryId;

            //    cs.Margin = new Thickness(left, up, right, down);
            //    cs.DataContext = foodUsercontrolViewModel;
            //    App.MyPanel.Children.Add(cs);
            //}

            //for (int i = 0; i < categories.Count; i++)
            //{
            //    CategoriesComboBoxItemSource.Add(categories[i]);
            //}


            SelectionChanged = new RelayCommand((obj) =>
            {
                App.MyPanel.Children.Clear();
                products.Clear();
                //GetAllProducts(products);
                CategoryServices services= new CategoryServices();

                var category = services.SeacrhCategoryName("Butun mehsullar");

                repo = new Repo();
                GetAllProducts(SelectedItem, category);

                //for (int i = 0; i < products.Count; i++)
                //{
                //    if (products[i].CategoryId == SelectedItem.Id || SelectedItem.Name == category.Name)
                //    {
                //        repo.SelectionChangeCategoriesComboBox(products[i].Name, products[i].Prices, products[i].Image, products[i].CategoryId);
                //        //cs = new FoodsUserControl();
                //        //foodUsercontrolViewModel = new FoodsUserControlViewModel();
                //        //foodUsercontrolViewModel.Foodname = products[i].Name;
                //        //foodUsercontrolViewModel.FoodPrice = products[i].Prices;
                //        //foodUsercontrolViewModel.Image = products[i].Image;
                //        //foodUsercontrolViewModel.Category = products[i].CategoryId;

                //        //cs.Margin = new Thickness(left, up, right, down);
                //        //cs.DataContext = foodUsercontrolViewModel;
                //        //App.MyPanel.Children.Add(cs);
                //    }
                //}
            });

            AddProduct = new RelayCommand((obj) =>
            {
                AddProductUserControl addProduct = new AddProductUserControl();
                addProduct.ShowDialog();
            });

        }
    }
}
