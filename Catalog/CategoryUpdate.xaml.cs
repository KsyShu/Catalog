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
    public sealed partial class CategoryUpdate : Page
    {
        private int price;
        private int? id;

        public CategoryUpdate()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.ProductCategory.ItemsSource = await CategoryService.getAll();
            this.Catalog.ItemsSource = await CatalogService.getAll();

            if (e.Parameter == null)
            {

            }
            else
            {
                var category = e.Parameter as Services.Category;

                UpdateHeader.Text = $"Редактирование товара №{category.id}";

                id = category.id;

                CatName.Text = category.name;
                ProductPrice.Text = category.price.ToString();
                Catalog.SelectedItem = (Catalog.ItemsSource as ObservableCollection<Catalog.Services.Catalog>).Where(x => x.id == category.program).Last();
            }
        }
        public bool Validate()
        {
            if (String.IsNullOrEmpty(CatName.Text) || !int.TryParse(ProductPrice.Text, out price))
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
        private async void Update(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                var category = new Services.Category
                {
                    id = (this.ProductCategory.SelectedItem as Catalog.Services.Category).id,
                    name = CatName.Text,
                    price = this.price,
                    program = (this.Catalog.SelectedItem as Catalog.Services.Catalog).id
                };
                await CategoryService.Update(category);
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.GoBack();
            }
        }
        private async void Delete(object sender, RoutedEventArgs e)
        {
            ContentDialog deleteConfirmationDialog = new ContentDialog
            {
                Title = "Подтверждение удаления",
                Content = "Вы уверены, что хотите удалить элемент?",
                PrimaryButtonText = "Удалить",
                CloseButtonText = "Отмена"
            };

            ContentDialogResult result = await deleteConfirmationDialog.ShowAsync();

            // Если выбрана опция "Удалить", выполнить удаление
            if (result == ContentDialogResult.Primary)
            { 
                var category = new Services.Category
                {
                    id = (this.ProductCategory.SelectedItem as Catalog.Services.Category).id
                };
                await CategoryService.Delete(category);
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

