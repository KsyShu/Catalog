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
    public sealed partial class MenuEditor : Page
    {
        private int? id;
        public MenuEditor()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.Week.ItemsSource = await MenuService.getAll();

            if (e.Parameter == null)
            {

            }
            else
            {
                var menu = e.Parameter as Services.Menu;

                id = menu.id;

                Week.SelectedItem = /*catalog.name;*/
                Eating.SelectedItem = (Eating.ItemsSource as ObservableCollection<Eating>).Where(x => x.id == menu.eating_id).Last();
            }
        }
        
        //private string selectedDay = "ПН";
        //private void UpdateMenuListBox()
        //{
        //    switch (selectedDay)
        //    {
        //        case "ПН":
        //            MenuListBox.ItemsSource = menuItemsMonday;
        //            break;
        //        case "ВТ":
        //            MenuListBox.ItemsSource = menuItemsTuesday;
        //            break;
        //        case "СР":
        //            MenuListBox.ItemsSource = menuItemsWednesday;
        //            break;
        //        case "ЧТ":
        //            MenuListBox.ItemsSource = menuItemsThursday;
        //            break;
        //        case "ПТ":
        //            MenuListBox.ItemsSource = menuItemsFriday;
        //            break;
        //        case "СБ":
        //            MenuListBox.ItemsSource = menuItemsSaturday;
        //            break;
        //        case "ВС":
        //            MenuListBox.ItemsSource = menuItemsSunday;
        //            break;
        //        default:
        //            break;
        //    }
        //}

        private void Add(object sender, RoutedEventArgs e)
        {
            //Frame rootFrame = Window.Current.Content as Frame;
            //EatingList eatingListPage = rootFrame.Content as EatingList;

            //ListBox EatingListBox = eatingListPage.FindName("EatingListBox") as ListBox;

            //if (EatingListBox != null)
            //{
            //    ContentDialog dialog = new ContentDialog()
            //    {
            //        Title = "Выберите прием пищи",
            //        Content = EatingListBox,
            //        PrimaryButtonText = "Добавить",
            //        SecondaryButtonText = "Отмена"
            //    };

            //    ContentDialogResult result = await dialog.ShowAsync();

            //    //if (result == ContentDialogResult.Primary)
            //    //{
            //    //    // Add selected item from EatingListBox to a new listbox
            //    //    ListBoxItem selected = (ListBoxItem)EatingListBox.SelectedItem;
            //    //    if (selected != null)
            //    //    {
            //    //        MenuListBox.Items.Add(selected);
            //    //    }
            //    //}
            //    if (result == ContentDialogResult.Primary)
            //    {
            //        ListBoxItem selected = (ListBoxItem)EatingListBox.SelectedItem;
            //        if (selected != null)
            //        {
            //            // Добавляем выбранный элемент в соответствующий список дня недели
            //            switch (selectedDay)
            //            {
            //                case "ПН":
            //                    menuItemsMonday.Add(selected.Content.ToString());
            //                    break;
            //                case "ВТ":
            //                    menuItemsTuesday.Add(selected.Content.ToString());
            //                    break;
            //                case "СР":
            //                    menuItemsWednesday.Add(selected.Content.ToString());
            //                    break;
            //                case "ЧТ":
            //                    menuItemsThursday.Add(selected.Content.ToString());
            //                    break;
            //                case "ПТ":
            //                    menuItemsFriday.Add(selected.Content.ToString());
            //                    break;
            //                case "СБ":
            //                    menuItemsSaturday.Add(selected.Content.ToString());
            //                    break;
            //                case "ВС":
            //                    menuItemsSunday.Add(selected.Content.ToString());
            //                    break;
            //                default:
            //                    break;
            //            }
            //            UpdateMenuListBox(); // Обновляем список на экране
            //        }
            //    }
            //}
        }
        private async void Delete(object sender, RoutedEventArgs e)
        {
            // Показать диалоговое окно подтверждения
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
                var menu = new Services.Menu
                {
                    //id = (this.CatalogList.SelectedItem as Catalog.Services.Catalog).id
                };
                await MenuService.Delete(menu);

                // Переход назад после удаления
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
