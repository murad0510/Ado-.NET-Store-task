using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.NET_Store_task.ViewModel
{
    public class FoodsUserControlViewModel : BaseViewModel
    {
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
    }
}
