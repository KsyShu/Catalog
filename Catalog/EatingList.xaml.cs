using Catalog.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class EatingList : Page
    {
        public EatingList()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.EatingListBox.ItemsSource = await EatingService.getAll();
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(EatingEditor));
        }

        private void SelectedEating(object sender, SelectionChangedEventArgs e)
        {
            var selectedEating = this.EatingListBox.SelectedItem as Services.Eating;

            if (selectedEating != null)
            {
                string message = "Название: " + selectedEating.name + "\n" +
                                 "Путь к фото: " + selectedEating.photoPath + "\n" +
                                 //"Фото: " + selectedEating.photo + "\n" +
                                 "Описание:" + selectedEating.description;

                var dialog = new MessageDialog(message);
                dialog.ShowAsync();
            }
        }
        private void Update(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(EatingUpdate));
        }
    }
}
