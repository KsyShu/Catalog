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
using System.Collections.ObjectModel;

namespace Catalog
{
    public sealed partial class CatalogEditor : Page
    {
        private int? id;
        public CatalogEditor()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //this.ProductCategory.ItemsSource = await CategoryService.getAll();
   
            if (e.Parameter == null)
            {

            }
            else
            {
                var catalog = e.Parameter as Services.Catalog;

                AddButton.Visibility = Visibility.Collapsed;
                AddHeader.Visibility = Visibility.Collapsed;

                id = catalog.id;

                ProductName.Text = catalog.name;
                //ProductCategory.SelectedItem = (ProductCategory.ItemsSource as ObservableCollection<Category>).Where(x => x.id == catalog.category_id).Last();
            }
        }

        public bool Validate()
        {
            if (String.IsNullOrEmpty(ProductName.Text)) 
            {
                var dialog = new ContentDialog()
                {
                    Title = "Ошибка при заполнении формы",
                    CloseButtonText = "Ok",
                    IsPrimaryButtonEnabled = false
                };
                dialog.ShowAsync();
                return false;
            }
            return true;
        }
        private async void Add(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                var catalog = new Services.Catalog
                {
                    name = ProductName.Text/*,*/
                    //category_id = (this.ProductCategory.SelectedItem as Category).id
                };
                await CatalogService.Add(catalog);
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.GoBack();
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.GoBack();
        }
        private void AddHeader_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
