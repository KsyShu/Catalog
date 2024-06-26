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
    public sealed partial class AccountClientUpdate : Page
    {
        private int? id;
        private int phone;
        public AccountClientUpdate()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.Category.ItemsSource = await CategoryService.getAll();
            base.OnNavigatedTo(e);

            if (e.Parameter == null)
            {

            }
            else
            {
                var client = e.Parameter as Services.Client;

                id = client.id;
                FIO.Text = client.fullname;
                Username.Text = client.username;
                Phone.Text = client.phone.ToString();
                Address.Text = client.address;
                Category.SelectedItem = (Category.ItemsSource as ObservableCollection<Category>).Where(x => x.id == client.category_id).Last();
            }
        }
        public bool Validate()
        {
            if (String.IsNullOrEmpty(FIO.Text) || String.IsNullOrEmpty(Username.Text) || !int.TryParse(Phone.Text, out phone) || String.IsNullOrEmpty(Address.Text) || String.IsNullOrEmpty(Password.Password))
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
        private async void Update(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                var client = new Services.Client
                {
                    id = id,
                    fullname = FIO.Text,
                    username = Username.Text,
                    password = Password.Password,
                    phone = this.phone,
                    address = Address.Text,
                    category_id = (this.Category.SelectedItem as Category).id
                };

                await ClientService.Update(client);

                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.GoBack();
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.GoBack();
        }
    }
}
