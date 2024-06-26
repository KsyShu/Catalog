using Catalog.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Catalog
{
    public sealed partial class CategoryList : Page
    {
        public CategoryList()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.CategoryListBox.ItemsSource = await CategoryService.getAll();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(CategoryEditor));
        }

        private void SelectedCategory(object sender, SelectionChangedEventArgs e)
        {
            //var selectedCategory = this.CategoryListBox.SelectedItem as Services.Category;

            //if (selectedCategory != null)
            //{
            //    string message = "Название: " + selectedCategory.name + "\n" +
            //                     "Цена: " + selectedCategory.price;

            //    var dialog = new MessageDialog(message);
            //    dialog.ShowAsync();
            //}
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Menu));
        }
        private void Update(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(CategoryUpdate));
        }
    }
}
