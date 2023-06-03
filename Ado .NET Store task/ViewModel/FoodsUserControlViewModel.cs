using Ado.NET_Store_task.Commands;
using Ado.NET_Store_task.Model;
using Ado.NET_Store_task.Repostories;
using Ado.NET_Store_task.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ado.NET_Store_task.ViewModel
{
    public class FoodsUserControlViewModel : BaseViewModel
    {
        public RelayCommand Delete { get; set; }

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

        public FoodsUserControlViewModel()
        {
            List<Product> products = new List<Product>();
            Repo repo = new Repo();
            repo.GetAllProducts(products);

            Delete = new RelayCommand((obj) =>
            {
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].Name == Foodname)
                    {
                        repo.DeleteProduct(products[i].CategoryId);
                        FoodsUserControl cs;
                        FoodsUserControlViewModel foodUsercontrolViewModel;
                        int left = 70;
                        int up = 10;
                        int right = 0;
                        int down = 70;
                        for (int k = 0; k < products.Count; k++)
                        {
                            cs = new FoodsUserControl();
                            foodUsercontrolViewModel = new FoodsUserControlViewModel();
                            foodUsercontrolViewModel.Foodname = products[k].Name;
                            foodUsercontrolViewModel.FoodPrice = products[k].Prices;
                            foodUsercontrolViewModel.Image = products[k].Image;
                            cs.Margin = new Thickness(left, up, right, down);
                            cs.DataContext = foodUsercontrolViewModel;
                            App.MyPanel.Children.Add(cs);
                        }
                        break;
                    }
                }
            });
        }
    }
}
