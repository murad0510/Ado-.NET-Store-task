using Ado.NET_Store_task.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ado.NET_Store_task.Views.UserControls
{
    /// <summary>
    /// Interaction logic for FoodsUserControl.xaml
    /// </summary>
    public partial class FoodsUserControl : UserControl
    {
        public FoodsUserControl()
        {
            InitializeComponent();
            FoodsUserControlViewModel fucvm = new FoodsUserControlViewModel();

            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(open.FileName));
                    previre.Source = bitmap;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            this.DataContext = fucvm;
        }
    }
}
