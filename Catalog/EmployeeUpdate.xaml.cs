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
    public sealed partial class EmployeeUpdate : Page
    {
        private int? id;
        private string password;

        public EmployeeUpdate()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.EmployeeList.ItemsSource = await EmployeeService.getAll();

            if (e.Parameter == null)
            {
               
            }
            else
            {
                ChangePasswordCheck.Visibility = Visibility.Collapsed;
                ChangePasswordCheck.IsChecked = true;

                var employee = e.Parameter as Employee;

                id = employee.id;

                Fullname.Text = employee.fullname;
                Username.Text = employee.username;
                password = employee.password;

            }
        }


        public bool Validate()
        {
            if (String.IsNullOrEmpty(Fullname.Text) || String.IsNullOrEmpty(Username.Text) /*||*/
              /*  String.IsNullOrEmpty(Password.Password)*/ /*|| String.IsNullOrEmpty(PasswordRepeat.Password)*/)
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

            if (!ChangePasswordCheck.IsChecked ?? true)
            {
                return true;
            }

            if (Password.Password != PasswordRepeat.Password)
            {
                var dialog = new ContentDialog()
                {
                    Title = "Пароли не совпадают",
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
                var employee = new Employee
                    {
                    id = (this.EmployeeList.SelectedItem as Catalog.Services.Employee).id,
                    fullname = Fullname.Text,
                    username = Username.Text,
                    password = password,
                };
                if (ChangePasswordCheck.IsChecked ?? true)
                {
                    employee.password = Password.Password;
                }
                await EmployeeService.Update(employee);

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
                var employee = new Services.Employee
                {
                    id = (this.EmployeeList.SelectedItem as Catalog.Services.Employee).id
                };
                await EmployeeService.Delete(employee);
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.GoBack();
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.GoBack();
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}

