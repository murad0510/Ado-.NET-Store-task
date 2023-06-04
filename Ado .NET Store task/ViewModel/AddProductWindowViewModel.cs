using Ado.NET_Store_task.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ado.NET_Store_task.ViewModel
{
    public class AddProductUserControlViewModel : BaseViewModel
    {
        public RelayCommand AddProduct { get; set; }

        private string foodName;

        public string FoodName
        {
            get { return foodName; }
            set { foodName = value; OnPropertyChanged(); }
        }

        private decimal foodPrice;

        public decimal FoodPrice
        {
            get { return foodPrice; }
            set { foodPrice = value; OnPropertyChanged(); }
        }

        public AddProductUserControlViewModel()
        {
            AddProduct = new RelayCommand((obj) =>
            {
                MessageBox.Show("a");
            });
        }

    }
}
