using Catalog.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
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
    public sealed partial class regin : Page
    {
        public regin()
        {
            this.InitializeComponent();
        }
        public bool Validate1()
        {
            if (Username.Text.Length == 0)
            {
                var dialog = new ContentDialog()
                {
                    Title = "Укажите логин",
                    CloseButtonText = "Ok",
                    IsPrimaryButtonEnabled = false
                };
                dialog.ShowAsync();
                return false;
            }
            
            else if (Password.Password.Length == 0)
            {
                var dialog = new ContentDialog()
                {
                    Title = "Укажите пароль",
                    CloseButtonText = "Ok",
                    IsPrimaryButtonEnabled = false
                };
                dialog.ShowAsync();
                return false;
            }

            else if (Password_Copy.Password.Length == 0)
            {
                var dialog = new ContentDialog()
                {
                    Title = "Повторите пароль",
                    CloseButtonText = "Ok",
                    IsPrimaryButtonEnabled = false
                };
                dialog.ShowAsync();
                return false;
            }

            if (Password.Password == Password_Copy.Password) // проверка на совпадение паролей
            {
                
                var dialog = new ContentDialog()
                {
                    Title = "Пользователь зарегистрирован",
                    CloseButtonText = "Ok",
                    IsPrimaryButtonEnabled = false
                };
                dialog.ShowAsync();
                //Frame rootFrame = Window.Current.Content as Frame;
                //rootFrame.Navigate(typeof(LoginPage));
                //return false;
                
            }
            else
            {
                var dialog = new ContentDialog()
                {
                    Title = "Пароли не совподают",
                    CloseButtonText = "Ok",
                    IsPrimaryButtonEnabled = false
                };
                dialog.ShowAsync();
                return false;
            }
            return true;
        }
        private async void reg_Click(object sender, RoutedEventArgs e)
        {
            if (Validate1())
            {
                var client = new Client
                {
                    fullname = Fullname.Text,
                    username = Username.Text,
                    phone = Convert.ToInt32(Phone.Text),
                    address = Address.Text,
                    password = Password.Password,
                };
                await ClientService.Add(client);
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.GoBack();
            }
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(LoginPage));
        }
    }
}
