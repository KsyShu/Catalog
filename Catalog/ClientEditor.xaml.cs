using Catalog.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Catalog
{
    public sealed partial class ClientEditor : Page
    {
        private int? id;
        private int phone;
        public ClientEditor()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.CategoryListBox.ItemsSource = await CategoryService.getAll();

            if (e.Parameter == null)
            {

            }
            else
            {
                var client = e.Parameter as Services.Client;

                id = client.id;

                Fullname.Text = client.fullname;
                Username.Text = client.username;
                Address.Text = client.address;
                CategoryListBox.SelectedItem = (CategoryListBox.ItemsSource as ObservableCollection<Category>).Where(x => x.id == client.category_id).Last();
            }
        }
        public bool Validate()
        {
            if (String.IsNullOrEmpty(Fullname.Text) || String.IsNullOrEmpty(Username.Text) ||
                !int.TryParse(Phone.Text,out phone) || String.IsNullOrEmpty(Address.Text) ||
                String.IsNullOrEmpty(Password.Password)) 
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
                var client = new Client
                {
                    fullname = Fullname.Text,
                    username = Username.Text,
                    phone = Convert.ToInt32(Phone.Text),
                    address = Address.Text,
                    password = Password.Password,
                    category_id = (this.CategoryListBox.SelectedItem as Category).id
                };
                await ClientService.Add(client);
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
