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
    public sealed partial class ClientUpdate : Page
    {
        private int? id;
        private int phone;

        public ClientUpdate()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.CategoryListBox.ItemsSource = await CategoryService.getAll();

            base.OnNavigatedTo(e);

            this.ClientList.ItemsSource = await ClientService.getAll();

            if (e.Parameter == null)
            {

            }
            else
            {
                var client = e.Parameter as Services.Client;

                id = client.id;

                Fullname.Text = client.fullname;
                Username.Text = client.username;
                Phone.Text = client.phone.ToString();
                Address.Text = client.address;
                CategoryListBox.SelectedItem = (CategoryListBox.ItemsSource as ObservableCollection<Category>).Where(x => x.id == client.category_id).Last();
            }
        }

        public bool Validate()
        {
            if (String.IsNullOrEmpty(Fullname.Text) || String.IsNullOrEmpty(Username.Text) ||
                String.IsNullOrEmpty(Password.Password) || !int.TryParse(Phone.Text, out phone) || String.IsNullOrEmpty(Address.Text))
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
                var client = new Services.Client
                {
                    id = (this.ClientList.SelectedItem as Catalog.Services.Client).id,
                    fullname = Fullname.Text,
                    username = Username.Text,
                    password = Password.Password,
                    phone = this.phone,
                    address = Address.Text,
                    category_id = (this.CategoryListBox.SelectedItem as Category).id
                };

                await ClientService.Update(client);

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
                var client = new Services.Client
                {
                    id = (this.ClientList.SelectedItem as Catalog.Services.Client).id
                };
                await ClientService.Delete(client);
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

