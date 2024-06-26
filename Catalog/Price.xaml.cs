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
    public sealed partial class Price : Page
    {
        private int price = 1;
        
        public Price()
        {
            this.InitializeComponent();
            UpdateNumberText();
        }
        private void UpdateNumberText()
        {
            //Category.Text =;
            //Sum.Text = ;
            Result.Text = price.ToString();
        }
        //public int price { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //Category.Text = price.ToString();
        }
        
        private void ChangeNumber(int multiplier)
        {
            price *= multiplier;
            UpdateNumberText();
        }
        private void Week(object sender, RoutedEventArgs e)
        {
            price = 1;
            ChangeNumber(7);
        }
        private void Week2(object sender, RoutedEventArgs e)
        {
            price = 1;
            ChangeNumber(14);
        }
        private void Week3(object sender, RoutedEventArgs e)
        {
            price = 1;
            ChangeNumber(21);
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.GoBack();
        }
    }
}
