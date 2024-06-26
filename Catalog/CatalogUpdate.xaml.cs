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
    public sealed partial class CatalogUpdate : Page
    {
        private int? id;

        public CatalogUpdate()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.CatalogList.ItemsSource = await CatalogService.getAll();
            //this.ProductCategory.ItemsSource = await CategoryService.getAll();

            if (e.Parameter == null)
            {

            }
            else
            {
                var catalog = e.Parameter as Services.Catalog;

                UpdateHeader.Text = $"Редактирование товара №{catalog.id}";

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
                var catalog = new Services.Catalog
                    {
                        id=(this.CatalogList.SelectedItem as Catalog.Services.Catalog).id,
                        name = ProductName.Text/*,*/
                        //category_id = (this.ProductCategory.SelectedItem as Category).id
                    };
                    await CatalogService.Update(catalog);
                    Frame rootFrame = Window.Current.Content as Frame;
                    rootFrame.GoBack();
                
            }
        }
        private async void Delete(object sender, RoutedEventArgs e)
        {
            // Показать диалоговое окно подтверждения
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
                var catalog = new Services.Catalog
                {
                    id = (this.CatalogList.SelectedItem as Catalog.Services.Catalog).id
                };
                await CatalogService.Delete(catalog);

                // Переход назад после удаления
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

