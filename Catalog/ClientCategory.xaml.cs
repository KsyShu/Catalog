using Catalog.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пользовательский элемент управления" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234236

namespace Catalog
{
    public sealed partial class ClientCategory : Page
    {
        public ClientCategory()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.CategoryListBox.ItemsSource = await CategoryService.getAll();
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
    }
}
