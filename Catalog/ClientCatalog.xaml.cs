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

// Документацию по шаблону элемента "Пользовательский элемент управления" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234236

namespace Catalog
{
    public sealed partial class ClientCatalog : Page
    {
        public ClientCatalog()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.CatalogListBox.ItemsSource = await CatalogService.getAll();
        }
        private void SelectedCatalog(object sender, SelectionChangedEventArgs e)
        {
            var selectedCatalog = this.CatalogListBox.SelectedItem as Services.Catalog;

            if (selectedCatalog != null)
            {
                string message = "Название: " + selectedCatalog.name + "\n"; /*+*/
                                 //"Категория: " + selectedCatalog.category_id;

                var dialog = new MessageDialog(message);
                dialog.ShowAsync();
            }
        }
    }
}
