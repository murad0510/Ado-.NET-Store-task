﻿using Ado.NET_Store_task.Commands;
using Ado.NET_Store_task.Model;
using Ado.NET_Store_task.Repostories;
using Ado.NET_Store_task.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ado.NET_Store_task.ViewModel
{
    public class AddProductUserControlViewModel : BaseViewModel
    {
        public RelayCommand AddProduct { get; set; }
        public RelayCommand SelectionChanged { get; set; }

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

        private ObservableCollection<Category> categories;

        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        private Category selectedItem;

        public Category SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(); }
        }

        Repo repo;

        public AddProductUserControlViewModel()
        {
            ObservableCollection<Category> categories = new ObservableCollection<Category>();
            ObservableCollection<Product> products = new ObservableCollection<Product>();

            repo = new Repo();
            repo.GetAllCategories(categories);
            Categories = categories;

            AddProduct = new RelayCommand((obj) =>
            {
                if (FoodName != null && FoodPrice != 0)
                {
                    repo.AddProduct(FoodName, FoodPrice, SelectedItem.Id);
                    products.Clear();
                    repo.GetAllProducts(products);
                    App.MyPanel.Children.Clear();
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
                        foodUsercontrolViewModel.Category = products[k].CategoryId;
                        cs.Margin = new Thickness(left, up, right, down);
                        cs.DataContext = foodUsercontrolViewModel;
                        App.MyPanel.Children.Add(cs);
                    }
                    MessageBox.Show($"Product added successfully");
                }
                else
                { 
                    MessageBox.Show("You type any information incorrectly !!!");
                }
            });
        }
    }
}
