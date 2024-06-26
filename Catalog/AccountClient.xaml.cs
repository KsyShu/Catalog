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

// Документацию по шаблону элемента "Пользовательский элемент управления" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234236

namespace Catalog
{
    public sealed partial class AccountClient : Page
    {
        public AccountClient()
        {
            this.InitializeComponent();

            Client client = Auth.UserClient;
            FIO.Text = client.fullname;
            Username.Text = client.username;
            Phone.Text = client.phone.ToString();
            Address.Text = client.address;
            Category.Text = client.category_id.ToString();
        }
        private void Edit(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(AccountClientUpdate), Auth.UserClient);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.GoBack();
        }
    }
}
