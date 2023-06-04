using Ado.NET_Store_task.Commands;
using Ado.NET_Store_task.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ado.NET_Store_task.ViewModel
{
    public class ProductUpdateWindowViewModel : BaseViewModel
    {
        public RelayCommand UpdateProduct { get; set; }

        private string foodName;

        public string FoodName
        {
            get { return foodName; }
            set { foodName = value; OnPropertyChanged(); }
        }

        private decimal foodprice;

        public decimal FoodPrice
        {
            get { return foodprice; }
            set { foodprice = value; OnPropertyChanged(); }
        }

        private string foodcategory;

        public string FoodCategory
        {
            get { return foodcategory; }
            set { foodcategory = value; OnPropertyChanged(); }
        }

        private string newfoodName;

        public string NewFoodName
        {
            get { return newfoodName; }
            set { newfoodName = value; OnPropertyChanged(); }
        }

        private decimal newfoodprice;

        public decimal NewFoodPrice
        {
            get { return newfoodprice; }
            set { newfoodprice = value; OnPropertyChanged(); }
        }

        public ProductUpdateWindowViewModel()
        {
            UpdateProduct = new RelayCommand((obj) =>
            {
                if (NewFoodName == null || NewFoodName.Trim() == string.Empty || NewFoodPrice <= 0)
                {
                    MessageBox.Show("You are not typing correct values !!!");
                }
                else if (NewFoodPrice != 0 && NewFoodName != string.Empty)
                {
                    Repo repo = new Repo();
                    repo.UpdateProduct(FoodName, NewFoodName, NewFoodPrice);
                    repo.AddPanelUserControl();
                    MessageBox.Show("Update was successful");
                }
            });
        }

    }
}
