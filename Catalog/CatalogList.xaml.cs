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
    public sealed partial class CatalogList : Page
    {
        public CatalogList()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.CatalogListBox.ItemsSource = await CatalogService.getAll();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(CatalogEditor));
        }

        private void SelectedCatalog(object sender, SelectionChangedEventArgs e)
        {
            var selectedCatalog = this.CatalogListBox.SelectedItem as Services.Catalog;

            if (selectedCatalog != null)
            {
                string message = "Название: " + selectedCatalog.name + "\n" +
                                 "Категории: " + selectedCatalog.category;

                var dialog = new MessageDialog(message);
                dialog.ShowAsync();
            }
        }
        private void Update(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(CatalogUpdate));
        }
    }
}
