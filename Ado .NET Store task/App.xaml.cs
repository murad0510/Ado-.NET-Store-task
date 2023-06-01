using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ado.NET_Store_task
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Grid MainWindowGrid { get; set; } = new Grid();
        public static WrapPanel MyPanel { get; set; } = new WrapPanel();
    }
}
