﻿using Ado.NET_Store_task.ViewModel;
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
using System.Windows.Shapes;

namespace Ado.NET_Store_task.Views
{
    /// <summary>
    /// Interaction logic for AddProductUserControl.xaml
    /// </summary>
    public partial class AddProductUserControl : Window
    {
        public AddProductUserControl()
        {
            InitializeComponent();
            AddProductUserControlViewModel addProduct = new AddProductUserControlViewModel();
            this.DataContext = addProduct;
        }
    }
}
