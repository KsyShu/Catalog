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
    public sealed partial class ClientList : Page
    {
        public ClientList()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.ClientListBox.ItemsSource = await ClientService.getAll();
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(ClientUpdate));
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(ClientEditor));
        }

        private void SelectedClient(object sender, SelectionChangedEventArgs e)
        {
            var selectedClient = this.ClientListBox.SelectedItem as Services.Client;

            if (selectedClient != null)
            {
                string message = "ФИО: " + selectedClient.fullname + "\n" +
                                 "Номер телефона: " + selectedClient.phone + "\n" +
                                 "Адрес: " + selectedClient.address + "\n" +
                                 "Подписка: " + selectedClient.category_id + "\n" +
                                 "Username: " + selectedClient.username + "\n" +
                                 "Пароль: " + selectedClient.password;

                var dialog = new MessageDialog(message);
                dialog.ShowAsync();
            }
        }
    }
}
