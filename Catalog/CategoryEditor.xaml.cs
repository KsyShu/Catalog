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
    public sealed partial class CategoryEditor : Page
    {
        private int price;
        private int? id;
        public CategoryEditor()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.Catalog.ItemsSource = await CatalogService.getAll();

            if (e.Parameter == null)
            {

            }
            else
            {
                var category = e.Parameter as Category;

                AddButton.Visibility = Visibility.Collapsed;
                AddHeader.Visibility = Visibility.Collapsed;

                id = category.id;

                CatName.Text = category.name;
                ProductPrice.Text = category.price.ToString();
                Catalog.SelectedItem = (Catalog.ItemsSource as ObservableCollection<Catalog.Services.Catalog>).Where(x => x.id == category.program).Last();
            }
        }
        public bool Validate()
        {
            if (String.IsNullOrEmpty(CatName.Text) ||!int.TryParse(ProductPrice.Text, out price))
            {
                var dialog = new ContentDialog()
                {
                    Title = "Ошибка при заполнении формы",
                    IsPrimaryButtonEnabled = true,
                    PrimaryButtonText = "Ok"
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
                var category = new Services.Category
                {
                    name = CatName.Text,
                    price = Convert.ToInt32(ProductPrice.Text),
                    program = (this.Catalog.SelectedItem as Catalog.Services.Catalog).id
                };
                await CategoryService.Add(category);
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.GoBack();
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.GoBack();
        }
    }
}
