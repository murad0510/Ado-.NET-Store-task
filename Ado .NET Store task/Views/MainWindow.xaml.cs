using Ado.NET_Store_task.Model;
using Ado.NET_Store_task.ViewModel;
using Ado.NET_Store_task.Views.UserControls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
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

namespace Ado.NET_Store_task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.MainWindowGrid = myGrid;
            App.MyPanel = MyPanel;

            MainWindowViewModel mwvm = new MainWindowViewModel();
            this.DataContext = mwvm;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog open = new OpenFileDialog();

        //    if (open.ShowDialog() == true)
        //    {
        //        try
        //        {
        //            BitmapImage bitmap = new BitmapImage(new Uri(open.FileName));
        //            previre.Source = bitmap;
        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }
        //    }
        //}
    }
}
