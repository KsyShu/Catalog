using Catalog.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class Menu : Page
    {
        public Menu()
        {
            this.InitializeComponent();
        }
       
        // Создаем коллекции элементов меню для каждого дня недели
        private ObservableCollection<string> menuItemsMonday = new ObservableCollection<string>
        {
            "Курица и картофель", "Горбуша с рисом", "Диетический салат с куриной грудкой"
        };
        private ObservableCollection<string> menuItemsTuesday = new ObservableCollection<string>
        {
            "Фаршированные перцы", "Паровые котлеты из говядины", "Салат с рисом, фасолью и авокадо"
        };
        private ObservableCollection<string> menuItemsWednesday = new ObservableCollection<string>
        {
            "Томатный суп с фрикадельками", "Киноа с овощами и шиитаке", "Салат с манго и огурцом"
        };
        private ObservableCollection<string> menuItemsThursday = new ObservableCollection<string>
        {
            "Запеканка из кабачков с индейкой", "Овощной бульон", "Капустный салат с огурцом"
        };
        private ObservableCollection<string> menuItemsFriday = new ObservableCollection<string>
        {
            "Каша из киноа с печеным перцем и тимьяном", "Зеленые бобы эдамаме с козьим сыром на тосте", "ПП-греческий салат"
        };
        private ObservableCollection<string> menuItemsSaturday = new ObservableCollection<string>
        {
            "Щи из кислой капусты со свининой", "ПП оладьи из кабачков", "Салат со слабосоленым лососем, айсбергом и заправкой из облепихи"
        };
        private ObservableCollection<string> menuItemsSunday = new ObservableCollection<string>
        {
            "Говядина, тушенная с овощами", "Сливочный суп с курицей и грибами", "Сытный салат из печеных овощей"
        };

        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            switch (button.Content.ToString())
            {
                case "ПН":
                    MenuListBox.ItemsSource = menuItemsMonday;
                    break;
                case "ВТ":
                    MenuListBox.ItemsSource = menuItemsTuesday;
                    break;
                case "СР":
                    MenuListBox.ItemsSource = menuItemsWednesday;
                    break;
                case "ЧТ":
                    MenuListBox.ItemsSource = menuItemsThursday;
                    break;
                case "ПТ":
                    MenuListBox.ItemsSource = menuItemsFriday;
                    break;
                case "СБ":
                    MenuListBox.ItemsSource = menuItemsSaturday;
                    break;
                case "ВС":
                    MenuListBox.ItemsSource = menuItemsSunday;
                    break;
                default:
                    break;
            }
        }
        private void SelectedMenu(object sender, SelectionChangedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Price));
        }
        private void Update(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MenuEditor));
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.GoBack();
        }
    }
}
