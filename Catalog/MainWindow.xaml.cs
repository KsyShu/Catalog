using Catalog.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    public sealed partial class MainWindow : Page
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //Username.Text = EmployeeService.Profile.fullname;
            CatalogButton.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(CatalogList));
        }

        private void GoCatalog(object sender, RoutedEventArgs e)
        {
            CatalogButton.Style = null;
            CategoryButton.Style = null;
            EatingButton.Style = null;
            ClientButton.Style = null;  
            UsersButton.Style = null;
            CatalogButton.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(CatalogList));
        }

        private void GoCategory(object sender, RoutedEventArgs e)
        {
            CatalogButton.Style = null;
            CategoryButton.Style = null;
            EatingButton.Style = null;
            ClientButton.Style = null;
            UsersButton.Style = null;
            CategoryButton.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(CategoryList));
        }

        private void GoUsers(object sender, RoutedEventArgs e)
        {
            CatalogButton.Style = null;
            CategoryButton.Style = null;
            EatingButton.Style = null;
            ClientButton.Style = null;
            UsersButton.Style = null;
            UsersButton.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(EmployeeList));
        }
        private void GoClient(object sender, RoutedEventArgs e)
        {
            CatalogButton.Style = null;
            CategoryButton.Style = null;
            EatingButton.Style = null;
            ClientButton.Style = null;
            UsersButton.Style = null;
            ClientButton.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(ClientList));
        }
        private void GoEating(object sender, RoutedEventArgs e)
        {
            CatalogButton.Style = null;
            CategoryButton.Style = null;
            EatingButton.Style = null;
            ClientButton.Style = null;
            UsersButton.Style = null;
            EatingButton.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(EatingList));
        }
        private void Logout(object sender, RoutedEventArgs e)
        {
            EmployeeService.Logout();

            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(LoginPage));
        }

        private void frx_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
