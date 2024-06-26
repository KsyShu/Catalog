using Catalog.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography.Certificates;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Catalog
{ 
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }
        public bool Validate()
        {
            if (String.IsNullOrEmpty(Username.Text) || String.IsNullOrEmpty(Password.Password))
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
        private async void Login(object sender, RoutedEventArgs e)
        {
            var password = Password.Password;
            var frm = Window.Current.Content as Frame;

            
                Frame rootFrame = Window.Current.Content as Frame;

            if (Validate())
            {
                try
                {
                    var profileStatus = await Auth.SignIn(Username.Text, password);
                    
                    if (profileStatus)
                    {
                        frm.Navigate(typeof(MainWindow));
                    }
                    else
                    {
                        frm.Navigate(typeof(MainWindow2));
                    }
                }
                catch (Exception)
                {
                    var dialog = new ContentDialog()
                    {
                        Title = "Неверное имя пользователя или пароль",
                        CloseButtonText = "Ok",
                        IsPrimaryButtonEnabled = false
                    };
                    await dialog.ShowAsync();
                }
            }
        }
        private void regin_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            rootFrame.Navigate(typeof(regin));
        }
    }
}
