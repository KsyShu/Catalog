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
    public sealed partial class EmployeeList : Page
    {
        public EmployeeList()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.EmployeeListBox.ItemsSource = await EmployeeService.getAll();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(EmployeeEditor));
        }
        private void Update(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(EmployeeUpdate));
        }
        private void SelectedEmployee(object sender, SelectionChangedEventArgs e)
        {
            var selectedEmployee = this.EmployeeListBox.SelectedItem as Services.Employee;

            if (selectedEmployee != null)
            {
                string message = "ФИО: " + selectedEmployee.fullname + "\n" +
                                 "Username: " + selectedEmployee.username + "\n" +
                                 "Пароль: " + selectedEmployee.password;

                var dialog = new MessageDialog(message);
                dialog.ShowAsync();
            }
        }
    }
}
