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
    public sealed partial class EmployeeEditor : Page
    {
        private int? id;
        public EmployeeEditor()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter == null)
            {

            }
            else
            {
                var employee = e.Parameter as Employee;

                id = employee.id;

                Fullname.Text = employee.fullname;
                Username.Text = employee.username;
            }
        }
        public bool Validate()
        {
            if (String.IsNullOrEmpty(Fullname.Text) || String.IsNullOrEmpty(Username.Text) ||
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
                var employee = new Employee
                {
                    fullname = Fullname.Text,
                    username = Username.Text,
                    password = Password.Password,
                };
                await EmployeeService.Add(employee);
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
