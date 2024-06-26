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

namespace Catalog
{
    public sealed partial class MainWindow2 : Page
    {
        public MainWindow2()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //Username.Text = ClientService.Profile.fullname;
            AccountButton.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(AccountClient));
        }
        private void GoCatalog(object sender, RoutedEventArgs e)
        {
            CatalogButton.Style = null;
            CategoryButton.Style = null;
            AccountButton.Style = null;
            CatalogButton.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(ClientCatalog));
        }
        private void GoCategory(object sender, RoutedEventArgs e)
        {
            CatalogButton.Style = null;
            CategoryButton.Style = null;
            AccountButton.Style = null;
            CategoryButton.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(ClientCategory));
        }
        private void GoAccount(object sender, RoutedEventArgs e)
        {
            CatalogButton.Style = null;
            CategoryButton.Style = null;
            AccountButton.Style = null;
            AccountButton.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(AccountClient));
        }
        private void Logout(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(LoginPage));
        }
    }
}
