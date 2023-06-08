//using Ado.NET_Store_task.Commands;
//using Ado.NET_Store_task.Model;
//using Ado.NET_Store_task.Repostories;
//using Ado.NET_Store_task.Views.UserControls;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;

using Ado.NET_Store_task.Commands;
using Ado.NET_Store_task.Model;
using Ado.NET_Store_task.Repostories;
using Ado.NET_Store_task.Views;
using Ado.NET_Store_task.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Ado.NET_Store_task.ViewModel
{
    public class FoodsUserControlViewModel : BaseViewModel
    {
        public RelayCommand Delete { get; set; }
        public RelayCommand UpdateProduct { get; set; }

        private string foodName;

        public string Foodname
        {
            get { return foodName; }
            set { foodName = value; OnPropertyChanged(); }
        }

        private string image;

        public string Image
        {
            get { return image; }
            set { image = value; OnPropertyChanged(); }
        }

        private decimal foodPrice;

        public decimal FoodPrice
        {
            get { return foodPrice; }
            set { foodPrice = value; OnPropertyChanged(); }
        }

        private int category;

        public int Category
        {
            get { return category; }
            set { category = value; OnPropertyChanged(); }
        }

        Repo repo;

        public async void GetAllProducts(ObservableCollection<Product> products)
        {
            repo = new Repo();
            await repo.GetAllProducts(products);
        }

        public async void DeleteProduct(ObservableCollection<Product> products,int i)
        {
            repo = new Repo();

            await repo.DeleteProduct(products[i].Id);
        }

        public async void AdPanel()
        {
            repo = new Repo();

            await repo.AddPanelUserControl();
        }

        public FoodsUserControlViewModel()
        {
            ObservableCollection<Product> products = new ObservableCollection<Product>();

            GetAllProducts(products);

            Delete = new RelayCommand((obj) =>
            {
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].Name == Foodname)
                    {
                        DialogResult dialog = System.Windows.Forms.MessageBox.Show("Are you sure you want to delete the product?", "Delete product", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {
                            DeleteProduct(products,i);
                            products.Clear();
                            GetAllProducts(products);
                            App.MyPanel.Children.Clear();
                            AdPanel();
                            //FoodsUserControl cs;
                            //FoodsUserControlViewModel foodUsercontrolViewModel;
                            //int left = 70;
                            //int up = 10;
                            //int right = 0;
                            //int down = 70;
                            //for (int k = 0; k < products.Count; k++)
                            //{
                            //    cs = new FoodsUserControl();
                            //    foodUsercontrolViewModel = new FoodsUserControlViewModel();
                            //    foodUsercontrolViewModel.Foodname = products[k].Name;
                            //    foodUsercontrolViewModel.FoodPrice = products[k].Prices;
                            //    foodUsercontrolViewModel.Image = products[k].Image;
                            //    foodUsercontrolViewModel.Category = products[k].CategoryId;
                            //    cs.Margin = new Thickness(left, up, right, down);
                            //    cs.DataContext = foodUsercontrolViewModel;
                            //    App.MyPanel.Children.Add(cs);
                            //}
                        }
                    }
                }
            });

            UpdateProduct = new RelayCommand((obj) =>
            {
                ProductUpdateUserControl productUpdate = new ProductUpdateUserControl();
                ProductUpdateWindowViewModel productUpdateUserControl = new ProductUpdateWindowViewModel();

                var category = repo.SeacrhCategory(Category);

                productUpdateUserControl.FoodName = Foodname;

                productUpdateUserControl.FoodPrice = FoodPrice;

                productUpdateUserControl.FoodCategory = category.Name;

                productUpdate.DataContext = productUpdateUserControl;

                productUpdate.ShowDialog();
            });
        }
    }
}
